partial class FoodSubsection : ItemsSubsection<FoodWithCount> //This could potentially break universe
{
    protected override DynamicWindowMenu SwitchMenu => DynamicWindowMenu.Food;

	protected override IEnumerable<FoodWithCount> AvaliableItems {
        get => GetNode<Program>("/root/Program").LocalItems.Food;
    }
    protected override Action<FoodWithCount> OnButtonPressed => 
        (item) => GetNode<Program>("/root/Program").RemoveLocalFood(item);

    protected override void _ChildReady()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewLocalFood += (FoodWithCount food) => UpdateItem(food);
        events.RemoveLocalFood += (FoodWithCount food) => RemoveItem(food);
    }
}