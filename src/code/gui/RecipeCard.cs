partial class RecipeCard : PanelContainer
{
	#nullable disable
	[Export] Label title;
	[Export] Label time;
	[Export] Label dishType;
	[Export] Label instructions;
	[Export] TextureRect image;
	[Export] Container foodContent;
	[Export] Container invContent;
    [Export] PackedScene recipeFoodScene;
    [Export] PackedScene recipeInvScene;
	#nullable restore

	public void Initialize(Recipe recipe)
	{
		title.Text = recipe.Title;
		time.Text = recipe.Minutes.ToString();
		dishType.Text = Enum.GetName<DishType>(recipe.DishType);
		instructions.Text = recipe.Instructions;
		image.Texture = GD.Load<Texture2D>(recipe.ImageTextureUID);

        var foodButtons = recipe.ItemSet.Food.OrderBy(i => i.Name).Select(f => {
            var button = recipeFoodScene.Instantiate<RecipeFoodButton>();
            button.Initialize(f);
            return button;
        });

        var invButtons = recipe.ItemSet.Inventory.OrderBy(i => i.Name).Select(f => {
            var button = recipeInvScene.Instantiate<RecipeInvButton>();
            button.Initialize(f);
            return button;
        });

        foodContent.AddChildren(foodButtons);
        invContent.AddChildren(invButtons);
	}
}