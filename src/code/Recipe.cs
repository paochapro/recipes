readonly record struct Recipe(
    string Title,
    string Instructions,
    string ImageTextureUID,
    int Minutes,
    ItemSet ItemSet,
    DishType DishType
);

enum DishType
{
    None,
    First,
    Second,
    Third,
}