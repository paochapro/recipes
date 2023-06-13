partial class RecipeFoodInspector : ItemsInspector<FoodWithCount>
{
    #nullable disable
    IEnumerable<FoodWithCount> avaliableItems;
    #nullable restore

    public void Initialize(IEnumerable<FoodWithCount> avaliableItems) {
        this.avaliableItems = avaliableItems;

        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.FoodModified += ModifyFood;

        this.TreeExiting += () => {
            events.FoodModified -= ModifyFood;
        };

        UpdateContent();
    }

    protected override IEnumerable<FoodWithCount> AvaliableItems => avaliableItems;

    protected override ButtonGenerator<FoodWithCount> ButtonGenerator {
        get {
            return new ButtonGenerator<FoodWithCount>(buttonScene, (i) => {});
        }
    }

    void ModifyFood(FoodItem item)
    {
        var modifyFood = avaliableItems.FirstOrDefault(f => f.Item.Name == item.Name);

        if(modifyFood != null) {
            RemoveItem(modifyFood);
            UpdateItem(new FoodWithCount(item, modifyFood.Count));
        }
    }
}