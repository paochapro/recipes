abstract partial class ItemsInspector<TItem> : VBoxContainer
	where TItem : Item
{
	#nullable disable
	protected ItemsInspectorContent<TItem> content;
	#nullable restore

	public override sealed void _Ready()
	{
		var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineedit.TextChanged += OnSearchTextChanged;

		content = GetNode<ItemsInspectorContent<TItem>>("ScrollContainer/Content");
		content.UpdateContent(AvaliableItems);

        _ChildReady();
	}

	protected void OnSearchTextChanged(string text)
	{
		var result = ItemSearch.Search(AvaliableItems.Cast<Item>(), text);
		IEnumerable<TItem> items = result.Cast<TItem>();

		bool autoExpand = text != "";
		content.UpdateContent(items, autoExpand);
	}

	protected abstract IEnumerable<TItem> AvaliableItems { get; }

    protected virtual void _ChildReady() {}
}