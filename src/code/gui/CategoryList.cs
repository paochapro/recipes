abstract partial class CategoryList : PanelContainer
{
	protected abstract IEnumerable<string> AvaliableCategories { get; }
	public event Action<string>? CategorySelected;

	PackedScene categoryButtonScene;

	public override void _Ready()
	{
		categoryButtonScene = GD.Load<PackedScene>("res://src/tscn/category_button.tscn");

		var events = GetNode<GlobalEvents>("/root/GlobalEvents");
		events.NewBankFood += (item) => UpdateContent();
		events.NewBankInv += (item) => UpdateContent();
		events.RemoveBankFood += (item) => UpdateContent();
		events.RemoveBankInv += (item) => UpdateContent();
        events.FoodModified += (item) => UpdateContent();
        events.InvModified += (item) => UpdateContent();
        events.FileLoaded += UpdateContent;
		
		UpdateContent();
	}

	public void UpdateContent()
	{
		var content = GetNode("ScrollContainer/Content");
        content.RemoveChildren();

		if(categoryButtonScene == null)
			throw new Exception("No category button scene");

		foreach(string categoryName in AvaliableCategories)
		{
			var button = categoryButtonScene.Instantiate<Button>();
			button.Text = categoryName;
			button.Pressed += () => CategorySelected?.Invoke(categoryName);
			content.AddChild(button);
		}
	}
}
