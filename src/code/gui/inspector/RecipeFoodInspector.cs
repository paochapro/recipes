partial class RecipeFoodInspector : CountedFoodInspector
{
    #nullable disable
    IEnumerable<FoodWithCount> avaliableItems;
    #nullable restore

    public void Initialize(IEnumerable<FoodWithCount> avaliableItems) {
        this.avaliableItems = avaliableItems;
        UpdateContent();
    }

    protected override IEnumerable<FoodWithCount> AvaliableItems => avaliableItems;

    protected override ButtonGenerator<FoodWithCount> ButtonGenerator {
        get {
            return new ButtonGenerator<FoodWithCount>(buttonScene, (i) => {});
        }
    }
}