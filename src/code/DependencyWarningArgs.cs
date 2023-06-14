record DependencyWarningEventArgs(
    Item BankItem,
    Item? LocalItem, 
    IEnumerable<Recipe> Recipes
);