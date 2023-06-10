partial class GlobalEvents : Node
{
    public event Action<DynamicWindowMenu>?     SwitchDynamicWindow;
    public event Action<SectionAMenu>?          SwitchSectionA;
    public event Action<FoodItem>?              NewBankFood;
    public event Action<InventoryItem>?         NewBankInv;
    public event Action<FoodWithCount>?         NewLocalFood;
    public event Action<InventoryItem>?         NewLocalInv;
    public event Action<FoodItem>?              RemoveBankFood;
    public event Action<InventoryItem>?         RemoveBankInv;
    public event Action<FoodWithCount>?         RemoveLocalFood;
    public event Action<InventoryItem>?         RemoveLocalInv;

    public event Action<Recipe>?         NewRecipe;
    public event Action<Recipe>?         RemoveRecipe;

    //public event Action<FoodWithCount>?         AddRecipeFood;
    //public event Action<InventoryItem>?         AddRecipeInv;

    public event Action<FormItemSetComponent>?  OpenRecipeItemsMenu;

    public void CallSwitchDynamicWindow(DynamicWindowMenu val)  => SwitchDynamicWindow?.Invoke(val);
    public void CallSwitchSectionA(SectionAMenu val)            => SwitchSectionA?.Invoke(val);
    public void CallNewBankFood(FoodItem val)                   => NewBankFood?.Invoke(val);
    public void CallNewBankInv(InventoryItem val)               => NewBankInv?.Invoke(val);
    public void CallNewLocalFood(FoodWithCount val)             => NewLocalFood?.Invoke(val);
    public void CallNewLocalInv(InventoryItem val)              => NewLocalInv?.Invoke(val);
    public void CallRemoveBankFood(FoodItem val)                => RemoveBankFood?.Invoke(val);
    public void CallRemoveBankInv(InventoryItem val)            => RemoveBankInv?.Invoke(val);
    public void CallRemoveLocalFood(FoodWithCount val)          => RemoveLocalFood?.Invoke(val);
    public void CallRemoveLocalInv(InventoryItem val)           => RemoveLocalInv?.Invoke(val);
    public void CallNewRecipe(Recipe val)                       => NewRecipe?.Invoke(val);
    public void CallRemoveRecipe(Recipe val)                    => RemoveRecipe?.Invoke(val);
    public void CallOpenRecipeItemsMenu(FormItemSetComponent component)  => OpenRecipeItemsMenu?.Invoke(component);
    //public void CallAddRecipeFood(FoodWithCount val)            => AddRecipeFood?.Invoke(val);
    //public void CallAddRecipeInv(InventoryItem val)             => AddRecipeInv?.Invoke(val);
}