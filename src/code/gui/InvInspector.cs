partial class InvInspector : ItemsInspector<InventoryItem>
{
    protected override IEnumerable<InventoryItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Inventory;
}
