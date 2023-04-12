public partial class Debug : Node
{
	const string texturePlaceholder = "texture_uid_placeholder";

	//Data
	RecipeGeneratorInfo recipeInfo;
	ItemsSelectorInfo itemsInfo;

	ItemSet _localItemSet;
	List<Recipe> _recipes;

	ItemSet localItemSet {
		get => _localItemSet;
		set {
			_localItemSet = value;
			control.LocalItemsUpdate(_localItemSet);
		}
	}

	List<Recipe> recipes {
		get => _recipes;
		set {
			_recipes = value;
			control.RecipesUpdate(_recipes);
		}
	}

	void ItemSetFoodAdd(FoodItem item) {
		localItemSet.FoodList.Add(item);
		control.LocalItemsUpdate(localItemSet);
	}

	void ItemSetInventoryAdd(InventoryItem item) {
		localItemSet.InventoryList.Add(item);
		control.LocalItemsUpdate(localItemSet);
	}

	void RecipesListAdd(Recipe recipe) {
		recipes.Add(recipe);
		control.RecipesUpdate(recipes);
	}

	//Nodes
	#nullable disable
	DebugControl control;
	#nullable restore

	public Debug()
	{
		_localItemSet = new();
		_recipes = new();
	}

	public override void _Ready()
	{
		string itemsJsonFile = "debug/items.json";
		ReadonlyItemSet localItemSet = ItemsFromJson.GetItemsFromJson(itemsJsonFile);

		itemsInfo = new ItemsSelectorInfo() {
			avaliableItems = localItemSet,
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
		localItemSet = ItemsSelector.GetRandomItemSet(itemsInfo);
	}

	public void GenerateRecipes() 
	{
		recipes = RecipesGenerator.GenerateRecipes(recipeInfo).ToList();
	}

	public void Compare()
	{
		if(recipes == null || localItemSet == null)
			return;

		var result = RecipeCompare.Compare(recipes, localItemSet);
		
		control.CompareUpdate(localItemSet, result);
	}

	public void ClearLocalItems() => localItemSet = new();
	public void ClearRecipes() => recipes = new();
	public void ClearCompare() => control.CompareUpdate(new ItemSet(), new Recipe[0]);

	public void AddRecipe(string title, string foodStr, string invStr)
	{
		var createFoodFunc = (string name, string category) => {
			return new FoodItem(name, category, texturePlaceholder);
		};

		var createInvFunc = (string name, string category) => {
			return new InventoryItem(name, category);
		};

		List<FoodItem> food = MakeItemList(foodStr, localItemSet?.Food , createFoodFunc);
		List<InventoryItem> inv = MakeItemList(invStr, localItemSet?.Inventory, createInvFunc);
		
		ItemSet itemSet = new(food, inv);
		
		if(title == "")
			title = $"recipe{recipes.Count+1}";
			
		RecipeSearchData searchData = new(itemSet, DishType.None, 0);
		Recipe recipe = new(title, "instructions_placeholder", texturePlaceholder, searchData);
		RecipesListAdd(recipe);
	}

	public void AddFoodItem(string name, string category) {
		FoodItem item = new(name, category, texturePlaceholder);
		ItemSetFoodAdd(item);
	}

	public void AddInventoryItem(string name, string category) {
		InventoryItem item = new(name, category);
		ItemSetInventoryAdd(item);
	}

	List<TItem> MakeItemList<TItem>(string itemStr, IEnumerable<TItem>? existingItems, Func<string, string, TItem> createFunc)
		where TItem : Item
	{
		string[] itemNames = itemStr.Split(',');
		var items = itemNames.Select(n => CreateRecipeItem(n, existingItems, createFunc));
		return items.ToList();
	}

	TItem CreateRecipeItem<TItem>(string itemName, IEnumerable<TItem>? existingItems, Func<string, string, TItem> createFunc)
		where TItem : Item
	{
		//If existing items contains an item with a name 'itemName'
		if(existingItems != null && existingItems.Select(i => i.Name).Contains(itemName))
			return existingItems.First(i => i.Name == itemName);

		var newItem = createFunc.Invoke(itemName, "category_placeholder");
		return newItem;
	}
}