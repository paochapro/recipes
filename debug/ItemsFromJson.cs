using Newtonsoft.Json;
using System.IO;

static class ItemsFromJson
{
    public static ItemBank GetItemsFromJson(string jsonPath)
    {
        List<FoodItem> food = new();
        List<InventoryItem> inventory = new();

        using(Godot.FileAccess file = Godot.FileAccess.Open(jsonPath, Godot.FileAccess.ModeFlags.Read))
        {
            if(file != null)
            {
                string jsonText = file.GetAsText();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string[]>>>(jsonText);

                if(dict != null)
                {
                    food = GetItemsFromItemType<FoodItem>(dict["food"], GetNewFoodItem).ToList();
                    inventory = GetItemsFromItemType<InventoryItem>(dict["inventory"], GetNewInventoryItem).ToList();
                }
            }
            else
            {
                GD.PrintErr(Godot.FileAccess.GetOpenError());
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

    static FoodItem GetNewFoodItem(string name, string category) => new FoodItem(name, category, "texture_placeholder");

    static InventoryItem GetNewInventoryItem(string name, string category) => new InventoryItem(name, category);
}