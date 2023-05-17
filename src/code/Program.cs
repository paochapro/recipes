partial class Program : Node
{
    List<Recipe> recipeBank = new();
    ItemBank itemsBank = new();
    ItemSet localItems = new();

    //Getters
    public IEnumerable<Recipe> RecipeBank => recipeBank;
    public ReadonlyItemBank ItemsBank => itemsBank;
    public ReadonlyItemSet LocalItems => localItems;

    public Program()
    {
        //Debug
        string itemsJsonFile = "content/items.json"; 
        var items = ItemsFromJson.GetItemsFromJson(itemsJsonFile);
        itemsBank = items;
        localItems = ItemSetGenerator.SelectAllFromBank(itemsBank, 1);
    }

    //Bank
    public void AddFoodItem(FoodItem item) 
    {
        itemsBank.FoodList.Add(item);
        GetNode<GlobalEvents>("/root/GlobalEvents").CallNewBankFoodItem(item);
    }

    public void AddInvItem(InventoryItem item) 
    {
        itemsBank.InventoryList.Add(item);
        GetNode<GlobalEvents>("/root/GlobalEvents").CallNewBankInvItem(item);
    }

    public void RemoveFoodItem(FoodItem item) => itemsBank.FoodList.Remove(item);
    public void RemoveInvItem(InventoryItem item) => itemsBank.InventoryList.Remove(item);

    public void AddRecipe(Recipe recipe) => recipeBank.Add(recipe);

    //Local items
    public void AddLocalFood(FoodWithCount item) => localItems.FoodList.Add(item);
    public void AddLocalInv(InventoryItem item) => localItems.InventoryList.Add(item);
    public void RemoveLocalFood(FoodWithCount item) => localItems.FoodList.Remove(item);
    public void RemoveLocalInv(InventoryItem item) => localItems.InventoryList.Remove(item);
}