partial class FoodItemButton : FoodButton
{
	public void Initialize(string name, Texture2D imageTexture, int count, Action removeButtonPressed, Action<double> onCountChanged)
	{
        base.Initialize(name, imageTexture, removeButtonPressed);

        //Spinbox
        var spinbox = GetNode<SpinBox>("HBoxContainer/SpinBox");
        Godot.Range.ValueChangedEventHandler changedFunc = (double value) => onCountChanged(value);
        spinbox.Value = count;
        spinbox.ValueChanged += changedFunc;
	}
}