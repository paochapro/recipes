public partial class RecipesSection : PanelContainer
{
	#nullable disable
	FiltersForm filters;
	RecipeContent content;
    LineEdit titleTb;
	#nullable restore

    const DynamicWindowMenu SWITCH_MENU = DynamicWindowMenu.RecipeCreation; 

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

		var program = GetNode<Program>("/root/Program");
		IEnumerable<Recipe> foundRecipes = RecipeSearch.Search(program.RecipeBank, searchInfo);

		content.UpdateContent(foundRecipes.ToList());
	}

	public void OnMenuButtonPressed()
	{
		GetNode<GlobalEvents>("/root/GlobalEvents").CallSwitchDynamicWindow(SWITCH_MENU);
	}
}