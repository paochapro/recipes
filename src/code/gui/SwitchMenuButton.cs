partial class SwitchMenuButton : Button
{
    DynamicWindowMenu dynamicMenu;
    SectionAMenu sectionA_Menu;

    public override void _Ready() {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        dynamicMenu = DynamicWindowMenu.Unknown;
        sectionA_Menu = SectionAMenu.Unknown;

        if(this.HasMeta("dynamicMenu"))
            dynamicMenu = (DynamicWindowMenu)this.GetMeta("dynamicMenu").AsInt32();

        if(this.HasMeta("sectionA_Menu"))
            sectionA_Menu = (SectionAMenu)this.GetMeta("sectionA_Menu").AsInt32();

        if(dynamicMenu != DynamicWindowMenu.Unknown) {
            this.Pressed += () => events.CallSwitchDynamicWindow(dynamicMenu);
            return;
        }

        if(sectionA_Menu != SectionAMenu.Unknown)
            this.Pressed += () => events.CallSwitchSectionA(sectionA_Menu);
    }
}