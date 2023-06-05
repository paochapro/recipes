abstract partial class ItemsSubsection<TItem> : ItemsInspector<TItem>
	where TItem : Item
{
    protected abstract DynamicWindowMenu SwitchMenu { get; }

    public ItemsSubsection()
    {
        var menuButton = GetNode<Button>("ControlPanel/Button");
        menuButton.Pressed += OnMenuButtonPressed;
    }

	void OnMenuButtonPressed()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.CallSwitchDynamicWindow(SwitchMenu);
    }
}