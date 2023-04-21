public partial class Fold : VBoxContainer
{
    static Texture2D arrowIconCollapsed = ThemeDB.GetDefaultTheme().GetIcon("arrow_collapsed", "Tree");
    static Texture2D arrowIconExpanded = ThemeDB.GetDefaultTheme().GetIcon("arrow", "Tree");

    #nullable disable
    Container mainContainer;
    TextureRect arrowIcon;
    Label titleLabel;
    #nullable restore

    string title {
        get => titleLabel.Text;
        set {
            titleLabel.Text = value;
        }
    }

    bool expanded {
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

    [Export] bool ExportExpanded = false;
    [Export] string ExportTitle = "";

    public override void _Ready()
    {
        mainContainer = GetMainContainer();
        arrowIcon = new TextureRect() { ExpandMode = TextureRect.ExpandModeEnum.FitWidth };
        titleLabel = new Label();

        HBoxContainer tab = new();
        tab.AddChild(arrowIcon);
        tab.AddChild(titleLabel);
        AddChild(tab);
        MoveChild(tab, 0);
        tab.GuiInput += TabGuiInput;

        expanded = ExportExpanded;
        title = ExportTitle;
    }

    public void TabGuiInput(InputEvent @event)
    {
        if( @event is InputEventMouseButton ev && 
            ev.ButtonIndex == MouseButton.Left &&
            ev.Pressed)
        {
            expanded = !expanded;
        }
    }

    Container GetMainContainer()
    {
        Container result = new PanelContainer() { Name = "Default" };
        var children = GetChildren();

        if(children.Count > 0)
        {
            var first = children[0];
            if(first is Container container)
                result = container;
        }

        return result;
    }
}