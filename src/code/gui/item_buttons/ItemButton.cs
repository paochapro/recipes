abstract partial class ItemButton<TItem> : Control
	where TItem : Item
{
	#nullable disable
	[Export] protected Button button;
	public TItem Item { get; private set; }
	#nullable restore

	public void ButtonInitialize(TItem item, Action<TItem> onButtonPressed) 
	{
		if(button == null)
			throw new Exception("No button scene in ItemButton");

		this.Item = item;
		button.Pressed += () => onButtonPressed(item);
		Initialize();
	}

	protected abstract void Initialize();
}
