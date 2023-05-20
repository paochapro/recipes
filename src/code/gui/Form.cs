partial class Form : Control
{
	public event Action? FormChanged;

	public bool IsFormCompleted {
		get {
			var components = GetChildren().OfType<FormComponent>();
			bool completed = components.All(c => c.IsCompleted);
			return completed;
		}
	}

	public override void _Ready()
	{
		var components = GetChildren().OfType<FormComponent>();

		foreach(Node component in components)
		     GD.Print($"{component.GetType()} ({component.GetNode<Label>("Label").Text})");

		components.Iterate(c => c.ComponentChanged += () => { FormChanged?.Invoke(); } );
	}
}