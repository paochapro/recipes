partial class FoodSubsectionContent : ItemsSubsectionContent<FoodItem>
{
	static PackedScene itemButtonScene = GD.Load<PackedScene>("res://src/tscn/food_item_button.tscn");

	protected override Control GetControlForItem(FoodItem item)
	{
        //Get food with count
        var program = GetNode<Program>("/root/Program");
        IEnumerable<FoodWithCount> food = program.LocalItems.Food;
        FoodWithCount foodWithCount = food.First(f => f.Item == item);
        
        //Spinbox change count function
        Action<double> changeCount = (double value) => {
            int count = (int)Math.Round(value);
            foodWithCount.Count = count;
        };

        //Delete button
        Action deleteItem = () => program.RemoveLocalFood(foodWithCount);
        
        //Initialize item button scene
		FoodItemButton itemButton = itemButtonScene.Instantiate<FoodItemButton>();
		Texture2D texture = GD.Load<Texture2D>(item.TexturePath); //TODO: texture ids, potentially heavy?
        int count = foodWithCount.Count;
        string name = item.Name;
		itemButton.Initialize(name, texture, count, deleteItem, changeCount);

		return itemButton;
	}
}