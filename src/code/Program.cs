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
		
		//TODO: Debug this, and remove
		// var itemset = ItemSetGenerator.GenerateItemSet(new ItemsSelectorInfo(itemsBank, 0.5f, 0.5f));
		// recipeBank = new List<Recipe>();
		// Recipe testRecipe = new("Test Recipe", "1. Hello\n2. Goodbye", "res://content/tomato.svg", 20, itemset, DishType.Second);
		// recipeBank.Add(testRecipe);
	}

	public void LoadFileTable(FileTable fileTable)
	{
		recipeBank = fileTable.recipeBank.ToList();
		itemsBank = new ItemBank(fileTable.bankFood.ToList(), fileTable.bankInv.ToList());

		var localFood = fileTable.localFood.Select(f => new FoodWithCount(f.FoodItem, f.Count)).ToList();
		localItems = new ItemSet(localFood, fileTable.localInv.ToList());

		events.CallFileLoaded();
	}

	public override void _Ready()
	{
		events = GetNode<GlobalEvents>("/root/GlobalEvents");

		const string testTable = "B:/projects/darya_recipes/content/test_table.json";
		var fileManager = new FileManager();
		AddChild(fileManager);
		fileManager.CallDeferred(FileManager.MethodName.LoadFile, testTable);

		//this.CallDeferred(Program.MethodName.DebugFileUpdate);
	}

	void DebugFileUpdate() => events.CallFileLoaded();
	
	public void FoodItemModified(FoodItem modifiedItem) => ItemModified(modifiedItem);
	public void InvItemModified(InventoryItem modifiedItem) => ItemModified(modifiedItem);

	void ItemModified(Item modifiedItem)
	{
		recipeBank.ForEach(r => ModifyItemSet(r.ItemSet, modifiedItem));
		ModifyItemSet(localItems, modifiedItem);

		if(modifiedItem is FoodItem modifyFood) {
			itemsBank.FoodList.Replace(i => i.Name == modifiedItem.Name, modifyFood);
			events.CallFoodModified(modifyFood);
		}

		if(modifiedItem is InventoryItem modifyInv) {
			itemsBank.InventoryList.Replace(i => i.Name == modifiedItem.Name, modifyInv);
			events.CallInvModified(modifyInv);
		} 
	}

	void ModifyItemSet(ItemSet itemSet, Item modifiedItem)
	{
		if(modifiedItem is FoodItem modifyFood) {
			var food = itemSet.FoodList.FirstOrDefault(f => f.Name == modifiedItem.Name);

			if(food != null)
				itemSet.FoodList.Replace(food, new FoodWithCount(modifyFood, food.Count));
		}

		if(modifiedItem is InventoryItem modifyInv) {
			var inv = itemSet.InventoryList.FirstOrDefault(f => f.Name == modifiedItem.Name);
			
			if(inv != new InventoryItem()) 
				itemSet.InventoryList.Replace(inv, modifyInv);
		}
	}

	public void AddFoodItem(FoodItem item) => Add(itemsBank.FoodList, item, events.CallNewBankFood);
	public void AddInvItem(InventoryItem item) => Add(itemsBank.InventoryList, item, events.CallNewBankInv);
	public void AddLocalFood(FoodWithCount item) => Add(localItems.FoodList, item, events.CallNewLocalFood);
	public void AddLocalInv(InventoryItem item) => Add(localItems.InventoryList, item, events.CallNewLocalInv);

	void Add<T>(List<T> list, T item, Action<T> eventAction)
		where T : Item
	{
		if(list.Contains(item, new ItemNameEqualityComparer<T>())) {
			GD.Print("Failed to add item " + item);
			return;
		}

		list.Add(item);
		eventAction.Invoke(item);
	}

	public void RemoveFoodItem(FoodItem item) 
	{
		var dependedLocalItem = localItems.Food.FirstOrDefault(f => f.Item == item);
		var dependedRecipes = recipeBank.Where(r => r.ItemSet.FoodItems.Contains(item));

		if(dependedLocalItem != null)
			localItems.FoodList.RemoveAll(i => i.Name == item.Name);

		if(dependedRecipes != null) {
			foreach(var foodList in recipeBank.Select(r => r.ItemSet.FoodList))
				foodList.RemoveAll(i => i.Name == item.Name);
		}

		itemsBank.FoodList.Remove(item);
		events.CallRemoveBankFood(item);
	}

	public void RemoveInvItem(InventoryItem item)
	{
		bool dependedLocalItemExists = localItems.Inventory.Contains(item);
		var dependedRecipes = recipeBank.Where(r => r.ItemSet.Inventory.Contains(item));

		if(dependedLocalItemExists)
			localItems.InventoryList.Remove(item);

		if(dependedRecipes != null) {
			foreach(var invList in recipeBank.Select(r => r.ItemSet.InventoryList))
				invList.Remove(item);
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

	public void RecipeModified(Recipe modifyRecipe) 
	{
		recipeBank.Replace(r => r.Title == modifyRecipe.Title, modifyRecipe);
		events.CallRecipeModified(modifyRecipe);
	}

	public void AddRecipe(Recipe recipe) 
	{
		if(recipeBank.Any(r => r.Title.ToLower() == recipe.Title.ToLower())) {
			GD.Print("Failed to add recipe: " + recipe.Title);
			return;
		}

		recipeBank.Add(recipe);
		events.CallNewRecipe(recipe);
	}

	public void RemoveRecipe(Recipe recipe) { 
		recipeBank.Remove(recipe);
		events.CallRemoveRecipe(recipe);
	}
}
