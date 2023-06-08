partial class AddLocalFoodInspector : ItemsInspector<FoodItem>
{
    protected override IEnumerable<FoodItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Food;

    protected override void _ChildReady()
    {
        if(content is AddLocalInspectorContent<FoodItem> bankContent)
        {
            var events = GetNode<GlobalEvents>("/root/GlobalEvents");
            events.NewBankFood += (item) => bankContent.UpdateItem(item);
            events.RemoveBankFood += (item) => bankContent.RemoveItem(item);
            events.NewLocalFood += (FoodWithCount food) => bankContent.SetLocked(food.Item, true);
            events.RemoveLocalFood += (FoodWithCount food) => bankContent.SetLocked(food.Item, false);
        }
        else
            throw new Exception("Content of AddLocalFoodInspector is not of AddLocalFoodInspectorContent type");
    }
}