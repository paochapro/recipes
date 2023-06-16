partial class ModifyPopup : PopupMenu
{
    #nullable disable
    object obj;
    BankRemovalSafety safety;
    #nullable restore

    public override void _Ready() {
        safety = new();
        AddChild(safety);
    }

    public void SetModificationObject(object obj) {
        this.obj = obj;
    }

    void ItemSelected(int id) 
    {
        if(obj == null)
            throw new Exception("Item was not set in ModifyPopup");

        var program = GetNode<Program>("/root/Program");
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        if(id == 0) 
        {
            if(obj is FoodItem food)
                events.CallFoodModificationMenu(food);

            if(obj is InventoryItem inv)
                events.CallInvModificationMenu(inv);

            if(obj is Recipe recipe)
                events.CallRecipeModificationMenu(recipe);
        }
        else if(id == 1) 
        {
            if(obj is FoodItem food)
                safety.CheckForDependencies(food);

            if(obj is InventoryItem inv)
                safety.CheckForDependencies(inv);

            if(obj is Recipe recipe)
                program.RemoveRecipe(recipe);
        }
    }

    public void PopupWindowInput(InputEvent @ev) {
        if(@ev is InputEventMouseButton mouseEv && mouseEv.Pressed)
            this.CallDeferred(ModifyPopup.MethodName.Hide);
    }

    public void Open(Vector2 pos) {
        this.Position = (Vector2I)pos;
        this.Popup();
    }
}
