partial class LocalFoodButton : ItemButton<FoodItem>
{
    public override void CustomInit(FoodItem item, Program program)
    {
        var nameLabel = GetNode<Label>("HBoxContainer/Label");
		var image = GetNode<TextureRect>("HBoxContainer/TextureRect");

		nameLabel.Text = item.Name;
		image.Texture = GD.Load<Texture2D>(item.TexturePath);

	    //Get food with count
		IEnumerable<FoodWithCount> food = program.LocalItems.Food;
		FoodWithCount foodWithCount = food.First(f => f.Item == item);
		
		//Delete button
		var button = GetNode<Button>("HBoxContainer/Button");
        button.Pressed += () => program.RemoveLocalFood(foodWithCount);

		//Spinbox change count function
		Action<double> changeCount = (double value) => {
			int count = (int)Math.Round(value);
			foodWithCount.Count = count;
		};

        //Spinbox
        var spinbox = GetNode<SpinBox>("HBoxContainer/SpinBox");
        spinbox.Value = foodWithCount.Count;
        spinbox.ValueChanged += (double value) => changeCount(value);
    }
}