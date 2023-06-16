partial class FormItemSetComponent : Container, FormComponent<ItemSet>
{
    public ItemSet GetValue => new ItemSet(itemSet);
    public bool IsCompleted => GetValue.Food.Count() != 0 && GetValue.Inventory.Count() != 0;
    public event Action? ComponentChanged;

    public event Action<FoodWithCount>? AddedFood;
    public event Action<InventoryItem>? AddedInv;
    public event Action<FoodWithCount>? RemovedFood;
    public event Action<InventoryItem>? RemovedInv;
    public event Action<ItemSet>? NewItemSet;

    ItemSet itemSet = new();

    public void SetValue(ItemSet value) {
        this.itemSet = new ItemSet(value);
        NewItemSet?.Invoke(this.itemSet);
        ComponentChanged?.Invoke();
    }

    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        var foodButton = GetNode<Button>("AllSubsection/Content/FoodTab/Button");
        var invButton = GetNode<Button>("AllSubsection/Content/InvTab/Button");

        events.FileLoaded += () => itemSet = new();

        foodButton.Pressed += () => { 
            events.CallOpenRecipeFoodMenu(this);
        };

        invButton.Pressed += () => {
            events.CallOpenRecipeInvMenu(this);
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

    public void RemoveFood(FoodWithCount remove) {
        itemSet.FoodList.RemoveAll(f => f.Name == remove.Name);
        RemovedFood?.Invoke(remove);
        ComponentChanged?.Invoke();
    }

    public void RemoveInv(InventoryItem item) {
        itemSet.InventoryList.Remove(item);
        RemovedInv?.Invoke(item);
        ComponentChanged?.Invoke();
    }
}