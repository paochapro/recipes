partial class RecipeInvButton : HBoxContainer
{
    public void Initialize(InventoryItem item) {
        GetNode<Label>("PanelContainer/MarginContainer/Label").Text = $"{item.Name} ({item.Category})";
    }
}
