partial class RecipeCard : PanelContainer
{
	#nullable disable
	Label title;
	Label time;
	Label dishType;
	Label instructions;
	TextureRect image;
	RecipeFoodInspector foodInspector;
	RecipeInvInspector invInspector;
    PackedScene recipeFoodScene;
    PackedScene recipeInvScene;

    GlobalEvents events;
    Recipe recipe;
    ModifyPopup modifyPopup;
    Fold foodFold;
    Fold invFold;
	#nullable restore

    public Recipe Recipe => recipe;

    const string foodFoldTitle = "Ингредиенты";
    const string invFoldTitle = "Инвентарь";

	public void Initialize(Recipe recipe, LabelSettings titleSettings) {
        this.recipe = recipe;

		title.Text = recipe.Title;
        title.LabelSettings = titleSettings;

		time.Text = recipe.Minutes.ToString();
		dishType.Text = Enum.GetName<DishType>(recipe.DishType);
		instructions.Text = recipe.Instructions;
		image.Texture = ImageLoader.GetImage(recipe.ImageTextureUID);

        foodInspector.Initialize(recipe.ItemSet.Food, new ItemSet());
        invInspector.Initialize(recipe.ItemSet.Inventory);

        modifyPopup.SetModificationObject(recipe);
	}

    public void InitializeFoodInspector(IEnumerable<FoodWithCount> food, ReadonlyItemSet set) {
        foodInspector.Initialize(food, set);
    }

    public override void _Ready() {
        foodFold = GetNode<Fold>("Content/FoodFold");
        invFold = GetNode<Fold>("Content/InvFold");
        foodFold.Title = foodFoldTitle;
        invFold.Title = invFoldTitle;

        title = GetNode<Label>("Content/PanelContainer/Banner/Title");
        time = GetNode<Label>("Content/Time/Value");
        dishType = GetNode<Label>("Content/DishType/Type");
        instructions = GetNode<Label>("Content/InstructionsPanel/Label");
        image = GetNode<TextureRect>("Content/PanelContainer/Banner/TextureRect");
        foodInspector = GetNode<RecipeFoodInspector>("Content/FoodFold/_FoldMainContainer/FoodInspector");
        invInspector = GetNode<RecipeInvInspector>("Content/InvFold/_FoldMainContainer/InvInspector");
        recipeFoodScene = GD.Load<PackedScene>("res://src/tscn/item_buttons/recipe_food_button.tscn");
        recipeInvScene = GD.Load<PackedScene>("res://src/tscn/item_buttons/recipe_inv_button.tscn");


        modifyPopup = GetNode<ModifyPopup>("ModifyPopup");
    }

    public void OpenModifyPopup() => modifyPopup.Open(this.GetGlobalMousePosition());
}