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

    public void RemoveItem(string removeName)
    {
        var button = GetChildren().Cast<ItemButton<TItem>>().FirstOrDefault(b => b.Item.Name == removeName);

        if(button == null) {
            GD.PrintErr("Attempted to remove item that doesnt exist in content");
            return;
        }

        this.RemoveChild(button);
        button.QueueFree();
    }
    
    void ReorderItems() => this.ReorderChildren((ItemButton<TItem> button) => button.Item.Name);
}