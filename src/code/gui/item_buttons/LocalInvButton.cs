partial class LocalInvButton : ItemButton<InventoryItem>
{
    protected override void Initialize()
    {
        var nameLabel = GetNode<Label>("PanelContainer/MarginContainer/Label");
		nameLabel.Text = Item.Name;
    }
}