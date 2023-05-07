public partial class DynamicWindow : VBoxContainer
{
    #nullable disable
    [Export] ItemSetSection itemSetSection;
    [Export] RecipesSection recipesSection;
    #nullable restore

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        itemSetSection.CreateFoodItem += () => SetWindow(GetNode<Control>("CreateFoodMenu"));
        itemSetSection.CreateInventoryItem += () => SetWindow(GetNode<Control>("CreateInvMenu"));
        recipesSection.CreateRecipe += () => SetWindow(GetNode<Control>("CreateRecipeMenu"));
	}

    void SetWindow(Control setControl)
    {
        foreach(Control node in GetChildren())
            node.Hide();
        
        setControl.Show();
    }
}