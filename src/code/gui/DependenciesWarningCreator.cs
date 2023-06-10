partial class DependenciesWarningCreator : Node 
{
    #nullable disable
    [Export] PackedScene scene;
    #nullable restore

    public override void _Ready() {
        if(scene == null)
            throw new Exception("No scene for warning window");

        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        
        events.FailedFoodRemove += (i,d,r) => ShowWarning(i,d,r);
        events.FailedInvRemove += (i,d,r) => ShowWarning(i,d,r);
    }

    void ShowWarning(Item item, Item? local, IEnumerable<Recipe> recipes) {
        GD.Print("creater show warning");

        this.RemoveChildren();
        var window = scene.Instantiate<DependenciesWarning>();
        AddChild(window);
        window.ShowWarning(item, local, recipes);
    }
}