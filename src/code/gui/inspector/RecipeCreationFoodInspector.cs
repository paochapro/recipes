partial class RecipeCreationFoodInspector : CountedFoodInspector
{
	#nullable disable
	FormItemSetComponent formComponent;
	#nullable restore

	protected override IEnumerable<FoodWithCount> AvaliableItems => formComponent.GetItemSetReference.Food;

	protected override ButtonGenerator<FoodWithCount> ButtonGenerator {
		get {
			var events = GetNode<GlobalEvents>("/root/GlobalEvents");
			var onPressed = (FoodWithCount food) => formComponent.RemoveFood(food);
			return new ButtonGenerator<FoodWithCount>(buttonScene, onPressed);
		}
	}

	protected override void _ChildReady() {
		formComponent = GetNode<FormItemSetComponent>("../../../../../../..");
		formComponent.NewItemSet += (set) => UpdateContent();
		formComponent.AddedFood += (i) => UpdateItem(i);
		formComponent.RemovedFood += (i) => RemoveItem(i);
	}
}
