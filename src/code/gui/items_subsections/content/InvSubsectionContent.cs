partial class InvSubsectionContent : ItemsInspectorContent<InventoryItem>
{
    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewLocalInv += (InventoryItem item) => UpdateItem(item);
    }
}