partial class AllSubsectionFoodContent : ItemsSubsectionContent<FoodItem>
{
	static PackedScene itemButtonScene = GD.Load<PackedScene>("res://src/tscn/item_buttons/bank_food_button.tscn");

    protected override Control GetControlForItem(FoodItem item)
    {
        var button = itemButtonScene.Instantiate<FoodButton>();
        string name = item.Name;
        Texture2D texture = GD.Load<Texture2D>(item.TexturePath);

        Action removeFunc = () => {
            var program = GetNode<Program>("/root/Program");
            program.RemoveFoodItem(item);
        };

        button.Initialize(name, texture, removeFunc);

        return button;
    }
}