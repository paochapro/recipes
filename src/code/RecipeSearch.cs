using System.Linq;

static class RecipeCompare
{
    public static IEnumerable<Recipe> Compare(IEnumerable<Recipe> recipes, ReadonlyItemSet localItems)
    {
        IEnumerable<Recipe> compareRecipes = GetNeededRecipes(recipes, localItems).ToArray();

        var inventoryCompare = new ItemsCompare<InventoryItem>(localItems.Inventory);
        var foodCompare = new ItemsCompare<FoodItem>(localItems.FoodItems);

        var result = compareRecipes
            .OrderByDescending(r => r.SearchData.ItemSet.Inventory, inventoryCompare);

        return result;
    }

    static IEnumerable<Recipe> GetNeededRecipes(IEnumerable<Recipe> recipes, ReadonlyItemSet localItems)
    {
        foreach(Recipe recipe in recipes)
        {
            var recipeFoodNames = recipe.SearchData.ItemSet.FoodNames;
            var recipeInventoryNames = recipe.SearchData.ItemSet.InventoryNames;
            var localFoodNames = localItems.FoodNames;
            var localInvNames = localItems.InventoryNames;

            int bothFoodCount = localFoodNames.Intersect(recipeFoodNames).Count();
            int bothInventoryCount = localInvNames.Intersect(recipeInventoryNames).Count();

            if(bothFoodCount > 0 && bothInventoryCount > 0)
                yield return recipe;
        }
    }
}

class ItemsCompare<TItem> : IComparer<IEnumerable<TItem>>
{
    IEnumerable<TItem> userItems;

    public ItemsCompare(IEnumerable<TItem> userItems)
    {
        this.userItems = userItems;
    }

    public int Compare(IEnumerable<TItem>? items1, IEnumerable<TItem>? items2)
    {
        if(items1 == null || items2 == null)
            return NullCompare(items1, items2);

        //Check how many items each recipe need
        int neededItems1 = GetNeededItemCount(items1);
        int neededItems2 = GetNeededItemCount(items2);
        int result = 0;

        //If both need the same amount of items
        //Then compare which has the most items
        //Else compare which one needs the least amount of items
        if(neededItems1 == neededItems2)
            result = items1.Count() - items2.Count();
        else
            result = neededItems2 - neededItems1;

        return result;
    }

    int GetNeededItemCount(IEnumerable<TItem> items) 
    {
        return items.Except(userItems).Count();
    }

    int NullCompare(object? x, object? y)
    {
        if(x == null && y == null)
            return 0;

        if(x == null && y != null)
            return -1;

        if(x != null && y == null)
            return 1;

        throw new Exception("Both objects in NullCompare were not null");
    }

    //Ratio comparing
    // public int Compare(IEnumerable<TItem>? items1, IEnumerable<TItem>? items2)
    // {
    //     if(items1 == null || items2 == null)
    //         return NullCompare(items1, items2);

    //     float GetItemRatio(IEnumerable<TItem> items) 
    //     {
    //         int avaliableItemsCount = userInventory.Intersect(items).Count();
    //         return avaliableItemsCount / items.Count();
    //     }

    //     //Compare avaliable items to item count ratios
    //     float items1Ratio = GetItemRatio(items1);
    //     float items2Ratio = GetItemRatio(items2);
    //     int result = items1Ratio.CompareTo(items2Ratio);

    //     //If ratios are the same
    //     //Then compare which one has smallest item count
    //     if(result == 0)
    //         result = items2.Count() - items1.Count();
        
    //     return result;
    // }
}
