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
    }

    public void AddFoodItem(FoodItem item)
    { 
        GD.Print("Added " + item.Name + " to program");
        itemsBank.FoodList.Add(item);
    }

    public void AddInvItem(InventoryItem item)
    {
        GD.Print("Added " + item.Name + " to program");
        itemsBank.InventoryList.Add(item);
    }
        
    public void AddRecipe(Recipe recipe)
    {
        GD.Print("Added " + recipe.Title + " to program");
        recipeBank.Add(recipe);
    }
}