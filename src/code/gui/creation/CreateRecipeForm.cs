partial class CreateRecipeForm : VBoxContainer, CreateForm<Recipe>
{
    public event Action<string> ErrorOccured;

    public CreateRecipeForm() => ErrorOccured += (m) => {};
    
    public void AddToBank()
    {
        //TODO: make recipe creation
        GetNode<Program>("/root/Program").AddRecipe(new Recipe());
    }
}