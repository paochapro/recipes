partial class ModifyFoodMenu : ModifyMenu
{
	public override void _Ready()
	{
        base._Ready();

        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        var nameComp = form.GetNode<FormLineEditComponent>("Name");
        var categoryComp = form.GetNode<FormCategoryComponent>("FormCategoryComponent");
        var imageComp = form.GetNodeOrNull<FormImageComponent>("Image");

        events.OpenFoodModificationMenu += (FoodItem foodItem) => {
            nameComp.SetValue(foodItem.Name);
            categoryComp.SetValue(foodItem.Category);
            imageComp.SetValue(foodItem.TexturePath);
        };
	}
}