interface FormComponent
{
    event Action? ComponentChanged;
    bool IsCompleted { get; }
}

interface FormComponent<T> : FormComponent
{
    T GetValue { get; }
    void SetValue(T value);
}