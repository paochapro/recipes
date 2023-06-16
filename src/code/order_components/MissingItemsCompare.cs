class MissingItemsCompare<TItem> : ItemsCompare<TItem>
    where TItem : Item
{
    public MissingItemsCompare(IEnumerable<TItem> userItems) 
        : base(userItems)
    {
    }

    protected override int ChildCompare(IEnumerable<TItem> items1, IEnumerable<TItem> items2) {
        int neededItems1 = GetNeededItemCount(items1);
        int neededItems2 = GetNeededItemCount(items2);
        return neededItems1 - neededItems2;
    }

    int GetNeededItemCount(IEnumerable<TItem> items) 
    {
        return items.Except(UserItems).Count();
    }
}