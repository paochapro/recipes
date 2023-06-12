public partial class FormLineEditComponent : HBoxContainer, FormComponent<string>
{
	public event Action? ComponentChanged;
	public bool IsCompleted => GetValue != "";
	public string GetValue => GetNode<LineEdit>("LineEdit").Text;

    public void SetValue(string value)
    {
        var lineedit = GetNode<LineEdit>("LineEdit");
        lineedit.Text = value;

        //Changing text property doesnt emit text_changed signal, so we do it ourselfs
        lineedit.EmitSignal("text_changed", value);
    }

	public void OnLineEditTextChanged(string text) => ComponentChanged?.Invoke();
}