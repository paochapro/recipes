partial class RecipeInvButton : ItemButton<InventoryItem>
{
    protected override void Initialize() {
        GetNode<Label>("PanelContainer/MarginContainer/Label").Text = $"{Item.Name} ({Item.Category})";
    }
}