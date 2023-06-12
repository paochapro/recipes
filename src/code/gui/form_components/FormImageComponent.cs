public partial class FormImageComponent : VBoxContainer, FormComponent<string>
{
	#nullable disable
	TextureRect previewImage;
	FormLineEditComponent imagePath;
    Label errorLabel;
	#nullable restore

	[Export] PackedScene? fileDialogScene;
	
	public event Action? ComponentChanged;
	public string GetValue => imagePath.GetValue;
	public bool IsCompleted => imagePath.IsCompleted;

    public void SetValue(string value) {
        imagePath.SetValue(value);
    }

	public override void _Ready()
	{
		previewImage = GetNode<TextureRect>("Load/TextureRect");
		imagePath = GetNode<FormLineEditComponent>("Path");
        imagePath.ComponentChanged += () => ComponentChanged?.Invoke();
		errorLabel = GetNode<Label>("ErrorLabel");
        this.VisibilityChanged += HideErrorMessage;
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
				throw new Exception("Wrong format for loading image (." + path.Split(".").Last() + ")");

			var pathTb = imagePath.GetNode<LineEdit>("LineEdit");
			pathTb.Text = path;

            //Text property doesnt emit text_changed signal, so we do it ourselfs
			pathTb.EmitSignal("text_changed", pathTb.Text);
			previewImage.Texture = texture;

            HideErrorMessage();
		}
		catch(Exception ex)
		{
			GD.PushError("Exception in [CreateFoodForm.cs]: " + ex.Message);
            ShowErrorMessage("Не удалось загрузить изображение.");
		}
	}

    void ShowErrorMessage(string msg) {
        errorLabel.Text = msg;
        errorLabel.Show();
    }

    void HideErrorMessage() => errorLabel.Hide();
}