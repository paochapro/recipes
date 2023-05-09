partial class FoodSubsectionContent : ItemsSubsectionContent<FoodItem>
{
	static PackedScene itemButtonScene = GD.Load<PackedScene>("res://src/tscn/food_item_button.tscn");

	protected override Control GetControlForItem(FoodItem item)
	{
		FoodItemButton itemButton = itemButtonScene.Instantiate<FoodItemButton>();
		//TODO: heavy, should optimize somehow
		Texture2D texture = GD.Load<Texture2D>("uid://do7eog3ovi26o");
		itemButton.Initialize(item.Name, texture, QueueFree);
		return itemButton;
	}
}