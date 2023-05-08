public partial class DynamicWindow : VBoxContainer
{
    void SetWindow(Control setControl)
    {
        foreach(Control node in GetChildren())
            node.Hide();
        
        setControl.Show();
    }

    public void SetFoodMenu() => SetWindow(GetNode<Control>("CreateFoodMenu"));
    public void SetInvMenu() => SetWindow(GetNode<Control>("CreateInvMenu"));
    public void SetRecipeMenu() => SetWindow(GetNode<Control>("CreateRecipeMenu"));
}