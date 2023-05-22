abstract partial class ItemsSubsection<TItem> : ItemsInspector<TItem>
	where TItem : Item
{
    protected abstract DynamicWindowMenu SwitchMenu { get; }

	void OnMenuButtonPressed()
    {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.CallSwitchDynamicWindow(SwitchMenu);
    }
}