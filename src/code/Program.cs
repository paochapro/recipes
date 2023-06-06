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
        GD.Print("fmwoafkiawf");

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

    //Bank
    public void AddFoodItem(FoodItem item) 
    {
        GD.Print("Added food item: " + item.Name);

        itemsBank.FoodList.Add(item);
        events.CallNewBankFoodItem(item);
    }

    public void AddInvItem(InventoryItem item) 
    {
        GD.Print("Added inv item: " + item.Name);

        itemsBank.InventoryList.Add(item);
        events.CallNewBankInvItem(item);
    }

    public void RemoveFoodItem(FoodItem item) => itemsBank.FoodList.Remove(item);
    public void RemoveInvItem(InventoryItem item) => itemsBank.InventoryList.Remove(item);

    public void AddRecipe(Recipe recipe) { GD.Print("Added recipe item: " + recipe.Title); recipeBank.Add(recipe); }

    //Local items
    public void AddLocalFood(FoodWithCount item) 
    {
        localItems.FoodList.Add(item);
        events.CallNewLocalFood(item);
    }

    public void AddLocalInv(InventoryItem item) 
    { 
        localItems.InventoryList.Add(item);
        events.CallNewLocalInv(item);
    }

    public void RemoveLocalFood(FoodWithCount item) 
    { 
        localItems.FoodList.Remove(item);
        events.CallRemoveLocalFood(item);
    }

    public void RemoveLocalInv(InventoryItem item) 
    {
        localItems.InventoryList.Remove(item);
        events.CallRemoveLocalInv(item);
    }
}