using Godot;
using System;

partial class RecipeContent : VBoxContainer
{
    [Export] PackedScene? recipeCardScene;

	public void UpdateContent(IEnumerable<Recipe> recipes, bool autoExpand = false)
    {
        if(recipeCardScene == null)
            throw new Exception("No recipe card scene [RecipeContent.cs]");

        this.RemoveChildren();

        foreach(Recipe recipe in recipes)
        {
            GD.Print($"Hello? Recipe: {recipe.Title}");

            Fold fold = new();
            fold.Title = recipe.Title;
            fold.Expanded = autoExpand;

            var card = recipeCardScene.Instantiate<RecipeCard>();
            card.Initialize(recipe);
            fold.MainContainer.AddChild(card);

            this.AddChild(fold);
        }
    }
}