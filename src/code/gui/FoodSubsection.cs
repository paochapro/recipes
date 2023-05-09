partial class FoodSubsection : ItemsSubsection<FoodItem>
{
	static PackedScene itemButtonScene = GD.Load<PackedScene>("uid://ckilqsajfuwmi");

	#nullable disable
	Program program;
	#nullable restore

	protected override IEnumerable<FoodItem> AvaliableItems {
        get => GetNode<Program>("/root/Program").ItemsBank.Food;
    }

	protected override void OnMenuButtonPressed()
	{
        var dynamicWindow = GetNode<DynamicWindow>("/root/GuiRoot/%DynamicWindow");
		dynamicWindow.SetFoodMenu();
	}

	protected override Control GetControlForItem(FoodItem item)
	{
		FoodItemButton itemButton = itemButtonScene.Instantiate<FoodItemButton>();
		//TODO: heavy, should optimize somehow
		Texture2D texture = GD.Load<Texture2D>("uid://do7eog3ovi26o"); 
		itemButton.Initialize(item.Name, texture, QueueFree);
		return itemButton;
	}
}