partial class AddFoodButton : AddItemButton<FoodItem>
{
    protected override void Initialize()
    {
        var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        var nameLabel = GetNode<Label>("HBoxContainer/Label");

        image.Texture = GD.Load<Texture2D>(Item.TexturePath);
        nameLabel.Text = Item.Name;
    }
}