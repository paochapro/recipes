partial class AddRecipeInvInspector : AddItemsInspector<InventoryItem>
{
    FormItemSetComponent? currentComponent;

    protected override IEnumerable<InventoryItem> AvaliableItems => 
		GetNode<Program>("/root/Program").ItemsBank.Inventory;

    protected override ButtonGenerator<InventoryItem> ButtonGenerator {
		get {
            if(currentComponent != null) {
			    var onPressed = (InventoryItem item) => currentComponent.AddInv(item);
                var disabledCondition = (InventoryItem item) => currentComponent.GetValue.Inventory.Contains(item);
			    return new AddItemButtonGenerator<InventoryItem>(buttonScene, onPressed, disabledCondition);
            }

            GD.PrintErr("FormItemSetComponent wasnt set in AddRecipeItemInspector");
			return new AddItemButtonGenerator<InventoryItem>(buttonScene, (i) => {}, (i) => true);
		}
	}

    protected override void ConnectEvents(AddItemsInspectorContent<InventoryItem> content)
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        events.OpenRecipeItemsMenu += (FormItemSetComponent newComponent) => {
            if(currentComponent != null && currentComponent != newComponent) {
                currentComponent.AddedInv -= (i) => content.SetLocked(i, true);
                currentComponent.RemovedInv -= (i) => content.SetLocked(i, false);
            }

            this.currentComponent = newComponent;

            currentComponent.AddedInv += (i) => content.SetLocked(i, true); 
            currentComponent.RemovedInv += (i) => content.SetLocked(i, false);

            UpdateContent();
        };
    }
}