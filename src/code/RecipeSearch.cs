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
        bool title = RecipePassTitle(recipe, info.Title);
        bool localItems = RecipePassItemSet(recipe, info.LocalItemSet);
        bool filterItems = RecipePassItemSet(recipe, info.FilterItemSet);
        bool dishType = RecipePassDishType(recipe, info.DishType);
        bool time = RecipePassTime(recipe, info.Minutes);
        return title && localItems && filterItems && dishType && time;
    }

    static bool RecipePassTitle(Recipe recipe, string searchTitle)
    {
        if(searchTitle == "")
            return true;

        return recipe.Title.Contains(searchTitle);
    }

    static bool RecipePassItemSet(Recipe recipe, ReadonlyItemSet searchItemSet)
    {
        bool foodPasses = RecipePassItems(recipe.ItemSet.FoodNames, searchItemSet.FoodNames);
        bool invPasses = RecipePassItems(recipe.ItemSet.InventoryNames, searchItemSet.InventoryNames);
        return foodPasses && invPasses;
    }

    static bool RecipePassItems(IEnumerable<string> recipeItems, IEnumerable<string> searchItems)
    {
        //If we arent searching for items of specific type, then we pass
        //If we are searching, then check if recipe contains all search items
        //If we its contains everything - pass
        //If it doesnt - dont pass
        bool shouldSearch = searchItems.Count() != 0; 
        bool containsAllSearchItems = searchItems.Except(recipeItems).Count() == 0;
        bool passes = !shouldSearch || (shouldSearch && containsAllSearchItems);

        return passes;
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