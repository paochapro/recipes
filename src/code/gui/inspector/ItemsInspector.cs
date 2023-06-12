abstract partial class ItemsInspector<TItem> : VBoxContainer
	where TItem : Item
{
	#nullable disable
    [Export] protected PackedScene buttonScene;
	protected ItemsInspectorContent<TItem> content;
    LineEdit lineEdit;
	#nullable restore

	public override sealed void _Ready()
	{
		lineEdit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineEdit.TextChanged += OnSearchTextChanged;

		content = GetNode<ItemsInspectorContent<TItem>>("ScrollContainer/Content");
		content.UpdateContent(AvaliableItems, ButtonGenerator);
        
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.FileLoaded += () => this.CallDeferred(ItemsInspector<TItem>.MethodName.UpdateContent);

        _ChildReady();
	}

	protected void OnSearchTextChanged(string text)
	{
		var result = ItemSearch.Search(AvaliableItems.Cast<Item>(), text);
		IEnumerable<TItem> items = result.Cast<TItem>();

		bool autoExpand = text != "";
		content.UpdateContent(items, ButtonGenerator, autoExpand);
	}

    protected void UpdateItem(TItem item) { 
        if(ItemSearch.ItemPasses(item, lineEdit.Text))
            content.UpdateItem(item, ButtonGenerator);
    }

    protected void RemoveItem(TItem item) => content.RemoveItem(item);
    
    protected void UpdateContent() => content.UpdateContent(AvaliableItems, ButtonGenerator);

	protected abstract IEnumerable<TItem> AvaliableItems { get; }

	protected abstract ButtonGenerator<TItem> ButtonGenerator { get; }

    protected virtual void _ChildReady() {}
}