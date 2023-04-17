static class RecipesGenerator
{
    public static IEnumerable<Recipe> GenerateRecipes(RecipeGeneratorInfo info)
    {
        return YieldGenerateRecipes(info).ToArray();
    }

    static IEnumerable<Recipe> YieldGenerateRecipes(RecipeGeneratorInfo info)
    {
        for(int i = 0; i < GD.RandRange(info.minRandomRecipes, info.maxRandomRecipes); i++)
        {
            yield return RandomRecipe(info, $"recipe{i+1}"); 
        }
    }

    static Recipe RandomRecipe(RecipeGeneratorInfo info, string name)
    {
        ItemSet set = ItemSetGenerator.GenerateItemSet(info.itemsGeneratorInfo);
        DishType dishType = (DishType)GD.RandRange(0, Enum.GetValues(typeof(DishType)).Length-1);
        int minutes = GD.RandRange(info.minRandomMinutes, info.maxRandomMinutes);

        RecipeSearchData searchData = new(set, dishType, minutes);
        Recipe result = new(name, "instructions", "image_uid", searchData);
        return result;
    }
}

readonly record struct RecipeGeneratorInfo
(
    ItemsSelectorInfo itemsGeneratorInfo,
    int minRandomRecipes,
    int maxRandomRecipes,
    int minRandomMinutes,
    int maxRandomMinutes
);