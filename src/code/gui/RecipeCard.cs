partial class RecipeCard : PanelContainer
{
	#nullable disable
	[Export] Label title;
	[Export] Label time;
	[Export] Label dishType;
	[Export] Label instructions;
	[Export] TextureRect image;
	[Export] RecipeFoodInspector foodInspector;
	[Export] RecipeInvInspector invInspector;
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

        foodInspector.Initialize(recipe.ItemSet.Food);
        invInspector.Initialize(recipe.ItemSet.Inventory);
	}
}