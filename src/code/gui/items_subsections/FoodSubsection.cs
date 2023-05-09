partial class FoodSubsection : ItemsSubsection<FoodItem>
{
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
}