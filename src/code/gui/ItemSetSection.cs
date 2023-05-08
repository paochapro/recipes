using Godot;
using System;

public partial class ItemSetSection : VBoxContainer
{  
    //public event Action CreateFoodItem;
    //public event Action CreateInventoryItem;
    
    public ItemSetSection()
    {
        //CreateFoodItem += () => {};
        //CreateInventoryItem += () => {};
    }

	public override void _Ready()
	{
        var tabContainer = GetNode<TabContainer>("TabContainer");
	}
}