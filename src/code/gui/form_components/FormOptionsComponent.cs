public partial class FormOptionsComponent : HBoxContainer, FormComponent<int>
{
    public int GetValue => GetNode<OptionButton>("OptionButton").Selected;
    public bool IsCompleted => GetValue != 0;
    public event Action? ComponentChanged;

    public void OnItemSelected(int index) => ComponentChanged?.Invoke();

    public void SetValue(int value) {
        var optionButton = GetNode<OptionButton>("OptionButton");
        optionButton.Selected = value; //TODO: does he call event ItemSelected?
    }
}