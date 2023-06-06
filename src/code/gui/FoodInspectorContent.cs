partial class FoodInspectorContent : ItemsInspectorContent<FoodItem> 
{
    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankFood += (item) => UpdateItem(item);
        events.RemoveBankFood += (item) => RemoveItem(item);
    }
}