abstract partial class ItemsSubsection<TItem> : VBoxContainer
    where TItem : Item
{
    #nullable disable
    protected ItemsSubsectionContent<TItem> content;
    #nullable restore

	public override sealed void _Ready()
	{
        content = GetNode<ItemsSubsectionContent<TItem>>("ScrollContainer/Content");

        var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
        lineedit.TextChanged += OnSearchTextChanged;

        var createButton = GetNode<Button>("ControlPanel/Button");
        createButton.Pressed += OnMenuButtonPressed;

        content.UpdateContent(AvaliableItems);
	}

    protected void OnSearchTextChanged(string text)
    {
        content.SearchUpdate(AvaliableItems, text);
    }

    protected abstract IEnumerable<TItem> AvaliableItems { get; }

    protected abstract void OnMenuButtonPressed();
}