partial class FoodSubsection : ItemsSubsection<FoodItem>
{
    protected override DynamicWindowMenu SwitchMenu => DynamicWindowMenu.Food;

	protected override IEnumerable<FoodItem> AvaliableItems {
        get => GetNode<Program>("/root/Program").LocalItems.FoodItems;
    }

    protected override void _ChildReady()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewLocalFood += (FoodWithCount food) => content.UpdateItem(food.Item);
        events.RemoveLocalFood += (FoodWithCount food) => content.RemoveItem(food.Item);
    }
}