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

		ConnectEvents();
	}

    void ConnectEvents() {
        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        events.FileLoaded += () => this.CallDeferred(RecipesSection.MethodName.FileUpdateContent);

        events.NewRecipe += (r) => UpdateRecipe(r);
        events.RemoveRecipe += (r) => RemoveRecipe(r);
        events.RecipeModified += (r) => ModifyRecipe(r);

        events.NewLocalFood += (i) => Reorder();
        events.RemoveLocalFood += (i) => Reorder();
        events.RemoveBankFood += (i) => Reorder();
        events.RemoveBankInv += (i) => Reorder();

        events.NewLocalInv += (i) => SearchRecipes();
        events.RemoveLocalInv += (i) => SearchRecipes();
    }

    void UpdateRecipe(Recipe recipe) {
        var info = GetSearchInfo();
        if(RecipeSearch.DoesRecipePass(recipe, info))
            content.UpdateRecipe(recipe, info.LocalItemSet);
    }

    void ModifyRecipe(Recipe modified) {
        var info = GetSearchInfo();
        if(RecipeSearch.DoesRecipePass(modified, info))
            content.ModifyRecipe(modified, info.LocalItemSet);
    }

    void RemoveRecipe(Recipe recipe) => content.RemoveRecipe(recipe);

	void SearchRecipes() {
		var program = GetNode<Program>("/root/Program");
        var info = GetSearchInfo();
        var foundRecipes = RecipeSearch.Search(program.RecipeBank, info);
        content.SearchUpdate(foundRecipes, info.LocalItemSet);
	}

    void FileUpdateContent()  {
		var program = GetNode<Program>("/root/Program");
        content.UpdateContent(program.RecipeBank, GetSearchInfo().LocalItemSet); 
    }

    void Reorder() {
        content.Reorder(GetSearchInfo().LocalItemSet);
    }

    SearchInfo GetSearchInfo()
    {
		string title = titleTb.Text;
        SearchInfo filtersSearchInfo = filters.GetSearchInfo();
        SearchInfo searchInfo = filtersSearchInfo with { Title = title };
        return searchInfo;
    }
}