partial class CreateRecipeForm : VBoxContainer, CreateForm<Recipe>
{
    public event Action<string> ErrorOccured;

    public CreateRecipeForm() => ErrorOccured += (m) => {};
    
    public void AddToBank()
    {
        //TODO: this stuff
        GetNode<Program>("/root/Program").AddRecipe(new Recipe());
    }
}