class TotalItemsCompare<TItem> : ItemsCompare<TItem>
    where TItem : Item
{
    public TotalItemsCompare(IEnumerable<TItem> userItems) 
        : base(userItems)
    {
    }

    protected override int ChildCompare(IEnumerable<TItem> items1, IEnumerable<TItem> items2) {
        return items1.Count() - items2.Count();
    }
}