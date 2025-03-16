partial class DependenciesWarningCreator : Node 
{
    #nullable disable
    PackedScene scene;
    #nullable restore

    public override void _Ready() {
        scene = GD.Load<PackedScene>("res://src/tscn/dependencies_warning.tscn");
        if(scene == null)
            throw new Exception("No scene for warning window");

        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
    }

    public DependenciesWarning ShowWarning(DependencyWarningEventArgs args) 
    {
        this.RemoveChildren();
        var window = scene.Instantiate<DependenciesWarning>();
        AddChild(window);
        window.ShowWarning(args);
        return window;
    }
}