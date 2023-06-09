partial class SwitchMenuButton : Button
{
    [Export] DynamicWindowMenu dynamicMenu;
    [Export] SectionAMenu sectionA_Menu;

    public override void _Ready() {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        if(dynamicMenu != DynamicWindowMenu.Unknown) {
            this.Pressed += () => events.CallSwitchDynamicWindow(dynamicMenu);
            return;
        }

        if(sectionA_Menu != SectionAMenu.Unknown)
            this.Pressed += () => events.CallSwitchSectionA(sectionA_Menu);
    }
}