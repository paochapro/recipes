abstract partial class ItemsSubsection<TItem> : VBoxContainer
{
    static ItemBank debugBank = ItemsFromJson.GetItemsFromJson("content/items.json");
    protected static ReadonlyItemBank DebugBank => debugBank;

    protected abstract IEnumerable<TItem> AvaliableItems { get; }

    #nullable disable
    Container content;
    #nullable restore

	public override sealed void _Ready()
	{
        OnReady();
        content = GetNode<Container>("ScrollContainer/Content");
        UpdateContent(AvaliableItems);
	}

    public void OnSearchTextChanged(string text)
    {
        GD.Print("ai count: " + AvaliableItems.Count());
        var result = ItemSearch.Search(AvaliableItems.Cast<Item>(), text);
        IEnumerable<TItem> items = result.Cast<TItem>();

        UpdateContent(items);
    }

    void UpdateContent(IEnumerable<TItem> items)
    {
        content.RemoveChildren();

        foreach(TItem item in items)
            content.AddChild(GetControlForItem(item));
    }

    protected abstract Control GetControlForItem(TItem item);
    protected abstract void OnReady();
}