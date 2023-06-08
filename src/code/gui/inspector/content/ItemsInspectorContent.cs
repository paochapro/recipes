abstract partial class ItemsInspectorContent<TItem> : Container
	where TItem : Item
{
	public void UpdateContent(IEnumerable<TItem> items, ButtonGenerator<TItem> generator, bool autoExpand = false)
	{
		this.RemoveChildren();

		var groups = items.GroupBy(i => i.Category);

		foreach(IGrouping<string, TItem> group in groups)
		{
			Fold fold = new Fold() { Expanded = autoExpand };
			fold.Title = group.Key;

			var controls = group.Select(i => generator.GetButton(i));
			fold.MainContainer.AddChildren(controls);

			this.AddChild(fold);
		}
	}

	//This could cause a ton of bugs, should handle new item creation better
	public void UpdateItem(TItem item, ButtonGenerator<TItem> generator)
	{
		var control = generator.GetButton(item);

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

	public void RemoveItem(TItem removing)
	{
		foreach(Fold fold in GetChildren())
		foreach(ItemButton<TItem> button in fold.MainContainer.GetChildren())
		{
			var container = fold.MainContainer;
            var item1 = button.Item as Item;
            var item2 = removing as Item;

			if(item1 == item2)
			{
				container.RemoveChild(button);

				if(container.GetChildCount() == 0)
					fold.QueueFree();
			}
		}
	}

	// Control GetControlForItem(TItem item)
	// {
	// 	if(itemButtonScene == null)
	// 		throw new Exception("No item button scene [ItemsInspectorContent.cs]");

	// 	var itemButton = itemButtonScene.Instantiate<ItemButton<TItem>>();
	// 	var program = GetNode<Program>("/root/Program");
	// 	itemButton.Initialize(item, program);
		
	// 	return itemButton;
	// }
}