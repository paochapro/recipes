partial class AddLocalFoodButton : AddLocalItemButton<FoodItem>
{
    protected override bool DisabledCondition => program.LocalItems.FoodItems.Contains(Item);
    protected override Action OnButtonPress => () => program.AddLocalFood(new FoodWithCount(Item, 1));

    public override void CustomInit(FoodItem item, Program program)
    {
        base.CustomInit(item, program);

        var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        var nameLabel = GetNode<Label>("HBoxContainer/Label");

        image.Texture = GD.Load<Texture2D>(item.TexturePath);
        nameLabel.Text = item.Name;
    }
}