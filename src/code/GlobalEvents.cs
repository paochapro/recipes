partial class GlobalEvents : Node
{
    public event Action? FileLoaded;
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

    public event Action<Recipe>?                NewRecipe;
    public event Action<Recipe>?                RemoveRecipe;
    public event Action<FormItemSetComponent>?  OpenRecipeFoodMenu;
    public event Action<FormItemSetComponent>?  OpenRecipeInvMenu;
    public event Action<FoodItem>?              OpenFoodModificationMenu;
    public event Action<InventoryItem>?         OpenInvModificationMenu;

    public event Action<FoodItem, FoodWithCount?, IEnumerable<Recipe>>? FailedFoodRemove;
    public event Action<InventoryItem, InventoryItem?, IEnumerable<Recipe>>? FailedInvRemove;
    
    public event Action<FoodItem>? FoodModified;
    public event Action<InventoryItem>? InvModified;

    public void CallFileLoaded()                                => FileLoaded?.Invoke();
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

    public void CallOpenRecipeFoodMenu(FormItemSetComponent component)      => OpenRecipeFoodMenu?.Invoke(component);
    public void CallOpenRecipeInvMenu(FormItemSetComponent component)       => OpenRecipeInvMenu?.Invoke(component);
    public void CallFoodModificationMenu(FoodItem val)                      => OpenFoodModificationMenu?.Invoke(val);
    public void CallInvModificationMenu(InventoryItem val)                  => OpenInvModificationMenu?.Invoke(val);
    
    public void CallFoodModified(FoodItem item) 
    {
        FoodModified?.Invoke(item);
    }
    public void CallInvModified(InventoryItem item) => InvModified?.Invoke(item);

    public void CallFailedFoodRemove(
        FoodItem item, 
        FoodWithCount? dependedLocalItem, 
        IEnumerable<Recipe> dependedRecipes) 
    { 
        FailedFoodRemove?.Invoke(item, dependedLocalItem, dependedRecipes);
    }

    public void CallFailedInvRemove(
        InventoryItem item, 
        InventoryItem? dependedLocalItem, 
        IEnumerable<Recipe> dependedRecipes)
    { 
        FailedInvRemove?.Invoke(item, dependedLocalItem, dependedRecipes);
    }

    public int GetFoodModifiedInvocationListCount()
    {
        return FoodModified?.GetInvocationList()?.Count() ?? 0;
    }
}