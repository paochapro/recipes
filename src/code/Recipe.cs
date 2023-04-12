readonly record struct  Recipe(
    string Title,
    string Instructions,
    string ImageTextureUID,
    RecipeSearchData SearchData
);

readonly record struct RecipeSearchData(
    ReadonlyItemSet ItemSet,
    DishType DishType,
    int Minutes
);

enum DishType
{
    None,
    Normal,
    Max,
}