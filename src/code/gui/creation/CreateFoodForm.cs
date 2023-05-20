partial class CreateFoodForm : CreateForm<FoodItem>
{
    public override FoodItem CreateObject()
    {
        string name = GetNode<FormLineEditComponent>("Name").GetValue;
        string category = GetNode<FormLineEditComponent>("Category").GetValue;
        string image = GetNode<FormImageComponent>("Image").GetValue;

        FoodItem result = new(name, category, image);
        return result;
    }
}