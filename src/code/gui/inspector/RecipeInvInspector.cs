partial class RecipeInvInspector : ItemsInspector<InventoryItem>
{
    #nullable disable
    IEnumerable<InventoryItem> avaliableItems;
    #nullable restore

    public void Initialize(IEnumerable<InventoryItem> avaliableItems) {
        this.avaliableItems = avaliableItems;

        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.InvModified += ModifyInv;

        this.TreeExiting += () => {
            events.InvModified -= ModifyInv;
        };

        UpdateContent();
    }

    protected override IEnumerable<InventoryItem> AvaliableItems => avaliableItems;

    protected override ButtonGenerator<InventoryItem> ButtonGenerator {
        get {
            return new ButtonGenerator<InventoryItem>(buttonScene, (i) => {});
        }
    }

    void ModifyInv(InventoryItem item)
    {
        InventoryItem modifyInv = avaliableItems.FirstOrDefault(i => i.Name == item.Name);

        if(modifyInv != new InventoryItem()) {
            RemoveItem(modifyInv);
            UpdateItem(item);
        }
    }
}