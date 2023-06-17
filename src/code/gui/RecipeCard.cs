partial class RecipeCard : PanelContainer
{
	#nullable disable
	[Export] Label title;
	[Export] Label time;
	[Export] Label dishType;
	[Export] Label instructions;
	[Export] TextureRect image;
	[Export] RecipeFoodInspector foodInspector;
	[Export] RecipeInvInspector invInspector;
    [Export] PackedScene recipeFoodScene;
    [Export] PackedScene recipeInvScene;

    GlobalEvents events;
    Recipe recipe;
    ModifyPopup modifyPopup;
	#nullable restore

    public Recipe Recipe => recipe;

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
        modifyPopup = GetNode<ModifyPopup>("ModifyPopup");
    }

    public void OpenModifyPopup() => modifyPopup.Open(this.GetGlobalMousePosition());
}