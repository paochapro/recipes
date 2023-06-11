partial class AllSubsection : VBoxContainer
{
    #nullable disable
    [Export] LineEdit foodLineEdit;
    [Export] LineEdit invLineEdit;
    #nullable restore

    public override void _Ready() {
        var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineedit.TextChanged += OnSearchTextChanged;
    }

    void OnSearchTextChanged(string text) {
        foodLineEdit.EmitSignal("text_changed", text);
        invLineEdit.EmitSignal("text_changed", text);
    }
}