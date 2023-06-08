abstract partial class AddItemButton<TItem> : ItemButton<TItem>
    where TItem : Item
{
    public bool IsLocked {
        get => button.Disabled;
        set => button.Disabled = value;
    }
}