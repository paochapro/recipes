static class RecipeOrdering
{
    public static IEnumerable<Recipe> OrderRecipes(IEnumerable<Recipe> recipes, ReadonlyItemSet userItems)
    {
        if(userItems.Food.Count() == 0 && userItems.Inventory.Count() == 0)
            return recipes.OrderBy(r => r.Title);

        var missingItemsCompare     = new MissingItemsCompare<FoodItem>(userItems.FoodItems);
        var totalFoodItemsCompare   = new TotalItemsCompare<FoodItem>(userItems.FoodItems);
        var totalInvItemsCompare    = new TotalItemsCompare<InventoryItem>(userItems.Inventory);
        var lackFoodCompare         = new LackFoodCompare(userItems.Food);
        var foodCountCompare        = new FoodCountCompare(userItems.Food);

        var invItems                = (Recipe r) => r.ItemSet.Inventory;
        var foodItems               = (Recipe r) => r.ItemSet.FoodItems;
        var food                    = (Recipe r) => r.ItemSet.Food;

        return recipes
            .OrderBy            (foodItems, missingItemsCompare)
            .ThenByDescending   (foodItems, totalFoodItemsCompare)
            .ThenBy             (food, lackFoodCompare)
            .ThenByDescending   (food, foodCountCompare)
            .ThenBy             (invItems, totalInvItemsCompare)
            .ThenBy             (r => r.Title);
    }
}