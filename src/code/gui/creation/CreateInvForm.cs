partial class CreateInvForm : CreateForm
{
    public override void AddToBank()
    {
        string name = GetNode<FormLineEditComponent>("Name").GetValue;
        string category = GetNode<FormLineEditComponent>("Category").GetValue;

        InventoryItem result = new(name, category);
        GetNode<Program>("/root/Program").AddInvItem(result);
    }
}