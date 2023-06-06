public partial class DynamicWindow : VBoxContainer
{
    public override void _Ready()
    {
        var func = (DynamicWindowMenu m) => SetWindow(GetNode<Control>(GetMenuControlName(m)));
        GetNode<GlobalEvents>("/root/GlobalEvents").SwitchDynamicWindow += func;
        GD.Print("wfmafa");
        GD.Print("mfwaifmaif");
    }

    void SetWindow(Control setControl)
    {
        foreach(Control node in GetChildren())
            node.Hide();
        
        setControl.Show();
    }

    string GetMenuControlName(DynamicWindowMenu menu)
    {
        return menu switch {
            DynamicWindowMenu.ItemCreation => "ItemCreationTabs",
            DynamicWindowMenu.RecipeCreation => "CreateRecipeMenu",
            DynamicWindowMenu.Food => "FoodInspectorMenu",
            DynamicWindowMenu.Inv => "InvInspectorMenu",
            _ => "UnknownMenu"
        };
    }
}