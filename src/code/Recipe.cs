readonly record struct  Recipe(
    string Title,
    string Instructions,
    string ImageTextureUID,
    int Minutes,
    ReadonlyItemSet ItemSet,
    DishType DishType
);

enum DishType
{
    None,
    Normal,
    Max,
}