partial class AddLocalInvButton : AddLocalItemButton<InventoryItem>
{
    protected override bool DisabledCondition => program.LocalItems.Inventory.Contains(item);
    protected override Action OnButtonPress => () => program.AddLocalInv(item);

    public override void Initialize(InventoryItem item, Program program)
    {
        var nameLabel = GetNode<Label>("PanelContainer/MarginContainer/Label");
        var button = GetNode<Button>("Button");

        nameLabel.Text = item.Name;
    }

    protected override void AddEvents()
    {
        var events = program.GetNode<GlobalEvents>("/root/GlobalEvents");
        events.RemoveLocalInv += CheckRemovedLocalItem;
        events.NewLocalInv += CheckNewLocalItem;
    }

    protected override void RemoveEvents()
    {
        var events = program.GetNode<GlobalEvents>("/root/GlobalEvents");
        events.RemoveLocalInv += CheckRemovedLocalItem;
        events.NewLocalInv += CheckNewLocalItem;
    }
}