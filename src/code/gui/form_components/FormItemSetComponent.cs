partial class FormItemSetComponent : Container, FormComponent<ItemSet>
{
    public ItemSet GetValue => itemSet;
    public bool IsCompleted => GetValue.Food.Count() != 0 && GetValue.Inventory.Count() != 0;
    public event Action? ComponentChanged;

    public event Action<FoodWithCount>? AddedFood;
    public event Action<InventoryItem>? AddedInv;
    public event Action<FoodWithCount>? RemovedFood;
    public event Action<InventoryItem>? RemovedInv;


    ItemSet itemSet;

    public FormItemSetComponent() {
        itemSet = new();
    }

    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        var foodButton = GetNode<Button>("AllSubsection/ScrollContainer/Content/FoodTab/Button");
        var invButton = GetNode<Button>("AllSubsection/ScrollContainer/Content/InvTab/Button");

        foodButton.Pressed += () => { 
            events.CallOpenRecipeItemsMenu(this);
            events.CallSwitchSectionA(SectionAMenu.RecipeFood);
        };

        invButton.Pressed += () => {
            events.CallOpenRecipeItemsMenu(this);
            events.CallSwitchSectionA(SectionAMenu.RecipeInv);
        };
    }

    public void AddFood(FoodWithCount food) {
        itemSet.FoodList.Add(food);
        AddedFood?.Invoke(food);
        ComponentChanged?.Invoke();
    }

    public void AddInv(InventoryItem item) {
        itemSet.InventoryList.Add(item);
        AddedInv?.Invoke(item);
        ComponentChanged?.Invoke();
    }

    public void RemoveFood(FoodWithCount food) {
        itemSet.FoodList.Remove(food);
        RemovedFood?.Invoke(food);
        ComponentChanged?.Invoke();
    }

    public void RemoveInv(InventoryItem item) {
        itemSet.InventoryList.Remove(item);
        RemovedInv?.Invoke(item);
        ComponentChanged?.Invoke();
    }
}