using Godot;
using System;

partial class RecipeContent : VBoxContainer
{
    [Export] PackedScene? recipeCardScene;

    const byte foldTabNormalAlpha = 10;
    const byte foldTabHoverAlpha = 25;

	public void UpdateContent(IEnumerable<Recipe> recipes, bool autoExpand = false)
    {
        this.RemoveChildren();

        foreach(Recipe recipe in recipes)
            AddRecipeCard(recipe, autoExpand);
    }

    public void UpdateRecipe(Recipe recipe) 
    {
        if(FindFoldByRecipe(recipe) != null) {
            GD.PrintErr("Tried to add recipe that content already has [RecipeContent.cs]");
            return;
        }

        AddRecipeCard(recipe);
    }

    public void RemoveRecipe(Recipe recipe) 
    {
        var foundFold = FindFoldByRecipe(recipe);

        if(foundFold == null) {
            GD.PrintErr("Tried to remove recipe that content doesnt have [RecipeContent.cs]");
            return;
        }

        //TODO: Replace every remove child with QueueFree
        this.RemoveChild(foundFold);
    }

    void AddRecipeCard(Recipe recipe, bool foldExpanded = false) 
    {
        if(recipeCardScene == null)
            throw new Exception("No recipe card scene [RecipeContent.cs]");

        Fold fold = new();
        fold.Title = recipe.Title;
        fold.Expanded = foldExpanded;
        fold.TabNormalAlpha = foldTabNormalAlpha;
        fold.TabHoverAlpha = foldTabHoverAlpha;
        this.AddChild(fold);

        var card = recipeCardScene.Instantiate<RecipeCard>();
        fold.MainContainer.AddChild(card);
        card.Initialize(recipe);
    }

    Fold? FindFoldByRecipe(Recipe recipe) {
        return GetChildren().Cast<Fold>().FirstOrDefault(f => f.Title == recipe.Title);
    }
}