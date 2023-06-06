partial class FoodCategoryList : CategoryList
{
    protected override IEnumerable<string> AvaliableCategories 
        => GetNode<Program>("/root/Program").ItemsBank.Food
            .GroupBy(f => f.Category)
            .Select(g => g.Key);
}