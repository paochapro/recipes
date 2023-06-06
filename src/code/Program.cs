partial class Program : Node
{
    List<Recipe> recipeBank = new();
    ItemBank itemsBank = new();
    ItemSet localItems = new();

    //Getters
    public IEnumerable<Recipe> RecipeBank => recipeBank;
    public ReadonlyItemBank ItemsBank => itemsBank;
    public ReadonlyItemSet LocalItems => localItems;

    #nullable disable
    GlobalEvents events;
    #nullable restore

    public Program()
    {
        //TODO: Debug this, and remove
        recipeBank = new List<Recipe>();
        Recipe testRecipe = new("Test Recipe", "1. Hello\n2. Goodbye", "res://content/tomato.svg", 20, new ItemSet(), DishType.Second);
        recipeBank.Add(testRecipe);

        //Debug
        string itemsJsonFile = "content/items.json"; 
        var items = ItemsFromJson.GetItemsFromJson(itemsJsonFile);
        itemsBank = items;
        localItems = ItemSetGenerator.SelectAllFromBank(itemsBank, 1);
    }

    public override void _Ready()
    {
        events = GetNode<GlobalEvents>("/root/GlobalEvents");
    }

    public void AddFoodItem(FoodItem item) {
        itemsBank.FoodList.Add(item);
        events.CallNewBankFood(item);
    }

    public void AddInvItem(InventoryItem item) {
        itemsBank.InventoryList.Add(item);
        events.CallNewBankInv(item);
    }

    public void AddLocalFood(FoodWithCount item) {
        localItems.FoodList.Add(item);
        events.CallNewLocalFood(item);
    }

    public void AddLocalInv(InventoryItem item) {
        localItems.InventoryList.Add(item);
        events.CallNewLocalInv(item);
    }

    public void RemoveFoodItem(FoodItem item) 
    {
        if(localItems.FoodItems.Contains(item))
        {
            GD.PrintErr("Local items contains an item that was reqeusted to be removed: " + item.Name);
            return;
        }

        itemsBank.FoodList.Remove(item);
        events.CallRemoveBankFood(item);
    }

    public void RemoveInvItem(InventoryItem item) 
    {
        if(localItems.Inventory.Contains(item))
        {
            GD.PrintErr("Local items contains an item that was reqeusted to be removed: " + item.Name);
            return;
        }

        itemsBank.InventoryList.Remove(item);
        events.CallRemoveBankInv(item);
    }

    public void RemoveLocalFood(FoodWithCount item) { 
        localItems.FoodList.Remove(item);
        events.CallRemoveLocalFood(item);
    }

    public void RemoveLocalInv(InventoryItem item) {
        localItems.InventoryList.Remove(item);
        events.CallRemoveLocalInv(item);
    }

    public void AddRecipe(Recipe recipe) => recipeBank.Add(recipe);
}