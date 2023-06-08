abstract partial class ItemsInspector<TItem> : VBoxContainer
	where TItem : Item
{
	#nullable disable
    [Export] protected PackedScene buttonScene;
	protected ItemsInspectorContent<TItem> content;
    ButtonGenerator<TItem> buttonGenerator;
	#nullable restore

	public override sealed void _Ready()
	{
        buttonGenerator = new ButtonGenerator<TItem>(buttonScene, OnButtonPressed);

		var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineedit.TextChanged += OnSearchTextChanged;

		content = GetNode<ItemsInspectorContent<TItem>>("ScrollContainer/Content");
		content.UpdateContent(AvaliableItems, buttonGenerator);

        _ChildReady();
	}

	protected void OnSearchTextChanged(string text)
	{
		var result = ItemSearch.Search(AvaliableItems.Cast<Item>(), text);
		IEnumerable<TItem> items = result.Cast<TItem>();

		bool autoExpand = text != "";
		content.UpdateContent(items, buttonGenerator, autoExpand);
	}

    protected void UpdateItem(TItem item) => content.UpdateItem(item, buttonGenerator);
    protected void RemoveItem(TItem item) => content.RemoveItem(item);

	protected abstract IEnumerable<TItem> AvaliableItems { get; }

	protected abstract Action<TItem> OnButtonPressed { get; }

    protected virtual void _ChildReady() {}
}