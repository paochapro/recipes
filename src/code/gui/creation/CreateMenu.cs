partial class CreateMenu<T> : PanelContainer
{
    #nullable disable
    Button createButton;
    CreateForm form;
    #nullable restore

	public override void _Ready()
	{
        createButton = GetNode<Button>("Content/Create");
        form = GetNode("Content/FormContainer/MarginContainer").GetChild<CreateForm>(0);
        form.FormChanged += () => OnFormChanged(form);
	}

    void OnFormChanged(CreateForm form)
    {
        createButton.Disabled = !form.IsFormCompleted;
    }

    void OnCreateButtonPressed() => form.AddToBank();
}