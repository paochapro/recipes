partial class FoodSubsection : ItemsSubsection<FoodItem>
{
    static PackedScene itemButtonScene = GD.Load<PackedScene>("uid://ckilqsajfuwmi");

    #nullable disable
    Program program;
    #nullable restore

    protected override IEnumerable<FoodItem> AvaliableItems => DebugBank.Food;

    protected override void OnReady() {}

    protected override Control GetControlForItem(FoodItem item)
    {
        FoodItemButton itemButton = itemButtonScene.Instantiate<FoodItemButton>();
        //TODO: heavy, should optimize somehow
        Texture2D texture = GD.Load<Texture2D>("uid://do7eog3ovi26o"); 
        itemButton.Initialize(item.Name, texture, QueueFree);
        return itemButton;
    }
}