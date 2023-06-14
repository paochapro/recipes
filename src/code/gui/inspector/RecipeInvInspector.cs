partial class RecipeInvInspector : InvInspector
{
    #nullable disable
    IEnumerable<InventoryItem> avaliableItems;
    #nullable restore

    public void Initialize(IEnumerable<InventoryItem> avaliableItems) {
        this.avaliableItems = avaliableItems;
        UpdateContent();
    }

    protected override IEnumerable<InventoryItem> AvaliableItems => avaliableItems;

    protected override ButtonGenerator<InventoryItem> ButtonGenerator {
        get {
            return new ButtonGenerator<InventoryItem>(buttonScene, (i) => {});
        }
    }
}