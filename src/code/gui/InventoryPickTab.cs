partial class InventoryPickTab : ItemPickTab<InventoryItem>
{
    protected override Control GenerateItemControl(InventoryItem item)
    {	
		var itemLabel = new Label();
		return itemLabel;
    }
}