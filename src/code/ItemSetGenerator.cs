static class ItemSetGenerator
{   
    public static ItemSet GenerateItemSet(ItemsSelectorInfo info)
    {
        var bank = ItemsSelector.GetRandomItemSet(info);
        List<FoodWithCount> food = new();

        foreach(FoodItem foodItem in bank.Food)
        {
            int count = GD.RandRange(1,5);
            food.Add(new(foodItem, count));   
        }

        return new(food, bank.Inventory.ToList());
    }

    public static ItemSet SelectAllFromBank(ReadonlyItemBank bank, int defaultCount)
    {
        List<FoodWithCount> foodWithCountList = new();
        bank.Food.Iterate(i => foodWithCountList.Add(new FoodWithCount(i, defaultCount)));
        return new ItemSet(foodWithCountList, bank.Inventory.ToList());
    }
}