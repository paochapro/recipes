partial class CreateMenu<T> : PanelContainer
{
    public event Action<T> ObjectCreated;

    public CreateMenu() => ObjectCreated += (t) => {};

    #nullable disable
    Label errorLabel;
    CreateForm<T> form;
    #nullable restore

	public override void _Ready()
	{
        errorLabel = GetNode<Label>("Content/ErrorLabel");
        form = GetNode("Content/FormRoot").GetChild<CreateForm<T>>(0);
        form.ErrorOccured += ShowErrorMessage;
        this.VisibilityChanged += HideErrorMessage;
	}

    void OnCreateButtonPressed()
    {
        try {
            form.AddToBank();
            HideErrorMessage();
        }
        catch(CustomErrorException ex)
        {
            ShowErrorMessage(ex.Message);
            
        }
        catch(Exception ex)
        {
            GD.PrintErr("Exception on creating object [CreateMenu.cs]: " + ex.Message);
            ShowErrorMessage($"Неизвестная ошибка. Объект создать не удалось.");
        }
    }

    void ShowErrorMessage(string message)
    {
        errorLabel.Text = message;
        errorLabel.Show();
    }

    void HideErrorMessage() => errorLabel.Hide();
}