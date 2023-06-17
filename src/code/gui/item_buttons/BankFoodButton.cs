partial class BankFoodButton : BankButton<FoodItem>
{
    protected override void Initialize()
    {
        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        var button = GetNode<Button>("HBoxContainer/Button");

		nameLabel.Text = Item.Name;
		image.Texture = ImageLoader.GetImage(Item.TexturePath);

        base.Initialize();        
    }
}