abstract partial class ModifyMenu : PanelContainer
{
    #nullable disable
    Button createButton;
    protected Form form;
    #nullable restore

	public override void _Ready()
	{
        createButton = GetNode<Button>("Content/Create");
        form = GetNode("Content/FormContainer/MarginContainer").GetChild<Form>(0);
        form.FormChanged += () => OnFormChanged(form);        
	}

    void OnFormChanged(Form form) {
        createButton.Disabled = !form.IsFormCompleted;
    }

    void OnCreateButtonPressed()
    {
        var program = GetNode<Program>("/root/Program");

        //TODO: Distribute this functionality to inherited classes
        if(form is CreateFoodForm foodForm)
            program.FoodItemModified(foodForm.CreateObject());

        if(form is CreateInvForm invForm)
            program.InvItemModified(invForm.CreateObject());

        if(form is CreateRecipeForm recipeForm)
            program.RecipeModified(recipeForm.CreateObject());
    }
}