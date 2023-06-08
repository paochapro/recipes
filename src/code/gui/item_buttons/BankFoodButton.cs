partial class BankFoodButton : ItemButton<FoodItem>
{
    protected override void Initialize()
    {
        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        var button = GetNode<Button>("HBoxContainer/Button");

		nameLabel.Text = Item.Name;
		image.Texture = GD.Load<Texture2D>(Item.TexturePath);
    }
}