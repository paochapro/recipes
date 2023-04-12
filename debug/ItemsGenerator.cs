using System.Linq;

readonly record struct Avaliable(string[] Names, string[] Categories);

static class ItemsSelector
{
	public static ItemSet GetRandomItemSet(ItemsSelectorInfo info)
	{
		ItemSet itemSet = new();

		var food = SelectItems(info.avaliableItems.Food, info.minItemRatio, info.maxItemRatio);
		var inv = SelectItems(info.avaliableItems.Inventory, info.minItemRatio, info.maxItemRatio);

		return new(food.ToList(), inv.ToList());
	}

	static IEnumerable<TItem> SelectItems<TItem>(IEnumerable<TItem> items, float minRatio, float maxRatio)
	{
        GD.Print("Select items!");

		List<TItem> pickItems = items.ToList();
		
		int count = GetItemCount(items.Count(), minRatio, maxRatio);

		for(int i = 0; i < count; i++)
		{
			int index = GD.RandRange(0, pickItems.Count()-1);
			yield return pickItems.ElementAt(index);
			pickItems.RemoveAt(index);
		}
	}

	static int GetItemCount(int itemCount, float minRatio, float maxRatio)
	{
		float t = (float)GD.RandRange(minRatio, maxRatio);
		float floatCount = Utils.Lerp(0, itemCount-1, t);
		int count = (int)Math.Round(floatCount);
		return (count == 0) ? 1 : count;
	}
}

readonly record struct ItemsSelectorInfo(
	ReadonlyItemSet avaliableItems,
	float minItemRatio,
	float maxItemRatio
);