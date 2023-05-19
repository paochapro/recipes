public partial class FormLineEditComponent : HBoxContainer, FormComponent<string>
{
	public event Action? ComponentChanged;
	public bool IsCompleted => GetValue != "";
	public string GetValue => GetNode<LineEdit>("LineEdit").Text;

	public void OnLineEditTextChanged(string text) => ComponentChanged?.Invoke();
}