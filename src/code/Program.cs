partial class Program : Node
{
    List<Recipe> recipeBank = new();

    ItemSet itemsBank = new();
    ItemSet localItems = new();

    //Getters
    public IEnumerable<Recipe> RecipeBank => recipeBank;
    public ReadonlyItemSet ItemsBank => itemsBank;
    public ReadonlyItemSet LocalItems => localItems;
}