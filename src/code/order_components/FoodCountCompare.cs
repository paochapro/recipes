class FoodCountCompare : ItemsCompare<FoodWithCount>
{
    public FoodCountCompare(IEnumerable<FoodWithCount> userItems) 
        : base(userItems)
    {
    }

    protected override int ChildCompare(IEnumerable<FoodWithCount> items1, IEnumerable<FoodWithCount> items2)
    {   
        return GetFoodCount(items1) - GetFoodCount(items2);
    }

    int GetFoodCount(IEnumerable<FoodWithCount> items) {
        return items.Select(f => f.Count).Sum();
    }
}