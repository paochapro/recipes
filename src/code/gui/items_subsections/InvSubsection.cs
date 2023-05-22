partial class InvSubsection : ItemsSubsection<InventoryItem>
{
    protected override IEnumerable<InventoryItem> AvaliableItems {
        get => GetNode<Program>("/root/Program").LocalItems.Inventory;
    }

    protected override DynamicWindowMenu SwitchMenu => DynamicWindowMenu.Inv;
}