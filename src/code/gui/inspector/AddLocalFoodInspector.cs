partial class AddLocalFoodInspector : AddItemsInspector<FoodItem>
{
    protected override IEnumerable<FoodItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Food;

    protected override ButtonGenerator<FoodItem> ButtonGenerator {
        get {
            var program = GetNode<Program>("/root/Program");
            var onPressed = (FoodItem item) => program.AddLocalFood(new FoodWithCount(item, 1));
            var disabledCondition = (FoodItem item) => program.LocalItems.FoodItems.Contains(item); 
            return new AddItemButtonGenerator<FoodItem>(buttonScene, onPressed, disabledCondition);
        }
    }

    protected override void ConnectEvents(AddItemsInspectorContent<FoodItem> content)
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankFood += (item) => UpdateItem(item);
        events.RemoveBankFood += (item) => RemoveItem(item);
        events.NewLocalFood += (FoodWithCount food) => content.SetLocked(food.Item, true);
        events.RemoveLocalFood += (FoodWithCount food) => content.SetLocked(food.Item, false);
    }
}