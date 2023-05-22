partial class BaseFoodButton : ItemButton<FoodItem>
{
    public override void Initialize(FoodItem item, Program program)
    {
        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");

		nameLabel.Text = item.Name;
		image.Texture = GD.Load<Texture2D>(item.TexturePath);
	}
}