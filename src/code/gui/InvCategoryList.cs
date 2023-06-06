partial class InvCategoryList : CategoryList
{
    protected override IEnumerable<string> AvaliableCategories 
        => GetNode<Program>("/root/Program").ItemsBank.Inventory
            .GroupBy(f => f.Category)
            .Select(g => g.Key);
}