abstract partial class ItemsSubsection<TItem> : VBoxContainer
{
    public event Action CreateItemButtonPress;

    //Debug
    static ItemBank debugBank = ItemsFromJson.GetItemsFromJson("content/items.json");
    protected static ReadonlyItemBank DebugBank => debugBank;

    #nullable disable
    protected Container content;
    #nullable restore

    public ItemsSubsection()
    {
        CreateItemButtonPress += () => {};
    }

	public override sealed void _Ready()
	{
        content = GetNode<Container>("ScrollContainer/Content");

        var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
        lineedit.TextChanged += OnSearchTextChanged;

        var createButton = GetNode<Button>("ControlPanel/Button");
        createButton.Pressed += OnMenuButtonPressed;

        UpdateContent(AvaliableItems);
	}

    protected void OnSearchTextChanged(string text)
    {
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

    protected abstract IEnumerable<TItem> AvaliableItems { get; }

    protected abstract Control GetControlForItem(TItem item);

    protected abstract void OnMenuButtonPressed();
}