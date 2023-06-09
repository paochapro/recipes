public partial class SectionA : VBoxContainer
{
    public override void _Ready()
    {
        var func = (SectionAMenu m) => SetWindow(GetNode<Control>(GetMenuControlName(m)));
        GetNode<GlobalEvents>("/root/GlobalEvents").SwitchSectionA += func;
    }

    void SetWindow(Control setControl)
    {
        foreach(Control node in GetChildren())
            node.Hide();
        
        setControl.Show();
    }

    string GetMenuControlName(SectionAMenu menu)
    {
        return menu switch {
            SectionAMenu.ItemSet => "ItemSetSection",
            SectionAMenu.RecipeFood => "RecipeFood",
            SectionAMenu.RecipeInv => "RecipeInv",
            _ => "UnknownMenu"
        };
    }
}