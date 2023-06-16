readonly record struct SearchInfo
(
    string Title,
    ReadonlyItemSet LocalItemSet,
    ReadonlyItemSet FilterItemSet,
    DishType DishType,
    int Minutes
);