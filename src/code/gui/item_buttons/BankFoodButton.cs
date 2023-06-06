partial class BankFoodButton : ItemButton<FoodItem>
{
    public override void CustomInit(FoodItem item, Program program)
    {
        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        var button = GetNode<Button>("HBoxContainer/Button");

		nameLabel.Text = item.Name;
		image.Texture = GD.Load<Texture2D>(item.TexturePath);

        button.Pressed += () => program.RemoveFoodItem(item);
    }
}