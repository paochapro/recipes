partial class SwitchMenuButton : Button
{
    [Export] DynamicWindowMenu menu;

    public override void _Ready() {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        this.Pressed += () => events.CallSwitchDynamicWindow(menu); 
    }
}