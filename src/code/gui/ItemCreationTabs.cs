using Godot;
using System;

public partial class ItemCreationTabs : TabContainer
{
    [Export] string foodMenuName = "";
    [Export] string invMenuName = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        var foodMenuIndex = this.GetTabIdxFromControl(GetNode<Control>("CreateFoodMenu"));
        var invMenuIndex = this.GetTabIdxFromControl(GetNode<Control>("CreateInvMenu"));
        this.SetTabTitle(foodMenuIndex, foodMenuName);
        this.SetTabTitle(invMenuIndex, invMenuName);
	}
}
