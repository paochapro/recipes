abstract partial class AddLocalItemButton<TItem> : ItemButton<TItem>
    where TItem : Item
{
    #nullable disable
    [Export] Button button;
    protected Program program;
    #nullable restore

    public override void Initialize(TItem item, Program program)
    {
        base.Initialize(item, program);
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
        GD.Print($"Check removed local {removed.Name}");

        if(Item.Equals(removed))
            button.Disabled = false;
    }

    protected void CheckNewLocalItem(TItem added)
    {
        GD.Print($"Check added local {added.Name}");

        if(Item.Equals(added))
            button.Disabled = true;
    }

    protected abstract void AddEvents();
    protected abstract void RemoveEvents();
    protected abstract bool DisabledCondition { get; }
    protected abstract Action OnButtonPress { get; }
}