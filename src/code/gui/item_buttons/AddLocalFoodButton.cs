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

    protected override void AddEvents()
    {
        var events = program.GetNode<GlobalEvents>("/root/GlobalEvents");
        events.RemoveLocalFood += CheckAdded;
        events.NewLocalFood += CheckRemoved;
    }

    protected override void RemoveEvents()
    {
        var events = program.GetNode<GlobalEvents>("/root/GlobalEvents");
        events.RemoveLocalFood -= CheckAdded;
        events.NewLocalFood -= CheckRemoved;
    }

    void CheckAdded(FoodWithCount food) => CheckRemovedLocalItem(food.Item);
    void CheckRemoved(FoodWithCount food) => CheckNewLocalItem(food.Item);
}