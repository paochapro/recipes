abstract partial class ItemButton<TItem> : Control
{
    //I am sorry for adding program parameter in this, but it solves the problem
    //of getting program node, when button is not in the scene tree yet
    public void Initialize(TItem item, Program program)
    {
        this.Item = item;
        this.Program = program;
        CustomInit(item, program);
    }

    public abstract void CustomInit(TItem item, Program program);

    #nullable disable
    public TItem Item { get; private set; }
    protected Program Program { get; private set; }
    #nullable restore
}