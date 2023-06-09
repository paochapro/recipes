partial class AllSubsection : VBoxContainer
{
    #nullable disable
    FoodInspectorContent foodContent;
    InvInspectorContent invContent;
    Fold foodTab;
    Fold invTab;

    [Export] PackedScene foodButtonScene;
    [Export] PackedScene invButtonScene;
    ButtonGenerator<FoodItem> foodButtonGenerator;
    ButtonGenerator<InventoryItem> invButtonGenerator;
    #nullable restore

    const DynamicWindowMenu SWITCH_MENU = DynamicWindowMenu.ItemCreation;

    public override void _Ready()
    {
        var program = GetNode<Program>("/root/Program");
        var foodButtonPressed = (FoodItem item) => program.RemoveFoodItem(item);
        var invButtonPressed = (InventoryItem item) => program.RemoveInvItem(item);
        foodButtonGenerator = new ButtonGenerator<FoodItem>(foodButtonScene, foodButtonPressed);
        invButtonGenerator = new ButtonGenerator<InventoryItem>(invButtonScene, invButtonPressed); 
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
        events.NewBankFood += (i) => foodContent.UpdateItem(i, foodButtonGenerator);
        events.NewBankInv += (i) => invContent.UpdateItem(i, invButtonGenerator);
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

        foodContent.UpdateContent(foodItems, foodButtonGenerator, autoExpand);
        invContent.UpdateContent(invItems, invButtonGenerator, autoExpand);
    }

    IEnumerable<Item> GetItems(IEnumerable<Item> avaliableItems, string text)
    {
        return ItemSearch.Search(avaliableItems, text);
    }

    void UpdateContent()
    {
        //TODO: How food and inv tabs should expand?
        var bank = GetNode<Program>("/root/Program").ItemsBank;
        foodContent.UpdateContent(bank.Food, foodButtonGenerator);
        invContent.UpdateContent(bank.Inventory, invButtonGenerator);
    }
}