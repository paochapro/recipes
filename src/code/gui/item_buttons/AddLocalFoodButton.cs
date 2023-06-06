partial class AddLocalFoodButton : AddLocalItemButton<FoodItem>
{
    protected override bool DisabledCondition => program.LocalItems.FoodItems.Contains(item);
    protected override Action OnButtonPress => () => program.AddLocalFood(new FoodWithCount(item, 1));

    public override void Initialize(FoodItem item, Program program)
    {
        var image = GetNode<TextureRect>("HBoxContainer/TextureRect");
        var nameLabel = GetNode<Label>("HBoxContainer/Label");

        image.Texture = GD.Load<Texture2D>(item.TexturePath);
        nameLabel.Text = item.Name;

        base.Initialize(item, program);
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