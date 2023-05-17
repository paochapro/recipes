public partial class RecipesSection : PanelContainer
{
    public void OnMenuButtonPressed()
    {
        GetNode<GlobalEvents>("/root/GlobalEvents").CallSwitchDynamicWindow(DynamicWindowMenu.Recipe);
    }
}