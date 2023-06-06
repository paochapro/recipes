public partial class FormSpinBoxComponent : HBoxContainer, FormComponent<double>
{
    public double GetValue => GetNode<SpinBox>("SpinBox").Value;
    public bool IsCompleted => GetValue != 0;
    public event Action? ComponentChanged;

    public void OnValueChanged(double value) => ComponentChanged?.Invoke();
}