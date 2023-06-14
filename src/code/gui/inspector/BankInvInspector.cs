partial class BankInvInspector : InvInspector
{
    #nullable disable
    BankRemovalSafety safety;
    #nullable restore

    protected override IEnumerable<InventoryItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Inventory;
        
    protected override ButtonGenerator<InventoryItem> ButtonGenerator {
        get {
            var program = GetNode<Program>("/root/Program");
            var onPressed = (InventoryItem item) => safety.CheckForDependencies(item);
            return new ButtonGenerator<InventoryItem>(buttonScene, onPressed);
        }
    }

    protected override void _ChildReady() {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankInv += (i) => UpdateItem(i);

        safety = new BankRemovalSafety();
        AddChild(safety);
    }
}