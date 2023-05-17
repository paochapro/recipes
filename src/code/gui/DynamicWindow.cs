public partial class DynamicWindow : VBoxContainer
{
    public override void _Ready()
    {
        GetNode<GlobalEvents>("/root/GlobalEvents").SwitchDynamicWindow += (m) => SetWindow(GetMenuControl(m));
    }

    void SetWindow(Control setControl)
    {
        foreach(Control node in GetChildren())
            node.Hide();
        
        setControl.Show();
    }

    Control GetMenuControl(DynamicWindowMenu menu)
    {
        return menu switch {
            DynamicWindowMenu.Food => GetNode<Control>("CreateFoodMenu"),
            DynamicWindowMenu.Inv => GetNode<Control>("CreateInvMenu"),
            DynamicWindowMenu.Recipe => GetNode<Control>("CreateRecipeMenu"),
            _ => GetNode<Control>("CreateFoodMenu")
        };
    }
}