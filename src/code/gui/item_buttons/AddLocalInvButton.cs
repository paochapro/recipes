partial class AddLocalInvButton : AddLocalItemButton<InventoryItem>
{
    protected override bool DisabledCondition => program.LocalItems.Inventory.Contains(Item);
    protected override Action OnButtonPress => () => program.AddLocalInv(Item);

    public override void CustomInit(InventoryItem item, Program program)
    {
        base.CustomInit(item, program);

        var nameLabel = GetNode<Label>("PanelContainer/MarginContainer/Label");
        var button = GetNode<Button>("Button");

        nameLabel.Text = item.Name;
    }
}