partial class ModifyPopup : PopupMenu
{
    #nullable disable
    Item item;
    #nullable restore

    public override void _Ready() {
        //this.WindowInput += PopupWindowInput;
    }

    public void SetModificationItem(Item item) {
        this.item = item;
    }

    void ItemSelected(int id) 
    {
        if(item == null)
            throw new Exception("Item was not set in ModifyPopup");

        var program = GetNode<Program>("/root/Program");
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        if(id == 0) {
            if(item is FoodItem food)
                events.CallFoodModificationMenu(food);
            else if(item is InventoryItem inv)
                events.CallInvModificationMenu(inv);
        }
        else if(id == 1) {
            if(item is FoodItem food)
                program.RemoveFoodItem(food);
            else if(item is InventoryItem inv)
                program.RemoveInvItem(inv);
        }
    }

    public void PopupWindowInput(InputEvent @ev) {
        if(@ev is InputEventMouseButton mouseEv && mouseEv.Pressed)
            this.CallDeferred(ModifyPopup.MethodName.Hide);
    }
}
