partial class ModifyFoodMenu : PanelContainer
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

        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        var nameComp = form.GetNode<FormLineEditComponent>("Name");
        var categoryComp = form.GetNode<FormCategoryComponent>("FormCategoryComponent");
        var imageComp = form.GetNodeOrNull<FormImageComponent>("Image");

        events.OpenFoodModificationMenu += (FoodItem foodItem) => {
            GD.Print("ModifyFoodMenu OpenFoodModificationMenu called");
            nameComp.SetValue(foodItem.Name);
            categoryComp.SetValue(foodItem.Category);
            imageComp.SetValue(foodItem.TexturePath);
        };

        events.OpenInvModificationMenu += (InventoryItem invItem) => {
            GD.Print("ModifyFoodMenu OpenInvModificationMenu called");
            nameComp.SetValue(invItem.Name);
            categoryComp.SetValue(invItem.Category);
        };
	}

    void OnFormChanged(Form form) {
        createButton.Disabled = !form.IsFormCompleted;
    }

    void OnCreateButtonPressed()
    {
        GD.Print("Create (modify) button was pressed!");

        var program = GetNode<Program>("/root/Program");

        //TODO: Distribute this functionality to inherited classes
        if(form is CreateFoodForm foodForm)
            program.FoodItemModified(foodForm.CreateObject());

        if(form is CreateInvForm invForm)
            program.InvItemModified(invForm.CreateObject());
    }
}