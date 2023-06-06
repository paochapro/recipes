partial class InvInspectorContent : BankInspectorContent<InventoryItem> 
{
    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankInv += (item) => UpdateItem(item);
        events.RemoveBankInv += (item) => RemoveItem(item);
        events.NewLocalInv += (item) => SetLocked(item, true);
        events.RemoveLocalInv += (item) => SetLocked(item, false);
    }
}