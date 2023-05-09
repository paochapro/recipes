using Godot;
using System;

public partial class RecipesSection : PanelContainer
{
	public override void _Ready()
	{
        var createButton = GetNode<Button>("Components/ControlPanel/Button");
        var dynamicWindow = GetNode<DynamicWindow>("/root/GuiRoot/%DynamicWindow");
        createButton.Pressed += dynamicWindow.SetRecipeMenu;
	}
}