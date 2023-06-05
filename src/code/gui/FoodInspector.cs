partial class FoodInspector : ItemsInspector<FoodItem>
{
    protected override IEnumerable<FoodItem> AvaliableItems => 
        GetNode<Program>("/root/Program").ItemsBank.Food;
}
