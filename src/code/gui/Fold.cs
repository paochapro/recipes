public partial class Fold : VBoxContainer
{
    static Texture2D arrowIconCollapsed = ThemeDB.GetDefaultTheme().GetIcon("arrow_collapsed", "Tree");
    static Texture2D arrowIconExpanded = ThemeDB.GetDefaultTheme().GetIcon("arrow", "Tree");

    //#nullable disable
    Container mainContainer;
    TextureRect arrowIcon;
    Label titleLabel;
    //#nullable restore

    public Container GetMainContainer => mainContainer; //TODO: get child

    [Export]
    public string Title {
        get => titleLabel.Text;
        set {
            titleLabel.Text = value;
        }
    }

    [Export]
    public bool Expanded {
        get => mainContainer.Visible;
        set {
            if(value == true) {
                arrowIcon.Texture = arrowIconExpanded;
                mainContainer.Show();
            }
            else {
                arrowIcon.Texture = arrowIconCollapsed;
                mainContainer.Hide();
            }
        }
    }

    public Fold()
    {
        titleLabel = new Label();
        arrowIcon = new TextureRect() { ExpandMode = TextureRect.ExpandModeEnum.FitWidth };
        mainContainer = new VBoxContainer() { Name = "_FoldContainer" };
        Expanded = false;
        Title = "";
    }

    public override void _Ready() 
    {
        SetupMainContainer();

        HBoxContainer tab = new() { Name = "_FoldTab" };
        tab.AddChild(arrowIcon);
        tab.AddChild(titleLabel);
        AddChild(tab);
        MoveChild(tab, 0);
        tab.GuiInput += TabGuiInput;
    }

    public void TabGuiInput(InputEvent @event)
    {
        if( @event is InputEventMouseButton ev && 
            ev.ButtonIndex == MouseButton.Left &&
            ev.Pressed)
        {
            Expanded = !Expanded;
        }
    }

    void SetupMainContainer()
    {
        var children = GetChildren();
        var first = GetChild(0);

        if(children.Count() == 1 && first is Container userContainer)
        {
            userContainer.Visible = Expanded;
            mainContainer = userContainer;
            return;
        }
        
        foreach(var child in children)
        {
            RemoveChild(child);
            mainContainer.AddChild(child);
        }

        AddChild(mainContainer);
    }
}