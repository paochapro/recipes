partial class AllSubsection : VBoxContainer
{
    #nullable disable
    FoodInspectorContent foodContent;
    InvInspectorContent invContent;
    Fold foodTab;
    Fold invTab;
    #nullable restore

    const DynamicWindowMenu SWITCH_MENU = DynamicWindowMenu.ItemCreation;

    public override void _Ready()
    {
        GetNodes();
        ConnectEvents();
        UpdateContent();
    }

    void GetNodes()
    {
        var content = GetNode("ScrollContainer/Content");
        foodTab = content.GetNode<Fold>("FoodTab");
        invTab = content.GetNode<Fold>("InvTab");

        foodContent = foodTab.MainContainer.GetNode<FoodInspectorContent>("MarginContainer/Content");
        invContent = invTab.MainContainer.GetNode<InvInspectorContent>("MarginContainer/Content");
    }

    void ConnectEvents()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankFood += (i) => foodContent.UpdateItem(i);
        events.NewBankInv += (i) => invContent.UpdateItem(i);
        events.RemoveBankFood += (i) => foodContent.RemoveItem(i);
        events.RemoveBankInv += (i) => invContent.RemoveItem(i);

        var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineedit.TextChanged += OnSearchTextChanged;

        var menuButton = GetNode<Button>("ControlPanel/Button");
        menuButton.Pressed += () => events.CallSwitchDynamicWindow(SWITCH_MENU);
    }

    void OnSearchTextChanged(string text)
    {
        var bank = GetNode<Program>("/root/Program").ItemsBank;
        bool autoExpand = text != "";

        var foodItems = GetItems(bank.Food.Cast<Item>(), text).Cast<FoodItem>();
        var invItems = GetItems(bank.Inventory.Cast<Item>(), text).Cast<InventoryItem>();

        foodContent.UpdateContent(foodItems, autoExpand);
        invContent.UpdateContent(invItems, autoExpand);
    }

    IEnumerable<Item> GetItems(IEnumerable<Item> avaliableItems, string text)
    {
        return ItemSearch.Search(avaliableItems, text);
    }

    void UpdateContent()
    {
        //TODO: How food and inv tabs should expand?
        var bank = GetNode<Program>("/root/Program").ItemsBank;
        foodContent.UpdateContent(bank.Food);
        invContent.UpdateContent(bank.Inventory);
    }
}