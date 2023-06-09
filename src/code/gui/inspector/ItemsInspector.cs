abstract partial class ItemsInspector<TItem> : VBoxContainer
	where TItem : Item
{
	#nullable disable
    [Export] protected PackedScene buttonScene;
	protected ItemsInspectorContent<TItem> content;
	#nullable restore

	public override sealed void _Ready()
	{
		var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineedit.TextChanged += OnSearchTextChanged;

		content = GetNode<ItemsInspectorContent<TItem>>("ScrollContainer/Content");
		content.UpdateContent(AvaliableItems, ButtonGenerator);

        _ChildReady();
	}

	protected void OnSearchTextChanged(string text)
	{
		var result = ItemSearch.Search(AvaliableItems.Cast<Item>(), text);
		IEnumerable<TItem> items = result.Cast<TItem>();

		bool autoExpand = text != "";
		content.UpdateContent(items, ButtonGenerator, autoExpand);
	}

    protected void UpdateItem(TItem item) => content.UpdateItem(item, ButtonGenerator);
    protected void RemoveItem(TItem item) => content.RemoveItem(item);
    protected void UpdateContent() => content.UpdateContent(AvaliableItems, ButtonGenerator);

	protected abstract IEnumerable<TItem> AvaliableItems { get; }

	protected abstract ButtonGenerator<TItem> ButtonGenerator { get; }

    protected virtual void _ChildReady() {}
}