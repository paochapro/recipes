using Godot;
using System;

public partial class ItemSetSection : VBoxContainer
{  
    public event Action CreateFoodItem;
    public event Action CreateInventoryItem;
    
    public ItemSetSection()
    {
        CreateFoodItem += () => {};
        CreateInventoryItem += () => {};
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        var tabContainer = GetNode<TabContainer>("TabContainer");

        foreach(var subsection in tabContainer.GetChildren())
        {
            if(subsection is FoodSubsection foodSubsection)
                foodSubsection.CreateItemButtonPress += () => { CreateFoodItem.Invoke(); };

            if(subsection is InvSubsection invSubsection)
                invSubsection.CreateItemButtonPress += () => { CreateInventoryItem.Invoke(); };
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}