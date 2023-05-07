partial class CreateRecipeForm : VBoxContainer, CreateForm<Recipe>
{
    public event Action<string> ErrorOccured;

    public CreateRecipeForm() => ErrorOccured += (m) => {};
    
    public Recipe CreateObject()
    {
        return new Recipe();
    }
}