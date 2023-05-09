partial class AllSubsection : VBoxContainer
{
    #nullable disable
    Container foodContainer;
    Container invContainer;
    #nullable restore

    public override void _Ready()
    {
        var content = GetNode("ScrollContainer/Content");
        foodContainer = content.GetNode<Container>("Food");
        invContainer = content.GetNode<Container>("Inv");
    }

    void OnSearchTextChanged(string text)
    {
        var bank = GetNode<Program>("/root/Program").ItemsBank;
        var foodResult = ItemSearch.Search(bank.Food.Cast<Item>(), text);
        var invResult = ItemSearch.Search(bank.Inventory.Cast<Item>(), text);
        ItemBank bankResult = new ItemBank(foodResult.Cast<FoodItem>().ToList(), invResult.Cast<InventoryItem>().ToList());

        UpdateContent(bankResult, true);
    }

    void UpdateContent(ReadonlyItemBank bank, bool autoExpand = false)
    {
        var foodTabs = GenerateTabsFromItems(bank.Food, GetControlForFood, autoExpand);
        foodContainer.RemoveChildren();
        foodContainer.AddChildren(foodTabs);

        var invTabs = GenerateTabsFromItems(bank.Inventory, GetControlForInv, autoExpand);
        invContainer.RemoveChildren();
        invContainer.AddChildren(invTabs);

        //UpdateTypeContent(bank.Food, foodContainer, GetControlForFood, autoExpand);
        //UpdateTypeContent(bank.Inventory, invContainer, GetControlForInv, autoExpand);
    }

    Control GetControlForFood(FoodItem item) => new Control();
    Control GetControlForInv(InventoryItem item) => new Control();

    IEnumerable<Fold> GenerateTabsFromItems<TItem>(IEnumerable<TItem> items, Func<TItem, Control> getControl, bool autoExpand = false)
        where TItem : Item
    {
        var groups = items.GroupBy(i => i.Category);

        foreach(IGrouping<string, TItem> group in groups)
        {
            Fold fold = new Fold() { Expanded = autoExpand };
            fold.Title = group.Key;

            var controls = group.Select(i => getControl(i));

            fold.AddChildren(controls);

            yield return fold;
        }
    }
}