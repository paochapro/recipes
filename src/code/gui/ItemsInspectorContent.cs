abstract partial class ItemsInspectorContent<TItem> : Container
	where TItem : Item
{
	[Export] PackedScene? itemButtonScene;

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

	Control GetControlForItem(TItem item)
	{
		if(itemButtonScene == null)
			throw new Exception("No item button scene [ItemsInspectorContent.cs]");

		var itemButton = itemButtonScene.Instantiate<ItemButton<TItem>>();
		var program = GetNode<Program>("/root/Program");
		itemButton.Initialize(item, program);
		
		return itemButton;
	}
}
