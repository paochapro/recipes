partial class SimpleInspectorContent<TItem> : Container, InspectorContent<TItem>
    where TItem : Item
{
    public void UpdateContent(IEnumerable<TItem> items, ButtonGenerator<TItem> generator, bool autoExpand = false)
    {
        this.RemoveChildren();

        foreach(var item in items) {
            var control = generator.GetButton(item);
            AddChild(control);
        }

        ReorderItems();
    }

    public void UpdateItem(TItem item, ButtonGenerator<TItem> generator)
    {
        var control = generator.GetButton(item);
        AddChild(control);
        ReorderItems();
    }

    public void RemoveItem(TItem item)
    {
        var button = GetChildren().Cast<ItemButton<TItem>>().FirstOrDefault(b => b.Item.Name == item.Name);

        if(button == null) {
            GD.PrintErr("Attempted to remove item that doesnt exist in content");
            return;
        }

        this.RemoveChild(button);
        button.QueueFree();
    }
    
    public void ModifyItem(IEnumerable<TItem> existingItems, ButtonGenerator<TItem> generator, TItem modify) 
    {
        var old = existingItems.FirstOrDefault(i => i.Name == modify.Name);

        if(old != null) {
            RemoveItem(old);
            UpdateItem(modify, generator);
        }
    }

    void ReorderItems() => this.ReorderChildren((ItemButton<TItem> button) => button.Item.Name);
}