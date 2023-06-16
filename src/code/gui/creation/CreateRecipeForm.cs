partial class CreateRecipeForm : CreateForm<Recipe>
{
	#nullable disable
	FormImageComponent imageComponent;
	#nullable restore

	public override void _Ready()
	{
		imageComponent = GetNode<FormImageComponent>("Image");
		base._Ready();

        // #if DEBUG
        //     GetNode<FormTextEditChanged>("Instructions").SetValue("debug");
        //     GetNode<FormOptionsComponent>("DishType").SetValue(1);
        //     GetNode<FormSpinBoxComponent>("Time").SetValue(1);
        //     imageComponent.SetValue("res://content/button_bg.png");
        // #endif
	}
	
	public override Recipe CreateObject()
	{
		string title = GetNode<FormLineEditComponent>("Title").GetValue;
		string instuctions = GetNode<FormTextEditChanged>("Instructions").GetValue;
		string imagePath = imageComponent.GetValue;
        ItemSet itemSet = GetNode<FormItemSetComponent>("ItemSet").GetValue;
		int selectedDishTypeInt = GetNode<FormOptionsComponent>("DishType").GetValue;
		DishType dishType = (DishType)selectedDishTypeInt;

		int time = (int)GetNode<FormSpinBoxComponent>("Time").GetValue;

		// var program = GetNode<Program>("/root/Program");
		// var foodResult = GetFoodWithCountByString(foodStr, program.ItemsBank.Food);
		// var invResult = GetItemsByString<InventoryItem>(invStr, program.ItemsBank.Inventory);
		// ItemSet set = new(foodResult.ToList(), invResult.ToList());

		Recipe recipe = new(title, instuctions, imagePath, time, itemSet, dishType);
		
		return recipe;
	}

	//Debug (Works for now) 
	IEnumerable<FoodWithCount> GetFoodWithCountByString(string value, IEnumerable<FoodItem> avaliableItems)
	{
		string[] foodValues = value.Split(',');
		return foodValues.Select(v => GetFoodItemByString(v, avaliableItems));
	}

	FoodWithCount GetFoodItemByString(string str, IEnumerable<FoodItem> foodBank)
	{
		int count = 1;
		string name = str;
		char lastChar = str.Last();

		if(char.IsDigit(lastChar)) 
		{
			count = (int)char.GetNumericValue(lastChar);
			name = str.Substring(0, str.Length-1);
		}

		FoodItem? resultItem = foodBank.FirstOrDefault(f => f.Name == name);
		
		if(resultItem == null)
			throw new CustomErrorException("Некоторые предметы не существует в банке");

		return new FoodWithCount((FoodItem)resultItem, count);
	}

	IEnumerable<TItem> GetItemsByString<TItem>(string value, IEnumerable<TItem> avaliableItems)
		where TItem : Item
	{
		string[] itemNames = value.Split(',');

		if(!itemNames.All(name => avaliableItems.Select(i => i.Name).Contains(name)))
			throw new CustomErrorException("Некоторые предметы не существует в банке");

		return avaliableItems.Where(i => itemNames.Contains(i.Name));
	}
}