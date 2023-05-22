partial class BankFoodButton : BaseFoodButton
{
    public override void Initialize(FoodItem item, Program program)
    {
        base.Initialize(item, program);

        var button = GetNode<Button>("HBoxContainer/Button");
        button.Pressed += () => program.RemoveFoodItem(item);
        button.Pressed += this.QueueFree;
	}
}