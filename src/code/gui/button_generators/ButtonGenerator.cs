class ButtonGenerator<TItem>
    where TItem : Item
{
    protected PackedScene buttonScene;
    protected Action<TItem> onButtonPressed;

    public ButtonGenerator(PackedScene buttonScene, Action<TItem> onButtonPressed) {
        this.buttonScene = buttonScene;
        this.onButtonPressed = onButtonPressed;
    }

    public Control GetButton(TItem item)
    {
        var button = buttonScene.Instantiate<ItemButton<TItem>>();
        button.ButtonInitialize(item, onButtonPressed);
        return button;
    }
}