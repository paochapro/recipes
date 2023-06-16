partial class BankRemovalSafety : Node 
{
    public void CheckForDependencies(Item bankItem)
    {
        var program = GetNode<Program>("/root/Program");

        Item? dependedLocalItem = null;
        IEnumerable<Recipe> dependedRecipes = Enumerable.Empty<Recipe>();

        if(bankItem is FoodItem bankFood) {
            dependedLocalItem = program.LocalItems.Food.FirstOrDefault(f => f.Item == bankFood);
            dependedRecipes = program.RecipeBank.Where(r => r.ItemSet.FoodItems.Contains(bankFood));
        }

        if(bankItem is InventoryItem bankInv) {
            dependedLocalItem = program.LocalItems.Inventory.FirstOrDefault(i => i == bankInv);
            dependedRecipes = program.RecipeBank.Where(r => r.ItemSet.InventoryList.Contains(bankInv));
        }

        if(dependedLocalItem != null || dependedRecipes.Count() != 0) {
            var args = new DependencyWarningEventArgs(bankItem, dependedLocalItem, dependedRecipes);
            ShowWarning(args);
        }
        else 
            RemoveItem(bankItem);   
    }

    void RemoveItem(Item bankItem) {
        var program = GetNode<Program>("/root/Program");

        if(bankItem is FoodItem bankFood)
            program.RemoveFoodItem(bankFood);

        if(bankItem is InventoryItem bankInv)
            program.RemoveInvItem(bankInv);
    }

    void ShowWarning(DependencyWarningEventArgs args)
    {
        var creator = GetNode<DependenciesWarningCreator>("/root/GuiRoot/%DependenciesWarningCreator");
        var window = creator.ShowWarning(args);

        Action? onConfirmed = GetRemoveAction(args.BankItem);

        if(onConfirmed != null)
            window.Confirmed += onConfirmed;
    }

    Action? GetRemoveAction(Item bankItem) {
        var program = GetNode<Program>("/root/Program");

        if(bankItem is FoodItem bankFood)
            return () => program.RemoveFoodItem(bankFood);

        if(bankItem is InventoryItem bankInv)
            return () => program.RemoveInvItem(bankInv);

        return null;
    }
}