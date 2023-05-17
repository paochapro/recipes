partial class FoodButton : PanelContainer
{
    public virtual void Initialize(string name, Texture2D imageTexture, Action removeButtonPressed)
    {
        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
		var deleteButton = GetNode<Button>("HBoxContainer/Button");

		nameLabel.Text = name;
		image.Texture = imageTexture;

		deleteButton.Pressed += removeButtonPressed;
        deleteButton.Pressed += this.QueueFree;
	}
}