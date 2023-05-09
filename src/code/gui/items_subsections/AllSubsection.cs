partial class AllSubsection : VBoxContainer
{
    #nullable disable
    FoodSubsectionContent foodContent;
    InvSubsectionContent invContent;
    #nullable restore

    public override void _Ready()
    {
        var content = GetNode("ScrollContainer/Content");
        foodContent = content.GetNode<FoodSubsectionContent>("FoodTab/MarginContainer/Content");
        invContent = content.GetNode<InvSubsectionContent>("InvTab/MarginContainer/Content");
    }

    void OnSearchTextChanged(string text)
    {
        var bank = GetNode<Program>("/root/Program").ItemsBank;
        foodContent.SearchUpdate(bank.Food, text);
        invContent.SearchUpdate(bank.Inventory, text);

        // var foodResult = ItemSearch.Search(bank.Food.Cast<Item>(), text);
        // var invResult = ItemSearch.Search(bank.Inventory.Cast<Item>(), text);
        // ItemBank bankResult = new ItemBank(foodResult.Cast<FoodItem>().ToList(), invResult.Cast<InventoryItem>().ToList());
        // UpdateContent(bankResult, true);
    }

    void UpdateContent(ReadonlyItemBank bank, bool autoExpand = false)
    {
        foodContent.UpdateContent(bank.Food);
        invContent.UpdateContent(bank.Inventory);

        // var foodTabs = GenerateTabsFromItems(bank.Food, GetControlForFood, autoExpand);
        // foodContent.RemoveChildren();
        // foodContent.AddChildren(foodTabs);

        // var invTabs = GenerateTabsFromItems(bank.Inventory, GetControlForInv, autoExpand);
        // invContent.RemoveChildren();
        // invContent.AddChildren(invTabs);

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