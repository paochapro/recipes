public partial class InvButton : HBoxContainer
{
    public virtual void Initialize(string name, Action removeButtonPressed)
    {
		var nameLabel = GetNode<Label>("PanelContainer/MarginContainer/Label");
		var deleteButton = GetNode<Button>("Button");

		nameLabel.Text = name;

		deleteButton.Pressed += removeButtonPressed;
        deleteButton.Pressed += this.QueueFree;
	}
}