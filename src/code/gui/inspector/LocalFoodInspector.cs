partial class LocalFoodInspector : CountedFoodInspector //This could potentially break universe
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

        // events.FoodModified += (modifyItem) => 
        // { 
        //     var food = AvaliableItems.FirstOrDefault(f => f.Name == modifyItem.Name);

        //     if(food != null) {
        //         ModifyItem(new FoodWithCount(modifyItem, food.Count));
        //     }
        // };
    }
}