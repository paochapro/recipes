partial class RecipeContent : VBoxContainer
{
    PackedScene? recipeCardScene;

    const byte foldTabNormalAlpha = 10;
    const byte foldTabHoverAlpha = 25;

    public override void _Ready()
    {
        recipeCardScene =  GD.Load<PackedScene>("res://src/tscn/recipe_card.tscn");
    }

    public void UpdateContent(IEnumerable<Recipe> recipes, ReadonlyItemSet localItems)
    {
        this.RemoveChildren();

        foreach(Recipe recipe in recipes)
            AddRecipeCard(recipe);

        Reorder(localItems);
    }

    public void UpdateRecipe(Recipe recipe, ReadonlyItemSet localItems) 
    {
        if(FindFoldByRecipe(recipe) != null) {
            GD.PrintErr("Tried to add recipe that content already has [RecipeContent.cs]");
            return;
        }

        AddRecipeCard(recipe);

        Reorder(localItems);
    }

    public void ModifyRecipe(Recipe recipe, ReadonlyItemSet localItems)
    {
        var foundFold = FindFoldByRecipe(recipe);

        if(foundFold == null) {
            GD.PrintErr("Attepmted to modify recipe that doesnt exist in content [RecipeContent.cs]");
            return;
        }

        var titleSettings = GetTitleSettings(recipe);
        var card = foundFold.MainContainer.GetChild<RecipeCard>(0);
        foundFold.GetLabel().LabelSettings = titleSettings;
        card.Initialize(recipe, titleSettings);

        Reorder(localItems);
    }

    public void RemoveRecipe(Recipe recipe) 
    {
        var foundFold = FindFoldByRecipe(recipe);

        if(foundFold == null) {
            GD.PrintErr("Attepmted to remove recipe that doesnt exist in content [RecipeContent.cs]");
            return;
        }

        //TODO: Replace every remove child with QueueFree
        this.RemoveChild(foundFold);
    }

    public void SearchUpdate(IEnumerable<Recipe> recipes, ReadonlyItemSet set)
    {
        var folds = GetChildren().Cast<Fold>();
        var foldTitles = folds.Select(f => f.Title);
        var addRecipes = recipes.Where(r => !foldTitles.Contains(r.Title));

        foreach(var recipe in addRecipes)
            AddRecipeCard(recipe);

        var recipesTitles = recipes.Select(r => r.Title);
        var removeFolds = folds.Where(f => !recipesTitles.Contains(f.Title)).ToArray();

        foreach(var fold in removeFolds) {
            this.RemoveChild(fold);
            fold.QueueFree();
        }

        Reorder(set);
    }

    void AddRecipeCard(Recipe recipe) 
    {
        if(recipeCardScene == null)
            throw new Exception("No recipe card scene [RecipeContent.cs]");

        var titleSettings = GetTitleSettings(recipe);

        Fold fold = new();
        fold.Title = recipe.Title;
        fold.TabNormalAlpha = foldTabNormalAlpha;
        fold.TabHoverAlpha = foldTabHoverAlpha;
        SetFoldTitleSettings(fold, titleSettings);
        this.AddChild(fold);

        var card = recipeCardScene.Instantiate<RecipeCard>();
        fold.MainContainer.AddChild(card);
        card.Initialize(recipe, titleSettings);
    }

    LabelSettings GetTitleSettings(Recipe recipe)
    {
        return new LabelSettings();
    }

    Fold? FindFoldByRecipe(Recipe recipe) {
        return GetChildren().Cast<Fold>().FirstOrDefault(f => f.Title == recipe.Title);
    }

    public void Reorder(ReadonlyItemSet localItems)
    {
        var folds = GetChildren().Cast<Fold>();

        var recipes = folds
            .Select(f => f.MainContainer.GetChild<RecipeCard>(0))
            .Select(c => c.Recipe);

        var orderedRecipes = RecipeOrdering.OrderRecipes(recipes, localItems).ToList();
        
        foreach(var fold in folds)
        {
            var card = fold.MainContainer.GetChild<RecipeCard>(0);
            var recipe = card.Recipe;
            var titleColor = HightlightColor.GetRecipeTitleColor(recipe, localItems);
            SetFoldTitleSettings(fold, new LabelSettings() { FontColor = titleColor } );
            card.InitializeFoodInspector(recipe.ItemSet.Food, localItems);

            int index = orderedRecipes.IndexOf(recipe);
            this.MoveChild(fold, index);
        }
    }

    public void SetFoldTitleSettings(Fold fold, LabelSettings settings)  {
        fold.GetLabel().LabelSettings = settings;
    }
}