partial class InvSubsectionContent : ItemsSubsectionContent<InventoryItem>
{
    protected override Control GetControlForItem(InventoryItem item)
    {
        Button button = new();
        button.Text = item.Name;
        return button;
    }
}