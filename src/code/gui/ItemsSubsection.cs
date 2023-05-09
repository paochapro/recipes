abstract partial class ItemsSubsection<TItem> : VBoxContainer
    where TItem : Item
{
    #nullable disable
    protected Container content;
    #nullable restore

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

        bool autoExpand = text != "";
        UpdateContent(items, autoExpand);
    }

    void UpdateContent(IEnumerable<TItem> items, bool autoExpand = false)
    {
        content.RemoveChildren();

        var groups = items.GroupBy(i => i.Category);

        foreach(IGrouping<string, TItem> group in groups)
        {
            Fold fold = new Fold() { Expanded = autoExpand };
            fold.Title = group.Key;

            var controls = group.Select(i => GetControlForItem(i));
            fold.AddChildren(controls);

            content.AddChild(fold);
        }
    }

    protected abstract IEnumerable<TItem> AvaliableItems { get; }

    protected abstract Control GetControlForItem(TItem item);

    protected abstract void OnMenuButtonPressed();
}