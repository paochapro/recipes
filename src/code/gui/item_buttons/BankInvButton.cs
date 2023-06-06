partial class BankInvButton : ItemButton<InventoryItem>
{
    public override void CustomInit(InventoryItem item, Program program)
    {
		var nameLabel = GetNode<Label>("PanelContainer/MarginContainer/Label");
        var button = GetNode<Button>("Button");
		nameLabel.Text = item.Name;

        button.Pressed += () => program.RemoveInvItem(item);
	}
}