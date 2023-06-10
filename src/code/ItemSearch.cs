static class ItemSearch
{
    public static IEnumerable<Item> Search(IEnumerable<Item> avaliableItems, string text)
    {
        var categorizedItems = GetItemsOfCategory(avaliableItems, text);

        if(categorizedItems != null)
            return categorizedItems;

        return avaliableItems.Where(i => ItemPasses(i, text));
    }

    static IEnumerable<Item>? GetItemsOfCategory(IEnumerable<Item> avaliableItems, string category)
    {
        var groups = avaliableItems.GroupBy(i => i.Category);
        var categories = avaliableItems.GroupBy(i => i.Category).Select(c => c.Key);

        IGrouping<string, Item>? group = groups.FirstOrDefault(c => c.Key == category);

        return group;
    }

    public static bool ItemPasses(Item item, string text)
    {
        return item.Name.Contains(text);
    }
}