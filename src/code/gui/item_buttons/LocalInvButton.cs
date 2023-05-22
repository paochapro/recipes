partial class LocalInvButton : ItemButton<InventoryItem>
{
    public override void Initialize(InventoryItem item, Program program)
    {
		var nameLabel = GetNode<Label>("PanelContainer/MarginContainer/Label");
		var deleteButton = GetNode<Button>("Button");

		nameLabel.Text = item.Name;

		deleteButton.Pressed += () => program.RemoveLocalInv(item);
        deleteButton.Pressed += this.QueueFree;
	}
}