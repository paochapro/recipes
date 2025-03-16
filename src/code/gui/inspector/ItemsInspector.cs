abstract partial class ItemsInspector<TItem> : VBoxContainer
	where TItem : Item
{
	#nullable disable
    protected PackedScene buttonScene;
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
        if(this.HasMeta("buttonScene")) {
            buttonScene = GD.Load<PackedScene>(this.GetMeta("buttonScene").AsString());
        }

		lineEdit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineEdit.TextChanged += OnSearchTextChanged;

		content = GetNode<InspectorContent<TItem>>("ScrollContainer/Content");
		//content.UpdateContent(AvaliableItems, ButtonGenerator);

        events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.FileLoaded += CallDeferredUpdateContent;

        if( this is AddItemsInspector<TItem> addItemsInspector &&
            content is AddItemsInspectorContent<TItem> addContent)
        {
            addItemsInspector._InspectorInitialize(addContent, AvaliableItems);
        }

        AddBankEvents(events);
        _ChildReady();
	}

    void CallDeferredUpdateContent() {
        this.CallDeferred(ItemsInspector<TItem>.MethodName.UpdateContent);
    }

    public override void _Notification(int what) {
        if(what == NotificationPredelete) {
            events.FileLoaded -= CallDeferredUpdateContent;
            RemoveBankEvents(events);
        }
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
        RemoveItem(bankItem.Name);
    }

    protected void BankItemModified(TItem bankItem) {
        RemoveItem(bankItem.Name);
        UpdateItem(bankItem);
    }

    TItem? GetItemBankMatch(TItem bankItem) {
        return AvaliableItems.FirstOrDefault(i => i.Name == bankItem.Name);
    }
}