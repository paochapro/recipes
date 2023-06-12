using Godot;
using System;

public partial class FormTextEditChanged : VBoxContainer, FormComponent<string>
{
    public string GetValue => GetNode<TextEdit>("TextEdit").Text;
    public bool IsCompleted => GetValue != "";
    public event Action? ComponentChanged;

    public void OnTextChanged() => ComponentChanged?.Invoke();

    public void SetValue(string value) {
        var textEdit = GetNode<TextEdit>("TextEdit");
        textEdit.Text = value; //TODO: does he call event ItemSelected?
    }
}