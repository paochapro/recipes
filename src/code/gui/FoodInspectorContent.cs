partial class FoodInspectorContent : ItemsInspectorContent<FoodItem> 
{
    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        
        events.NewBankFood += (item) => UpdateItem(item);
        events.RemoveBankFood += (item) => RemoveItem(item);
        // events.NewLocalFood += (FoodWithCount food) => SetLocked(food.Item, true);
        // events.RemoveLocalFood += (FoodWithCount food) => SetLocked(food.Item, false);
    }

    // void SetLocked(FoodItem compare, bool shouldLock) {
    //     foreach(AddLocalItemButton<FoodItem> button in GetChildren())
    //         if(button.Item == compare)
    //             button.IsLocked = shouldLock;
    // }
}