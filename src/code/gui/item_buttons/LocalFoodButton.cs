partial class LocalFoodButton : ItemButton<FoodWithCount>
{
    protected override void Initialize()
    {
        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        var food = base.Item;

		nameLabel.Text = food.Item.Name;
		image.Texture = GD.Load<Texture2D>(food.Item.TexturePath);

		//Spinbox change count function
		Action<double> changeCount = (double value) => {
			int count = (int)Math.Round(value);
			food.Count = count;
		};

        //Spinbox
        var spinbox = GetNode<SpinBox>("HBoxContainer/SpinBox");
        spinbox.Value = food.Count;
        spinbox.ValueChanged += (double value) => changeCount(value);
    }
}