partial class RecipeFoodButton : ItemButton<FoodWithCount>
{
    protected override void Initialize() {
        var food = base.Item;
        GetNode<TextureRect>("HBoxContainer/TextureRect").Texture = ImageLoader.GetImage(food.Item.TexturePath);
        GetNode<Label>("HBoxContainer/Label").Text = $"{food.Name} ({food.Category})";
        GetNode<Label>("HBoxContainer/MarginContainer/Count").Text = food.Count.ToString() + "x";
    }
}