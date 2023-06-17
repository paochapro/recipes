partial class FormCheckBoxComponent : Control, FormComponent<bool>
{
    public bool GetValue => GetNode<CheckBox>("CheckBox").ButtonPressed;
    public bool IsCompleted => GetValue == true;
    public event Action? ComponentChanged;

    public void SetValue(bool value) {
        GetNode<CheckBox>("CheckBox").ButtonPressed = value;
        ComponentChanged?.Invoke();
    }

    public void OnCheckBoxPressed() => ComponentChanged?.Invoke();
}