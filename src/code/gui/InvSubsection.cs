partial class InvSubsection : ItemsSubsection<InventoryItem>
{
    #nullable disable
    Program program;
    #nullable restore

    protected override IEnumerable<InventoryItem> AvaliableItems => DebugBank.Inventory;

    protected override void OnMenuButtonPressed()
    {
        var dynamicWindow = GetNode<DynamicWindow>("/root/Program/Control/MarginContainer/HBoxContainer/HSplitContainer/HSplitContainer/DynamicWindow");
        dynamicWindow.SetInvMenu();
    }

    protected override Control GetControlForItem(InventoryItem item)
    {
        Button button = new();
        button.Text = item.Name;
        return button;
    }
}