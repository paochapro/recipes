partial class LocalFoodInspector : ItemsInspector<FoodWithCount> //This could potentially break universe
{
	protected override IEnumerable<FoodWithCount> AvaliableItems {
        get => GetNode<Program>("/root/Program").LocalItems.Food;
    }

    protected override ButtonGenerator<FoodWithCount> ButtonGenerator {
        get {
            var program = GetNode<Program>("/root/Program");
            var onPressed = (FoodWithCount item) => program.RemoveLocalFood(item);
            return new ButtonGenerator<FoodWithCount>(buttonScene, onPressed);
        }
    }
    
    protected override void _ChildReady()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewLocalFood += (FoodWithCount food) => UpdateItem(food);
        events.RemoveLocalFood += (FoodWithCount food) => RemoveItem(food);
        //events.FileLoaded += UpdateContent;
    }
}