abstract partial class ItemsSubsectionContent<TItem> : Container
	where TItem : Item
{
	public void SearchUpdate(IEnumerable<TItem> avaliableItems, string text)
	{
		var result = ItemSearch.Search(avaliableItems.Cast<Item>(), text);
		IEnumerable<TItem> items = result.Cast<TItem>();

		bool autoExpand = text != "";
		UpdateContent(items, autoExpand);
	}

	public void UpdateContent(IEnumerable<TItem> items, bool autoExpand = false)
	{
		this.RemoveChildren();

		var groups = items.GroupBy(i => i.Category);

		foreach(IGrouping<string, TItem> group in groups)
		{
			Fold fold = new Fold() { Expanded = autoExpand };
			fold.Title = group.Key;

			var controls = group.Select(i => GetControlForItem(i));
			fold.MainContainer.AddChildren(controls);

			this.AddChild(fold);
		}
	}

    //This could cause a ton of bugs, should handle new item creation better
    public void UpdateItem(TItem item)
    {
        var control = GetControlForItem(item);

        var foundFold = GetChildren().Cast<Fold>().FirstOrDefault(f => f.Title == item.Category);

        if(foundFold != null)
            foundFold.MainContainer.AddChild(control);
        else
        {
            Fold newFold = new() { Title = item.Category };
            newFold.AddChild(control);
            this.AddChild(newFold);
        }
    }

	protected abstract Control GetControlForItem(TItem item);
}
