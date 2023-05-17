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
        var content = GetNode("ScrollContainer/Content");
        foodTab = content.GetNode<Fold>("FoodTab");
        invTab = content.GetNode<Fold>("InvTab");

        foodContent = foodTab.MainContainer.GetNode<AllSubsectionFoodContent>("MarginContainer/Content");
        invContent = invTab.MainContainer.GetNode<InvSubsectionContent>("MarginContainer/Content");

        var bank = GetNode<Program>("/root/Program").ItemsBank;
        UpdateContent(bank);
    }

    void OnSearchTextChanged(string text)
    {
        var bank = GetNode<Program>("/root/Program").ItemsBank;
        foodContent.SearchUpdate(bank.Food, text);
        invContent.SearchUpdate(bank.Inventory, text);
    }

    void UpdateContent(ReadonlyItemBank bank, bool autoExpand = false)
    {   
        //TODO: How food and inv tabs should expand?
        foodContent.UpdateContent(bank.Food);
        invContent.UpdateContent(bank.Inventory);
    }
}