partial class CreateFoodForm : VBoxContainer, CreateForm<FoodItem>
{
    #nullable disable
    TextureRect image;
    #nullable restore

    [Export] PackedScene? fileDialogScene;

    public event Action<string> ErrorOccured;

    public CreateFoodForm() => ErrorOccured += (m) => {};
    
    public override void _Ready()
    {
        image = GetNode<TextureRect>("Image/TextureRect");
    }

    public void OnImageButtonPressed()
    {
        if(fileDialogScene == null) {
            GD.PrintErr("FileDialog scene is not set (null) [CreateFood.cs]");
            return;
        }

        var fileDialog = fileDialogScene.Instantiate<FileDialog>();
        fileDialog.Confirmed += () => { LoadImage(fileDialog); };
        AddChild(fileDialog);
        fileDialog.Show();
    }

    public void LoadImage(FileDialog fileDialog)
    {
        try {
            var texture = GD.Load<Texture2D>(fileDialog.CurrentPath);

            if(texture == null)
                throw new Exception("Wrong format for loading image (" + fileDialog.CurrentFile.Split(".").Last() + ")");

            image.Texture = texture;
        }
        catch(Exception ex)
        {
            GD.PushError("Exception in [CreateFoodForm.cs]: " + ex.Message);
            ErrorOccured.Invoke("Не удалось загрузить изображение.");
        }

        fileDialog.QueueFree();
    }

    public FoodItem CreateObject()
    {
        string name = GetNode<LineEdit>("Name/LineEdit").Text;
        string category = GetNode<LineEdit>("Category/LineEdit").Text; 
        Texture2D texture = GetNode<TextureRect>("Image/TextureRect").Texture;

        if(name == "" && category == "")
            throw new CustomErrorException("Имя и категория отсутствуют.");

        if(name == "")
            throw new CustomErrorException("Имя отсутствует.");

        if(category == "")
            throw new CustomErrorException("Категория отсутствует.");

        return new FoodItem(name, category, "fix this!"); //TODO: fix texture uids
    }
}

class CustomErrorException : Exception 
{
    public CustomErrorException(string message)
        : base(message)
    {
    }
}