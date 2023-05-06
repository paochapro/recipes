partial class FoodItemButton : PanelContainer
{
	public void Initialize(string name, Texture2D imageTexture, Action onButtonClick)
	{
        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
		var button = GetNode<Button>("Button");

		nameLabel.Text = name;
		image.Texture = imageTexture;
		button.ButtonUp += onButtonClick;
	}
}
