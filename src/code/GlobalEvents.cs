partial class GlobalEvents : Node
{
    public event Action<DynamicWindowMenu>? SwitchDynamicWindow;
    public event Action<FoodItem>? NewBankFoodItem;
    public event Action<InventoryItem>? NewBankInvItem;

    public void CallSwitchDynamicWindow(DynamicWindowMenu val) => SwitchDynamicWindow?.Invoke(val);
    public void CallNewBankFoodItem(FoodItem val) => NewBankFoodItem?.Invoke(val);
    public void CallNewBankInvItem(InventoryItem val) => NewBankInvItem?.Invoke(val);
}