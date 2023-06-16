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

	public void Initialize(Recipe recipe) {
        this.recipe = recipe;
		title.Text = recipe.Title;
		time.Text = recipe.Minutes.ToString();
		dishType.Text = Enum.GetName<DishType>(recipe.DishType);
		instructions.Text = recipe.Instructions;
		image.Texture = GD.Load<Texture2D>(recipe.ImageTextureUID);

        foodInspector.Initialize(recipe.ItemSet.Food);
        invInspector.Initialize(recipe.ItemSet.Inventory);

        modifyPopup.SetModificationObject(recipe);
	}

    public override void _Ready() {
        modifyPopup = GetNode<ModifyPopup>("ModifyPopup");
        events = GetNode<GlobalEvents>("/root/GlobalEvents");
        events.RecipeModified += RecipeModified;
    }

    public void OpenModifyPopup() => modifyPopup.Open(this.GetGlobalMousePosition());

    public override void _Notification(int what) {
        if(what == NotificationPredelete)
            events.RecipeModified -= RecipeModified;
    }

    void RecipeModified(Recipe modified) {
        if(recipe == new Recipe())
            return;

        if(recipe.Title == modified.Title)
            Initialize(modified);
    }
}