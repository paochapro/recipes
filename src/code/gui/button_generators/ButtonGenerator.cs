class ButtonGenerator<TItem>
    where TItem : Item
{
    protected PackedScene buttonScene;
    protected Action<TItem> onButtonPressed;

    public ButtonGenerator(PackedScene buttonScene, Action<TItem> onButtonPressed) {
        this.buttonScene = buttonScene;
        this.onButtonPressed = onButtonPressed;
    }

    public virtual Control GetButton(TItem item)
    {
        var button = buttonScene.Instantiate<ItemButton<TItem>>();
        button.ButtonInitialize(item, onButtonPressed);
        return button;
    }
}

class AddItemButtonGenerator<TItem> : ButtonGenerator<TItem>
    where TItem : Item
{
    protected Func<TItem, bool> disabledCondition;

    public AddItemButtonGenerator(PackedScene buttonScene, Action<TItem> onButtonPressed, Func<TItem, bool> disabledCondition)
        : base(buttonScene, onButtonPressed)
    {
        this.disabledCondition = disabledCondition;
    }

    public override Control GetButton(TItem item)
    {
        var button = buttonScene.Instantiate<AddItemButton<TItem>>();
        button.ButtonInitialize(item, onButtonPressed);
        button.IsLocked = disabledCondition(item);
        return button;
    }
}

class RecipeButtonGenerator<TItem> : ButtonGenerator<TItem>
    where TItem : Item
{
    Func<TItem, LabelSettings> getSettings;

    public RecipeButtonGenerator(
        PackedScene buttonScene, 
        Action<TItem> onButtonPressed, 
        Func<TItem, LabelSettings> getSettings) 
        : base(buttonScene, onButtonPressed)
    {
        this.getSettings = getSettings;
    }

    public override Control GetButton(TItem item) {
        var button = buttonScene.Instantiate<ItemButton<TItem>>();
        button.ButtonInitialize(item, onButtonPressed);
        button.GetNode<Label>("HBoxContainer/Label").LabelSettings = getSettings(item);
        return button;
    }
}
