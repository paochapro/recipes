public partial class Debug : Node
{
    const string categoryPlaceholder = "category_placeholder";
    const string texturePlaceholder = "texture_uid_placeholder";

    RecipeGeneratorInfo recipeInfo;
    ItemsSelectorInfo itemsInfo;

    ItemSet userItemSet;
    List<Recipe> recipes;

    //Nodes
    #nullable disable
    DebugControl control;
    #nullable restore

    public Debug()
    {
        userItemSet = new();
        recipes = new();
    }

    public override void _Ready()
    {
        string itemsJsonFile = "debug/items.json";
        ReadonlyItemBank avaliableItemsBank = ItemsFromJson.GetItemsFromJson(itemsJsonFile);

        itemsInfo = new ItemsSelectorInfo() {
            avaliableItems = avaliableItemsBank,
            minItemRatio = 1/3,
            maxItemRatio = 1.0f,
        };

        recipeInfo = new RecipeGeneratorInfo() {
            itemsGeneratorInfo = itemsInfo,
            minRandomMinutes = 20,
            maxRandomMinutes = 100,
            minRandomRecipes = 3,
            maxRandomRecipes = 10,
        };

        control = GetNode<DebugControl>("Control");
    }

    public void GenerateLocalItems()
    {
        userItemSet = ItemSetGenerator.GenerateItemSet(itemsInfo);
        control.FoodUpdate(userItemSet.Food);
        control.InventoryUpdate(userItemSet.Inventory);
    }

    public void GenerateRecipes() 
    {
        recipes = RecipesGenerator.GenerateRecipes(recipeInfo).ToList();
        control.RecipesUpdate(recipes);
    }

    public void Compare()
    {
        var result = RecipeCompare.Compare(recipes, userItemSet);
        control.CompareUpdate(userItemSet, result);
    }

    public void ClearLocalItems()
    {
        userItemSet = new();
        control.FoodUpdate(userItemSet.Food);
        control.InventoryUpdate(userItemSet.Inventory);
    }

    public void ClearRecipes() 
    {
        recipes.Clear();
        control.RecipesUpdate(recipes);
    }
    
    public void ClearCompare() => control.CompareUpdate(new ItemSet(), new Recipe[0]);

    public void AddRecipe(string title, string foodStr, string invStr)
    {
        List<FoodWithCount> food = CreateFoodByString(foodStr, userItemSet.Food.Select(i => i.Item));
        List<InventoryItem> inv = CreateInventoryByString(invStr, userItemSet.Inventory);
        
        ItemSet itemSet = new(food, inv);
        
        if(title == "")
            title = $"recipe{recipes.Count+1}";
            
        RecipeSearchData searchData = new(itemSet, DishType.None, 0);
        Recipe recipe = new(title, "instructions_placeholder", texturePlaceholder, searchData);

        recipes.Add(recipe);
    }

    public void AddFoodItem(string str) 
    {
        var result = CreateFoodItemByString(str, userItemSet.Food.Select(f => f.Item).ToList());
        userItemSet.FoodList.Add(result);
        control.FoodUpdate(userItemSet.Food);
    }

    public void AddInventoryItem(string str) 
    {
        userItemSet.InventoryList.Add(CreateInventoryItemByString(str, userItemSet.Inventory));
        control.InventoryUpdate(userItemSet.Inventory);
    }

    //fuck dry im lazy
    List<FoodWithCount> CreateFoodByString(string text, IEnumerable<FoodItem> foodBank)
    {
        string[] foodData = text.Split(',');
        return foodData.Select(str => CreateFoodItemByString(str, foodBank)).ToList();
    }

    List<InventoryItem> CreateInventoryByString(string itemStr, IEnumerable<InventoryItem> invBank)
    {
        string[] itemNames = itemStr.Split(',');
        var items = itemNames.Select(n => new InventoryItem(n, categoryPlaceholder));
        return items.ToList();
    }

    FoodWithCount CreateFoodItemByString(string str, IEnumerable<FoodItem> foodBank)
    {
        int count = Convert.ToInt32(str.Last());
        string name = new string(str.SkipLast(1).ToArray());

        FoodItem defaultItem = new FoodItem();
        FoodItem resultItem = foodBank.FirstOrDefault(f => f.Name == name, defaultItem);

        if(resultItem == defaultItem)
        {
            resultItem = new(name, categoryPlaceholder, texturePlaceholder);
        }

        return new FoodWithCount(resultItem, count);
    }

    InventoryItem CreateInventoryItemByString(string str, IEnumerable<InventoryItem> invBank)
    {
        InventoryItem defaultItem = new InventoryItem();
        InventoryItem resultItem = invBank.FirstOrDefault(f => f.Name == str, defaultItem);

        if(resultItem == defaultItem) {
            resultItem = new(str, categoryPlaceholder);
        }

        return resultItem;
    }
}