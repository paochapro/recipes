public partial class FormCategoryComponent : VBoxContainer, FormComponent<string>
{
    #nullable disable
    FormLineEditComponent categoryComp;
    #nullable restore

    public string GetValue => categoryComp.GetValue;
    public bool IsCompleted => GetValue != "";
    public event Action? ComponentChanged;

    public void SetValue(string value) {
        categoryComp.SetValue(value);
    }

    public override void _Ready()
    {
        var list = GetNode<CategoryList>("CatergoryList");
        categoryComp = GetNode<FormLineEditComponent>("Category");
        categoryComp.ComponentChanged += () => ComponentChanged?.Invoke();
        
        list.CategorySelected += (category) => {
            categoryComp.SetValue(category);
        };
    }
}