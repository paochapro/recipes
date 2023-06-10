partial class RecipeFoodButton : PanelContainer
{
    public void Initialize(FoodWithCount food) {
        GetNode<TextureRect>("HBoxContainer/TextureRect").Texture = GD.Load<Texture2D>(food.Item.TexturePath);
        GetNode<Label>("HBoxContainer/Title").Text = $"{food.Name} ({food.Category})";
        GetNode<Label>("HBoxContainer/MarginContainer/Count").Text = food.Count.ToString() + "x";
    }
}