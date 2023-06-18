partial class CreateRecipeForm : CreateForm<Recipe>
{
	#nullable disable
	FormImageComponent imageComponent;
	#nullable restore

	public override void _Ready()
	{
		imageComponent = GetNode<FormImageComponent>("Image");
		base._Ready();

        // #if DEBUG
        //     GetNode<FormTextEditChanged>("Instructions").SetValue("debug");
        //     GetNode<FormOptionsComponent>("DishType").SetValue(1);
        //     GetNode<FormSpinBoxComponent>("Time").SetValue(1);
        //     imageComponent.SetValue("res://content/tomato.svg");
        // #endif
	}
	
	public override Recipe CreateObject()
	{
		string title = GetNode<FormLineEditComponent>("Title").GetValue;
		string instuctions = GetNode<FormTextEditChanged>("Instructions").GetValue;
		string imagePath = imageComponent.GetValue;
        ItemSet itemSet = GetNode<FormItemSetComponent>("ItemSet").GetValue;
		int selectedDishTypeInt = GetNode<FormOptionsComponent>("DishType").GetValue;
		DishType dishType = (DishType)selectedDishTypeInt;

		int time = (int)GetNode<FormSpinBoxComponent>("Time").GetValue;

		Recipe recipe = new(title, instuctions, imagePath, time, itemSet, dishType);
		
		return recipe;
	}
}