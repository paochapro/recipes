abstract partial class ItemsInspector<TItem> : VBoxContainer
	where TItem : Item
{
	#nullable disable
    [Export] protected PackedScene buttonScene;
	protected InspectorContent<TItem> content;
    protected GlobalEvents events;
    LineEdit lineEdit;
	#nullable restore

	protected abstract IEnumerable<TItem> AvaliableItems { get; }
	protected abstract ButtonGenerator<TItem> ButtonGenerator { get; }
    protected abstract void AddBankEvents(GlobalEvents events);
    protected abstract void RemoveBankEvents(GlobalEvents events);
    protected virtual void _ChildReady() {}

	public override sealed void _Ready()
	{
		lineEdit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineEdit.TextChanged += OnSearchTextChanged;

		content = GetNode<InspectorContent<TItem>>("ScrollContainer/Content");
		//content.UpdateContent(AvaliableItems, ButtonGenerator);

        events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.FileLoaded += () => this.CallDeferred(ItemsInspector<TItem>.MethodName.UpdateContent);

        if( this is AddItemsInspector<TItem> addItemsInspector &&
            content is AddItemsInspectorContent<TItem> addContent)
        {
            addItemsInspector._InspectorInitialize(addContent, AvaliableItems);
        }

        AddBankEvents(events);
        _ChildReady();
	}

    public override void _Notification(int what) {
        if(what == NotificationPredelete)
            RemoveBankEvents(events);
    }

	protected void OnSearchTextChanged(string text)
	{
		var result = ItemSearch.Search(AvaliableItems.Cast<Item>(), text);
		IEnumerable<TItem> items = result.Cast<TItem>();

		bool autoExpand = text != "";
		content.UpdateContent(items, ButtonGenerator, autoExpand);
	}

    protected void UpdateContent() => content.UpdateContent(AvaliableItems, ButtonGenerator);

    protected void RemoveItem(TItem item) => content.RemoveItem(item);
    protected void RemoveItem(string removeName) => content.RemoveItem(removeName);

    protected void UpdateItem(TItem item) { 
        if(ItemSearch.ItemPasses(item, lineEdit.Text))
            content.UpdateItem(item, ButtonGenerator);
    }

    protected void BankItemRemoved(TItem bankItem) {
        GD.Print("Bank item removed");
        RemoveItem(bankItem.Name);
    }

    protected void BankItemModified(TItem bankItem) {
        GD.Print("Bank item modified");
        RemoveItem(bankItem.Name);
        UpdateItem(bankItem);
    }

    TItem? GetItemBankMatch(TItem bankItem) {
        return AvaliableItems.FirstOrDefault(i => i.Name == bankItem.Name);
    }
}