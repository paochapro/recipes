partial class GlobalEvents : Node
{
    public event Action<DynamicWindowMenu>?     SwitchDynamicWindow;
    public event Action<FoodItem>?              NewBankFoodItem;
    public event Action<InventoryItem>?         NewBankInvItem;
    public event Action<FoodWithCount>?         NewLocalFood;
    public event Action<InventoryItem>?         NewLocalInv;
    public event Action<FoodWithCount>?         RemoveLocalFood;
    public event Action<InventoryItem>?         RemoveLocalInv;

    public void CallSwitchDynamicWindow(DynamicWindowMenu val)  => SwitchDynamicWindow?.Invoke(val);
    public void CallNewBankFoodItem(FoodItem val)               => NewBankFoodItem?.Invoke(val);
    public void CallNewBankInvItem(InventoryItem val)           => NewBankInvItem?.Invoke(val);
    public void CallNewLocalFood(FoodWithCount val)             => NewLocalFood?.Invoke(val);
    public void CallNewLocalInv(InventoryItem val)              => NewLocalInv?.Invoke(val);
    public void CallRemoveLocalFood(FoodWithCount val)          => RemoveLocalFood?.Invoke(val);
    public void CallRemoveLocalInv(InventoryItem val)           => RemoveLocalInv?.Invoke(val);
}