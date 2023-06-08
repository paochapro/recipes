partial class AddLocalFoodInspector : ItemsInspector<FoodItem>
{
    protected override IEnumerable<FoodItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Food;

    protected override Action<FoodItem> OnButtonPressed =>
        (item) => GetNode<Program>("/root/Program").AddLocalFood(new FoodWithCount(item, 1));

    protected override void _ChildReady()
    {
        if(content is AddLocalInspectorContent<FoodItem> bankContent)
        {
            var events = GetNode<GlobalEvents>("/root/GlobalEvents");
            events.NewBankFood += (item) => UpdateItem(item);
            events.RemoveBankFood += (item) => RemoveItem(item);
            events.NewLocalFood += (FoodWithCount food) => bankContent.SetLocked(food.Item, true);
            events.RemoveLocalFood += (FoodWithCount food) => bankContent.SetLocked(food.Item, false);
        }
        else
            throw new Exception("Content of AddLocalFoodInspector is not of AddLocalFoodInspectorContent type");
    }
}