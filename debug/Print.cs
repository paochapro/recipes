static class Print
{
    public static void Recipes(IEnumerable<Recipe> recipes)
    {
        foreach(Recipe recipe in recipes) {
            GD.Print(recipe.Title.ToUpper() + ":");
            ItemSet(recipe.SearchData.ItemSet);
        }
    }

    public static void ItemSet(ReadonlyItemSet items)
    {
        GD.Print("ItemSet:");
        
        GD.Print("\tFood:");
        foreach(FoodWithCount food in items.Food)
            GD.Print($"\t\t{food.Item.Name} [{food.Count}] ({food.Item.Category})");
               
        GD.Print("\tInv:");
        foreach(InventoryItem inv in items.Inventory)
            GD.Print($"\t\t{inv.Name} ({inv.Category})");
    }

    public static void Section<T>(string title, Action<T> printFunc, T printInput)
    {
        GD.Print($"----{title}----");
        GD.Print("{");
        printFunc.Invoke(printInput);
        GD.Print("}");
    }
}