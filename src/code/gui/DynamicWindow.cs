public partial class DynamicWindow : VBoxContainer
{
    public override void _Ready()
    {
        var func = (DynamicWindowMenu m) => SetWindow(GetMenuControlName(m));
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.SwitchDynamicWindow += func;
        events.OpenFoodModificationMenu += (FoodItem item) => SetWindow("ModifyFoodMenu");
        events.OpenInvModificationMenu += (InventoryItem item) => SetWindow("ModifyInvMenu");
        events.OpenRecipeModificationMenu += (Recipe item) => {
            events.CallSwitchSectionA(SectionAMenu.ItemSet);
            SetWindow("ModifyRecipeMenu");
        };
    }

    void SetWindow(string controlName)
    {
        Control setControl = GetNode<Control>(controlName);

        foreach(Control node in GetChildren())
            node.Hide();
        
        setControl.Show();
    }

    string GetMenuControlName(DynamicWindowMenu menu)
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        if(menu == DynamicWindowMenu.RecipeCreation)
            events.CallSwitchSectionA(SectionAMenu.ItemSet);

        return menu switch {
            DynamicWindowMenu.ItemCreation => "ItemCreationTabs",
            DynamicWindowMenu.RecipeCreation => "CreateRecipeMenu",
            DynamicWindowMenu.Food => "FoodInspectorMenu",
            DynamicWindowMenu.Inv => "InvInspectorMenu",
            _ => "UnknownMenu"
        };
    }
}