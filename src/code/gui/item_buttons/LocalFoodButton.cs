partial class LocalFoodButton : BaseFoodButton
{
	public override void Initialize(FoodItem item, Program program)
	{
        base.Initialize(item, program);

		//Get food with count
		IEnumerable<FoodWithCount> food = program.LocalItems.Food;
		FoodWithCount foodWithCount = food.First(f => f.Item == item);
		
		//Delete button
		Action deleteItem = () => program.RemoveLocalFood(foodWithCount);
		var button = GetNode<Button>("HBoxContainer/Button");
        button.Pressed += deleteItem;
        button.Pressed += this.QueueFree;

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