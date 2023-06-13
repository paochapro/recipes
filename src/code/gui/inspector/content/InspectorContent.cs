interface InspectorContent<TItem>
    where TItem : Item
{
    void UpdateContent(IEnumerable<TItem> items, ButtonGenerator<TItem> generator, bool autoExpand = false);
    void UpdateItem(TItem item, ButtonGenerator<TItem> generator);
    void RemoveItem(TItem item);
    void ModifyItem(IEnumerable<TItem> existingItems, ButtonGenerator<TItem> generator, TItem modify);
}