abstract partial class AddItemsInspector<TItem> : ItemsInspector<TItem>
    where TItem : Item
{
    protected override void _ChildReady()
    {
        if(content is AddItemsInspectorContent<TItem> addContent)
        {
            var program = GetNode<Program>("/root/Program");
            foreach(var item in AvaliableItems)
                addContent.SetLocked(item, true);
            
            ConnectEvents(addContent);
        }
        else
            throw new Exception("Content of AddItemsInspector is not of AddItemsInspectorContent type");

    }

    protected abstract void ConnectEvents(AddItemsInspectorContent<TItem> content);
}