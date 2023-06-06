partial class CreateFoodForm : CreateForm<FoodItem>
{
    public override FoodItem CreateObject()
    {
        string name = GetNode<FormLineEditComponent>("Name").GetValue;
        string category = GetNode<FormComponent<string>>("FormCategoryComponent").GetValue;
        string image = GetNode<FormImageComponent>("Image").GetValue;

        FoodItem result = new(name, category, image);
        return result;
    }
}