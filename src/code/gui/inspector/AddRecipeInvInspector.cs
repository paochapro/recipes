partial class AddRecipeInvInspector : InvInspector, AddItemsInspector<InventoryItem>
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

    public void _ConnectEvents(AddItemsInspectorContent<InventoryItem> content)
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        var newItemSet = (ItemSet set) => 
        {
            foreach(var item in set.Inventory)
                content.SetLocked(item, true);
        };

        events.OpenRecipeInvMenu += (FormItemSetComponent newComponent) => 
        {
            if(currentComponent != null && currentComponent != newComponent) 
            {
                currentComponent.AddedInv -= (i) => content.SetLocked(i, true);
                currentComponent.RemovedInv -= (i) => content.SetLocked(i, false);
                currentComponent.NewItemSet -= newItemSet;
            }

            this.currentComponent = newComponent;

            currentComponent.NewItemSet += newItemSet;
            currentComponent.AddedInv += (i) => content.SetLocked(i, true);
            currentComponent.RemovedInv += (i) => content.SetLocked(i, false);

            UpdateContent();
        };
    }

}