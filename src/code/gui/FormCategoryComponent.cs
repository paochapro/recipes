public partial class FormCategoryComponent : VBoxContainer, FormComponent<string>
{
    #nullable disable
    LineEdit categoryTb;
    #nullable restore

    public string GetValue => categoryTb.Text;
    public bool IsCompleted => GetValue != "";
    public event Action? ComponentChanged;

    public override void _Ready()
    {
        var list = GetNode<CategoryList>("CatergoryList");
        var category = GetNode<FormLineEditComponent>("Category");
        category.ComponentChanged += () => ComponentChanged?.Invoke();
        categoryTb = category.GetNode<LineEdit>("LineEdit");
        
        list.CategorySelected += (category) =>
        {
            categoryTb.Text = category;

            //Changing text property doesnt emit text_changed signal, so we do it ourselfs
            categoryTb.EmitSignal("text_changed", categoryTb.Text);
        };
    }
}