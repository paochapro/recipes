interface InspectorContent<TItem>
    where TItem : Item
{
    void UpdateContent(IEnumerable<TItem> items, ButtonGenerator<TItem> generator, bool autoExpand = false);
    void UpdateItem(TItem item, ButtonGenerator<TItem> generator);
    void RemoveItem(string removeName);
    
    void RemoveItem(TItem item) => RemoveItem(item.Name);
}