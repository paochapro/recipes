interface AddItemsInspector<TItem>
    where TItem : Item
{
    void _InspectorInitialize(AddItemsInspectorContent<TItem> addContent, IEnumerable<TItem> avaliableItems)
    {
        foreach(var item in avaliableItems)
            addContent.SetLocked(item, true);
       
        _ConnectEvents(addContent);
    }

    void _ConnectEvents(AddItemsInspectorContent<TItem> content);
}