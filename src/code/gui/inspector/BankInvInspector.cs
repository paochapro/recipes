partial class BankInvInspector : InvInspector
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
        // events.RemoveBankInv += (i) => RemoveItem(i);
        // events.InvModified += (modified) => {
        //     var item = AvaliableItems.FirstOrDefault(i => i.Name == modified.Name);
            
        //     if(item != new InventoryItem()) {
        //         RemoveItem(item);
        //         UpdateItem(modified);
        //     }
        // };
    }
}