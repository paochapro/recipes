partial class CreateInvForm : VBoxContainer, CreateForm<InventoryItem>
{
    public event Action<string> ErrorOccured;

    public CreateInvForm() => ErrorOccured += (m) => {};

    public void AddToBank()
    {
        string name = GetNode<LineEdit>("Name/LineEdit").Text;
        string category = GetNode<LineEdit>("Category/LineEdit").Text;

        List<string> errors = new();

        if(name == "") errors.Add("Имя отсутствует.");
        if(category == "") errors.Add("Категория отсутствует.");

        if(errors.Count() != 0)
            throw new CustomErrorException(string.Join('\n', errors));

        InventoryItem result = new(name, category);
        
        GetNode<Program>("/root/Program").AddInvItem(result);
    }
}