abstract class ItemsCompare<TItem> : IComparer<IEnumerable<TItem>>
    where TItem : Item
{
    protected IEnumerable<TItem> UserItems { get; private set; }

    public ItemsCompare(IEnumerable<TItem> userItems) {
        this.UserItems = userItems;
    }

    public int Compare(IEnumerable<TItem>? items1, IEnumerable<TItem>? items2)
    {
        if(items1 == null || items2 == null)
            return NullCompare(items1, items2);

        return ChildCompare(items1, items2);
    }
    
    protected int NullCompare(object? x, object? y)
    {
        if(x == null && y != null)
            return -1;

        if(x != null && y == null)
            return 1;

        return 0;
    }

    protected abstract int ChildCompare(IEnumerable<TItem> items1, IEnumerable<TItem> items2);
}

class InvCompare : ItemsCompare<InventoryItem>
{
    public InvCompare(IEnumerable<InventoryItem> userItems) 
        : base(userItems)
    {
    }

    protected override int ChildCompare(IEnumerable<InventoryItem>? items1, IEnumerable<InventoryItem>? items2)
    {
        throw new NotImplementedException();
    }
}