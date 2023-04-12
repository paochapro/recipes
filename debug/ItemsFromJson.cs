using Newtonsoft.Json;
using System.IO;

static class ItemsFromJson
{
    public static ItemSet GetItemsFromJson(string itemsJsonFile)
	{
		List<FoodItem> food = new();
		List<InventoryItem> inventory = new();

		using(StreamReader reader = new(itemsJsonFile))
		{
			string json = reader.ReadToEnd();
			var dict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string[]>>>(json);

			if(dict != null)
			{
				food = GetItemsFromItemType<FoodItem>(dict["food"], GetNewFoodItem).ToList();
				inventory = GetItemsFromItemType<InventoryItem>(dict["inventory"], GetNewInventoryItem).ToList();
			}
		}

		return new(food, inventory);
	}

	static IEnumerable<TItem> GetItemsFromItemType<TItem>(Dictionary<string, string[]> categories, Func<string, string, TItem> getNewItem)
	{
		foreach(var categoryPair in categories)
		{
			string category = categoryPair.Key;
			string[] items = categoryPair.Value;

			foreach(string item in items)
			{
				yield return getNewItem.Invoke(item, category);
			}
		}
	}

	static FoodItem GetNewFoodItem(string name, string category) => new FoodItem(name, category, "texture_uid_placeholder");
	static InventoryItem GetNewInventoryItem(string name, string category) => new InventoryItem(name, category);
}