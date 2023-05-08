using Godot;
using System;

public partial class RecipesSection : PanelContainer
{
    public event Action CreateRecipe;

    public RecipesSection()
    {
        CreateRecipe += () => {};
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        var createButton = GetNode<Button>("Components/ControlPanel/Button");
        var dynamicWindow = GetNode<DynamicWindow>("/root/GuiRoot/%DynamicWindow");
        createButton.Pressed += dynamicWindow.SetRecipeMenu;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}