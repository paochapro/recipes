readonly record struct FileLocalFood (
    FoodItem FoodItem, 
    int Count
);

readonly record struct FileTable(
    Recipe[] recipeBank,
    FoodItem[] bankFood,
    InventoryItem[] bankInv,
    FileLocalFood[] localFood,
    InventoryItem[] localInv
);