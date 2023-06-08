partial class FormItemSetComponent : Container, FormComponent<ItemSet>
{
    public ItemSet GetValue => _itemSet;
    public bool IsCompleted => GetValue.Food.Count() != 0 && GetValue.Inventory.Count() != 0;
    public event Action? ComponentChanged;

    ItemSet _itemSet;

    public FormItemSetComponent()
    {
        ComponentChanged += () => {};
        ComponentChanged.Invoke();
        _itemSet = new();
    }
}