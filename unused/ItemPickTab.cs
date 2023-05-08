abstract partial class ItemPickTab<TItem> : Container
    where TItem : Item
{
    public void UpdateItems(IEnumerable<TItem> items)
    {
        //Remove all previous item controls
        foreach(var child in GetChildren()) {
            RemoveChild(child);
            child.QueueFree();
        }

        //Add new item controls
        foreach(var item in items) {
            var customControl = GenerateItemControl(item);
            AddChild(customControl);
        }
    }

    abstract protected Control GenerateItemControl(TItem item);
}