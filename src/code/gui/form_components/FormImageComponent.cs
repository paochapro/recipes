public partial class FormImageComponent : VBoxContainer, FormComponent<string>
{
	#nullable disable
	TextureRect previewImage;
	FormLineEditComponent imagePath;
    Label errorLabel;
    FileDialog fileDialog;
	#nullable restore

	public event Action? ComponentChanged;
	public string GetValue => imagePath.GetValue;
	public bool IsCompleted => imagePath.IsCompleted;

    public void SetValue(string value) {
        LoadImage(value);
    }

	public override void _Ready()
	{
		imagePath = GetNode<FormLineEditComponent>("Path");
        imagePath.ComponentChanged += () => ComponentChanged?.Invoke();
		previewImage = GetNode<TextureRect>("Load/TextureRect");
		errorLabel = GetNode<Label>("ErrorLabel");

        fileDialog = GetNode<FileDialog>("FileDialog");
		fileDialog.FileSelected += LoadImage;
        
        this.VisibilityChanged += HideErrorMessage;
	}

	public void OnLoadImageButtonPressed()
	{
		if(fileDialog == null) {
			GD.PrintErr("FileDialog is not set (null) [FormImageComponent]");
			return;
		}

		fileDialog.Popup();
	}

	public void LoadImage(string path)
	{
		try {
			var texture = ImageLoader.GetImage(path);

			if(texture == null)
				throw new Exception("Wrong format for loading image (." + path.Split(".").Last() + ")");

            imagePath.SetValue(path);
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