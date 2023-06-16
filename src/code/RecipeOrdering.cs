static class RecipeOrdering
{
    public static IEnumerable<Recipe> OrderRecipes(IEnumerable<Recipe> recipes, ReadonlyItemSet userItems)
    {
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
            .ThenByDescending   (invItems, totalInvItemsCompare)
            .ThenBy             (r => r.Title);
    }

    // static OrderComponent GetOrderComponent(ItemsCompare<Item> compare, bool descending, Func<Recipe, IEnumerable<Item>> selector)
    // {
    //     var component = new OrderComponent();
    //     component.Compare = compare;
    //     component.Descending = descending;
    //     component.Selector = selector;
    //     return component;
    // }

    // static IEnumerable<Recipe> GetOrderedRecipes<TItem>(IEnumerable<Recipe> recipes, IEnumerable<OrderComponent> components)
    // {
    //     var orderedRecipes = FirstOrderRecipesByComponent(recipes, components.First());

    //     foreach(var component in components.Skip(1)) {
    //         orderedRecipes = ThenOrderRecipesByComponent(orderedRecipes, component);
    //     }

    //     return orderedRecipes;
    // }

    // static IOrderedEnumerable<Recipe> FirstOrderRecipesByComponent(IEnumerable<Recipe> recipes, OrderComponent component)
    // {
    //     if(component.Descending)
    //         return recipes.OrderByDescending(component.Selector, component.Compare);

    //     return recipes.OrderBy(component.Selector, component.Compare);
    // }

    // static IOrderedEnumerable<Recipe> ThenOrderRecipesByComponent(IOrderedEnumerable<Recipe> recipes, OrderComponent component)
    // {
    //     if(component.Descending)
    //         return recipes.ThenByDescending(component.Selector, component.Compare);

    //     return recipes.ThenBy(component.Selector, component.Compare);
    // }

    
    // struct OrderComponent {
    //     public object Compare;
    //     public bool Descending;
    //     public Func<Recipe, IEnumerable<Item>> Selector;
    // }
}