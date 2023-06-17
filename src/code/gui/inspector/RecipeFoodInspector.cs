partial class RecipeFoodInspector : CountedFoodInspector
{
    #nullable disable
    IEnumerable<FoodWithCount> avaliableItems;
    ReadonlyItemSet localItems;
    #nullable restore

    public void Initialize(IEnumerable<FoodWithCount> avaliableItems, ReadonlyItemSet localItems) {
        this.avaliableItems = avaliableItems;
        this.localItems = localItems;
        UpdateContent();
    }

    protected override IEnumerable<FoodWithCount> AvaliableItems => avaliableItems;

    protected override ButtonGenerator<FoodWithCount> ButtonGenerator {
        get {
            return new RecipeButtonGenerator<FoodWithCount>(buttonScene, (i) => {}, NameLabelSettingsGenerator);
        }
    }

    LabelSettings NameLabelSettingsGenerator(FoodWithCount food) {
        var program = GetNode<Program>("/root/Program");
        Color color = HightlightColor.GetFoodColor(food, localItems);
        return new LabelSettings() { FontColor = color };
    }
}