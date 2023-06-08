partial class AddInvButton : AddItemButton<InventoryItem>
{
    protected override void Initialize()
    {
        var nameLabel = GetNode<Label>("PanelContainer/MarginContainer/Label");
        var button = GetNode<Button>("Button");
        nameLabel.Text = Item.Name;
    }
}