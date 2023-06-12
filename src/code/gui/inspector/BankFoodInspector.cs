partial class BankFoodInspector : ItemsInspector<FoodItem>
{
	protected override IEnumerable<FoodItem> AvaliableItems =>
		GetNode<Program>("/root/Program").ItemsBank.Food;

	protected override ButtonGenerator<FoodItem> ButtonGenerator {
		get {
			var program = GetNode<Program>("/root/Program");
			var onPressed = (FoodItem item) => program.RemoveFoodItem(item);
			return new ButtonGenerator<FoodItem>(buttonScene, onPressed);
		}
	}

	protected override void _ChildReady() {
		var events = GetNode<GlobalEvents>("/root/GlobalEvents");
		events.NewBankFood += (i) => UpdateItem(i);
		events.RemoveBankFood += (i) => RemoveItem(i);
        //events.FileLoaded += UpdateContent;
	}
}
