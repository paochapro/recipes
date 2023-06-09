partial class AllSubsection : VBoxContainer
{
    #nullable disable
    [Export] LineEdit foodLineEdit;
    [Export] LineEdit invLineEdit;
    #nullable restore

    public override void _Ready() {
        var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineedit.TextChanged += OnSearchTextChanged;
        //GetNodes();
    }

    // void GetNodes() {
    //     var content = GetNode("ScrollContainer/Content");
    //     var foodTab = content.GetNode<Fold>("FoodTab");
    //     var invTab = content.GetNode<Fold>("InvTab");
    //     var foodInspector = foodTab.MainContainer.GetNode("MarginContainer/FoodInspector");
    //     var invInspector = invTab.MainContainer.GetNode("MarginContainer/InvInspector");
    //     foodLineEdit = foodInspector.GetNode<LineEdit>("ControlPanel/LineEdit");
    //     invLineEdit = invInspector.GetNode<LineEdit>("ControlPanel/LineEdit");
    // }

    void OnSearchTextChanged(string text) {
        foodLineEdit.EmitSignal("text_changed", text);
        invLineEdit.EmitSignal("text_changed", text);
    }
}