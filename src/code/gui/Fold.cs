public partial class Fold : VBoxContainer
{
    static Texture2D arrowIconCollapsed = ThemeDB.GetDefaultTheme().GetIcon("arrow_collapsed", "Tree");
    static Texture2D arrowIconExpanded = ThemeDB.GetDefaultTheme().GetIcon("arrow", "Tree");

    #nullable disable
    Container mainContainer;
    TextureRect arrowIcon;
    Label titleLabel;
    #nullable restore

    public string Title {
        get => titleLabel.Text;
        set {
            titleLabel.Text = value;
        }
    }

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
        mainContainer = new VBoxContainer();
        arrowIcon = new TextureRect() { ExpandMode = TextureRect.ExpandModeEnum.FitWidth };
        Expanded = false;
        Title = "";
    }

    public override void _Ready()
    {
        var children = GetChildren();

        foreach(var child in children)
        {
            RemoveChild(child);
            mainContainer.AddChild(child);
        }

        HBoxContainer tab = new();
        tab.AddChild(arrowIcon);
        tab.AddChild(titleLabel);
        AddChild(tab);
        AddChild(mainContainer);
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
}