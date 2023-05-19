partial class CreateFoodForm : CreateForm
{
    public override void AddToBank()
    {
        string name = GetNode<FormLineEditComponent>("Name").GetValue;
        string category = GetNode<FormLineEditComponent>("Category").GetValue;
        string image = GetNode<FormImageComponent>("Image").GetValue;

        FoodItem result = new(name, category, image);
        GetNode<Program>("/root/Program").AddFoodItem(result);
    }
}