class LackFoodCompare : ItemsCompare<FoodWithCount>
{
    public LackFoodCompare(IEnumerable<FoodWithCount> userItems)
        : base(userItems)
    {
    }

    protected override int ChildCompare(IEnumerable<FoodWithCount> items1, IEnumerable<FoodWithCount> items2)
    {
        return FoodCountLack(items1) - FoodCountLack(items2);
    }

    public int FoodCountLack(IEnumerable<FoodWithCount> recipeItems)
    {
        if(UserItems.Count() == 0)
            return 0;

        var uniqueRecipeItems = recipeItems.Select(i => i.Name).Except(UserItems.Select(i => i.Name));

        if(uniqueRecipeItems.Count() != 0)
            return 0;

        int totalLackCount = 0;

        //Comparing item count in local and in recipe items
        //Calculating how much local items user is lacking for recipe
        foreach(var recipeItem in recipeItems) {
            var userItem = UserItems.First(i => i.Name == recipeItem.Name);
            int lack = recipeItem.Count - userItem.Count;

            if(lack <= 0)
                continue;
            
            totalLackCount += lack;
        }

        return totalLackCount;
    }
}