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
        GD.Print("start program");

        //Debug
        string itemsJsonFile = "content/items.json"; 
        var items = ItemsFromJson.GetItemsFromJson(itemsJsonFile);
        itemsBank = items;
        localItems = ItemSetGenerator.SelectAllFromBank(itemsBank, 1);

        //TODO: Debug this, and remove
        var itemset = ItemSetGenerator.GenerateItemSet(new ItemsSelectorInfo(itemsBank, 0.5f, 0.5f));
        recipeBank = new List<Recipe>();
        Recipe testRecipe = new("Test Recipe", "1. Hello\n2. Goodbye", "res://content/tomato.svg", 20, itemset, DishType.Second);
        recipeBank.Add(testRecipe);
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
        var dependedLocalItem = localItems.Food.FirstOrDefault(f => f.Item == item);
        var dependedRecipes = recipeBank.Where(r => r.ItemSet.FoodItems.Contains(item));

        if(dependedLocalItem != null || dependedRecipes.Count() != 0) {
            events.CallFailedFoodRemove(item, dependedLocalItem, dependedRecipes);
            return;
        }

        itemsBank.FoodList.Remove(item);
        events.CallRemoveBankFood(item);
    }

    public void RemoveInvItem(InventoryItem item) 
    {
        var dependedLocalItemExists = localItems.Inventory.Contains(item);
        var dependedRecipes = recipeBank.Where(r => r.ItemSet.Inventory.Contains(item));

        if(dependedLocalItemExists || dependedRecipes.Count() != 0) {
            events.CallFailedInvRemove(item, dependedLocalItemExists ? item : null, dependedRecipes);
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

    public void AddRecipe(Recipe recipe) { 
        recipeBank.Add(recipe);
        events.CallNewRecipe(recipe);
    }

    public void RemoveRecipe(Recipe recipe) { 
        recipeBank.Remove(recipe);
        events.CallRemoveRecipe(recipe);
    }
}