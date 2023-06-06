partial class FoodSubsectionContent : ItemsInspectorContent<FoodItem>
{
    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewLocalFood += (FoodWithCount food) => UpdateItem(food.Item);
    }
}