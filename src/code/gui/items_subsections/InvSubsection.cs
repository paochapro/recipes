partial class InvSubsection : ItemsSubsection<InventoryItem>
{
    protected override IEnumerable<InventoryItem> AvaliableItems {
        get => GetNode<Program>("/root/Program").LocalItems.Inventory;
    }

    protected override DynamicWindowMenu SwitchMenu => DynamicWindowMenu.Inv;

    protected override Action<InventoryItem> OnButtonPressed => 
        (item) => GetNode<Program>("/root/Program").RemoveLocalInv(item);

    protected override void _ChildReady()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewLocalInv += (InventoryItem item) => UpdateItem(item);
        events.RemoveLocalInv += (InventoryItem item) => RemoveItem(item);
    }
}