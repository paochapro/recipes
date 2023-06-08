partial class CreateRecipeForm : CreateForm<Recipe>
{
	#nullable disable
	FormImageComponent imageComponent;
	#nullable restore

	public override void _Ready()
	{
		imageComponent = GetNode<FormImageComponent>("Image");
		base._Ready();
	}
	
	public override Recipe CreateObject()
	{
		string title = GetNode<FormLineEditComponent>("Title").GetValue;
		string foodStr = GetNode<FormLineEditComponent>("Food").GetValue;
		string invStr = GetNode<FormLineEditComponent>("Inv").GetValue;
		string instuctions = GetNode<TextEdit>("Instructions/TextEdit").Text;
		string imagePath = imageComponent.GetValue;

		int selectedDishTypeInt = GetNode<OptionButton>("DishType/OptionButton").Selected;
		DishType dishType = (DishType)selectedDishTypeInt;

		int time = (int)GetNode<SpinBox>("Time/SpinBox").Value;

		var program = GetNode<Program>("/root/Program");
		var foodResult = GetFoodWithCountByString(foodStr, program.ItemsBank.Food);
		var invResult = GetItemsByString<InventoryItem>(invStr, program.ItemsBank.Inventory);
		ItemSet set = new(foodResult.ToList(), invResult.ToList());
		Recipe recipe = new(title, instuctions, imagePath, time, set, dishType);
		
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