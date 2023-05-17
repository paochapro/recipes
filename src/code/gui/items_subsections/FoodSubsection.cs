partial class FoodSubsection : ItemsSubsection<FoodItem>
{
	#nullable disable
	Program program;
	#nullable restore

	protected override IEnumerable<FoodItem> AvaliableItems {
        get => GetNode<Program>("/root/Program").LocalItems.FoodItems;
    }

	protected override void OnMenuButtonPressed()
	{
        GetNode<GlobalEvents>("/root/GlobalEvents").CallSwitchDynamicWindow(DynamicWindowMenu.Food);
	}
}