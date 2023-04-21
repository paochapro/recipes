using System.Collections.Generic;

public partial class ItemsSection : TabContainer
{    
    const string rusFoodTabName = "Мои продукты";
    const string rusInventoryTabName = "Мой инвентарь";
    const string rusAllTabName = "Все предметы";

    #nullable disable
    Program program;
    #nullable restore

    void LocalizeTabNames() {
        var foodTab = GetNode<Control>("FoodTab");
        var inventoryTab = GetNode<Control>("InventoryTab");
        var allTab = GetNode<Control>("AllTab");

        SetTabTitle(GetTabIdxFromControl(foodTab), rusFoodTabName);
        SetTabTitle(GetTabIdxFromControl(inventoryTab), rusInventoryTabName);
        SetTabTitle(GetTabIdxFromControl(allTab), rusAllTabName);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() 
    {
        program = GetNode<Program>("/root/Program");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    void OnTabChanged(int tab)
    {
        var container = GetTabControl(tab);

        if(container is FoodPickTab foodTab)
            foodTab.UpdateItems(program.LocalItems.FoodItems);

        if(container is InventoryPickTab inventoryTab)
            inventoryTab.UpdateItems(program.LocalItems.Inventory);
    }
}