partial class AllSubsectionFoodContent : ItemsSubsectionContent<FoodItem>
{
	[Export] PackedScene? itemButtonScene;

    protected override Control GetControlForItem(FoodItem item)
    {
        if(itemButtonScene == null)
            throw new Exception("No item button packed scene [AllSubsectionFoodContent.cs]");

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