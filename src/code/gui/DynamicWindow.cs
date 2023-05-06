public partial class DynamicWindow : Control
{
    #nullable disable
    [Export] ItemSetSection itemSetSection;
    [Export] RecipesSection recipesSection;
    #nullable restore

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        itemSetSection.CreateFoodItem += () => { GD.Print("[DW] Create food"); };
        itemSetSection.CreateInventoryItem += () => { GD.Print("[DW] Create inventory"); };
        recipesSection.CreateRecipe += () => { GD.Print("[DW] Create recipe"); };
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}