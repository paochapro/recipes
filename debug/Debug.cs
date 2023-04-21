partial class Debug : Node
{
    const string defCategory = "category_placeholder";
    const string defTexture = "texture_uid_placeholder";

    RecipeGeneratorInfo recipeInfo;
    ItemsSelectorInfo itemsInfo;

    List<Recipe> recipes;
    ItemSet userItemSet;
    ItemSet filterItemSet;

    //Nodes
    #nullable disable
    DebugControl control;
    #nullable restore

    public Debug()
    {
        userItemSet = new();
        filterItemSet = new();
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

    public void Compare(double minutesValue, int dishTypeValue)
    {   
        DishType dishType = (DishType)Mathf.Clamp(dishTypeValue, 0, Enum.GetValues(typeof(DishType)).Length);
        int minutes = Mathf.RoundToInt(minutesValue);
        SearchInfo info = new(userItemSet, filterItemSet, dishType, minutes);
        var result = RecipeSearch.Search(recipes, info);
        control.CompareUpdate(userItemSet, result);
    }

    public void ClearLocalItems()
    {
        userItemSet = new();
        control.FoodUpdate(userItemSet.Food);
        control.InventoryUpdate(userItemSet.Inventory);
    }

    public void ClearFilterItems()
    {
        filterItemSet = new();
        control.FilterFoodUpdate(filterItemSet.Food);
        control.FilterInventoryUpdate(filterItemSet.Inventory);
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
            
        Recipe recipe = new(title, defCategory, defTexture, 0, itemSet, DishType.None);

        recipes.Add(recipe);
        control.RecipesUpdate(recipes);
    }

    public void AddFoodItem(string str) 
    {
        var result = CreateFoodItemByString(str, userItemSet.FoodItems);
        userItemSet.FoodList.Add(result);
        control.FoodUpdate(userItemSet.Food);
    }

    public void AddInventoryItem(string str) 
    {
        userItemSet.InventoryList.Add(CreateInventoryItemByString(str, userItemSet.Inventory));
        control.InventoryUpdate(userItemSet.Inventory);
    }

    public void AddFilterFoodItem(string str)
    {
        var food = CreateFoodItemByString(str, filterItemSet.FoodItems);
        filterItemSet.FoodList.Add(food);
        control.FilterFoodUpdate(filterItemSet.Food);
    }

    public void AddFilterInvItem(string str)
    {
        filterItemSet.InventoryList.Add(CreateInventoryItemByString(str, filterItemSet.Inventory));
        control.FilterInventoryUpdate(filterItemSet.Inventory);
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
        var items = itemNames.Select(n => new InventoryItem(n, defCategory));
        return items.ToList();
    }

    FoodWithCount CreateFoodItemByString(string str, IEnumerable<FoodItem> foodBank)
    {
        int count = 1;
        string name = str;
        char lastChar = str.Last();

        if(char.IsDigit(lastChar))
        {
            count = (int)char.GetNumericValue(lastChar);
            name = str.Substring(0, str.Length-1);
        }

        FoodItem defaultItem = new FoodItem();
        FoodItem resultItem = foodBank.FirstOrDefault(f => f.Name == name, defaultItem);

        if(resultItem == defaultItem)
        {
            resultItem = new(name, defCategory, defTexture);
        }

        return new FoodWithCount(resultItem, count);
    }

    InventoryItem CreateInventoryItemByString(string str, IEnumerable<InventoryItem> invBank)
    {
        InventoryItem defaultItem = new InventoryItem();
        InventoryItem resultItem = invBank.FirstOrDefault(f => f.Name == str, defaultItem);

        if(resultItem == defaultItem) {
            resultItem = new(str, defCategory);
        }

        return resultItem;
    }
}