public partial class Fold : VBoxContainer
{
    Texture2D arrowIconCollapsed = ThemeDB.GetDefaultTheme().GetIcon("arrow_collapsed", "Tree");
    Texture2D arrowIconExpanded = ThemeDB.GetDefaultTheme().GetIcon("arrow", "Tree");

    const string tabName = "_FoldTab";
    const string mainContainerName = "_FoldMainContainer";
    const byte hoverAlpha = 35;

    VBoxContainer mainContainer;
    TextureRect arrowIcon;
    Label titleLabel;

    public VBoxContainer MainContainer => mainContainer;

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

        PanelContainer tab = new() { Name = tabName };
        StylizeTab(tab);
        
        HBoxContainer tabHContainer = new();
        tabHContainer.AddChild(arrowIcon);
        tabHContainer.AddChild(titleLabel);
        tab.AddChild(tabHContainer);

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
        Node? node = GetChildren().FirstOrDefault(n => n.Name == mainContainerName);

        if(node != null && node is VBoxContainer container)
        {
            this.RemoveChild(container);
            container.Visible = Expanded;
            mainContainer = container;
        }

        foreach(var child in GetChildren())
        {
            this.RemoveChild(child);
            mainContainer.AddChild(child);
        }

        AddChild(mainContainer);
    }

    void StylizeTab(PanelContainer tab) {
        tab.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());

        tab.MouseEntered += () => {
            tab.AddThemeStyleboxOverride("panel", new StyleBoxFlat() { BgColor = Color.Color8(255,255,255, hoverAlpha)} );
        };

        tab.MouseExited += () => {
            tab.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        };
    }
}