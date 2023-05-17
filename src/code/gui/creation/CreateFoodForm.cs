partial class CreateFoodForm : VBoxContainer, CreateForm<FoodItem>
{
    #nullable disable
    TextureRect image;
    LineEdit imagePathTb;
    #nullable restore

    [Export] PackedScene? fileDialogScene;

    public event Action<string>? ErrorOccured;

    public override void _Ready()
    {
        image = GetNode<TextureRect>("Image/TextureRect");
        imagePathTb = GetNode<LineEdit>("ImagePath/LineEdit");
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

            imagePathTb.Text = fileDialog.CurrentPath;
            image.Texture = texture;
        }
        catch(Exception ex)
        {
            GD.PushError("Exception in [CreateFoodForm.cs]: " + ex.Message);
            ErrorOccured?.Invoke("Не удалось загрузить изображение.");
        }

        fileDialog.QueueFree();
    }

    public void AddToBank()
    {
        string name = GetNode<LineEdit>("Name/LineEdit").Text;
        string category = GetNode<LineEdit>("Category/LineEdit").Text;
        string imagePath = imagePathTb.Text;

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

class CustomErrorException : Exception 
{
    public CustomErrorException(string message)
        : base(message)
    {
    }
}