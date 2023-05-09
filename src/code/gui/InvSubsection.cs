partial class InvSubsection : ItemsSubsection<InventoryItem>
{
    #nullable disable
    Program program;
    #nullable restore

    protected override IEnumerable<InventoryItem> AvaliableItems {
        get => GetNode<Program>("/root/Program").ItemsBank.Inventory;
    }

    protected override void OnMenuButtonPressed()
    {
        var dynamicWindow = GetNode<DynamicWindow>("/root/GuiRoot/%DynamicWindow");
		dynamicWindow.SetFoodMenu();
        dynamicWindow.SetInvMenu();
    }

    protected override Control GetControlForItem(InventoryItem item)
    {
        Button button = new();
        button.Text = item.Name;
        return button;
    }
}