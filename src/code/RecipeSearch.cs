using System.Linq;

static class RecipeSearch
{
    static bool orderingEnabled = false;

    public static IEnumerable<Recipe> Search(IEnumerable<Recipe> recipes, SearchInfo searchInfo)
    {
        IEnumerable<Recipe> neededRecipes = GetNeededRecipes(recipes, searchInfo).ToArray();

        var inventoryCompare = new ItemsCompare<InventoryItem>(searchInfo.LocalItemSet.Inventory);
        var foodCompare = new ItemsCompare<FoodItem>(searchInfo.LocalItemSet.FoodItems);

        IEnumerable<Recipe> result = neededRecipes;

        if(orderingEnabled)
        {
            result = neededRecipes
                .OrderByDescending(r => r.ItemSet.Inventory, inventoryCompare);
        }

        return result;
    }

    static IEnumerable<Recipe> GetNeededRecipes(IEnumerable<Recipe> recipes, SearchInfo info)
    {
        foreach(Recipe recipe in recipes)
            if(DoesRecipePass(recipe, info))
                yield return recipe;
    }

    static bool DoesRecipePass(Recipe recipe, SearchInfo info)
    {
        bool items = RecipePassItems(recipe, info.FilterItemSet);
        bool dishType = RecipePassDishType(recipe, info.DishType);
        bool time = RecipePassTime(recipe, info.Minutes);
        return items && dishType && time;
    }

    static bool RecipePassItems(Recipe recipe, ReadonlyItemSet searchItemSet)
    {
        bool searchFood = searchItemSet.Food.Count() != 0;
        bool searchInv = searchItemSet.Inventory.Count() != 0;

        var recipeFoodNames = recipe.ItemSet.FoodNames;
        var recipeInventoryNames = recipe.ItemSet.InventoryNames;
        var localFoodNames = searchItemSet.FoodNames;
        var localInvNames = searchItemSet.InventoryNames;

        int bothFoodCount = localFoodNames.Intersect(recipeFoodNames).Count();
        int bothInventoryCount = localInvNames.Intersect(recipeInventoryNames).Count();

        //Check for food and inventory passes:
        //If we arent searching for items of certain type, then item condition passes
        //Else check if local items and recipe items have common items
        //If they have common items - pass, dont have common - dont pass
        bool foodPasses = !searchFood || (searchFood && bothFoodCount > 0);
        bool invPasses = !searchInv || (searchInv && bothInventoryCount > 0);

        return foodPasses && invPasses;
    }

    static bool RecipePassDishType(Recipe recipe, DishType searchDishType)
    {
        if(searchDishType == DishType.None)
            return true;

        return recipe.DishType == searchDishType;
    }

    static bool RecipePassTime(Recipe recipe, int searchMinutes)
    {
        if(searchMinutes <= 0)
            return true;

        return recipe.Minutes <= searchMinutes;
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
        if(x == null && y != null)
            return -1;

        if(x != null && y == null)
            return 1;

        return 0;
    }
}
