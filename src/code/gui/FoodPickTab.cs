partial class FoodPickTab : ItemPickTab<FoodItem>
{
	static PackedScene foodButtonScene = GD.Load<PackedScene>("uid://duhcl71gumaci");
	
	protected override Control GenerateItemControl(FoodItem item)
	{
		var foodButton = foodButtonScene.Instantiate<FoodItemButton>();
		string name = item.Name;
		Texture2D texture = GD.Load<Texture2D>(item.TextureUID);

		//add food item to filter
		Action onButtonClick = () => {};

		foodButton.Initialize(name, texture, onButtonClick);

		return foodButton;
	}
}