partial class RecipeCardBanner : PanelContainer
{
    #nullable disable
    RecipeCard card;
    #nullable restore

    public override void _Ready() {
        this.GuiInput += BannerGuiInput;
        card = GetParent().GetParent<RecipeCard>();
    }

    void BannerGuiInput(InputEvent @ev) {
        if(@ev is InputEventMouseButton mouseEv)
            if(mouseEv.ButtonIndex == MouseButton.Right && mouseEv.Pressed)
                card.OpenModifyPopup();
    }
}