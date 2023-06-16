class ItemTypesCompare<TItem> : ItemsCompare<TItem>
    where TItem : Item
{
    public ItemTypesCompare(IEnumerable<TItem> userItems) 
        : base(userItems)
    {
    }

    protected override int ChildCompare(IEnumerable<TItem> items1, IEnumerable<TItem> items2)
    {
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
        return items.Except(UserItems).Count();
    }
}