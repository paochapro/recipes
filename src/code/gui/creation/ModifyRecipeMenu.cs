partial class ModifyRecipeMenu : ModifyMenu
{
	public override void _Ready()
	{
        base._Ready();

        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        var title = form.GetNode<FormLineEditComponent>("Title");
        var image = form.GetNode<FormImageComponent>("Image");
        var dishType = form.GetNode<FormOptionsComponent>("DishType");
        var time = form.GetNode<FormSpinBoxComponent>("Time");
        var itemSet = form.GetNode<FormItemSetComponent>("ItemSet"); //TODO: itemset reference here
        var instructions = form.GetNode<FormTextEditChanged>("Instructions");

        events.OpenRecipeModificationMenu += (Recipe recipe) => {   
            title.SetValue(recipe.Title);
            image.SetValue(recipe.ImageTextureUID);
            dishType.SetValue((int)recipe.DishType);
            time.SetValue(recipe.Minutes);
            itemSet.SetValue(recipe.ItemSet);
            instructions.SetValue(recipe.Instructions);
        };
	}
}