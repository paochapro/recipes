partial class AddRecipeFoodInspector : FoodInspector, AddItemsInspector<FoodItem>
{
    FormItemSetComponent? currentComponent;

    protected override IEnumerable<FoodItem> AvaliableItems => 
		GetNode<Program>("/root/Program").ItemsBank.Food;

    protected override ButtonGenerator<FoodItem> ButtonGenerator {
		get {
            if(currentComponent != null) {
                var onPressed = (FoodItem item) => currentComponent.AddFood(new FoodWithCount(item,1));
                var disabledCondition = (FoodItem item) => {
                    //GD.Print("Disabled cond: " + currentComponent.GetValue.FoodItems.Contains(item));
                    return currentComponent.GetValue.FoodItems.Contains(item);
                };
			    return new AddItemButtonGenerator<FoodItem>(buttonScene, onPressed, disabledCondition);
            }

            GD.PrintErr("FormItemSetComponent wasnt set in AddRecipeItemInspector");
            return new AddItemButtonGenerator<FoodItem>(buttonScene, (i) => {}, (i) => false);
		}
	}

    public void _ConnectEvents(AddItemsInspectorContent<FoodItem> content)
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        events.OpenRecipeFoodMenu += (FormItemSetComponent newComponent) => {
            if(currentComponent != null && currentComponent != newComponent) {
                currentComponent.AddedFood -= (i) => content.SetLocked(i.Item, true);
                currentComponent.RemovedFood -= (i) => content.SetLocked(i.Item, false);
            }

            this.currentComponent = newComponent;

            currentComponent.AddedFood += (i) => content.SetLocked(i.Item, true); 
            currentComponent.RemovedFood += (i) => content.SetLocked(i.Item, false);

            UpdateContent();
        };
    }
}