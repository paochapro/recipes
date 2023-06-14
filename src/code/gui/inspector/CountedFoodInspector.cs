abstract partial class CountedFoodInspector : ItemsInspector<FoodWithCount>
{
    protected sealed override void AddBankEvents(GlobalEvents events) {
        events.RemoveBankFood += InspectorRemoveBankFood;
        events.FoodModified += InspectorFoodModified;
    }

    protected sealed override void RemoveBankEvents(GlobalEvents events) {
        events.RemoveBankFood -= InspectorRemoveBankFood;
        events.FoodModified -= InspectorFoodModified;
    }

    void InspectorRemoveBankFood(FoodItem foodItem) => RemoveItem(foodItem.Name);

    void InspectorFoodModified(FoodItem foodItem) {
        var food = AvaliableItems.FirstOrDefault(f => f.Name == foodItem.Name);

        if(food != null) {
            RemoveItem(foodItem.Name);
            UpdateItem(new FoodWithCount(foodItem, food.Count));
        }
    }
}