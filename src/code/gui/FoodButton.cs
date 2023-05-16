partial class FoodButton : PanelContainer
{
    #nullable disable
    Label titleLabel;
    TextureRect image;
    Button removeButton;
    #nullable restore

    public override void _Ready()
    {
        titleLabel = GetNode<Label>("HBoxContainer/Label");
		image = GetNode<TextureRect>("HBoxContainer/TextureRect");
		removeButton = GetNode<Button>("HBoxContainer/Button");
    }

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