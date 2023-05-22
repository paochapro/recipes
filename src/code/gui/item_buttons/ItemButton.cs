abstract partial class ItemButton<TItem> : Control
{
    //I am sorry for adding program parameter in this, but it solves the problem
    //of getting program node, when button is not in the scene tree yet
    public abstract void Initialize(TItem item, Program program);
}