partial class InvSubsection : ItemsSubsection<InventoryItem>
{
    #nullable disable
    Program program;
    #nullable restore

    protected override IEnumerable<InventoryItem> AvaliableItems => DebugBank.Inventory;

    protected override void OnReady() {}

    protected override Control GetControlForItem(InventoryItem item)
    {
        Button button = new();
        button.Text = item.Name;
        return button;
    }
}