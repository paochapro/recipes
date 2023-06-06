partial class FoodInspectorContent : BankInspectorContent<FoodItem> 
{
    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankFood += (item) => UpdateItem(item);
        events.RemoveBankFood += (item) => RemoveItem(item);
        events.NewLocalFood += (FoodWithCount food) => SetLocked(food.Item, true);
        events.RemoveLocalFood += (FoodWithCount food) => SetLocked(food.Item, false);
    }
}