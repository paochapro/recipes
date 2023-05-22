partial class BankInvButton : ItemButton<InventoryItem>
{
    public override void Initialize(InventoryItem item, Program program)
    {

		var nameLabel = GetNode<Label>("PanelContainer/MarginContainer/Label");
		var deleteButton = GetNode<Button>("Button");

		nameLabel.Text = item.Name;

		deleteButton.Pressed += () => program.RemoveInvItem(item);
        deleteButton.Pressed += this.QueueFree;
	}
}