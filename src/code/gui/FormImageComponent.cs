using Godot;
using System;

public partial class FormImageComponent : VBoxContainer
{
    #nullable disable
    TextureRect previewImage;
    LineEdit imagePathTb;
    #nullable restore

    [Export] PackedScene? fileDialogScene;
    
    public Action<string>? ErrorOccured;
    public string ImagePath => imagePathTb.Text;

	public override void _Ready()
    {
        previewImage = GetNode<TextureRect>("Load/TextureRect");
        imagePathTb = GetNode<LineEdit>("Path/LineEdit");
    }

    public void OnLoadImageButtonPressed()
    {
        if(fileDialogScene == null) {
            GD.PrintErr("FileDialog scene is not set (null) [FormImageComponent]");
            return;
        }

        var fileDialog = fileDialogScene.Instantiate<FileDialog>();
        fileDialog.Confirmed += () => DialogConfomation(fileDialog);
        AddChild(fileDialog);
        fileDialog.Popup();
    }

    void DialogConfomation(FileDialog fileDialog)
    {
        LoadImage(fileDialog.CurrentPath);
        fileDialog.QueueFree();
    }

    public void LoadImage(string path)
    {
        try 
        {
            var texture = GD.Load<Texture2D>(path);

            if(texture == null)
                throw new Exception("Wrong format for loading image (" + path.Split(".").Last() + ")");

            imagePathTb.Text = path;
            previewImage.Texture = texture;
        }
        catch(Exception ex)
        {
            GD.PushError("Exception in [CreateFoodForm.cs]: " + ex.Message);
            ErrorOccured?.Invoke("Не удалось загрузить изображение.");
        }
    }
}