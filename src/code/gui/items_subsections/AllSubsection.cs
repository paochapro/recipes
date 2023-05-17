partial class AllSubsection : VBoxContainer
{
    #nullable disable
    AllSubsectionFoodContent foodContent;
    InvSubsectionContent invContent;
    Fold foodTab;
    Fold invTab;
    #nullable restore

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

        foodContent = foodTab.MainContainer.GetNode<AllSubsectionFoodContent>("MarginContainer/Content");
        invContent = invTab.MainContainer.GetNode<InvSubsectionContent>("MarginContainer/Content");
    }

    void ConnectEvents()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewBankFoodItem += (i) => foodContent.UpdateItem(i);
        events.NewBankInvItem += (i) => invContent.UpdateItem(i);
    }

    void OnSearchTextChanged(string text)
    {
        var bank = GetNode<Program>("/root/Program").ItemsBank;
        foodContent.SearchUpdate(bank.Food, text);
        invContent.SearchUpdate(bank.Inventory, text);
    }

    void UpdateContent()
    {
        var bank = GetNode<Program>("/root/Program").ItemsBank;
        //TODO: How food and inv tabs should expand?
        foodContent.UpdateContent(bank.Food);
        invContent.UpdateContent(bank.Inventory);
    }
}