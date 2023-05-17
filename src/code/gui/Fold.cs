public partial class Fold : VBoxContainer
{
    static Texture2D arrowIconCollapsed = ThemeDB.GetDefaultTheme().GetIcon("arrow_collapsed", "Tree");
    static Texture2D arrowIconExpanded = ThemeDB.GetDefaultTheme().GetIcon("arrow", "Tree");

    const string tabName = "_FoldTab";
    const string mainContainerName = "_FoldMainContainer";

    VBoxContainer mainContainer;
    TextureRect arrowIcon;
    Label titleLabel;

    public VBoxContainer MainContainer => mainContainer; //TODO: get child?

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
        mainContainer = new VBoxContainer() { Name = mainContainerName };
        Expanded = false;
        Title = "";
    }

    public override void _Ready() 
    {
        SetupMainContainer();

        HBoxContainer tab = new() { Name = tabName };
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
        foreach(var child in this.GetChildren())
        {
            this.RemoveChild(child);
            mainContainer.AddChild(child);
        }

        AddChild(mainContainer);
    }
}