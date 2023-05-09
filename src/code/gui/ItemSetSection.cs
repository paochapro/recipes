using Godot;
using System;

public partial class ItemSetSection : VBoxContainer
{
    [Export] string foodTabName = "";
    [Export] string invTabName = "";
    [Export] string allTabName = "";

	public override void _Ready()
	{
        var tabContainer = GetNode<TabContainer>("TabContainer");
        var foodIndex = tabContainer.GetTabIdxFromControl(tabContainer.GetNode<Control>("Food"));
        var invIndex = tabContainer.GetTabIdxFromControl(tabContainer.GetNode<Control>("Inv"));
        var allIndex = tabContainer.GetTabIdxFromControl(tabContainer.GetNode<Control>("All"));

        tabContainer.SetTabTitle(foodIndex, foodTabName);
        tabContainer.SetTabTitle(invIndex, invTabName);
        tabContainer.SetTabTitle(allIndex, allTabName);
	}
}