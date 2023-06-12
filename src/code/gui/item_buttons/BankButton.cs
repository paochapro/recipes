partial class BankButton<TItem> : ItemButton<TItem>
    where TItem : Item
{
    #nullable disable
    ModifyPopup modifyPopup;
    #nullable restore

    protected override void Initialize() {
        modifyPopup = GetNode<ModifyPopup>("ModifyPopup");
        modifyPopup.SetModificationItem(base.Item);
        this.GuiInput += ButtonGuiInput;
    }

    void ButtonGuiInput(InputEvent @ev)
    {
        if(@ev is InputEventMouseButton mouseEv)
            if(mouseEv.ButtonIndex == MouseButton.Right && mouseEv.Pressed)
                OpenModifyPopup(this.GetGlobalMousePosition());
    }

    void OpenModifyPopup(Vector2 pos) {
        modifyPopup.Position = (Vector2I)pos;
        modifyPopup.Popup();
    }
}