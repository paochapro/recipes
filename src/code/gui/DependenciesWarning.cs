partial class DependenciesWarning : ConfirmationDialog
{
    #nullable disable
    [Export] Container localItems;
    [Export] Container recipes;
    [Export] Container buttonRoot;
    [Export] Container recipeLabels;
    [Export] PackedScene foodButton;
    [Export] PackedScene invButton;
    [Export] Fold recipesFold;
    #nullable restore

    public override void _Ready() {
        this.VisibilityChanged += () => {
            if(Visible == false)
                this.QueueFree();
        };

        recipesFold.FoldExpanded += base.ResetSize;
    }

    public void ShowWarning(Item item, Item? dependedItem, IEnumerable<Recipe> dependedRecipes)
    {
        if(dependedItem != null) {
            localItems.Show();
            buttonRoot.AddChild(GetButton(dependedItem));
        }

        if(dependedRecipes.Count() != 0) {
            recipes.Show();
            int i = 1;
            var labels = dependedRecipes.Select(r => new Label() { Text = $"{i++}. {r.Title}"} );
            recipeLabels.AddChildren(labels);
        }

        this.Confirmed += () => RemoveItem(item, dependedItem, dependedRecipes);
        this.PopupCentered();
        this.CallDeferred(Window.MethodName.ResetSize);
    }

    public void RemoveItem(Item item, Item? dependedItem, IEnumerable<Recipe> dependedRecipes) 
    {
        GD.Print("remove item");

        var program = GetNode<Program>("/root/Program");
        
        //Making a list from dependedRecipes, because we are removing them here
        //And IEnumerable<Recipe> dependedRecipes is not a seperate collection
        //So removing recipes will remove recipes from dependedRecipes (which causes collection changed exception)
        try{
            foreach(Recipe recipe in dependedRecipes.ToList()) {
                program.RemoveRecipe(recipe);
                program.AddRecipe(GetModifiedRecipe(recipe, item));
            }
        }
        catch(Exception ex) {
            GD.Print(ex.Message);
        }

        if(dependedItem != null) {
            if(dependedItem is FoodWithCount food && item is FoodItem foodItem) {
                program.RemoveLocalFood(food);
                program.RemoveFoodItem(foodItem);
            }
            
            if(dependedItem is InventoryItem invLocalItem && item is InventoryItem invItem) {
                program.RemoveLocalInv(invLocalItem);
                program.RemoveInvItem(invItem);
            }
        }
    }

    Recipe GetModifiedRecipe(Recipe recipe, Item item) {
        ReadonlyItemSet readonlyItemset = recipe.ItemSet;
        List<FoodWithCount> foodList = readonlyItemset.Food.ToList();
        List<InventoryItem> invList = readonlyItemset.Inventory.ToList();

        if(item is FoodItem foodItem) {
            foodList.RemoveAll(f => f.Item == foodItem);
        }

        if(item is InventoryItem invItem) {
            invList.Remove(invItem);
        }

        ItemSet modifiedItemSet = new(foodList, invList);
        Recipe modifiedRecipe = recipe with { ItemSet = modifiedItemSet };
        return modifiedRecipe;
    }

    Control GetButton(Item dependedItem) {
        if(dependedItem is FoodWithCount food)
            return GetFoodButton(food);

        if(dependedItem is InventoryItem invItem)
            return GetInvButton(invItem);

        return new Control();
    }

    Control GetFoodButton(FoodWithCount food) {
        var recipeFoodButton = foodButton.Instantiate<RecipeFoodButton>();
        recipeFoodButton.Initialize(food);
        return recipeFoodButton;
    }

    Control GetInvButton(InventoryItem item) {
        var recipeInvButton = invButton.Instantiate<RecipeInvButton>();
        recipeInvButton.Initialize(item);
        return recipeInvButton;
    }

    string GetRecipesText(IEnumerable<Recipe> dependedRecipes)  
    {
        IEnumerable<string> titles = dependedRecipes.Select(r => r.Title);
        string fullText = string.Join(", ", titles);

        if(fullText.Length > 50) {
            const int showCount = 3;
            string firstPart = string.Join(", ", titles.Take(showCount));
            string remaining = (titles.Count() - showCount).ToString();
            return $"{firstPart} и ещё {remaining}";
        }

        return fullText;
    }
}
