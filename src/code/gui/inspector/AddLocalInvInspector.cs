partial class AddLocalInvInspector : InvInspector, AddItemsInspector<InventoryItem>
{
    protected override IEnumerable<InventoryItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Inventory;

    protected override ButtonGenerator<InventoryItem> ButtonGenerator {
        get {
            var program = GetNode<Program>("/root/Program");
            var onPressed = (InventoryItem item) => program.AddLocalInv(item);
            var disabledCondition = (InventoryItem item) => program.LocalItems.Inventory.Contains(item); 
            return new AddItemButtonGenerator<InventoryItem>(buttonScene, onPressed, disabledCondition);
        }
    }

    public void _ConnectEvents(AddItemsInspectorContent<InventoryItem> content)
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankInv += (item) => UpdateItem(item);
        events.RemoveBankInv += (item) => RemoveItem(item);
        events.NewLocalInv += (item) => content.SetLocked(item, true);
        events.RemoveLocalInv += (item) => content.SetLocked(item, false);
    }
}