partial class CreateMenu<T> : PanelContainer
{
    #nullable disable
    Button createButton;
    Form form;
    #nullable restore

	public override void _Ready()
	{
        createButton = GetNode<Button>("Content/Create");
        form = GetNode("Content/FormContainer/MarginContainer").GetChild<Form>(0);
        form.FormChanged += () => OnFormChanged(form);
	}

    void OnFormChanged(Form form)
    {
        createButton.Disabled = !form.IsFormCompleted;
    }

    void OnCreateButtonPressed()
    {
        var program = GetNode<Program>("/root/Program");

        if(form is CreateFoodForm foodForm)
            program.AddFoodItem(foodForm.CreateObject());

        if(form is CreateInvForm invForm)
            program.AddInvItem(invForm.CreateObject());

        if(form is CreateRecipeForm recipeForm)
            program.AddRecipe(recipeForm.CreateObject());
    }
}