static class ItemSearch
{
    public static IEnumerable<Item> Search(IEnumerable<Item> avaliableItems, string text)
    {
        return avaliableItems.Where(i => ItemPasses(i, text));
    }

    static bool ItemPasses(Item item, string text)
    {
        return item.Name.Contains(text);
    }
}