abstract partial class AddItemsInspectorContent<TItem> : ItemsInspectorContent<TItem>
    where TItem : Item
{
    public void SetLocked(TItem compare, bool shouldLock) {
        foreach(Fold fold in GetChildren())
        foreach(AddItemButton<TItem> button in fold.MainContainer.GetChildren())
            if(button.Item.Equals(compare))
                button.IsLocked = shouldLock;
    }
}