abstract partial class ItemsSubsectionContent<TItem> : Container
    where TItem : Item
{
    public void SearchUpdate(IEnumerable<TItem> avaliableItems, string text)
    {
        var result = ItemSearch.Search(avaliableItems.Cast<Item>(), text);
        IEnumerable<TItem> items = result.Cast<TItem>();

        bool autoExpand = text != "";
        UpdateContent(items, autoExpand);
    }

    public void UpdateContent(IEnumerable<TItem> items, bool autoExpand = false)
    {
        this.RemoveChildren();

        var groups = items.GroupBy(i => i.Category);

        foreach(IGrouping<string, TItem> group in groups)
        {
            Fold fold = new Fold() { Expanded = autoExpand };
            fold.Title = group.Key;

            var controls = group.Select(i => GetControlForItem(i));
            fold.AddChildren(controls);

            this.AddChild(fold);
        }
    }

    protected abstract Control GetControlForItem(TItem item);
}