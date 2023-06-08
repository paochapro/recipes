partial class AddLocalInvInspector : ItemsInspector<InventoryItem>
{
    protected override IEnumerable<InventoryItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Inventory;

    protected override void _ChildReady()
    {
        if(content is AddLocalInspectorContent<InventoryItem> bankContent)
        {
            var events = GetNode<GlobalEvents>("/root/GlobalEvents");
            events.NewBankInv += (item) => bankContent.UpdateItem(item);
            events.RemoveBankInv += (item) => bankContent.RemoveItem(item);
            events.NewLocalInv += (item) => bankContent.SetLocked(item, true);
            events.RemoveLocalInv += (item) => bankContent.SetLocked(item, false);
        }
        else
            throw new Exception("Content of AddLocalInvInspector is not of AddLocalInvInspectorContent type");
    }
}