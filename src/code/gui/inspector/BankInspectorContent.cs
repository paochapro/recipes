abstract partial class BankInspectorContent<TItem> : ItemsInspectorContent<TItem>
    where TItem : Item
{
    protected void SetLocked(TItem compare, bool shouldLock) {
        foreach(Fold fold in GetChildren())
        foreach(AddLocalItemButton<TItem> button in fold.MainContainer.GetChildren())
            if(button.Item.Equals(compare))
                button.IsLocked = shouldLock;
    }
}