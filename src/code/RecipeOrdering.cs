class ItemsCompare<TItem> : IComparer<IEnumerable<TItem>>
{
    IEnumerable<TItem> userItems;

    public ItemsCompare(IEnumerable<TItem> userItems)
    {
        this.userItems = userItems;
    }

    public int Compare(IEnumerable<TItem>? items1, IEnumerable<TItem>? items2)
    {
        if(items1 == null || items2 == null)
            return NullCompare(items1, items2);

        //Check how many items each recipe need
        int neededItems1 = GetNeededItemCount(items1);
        int neededItems2 = GetNeededItemCount(items2);
        int result = 0;

        //If both need the same amount of items
        //Then compare which has the most items
        //Else compare which one needs the least amount of items
        if(neededItems1 == neededItems2)
            result = items1.Count() - items2.Count();
        else
            result = neededItems2 - neededItems1;

        return result;
    }

    int GetNeededItemCount(IEnumerable<TItem> items) 
    {
        return items.Except(userItems).Count();
    }

    int NullCompare(object? x, object? y)
    {
        if(x == null && y != null)
            return -1;

        if(x != null && y == null)
            return 1;

        return 0;
    }
}