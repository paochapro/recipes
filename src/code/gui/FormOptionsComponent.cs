public partial class FormOptionsComponent : HBoxContainer, FormComponent<int>
{
    public int GetValue => GetNode<OptionButton>("OptionButton").Selected;
    public bool IsCompleted => GetValue != 0;
    public event Action? ComponentChanged;

    public void OnItemSelected(int index) => ComponentChanged?.Invoke();
}