abstract partial class AddLocalItemButton<TItem> : ItemButton<TItem>
    where TItem : Item
{
    #nullable disable
    [Export] Button button;
    protected Program program;
    #nullable restore

    public bool IsLocked {
        get => button.Disabled;
        set => button.Disabled = value;
    }

    public override void CustomInit(TItem item, Program program)
    {
        this.program = program;
        button.Pressed += OnButtonPress;
        button.Disabled = DisabledCondition;
    }

    protected abstract bool DisabledCondition { get; }
    protected abstract Action OnButtonPress { get; }
}