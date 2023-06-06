partial class CreateInvForm : CreateForm<InventoryItem>
{
    public override InventoryItem CreateObject()
    {
        string name = GetNode<FormLineEditComponent>("Name").GetValue;
        string category = GetNode<FormComponent<string>>("FormCategoryComponent").GetValue;

        InventoryItem result = new(name, category);
        return result;
    }
}