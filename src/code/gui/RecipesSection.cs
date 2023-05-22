public partial class RecipesSection : PanelContainer
{
	#nullable disable
	FiltersForm filters;
	RecipeContent content;
    LineEdit titleTb;
	#nullable restore

	public override void _Ready()
	{
		content = GetNode<RecipeContent>("Components/ContentScroll/Content");
        titleTb = GetNode<LineEdit>("Components/ControlPanel/LineEdit");

        var filtersFoldContainer = GetNode<Fold>("Components/FiltersRoot/Fold").MainContainer;
        filters = filtersFoldContainer.GetNode<FiltersForm>("FiltersForm");
		filters.FormChanged += SearchRecipes;
        titleTb.TextChanged += (msg) => SearchRecipes();

        SearchRecipes();
	}

	void SearchRecipes()
	{
		string title = titleTb.Text;
        SearchInfo filtersSearchInfo = filters.GetSearchInfo();
        SearchInfo searchInfo = filtersSearchInfo with { Title = title };

        GD.Print("Search info:"  + searchInfo);

		var program = GetNode<Program>("/root/Program");
        program.RecipeBank.Iterate(r => GD.Print($"Bank recipe: {r.Title}"));
		IEnumerable<Recipe> foundRecipes = RecipeSearch.Search(program.RecipeBank, searchInfo);
        program.RecipeBank.Iterate(r => GD.Print($"Found recipe: {r.Title}"));

		content.UpdateContent(foundRecipes.ToList());
	}

	public void OnMenuButtonPressed()
	{
		GetNode<GlobalEvents>("/root/GlobalEvents").CallSwitchDynamicWindow(DynamicWindowMenu.Recipe);
	}
}