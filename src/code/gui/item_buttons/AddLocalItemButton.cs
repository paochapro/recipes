abstract partial class AddLocalItemButton<TItem> : ItemButton<TItem>
    where TItem : Item
{
    #nullable disable
    [Export] Button button;
    protected TItem item;
    protected Program program;
    #nullable restore

    public override void Initialize(TItem item, Program program)
    {
        this.item = item;
        this.program = program;
        button.Pressed += OnButtonPress;
        button.Disabled = DisabledCondition;
        AddEvents();
    }

    public override void _Notification(int what)
    {
        if(what == NotificationPredelete)
            RemoveEvents();
    }

    protected void CheckRemovedLocalItem(TItem removed)
    {
        if(item.Equals(removed))
            button.Disabled = true;
    }

    protected void CheckNewLocalItem(TItem added)
    {
        if(item.Equals(added))
            button.Disabled = false;
    }

    protected abstract void AddEvents();
    protected abstract void RemoveEvents();
    protected abstract bool DisabledCondition { get; }
    protected abstract Action OnButtonPress { get; }
}