abstract partial class ItemButton<TItem> : Control
{
    //I am sorry for adding program parameter in this, but it solves the problem
    //of getting program node, when button is not in the scene tree yet
    public virtual void Initialize(TItem item, Program program)
    {
        this.Item = item;
    }

    public TItem Item { get; protected set; }
}