partial class RecipeCreationInvInspector : InvInspector
{
    #nullable disable
    FormItemSetComponent formComponent;
    #nullable restore

    protected override IEnumerable<InventoryItem> AvaliableItems => formComponent.GetValue.Inventory;

    protected override ButtonGenerator<InventoryItem> ButtonGenerator {
        get {
            var events = GetNode<GlobalEvents>("/root/GlobalEvents");
            var onPressed = (InventoryItem item) => formComponent.RemoveInv(item);
            return new ButtonGenerator<InventoryItem>(buttonScene, onPressed);
        }
    }

    protected override void _ChildReady() {
        formComponent = GetNode<FormItemSetComponent>("../../../../../../..");
        formComponent.NewItemSet += (set) => UpdateContent();
        formComponent.AddedInv += (i) => UpdateItem(i);
        formComponent.RemovedInv += (i) => RemoveItem(i);
    }
} 