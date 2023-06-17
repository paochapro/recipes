static class HightlightColor
{
    static readonly Color NOT_IN_SET = new Color(1, 0.27058824896812f, 0.32941177487373f);
    static readonly Color LACK_FOOD = new Color(1, 0.91372549533844f, 0.20784313976765f);
    static readonly Color MET_CONDITIONS = new Color(0.59607845544815f, 0.94117647409439f, 0.32941177487373f);
    static readonly Color DEFAULT = new Color(1,1,1);

    public static Color GetRecipeTitleColor(Recipe recipe, ReadonlyItemSet set)
    {
        if(set.Food.Count() == 0)
            return DEFAULT;

        if(recipe.ItemSet.FoodItems.Any(i => SetContainsItem(i, set) == false))
            return NOT_IN_SET;

        if(recipe.ItemSet.Food.Any(i => GetFoodColor(i, set) == LACK_FOOD))
            return LACK_FOOD;

        return MET_CONDITIONS;
    }

    public static Color GetFoodColor(FoodWithCount food, ReadonlyItemSet set)
    {
        if(GetItemColor(food.Item, set) == MET_CONDITIONS) 
        {
            var setFood = set.Food.First(i => i.Name == food.Name);

            if(food.Count > setFood.Count)
                return LACK_FOOD;

            return MET_CONDITIONS;
        }

        return NOT_IN_SET;
    }

    public static Color GetItemColor(Item item, ReadonlyItemSet set)
    {
        if(SetContainsItem(item, set))
            return MET_CONDITIONS;

        return NOT_IN_SET;
    }

    static bool SetContainsItem(Item item, ReadonlyItemSet set)
    {
        if(item is FoodWithCount foodWithCount)
            item = foodWithCount.Item;

        if(item is FoodItem food)
            return set.FoodItems.Contains(food);

        if(item is InventoryItem inv)
            return set.Inventory.Contains(inv);

        return false;
    }
}