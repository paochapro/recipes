partial class FoodItemButton : PanelContainer
{
    #nullable disable
    Label nameLabel;
    TextureRect image;
    Button button;
    #nullable restore

    public override void _Ready()
    {
        nameLabel = GetNode<Label>("HBoxContainer/Label");
        image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        button = GetNode<Button>("Button");
    }

    public void Initialize(string name, Texture2D imageTexture, Action onButtonClick)
    {
        nameLabel.Text = name;
        image.Texture = imageTexture;
        button.ButtonUp += onButtonClick;
    }
}