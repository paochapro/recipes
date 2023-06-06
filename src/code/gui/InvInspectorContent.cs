partial class InvInspectorContent : ItemsInspectorContent<InventoryItem> 
{
    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankInv += (item) => UpdateItem(item);
        events.RemoveBankInv += (item) => RemoveItem(item);
    }
}