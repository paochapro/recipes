public partial class SectionA : VBoxContainer
{
    public override void _Ready()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.OpenRecipeFoodMenu += (c) => events.CallSwitchSectionA(SectionAMenu.RecipeFood);
        events.OpenRecipeInvMenu += (c) => events.CallSwitchSectionA(SectionAMenu.RecipeInv);
        events.SwitchSectionA += (menu) => SetWindow(GetNode<Control>(GetMenuControlName(menu)));
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