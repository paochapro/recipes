partial class RecipesSection : PanelContainer
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

		var program = GetNode<Program>("/root/Program");
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.NewRecipe += (r) => UpdateRecipe(r);
        events.RemoveRecipe += (r) => RemoveRecipe(r);
        events.FileLoaded += () => this.CallDeferred(RecipesSection.MethodName.SearchRecipes);
	}

    void UpdateRecipe(Recipe recipe) {
        if(RecipeSearch.DoesRecipePass(recipe, GetSearchInfo()))
            content.UpdateRecipe(recipe);
    }

    void RemoveRecipe(Recipe recipe) {
        content.RemoveRecipe(recipe);
    }

	void SearchRecipes() {
		var program = GetNode<Program>("/root/Program");
		IEnumerable<Recipe> foundRecipes = RecipeSearch.Search(program.RecipeBank, GetSearchInfo());
		UpdateContent(foundRecipes.ToList());
	}

    void UpdateContent(IEnumerable<Recipe> recipes) => content.UpdateContent(recipes); 

    SearchInfo GetSearchInfo()
    {
		string title = titleTb.Text;
        SearchInfo filtersSearchInfo = filters.GetSearchInfo();
        SearchInfo searchInfo = filtersSearchInfo with { Title = title };
        return searchInfo;
    }
}