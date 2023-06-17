partial class CreateMenu : PanelContainer
{
    #nullable disable
    Button createButton;
    Form form;
    Label errorLabel;
    #nullable restore

	public override void _Ready()
	{
        createButton = GetNode<Button>("Content/Create");
        errorLabel = GetNode<Label>("Content/ErrorLabel");
        form = GetNode("Content/FormContainer/MarginContainer").GetChild<Form>(0);
        form.FormChanged += () => UpdateCreateButton();
	}

    void UpdateCreateButton() {
        bool isDuplicate = CheckForDuplicate();
        errorLabel.Visible = isDuplicate;
        
        bool enabled = form.IsFormCompleted && !isDuplicate;
        createButton.Disabled = !enabled;
    }

    void OnCreateButtonPressed()
    {
        var program = GetNode<Program>("/root/Program");

        //TODO: Distribute this functionality to inherited classes
        if(form is CreateFoodForm foodForm)
            program.AddFoodItem(foodForm.CreateObject());

        if(form is CreateInvForm invForm)
            program.AddInvItem(invForm.CreateObject());

        if(form is CreateRecipeForm recipeForm)
            program.AddRecipe(recipeForm.CreateObject());

        UpdateCreateButton();
    }

    bool CheckForDuplicate()
    {
        var program = GetNode<Program>("/root/Program");

        if(form is CreateFoodForm foodForm) {
            var food = foodForm.CreateObject();
            return program.ItemsBank.Food.Select(i => i.Name).Contains(food.Name);
        }

        if(form is CreateInvForm invForm) {
            var inv = invForm.CreateObject();
            return program.ItemsBank.Inventory.Select(i => i.Name).Contains(inv.Name);
        }

        if(form is CreateRecipeForm recipeForm) {
            var recipe = recipeForm.CreateObject();
            return program.RecipeBank.Select(r => r.Title).Contains(recipe.Title);
        }

        return false;
    }
}