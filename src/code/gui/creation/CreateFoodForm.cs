partial class CreateFoodForm : VBoxContainer, CreateForm<FoodItem>
{
    #nullable disable
    FormImageComponent imageComponent;
    #nullable restore

    [Export] PackedScene? fileDialogScene;

    public event Action<string>? ErrorOccured;

    public override void _Ready()
    {
        imageComponent = GetNode<FormImageComponent>("Image");
        imageComponent.ErrorOccured += (msg) => this.ErrorOccured?.Invoke(msg);
    }

    public void AddToBank()
    {
        string name = GetNode<LineEdit>("Name/LineEdit").Text;
        string category = GetNode<LineEdit>("Category/LineEdit").Text;
        string imagePath = imageComponent.ImagePath;

        List<string> errors = new();

        if(name == "") errors.Add("Имя отсутствует.");
        if(category == "") errors.Add("Категория отсутствует.");
        if(imagePath == "") errors.Add("Изображение отсутствует.");

        if(errors.Count() != 0)
            throw new CustomErrorException(string.Join('\n', errors));

        FoodItem result = new(name, category, imagePath);
        
        GetNode<Program>("/root/Program").AddFoodItem(result);
    }
}