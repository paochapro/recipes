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
        GetNode<GlobalEvents>("/root/GlobalEvents").CallSwitchDynamicWindow(DynamicWindowMenu.Inv);
    }
}