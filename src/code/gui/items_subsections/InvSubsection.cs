partial class InvSubsection : ItemsSubsection<InventoryItem>
{
    protected override IEnumerable<InventoryItem> AvaliableItems {
        get => GetNode<Program>("/root/Program").LocalItems.Inventory;
    }

    protected override DynamicWindowMenu SwitchMenu => DynamicWindowMenu.Inv;

    protected override ButtonGenerator<InventoryItem> ButtonGenerator {
        get {
            var program = GetNode<Program>("/root/Program");
            var onPressed = (InventoryItem item) => program.RemoveLocalInv(item);
            return new ButtonGenerator<InventoryItem>(buttonScene, onPressed);
        }
    }
    protected override void _ChildReady()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewLocalInv += (InventoryItem item) => UpdateItem(item);
        events.RemoveLocalInv += (InventoryItem item) => RemoveItem(item);
    }
}