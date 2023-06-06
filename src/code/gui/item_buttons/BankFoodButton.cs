partial class BankFoodButton : ItemButton<FoodItem>
{
    public override void Initialize(FoodItem item, Program program)
    {
        base.Initialize(item, program);

        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        var button = GetNode<Button>("HBoxContainer/Button");

		nameLabel.Text = item.Name;
		image.Texture = GD.Load<Texture2D>(item.TexturePath);

        button.Pressed += () => program.RemoveFoodItem(item);
    }
}