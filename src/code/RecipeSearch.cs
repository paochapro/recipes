static class RecipeSearch
{
    public static IEnumerable<Recipe> Search(IEnumerable<Recipe> recipes, SearchInfo searchInfo)
    {
        return GetNeededRecipes(recipes, searchInfo).ToArray();
    }

    static IEnumerable<Recipe> GetNeededRecipes(IEnumerable<Recipe> recipes, SearchInfo info)
    {
        foreach(Recipe recipe in recipes)
            if(DoesRecipePass(recipe, info))
                yield return recipe;
    }

    public static bool DoesRecipePass(Recipe recipe, SearchInfo info)
    {
        //bool localItems = RecipePassItemSet(recipe, info.LocalItemSet);
        //bool filterItems = RecipePassItemSet(recipe, info.FilterItemSet);
        bool localInvItems = RecipePassItems(recipe.ItemSet.InventoryNames, info.LocalItemSet.InventoryNames);
        bool title = RecipePassTitle(recipe, info.Title);
        bool dishType = RecipePassDishType(recipe, info.DishType);
        bool time = RecipePassTime(recipe, info.Minutes);
        return title && dishType && time && localInvItems; //&& localItems && filterItems;
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
        bool recipeContainsUniqueItems = recipeItems.Except(searchItems).Count() != 0;
        bool passes = !shouldSearch || (shouldSearch && !recipeContainsUniqueItems);

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