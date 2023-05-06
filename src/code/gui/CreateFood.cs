using Godot;
using System;

partial class CreateFood : PanelContainer
{
    [Export] PackedScene? fileDialogScene;
    public event Action<FoodItem> NewFoodItem;

    public CreateFood()
    {
        NewFoodItem += (f) => {};
    }

	public override void _Ready()
	{
        image = GetNode<TextureRect>("Components/HBoxContainer/TextureRect");
        nameLineEdit = GetNode<LineEdit>("Components/Name/LineEdit");
        catergoryLineEdit = GetNode<LineEdit>("Components/Category/LineEdit");
        loadErrorLabel = GetNode<Label>("Components/LoadErrorLabel");
        emptyErrorLabel = GetNode<Label>("Components/EmptyErrorLabel");
        program = GetNode<Program>("/root/Program");

        loadErrorLabel.VisibilityChanged += () => 
        {
            if(loadErrorLabel.Visible)
                emptyErrorLabel.Hide();
        };

        emptyErrorLabel.VisibilityChanged += () =>
        {
            if(emptyErrorLabel.Visible)
                loadErrorLabel.Hide();
        };
	}

    public void OnCreateButtonPressed()
    {
        string name = nameLineEdit.Text;
        string category = catergoryLineEdit.Text;
        Texture2D texture = image.Texture;

        if(name == "" || category == "") {
            emptyErrorLabel.Show();
            return;
        }

        emptyErrorLabel.Hide();
        FoodItem food = new(name, category, "fix this! [CreateFood.cs]");
        NewFoodItem.Invoke(food);
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
            loadErrorLabel.Hide();
        }
        catch(Exception ex) {
            loadErrorLabel.Show();
            GD.Print("Image load exception [CreateFood.cs]: " + ex.Message);
        }

        fileDialog.QueueFree();
    }

    #nullable disable
    TextureRect image;
    LineEdit nameLineEdit;
    LineEdit catergoryLineEdit;
    Label loadErrorLabel;
    Label emptyErrorLabel;
    Program program;
}