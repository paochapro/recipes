partial class CreateInvForm : VBoxContainer, CreateForm<InventoryItem>
{
    public event Action<string> ErrorOccured;

    public CreateInvForm() => ErrorOccured += (m) => {};

    public void AddToBank()
    {
        string name = GetNode<LineEdit>("Name/LineEdit").Text;
        string category = GetNode<LineEdit>("Category/LineEdit").Text;

        if(name == "" && category == "")
            throw new CustomErrorException("Имя и категория отсутствуют.");

        if(name == "")
            throw new CustomErrorException("Имя отсутствует.");

        if(category == "")
            throw new CustomErrorException("Категория отсутствует.");

        GetNode<Program>("/root/Program").AddInvItem(new InventoryItem(name, category));
    }
}