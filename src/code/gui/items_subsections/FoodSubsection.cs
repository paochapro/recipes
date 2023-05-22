partial class FoodSubsection : ItemsSubsection<FoodItem>
{
	protected override IEnumerable<FoodItem> AvaliableItems {
        get => GetNode<Program>("/root/Program").LocalItems.FoodItems;
    }

    protected override DynamicWindowMenu SwitchMenu => DynamicWindowMenu.Food;
}