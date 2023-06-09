partial class BankInvInspector : ItemsInspector<InventoryItem>
{
    protected override IEnumerable<InventoryItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Inventory;
        
    protected override ButtonGenerator<InventoryItem> ButtonGenerator {
        get {
            var program = GetNode<Program>("/root/Program");
            var onPressed = (InventoryItem item) => program.RemoveInvItem(item);
            return new ButtonGenerator<InventoryItem>(buttonScene, onPressed);
        }
    }

    protected override void _ChildReady() {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankInv += (i) => UpdateItem(i);
        events.RemoveBankInv += (i) => RemoveItem(i);
    }
}