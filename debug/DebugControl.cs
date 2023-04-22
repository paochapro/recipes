partial class DebugControl : Control
{
    bool showCategories = false;
    bool moreRecipeInfo = true;

    const string sectionContentPath = "VBoxContainer/PanelContainer/ScrollContainer/Content";
    readonly PackedScene recipePackedScene = GD.Load<PackedScene>("uid://cnqfeh5fpq4ua");
    readonly Color noMatchColor = Color.Color8(255,255,255);
    readonly Color foundMatchColor = Color.Color8(70,70,255);

    #nullable disable
    Debug root;
    Container itemSetSection;
    Container recipeSection;
    Container compareSection;
    Container filterSection;
    #nullable restore

    public override void _Ready() => GetNodes();

    void GetNodes()
    {
        root = GetParent<Debug>();

        var sectionRoot = GetNode("HBoxContainer");

        itemSetSection = sectionRoot.GetNode<Container>("ItemSetSection");
        recipeSection = sectionRoot.GetNode<Container>("RecipeSection");
        compareSection = sectionRoot.GetNode<Container>("SearchSection");
        filterSection = compareSection.GetNode<Container>("Filters");
        
        AddDishTypesToFilterOptionButton();
    }

    void AddDishTypesToFilterOptionButton()
    {
        var optionButton = filterSection.GetNode<OptionButton>("Fold/Content/DishType/OptionButton");

        foreach(string name in Enum.GetNames(typeof(DishType)))
            optionButton.AddItem(name);
    }

    Label CreateLabel(string text) => CreateLabelColored(text, new Color(255,255,255));
    
    Label CreateLabelColored(string text, Color color)
    {    
        Label label = new();
        label.Text = text;
        label.LabelSettings = new() {
            FontColor = color
        };
        return label;
    }

    Control CreateRecipeScene(Recipe recipe, IEnumerable<Control> contentNodes1, IEnumerable<Control> contentNodes2)
    {
        var recipeScene = recipePackedScene.Instantiate<Control>();

        var titleLabel = recipeScene.GetNode<Label>("HBoxContainer/Title");
        var infoLabel = recipeScene.GetNode<Label>("HBoxContainer/Info");
        var content1 = recipeScene.GetNode<Container>("MarginContainer/HBoxContainer/Content1");
        var content2 = recipeScene.GetNode<Container>("MarginContainer/HBoxContainer/Content2");

        string dishTypeName = Enum.GetNames(typeof(DishType)).ElementAt((int)recipe.DishType);

        titleLabel.Text = recipe.Title;
        infoLabel.Text = moreRecipeInfo ? $"[TIME:{recipe.Minutes}, DISH_TYPE:{dishTypeName}]" : "";

        foreach(var node in contentNodes1)
            content1.AddChild(node);

        foreach(var node in contentNodes2)
            content2.AddChild(node);

        return recipeScene;
    }

    IEnumerable<Label> GetCompareItemLabels<TItem>(IEnumerable<TItem> items, IEnumerable<TItem> compareColl) where TItem : Item 
    {
        foreach(TItem item in items)
        {
            bool match = compareColl.Contains(item);
            yield return CreateLabelColored(item.Name, match ? foundMatchColor : noMatchColor);
        }
    }

    IEnumerable<Label> GenerateLabelsFromStrings(IEnumerable<string> texts)
    {
        return texts.Select(t => CreateLabel(t));
    }

    IEnumerable<string> GenerateFoodText(IEnumerable<FoodWithCount> food)
    {
        foreach(var i in food)
        {
            string count = (i.Count == 1) ? "" : $"[{i.Count}x]"; 
            string category = showCategories ? $"({i.Item.Category})" : "";
            yield return $"{i.Item.Name} {count} {category}";
        }
    }
    
    IEnumerable<string> GenerateInvText(IEnumerable<InventoryItem> inv)
    {
        foreach(var i in inv)
        {
            string category = showCategories ? $"({i.Category})" : "";
            yield return $"{i.Name} {category}";
        }
    }

    void ContentItemsUpdate(Container content, IEnumerable<string> strings)
    {
        content.RemoveChildren();
        content.AddChildren(GenerateLabelsFromStrings(strings));
    }

    public void FoodUpdate(IEnumerable<FoodWithCount> food)
    {
        var itemSetContent = itemSetSection.GetNode(sectionContentPath);
        var foodContent = itemSetContent.GetNode<Container>("FoodSubsection/MarginContainer/Content");
        ContentItemsUpdate(foodContent, GenerateFoodText(food));
    }

    public void InventoryUpdate(IEnumerable<InventoryItem> inv)
    {
        var itemSetContent = itemSetSection.GetNode(sectionContentPath);
        var invContent = itemSetContent.GetNode<Container>("InvSubsection/MarginContainer/Content");
        ContentItemsUpdate(invContent, GenerateInvText(inv));
    }

    public void RecipesUpdate(IEnumerable<Recipe> recipes)
    {
        var recipesContent = recipeSection.GetNode<Container>(sectionContentPath);

        recipesContent.RemoveChildren();

        foreach(Recipe recipe in recipes) {
            var nodes1 = GenerateLabelsFromStrings(recipe.ItemSet.FoodNames);
            var nodes2 = GenerateLabelsFromStrings(recipe.ItemSet.InventoryNames);
            recipesContent.AddChild(CreateRecipeScene(recipe, nodes1, nodes2));
        }
    }

    public void CompareUpdate(ReadonlyItemSet itemSet, IEnumerable<Recipe> recipes) 
    {
        var compareContent = compareSection.GetNode<Container>("Search/" + sectionContentPath);

        compareContent.RemoveChildren();

        foreach(Recipe recipe in recipes) {
            var nodes1 = GetCompareItemLabels(recipe.ItemSet.FoodItems, itemSet.FoodItems);
            var nodes2 = GetCompareItemLabels(recipe.ItemSet.Inventory, itemSet.Inventory);
            compareContent.AddChild(CreateRecipeScene(recipe, nodes1, nodes2));
        }
    }

    public void FilterFoodUpdate(IEnumerable<FoodWithCount> food)
    {
        var itemSet = filterSection.GetNode("Fold/Content/ItemSet");
        var foodLabel = itemSet.GetNode<Label>("Food/Items");

        foodLabel.Text = string.Join(',', food.Select(f => f.Item.Name));
    }

    public void FilterInventoryUpdate(IEnumerable<InventoryItem> inv) 
    {
        var itemSet = filterSection.GetNode("Fold/Content/ItemSet");
        var invLabel = itemSet.GetNode<Label>("Inventory/Items");

        invLabel.Text = string.Join(',', inv.Select(i => i.Name));
    }

    public void OnAddRecipeButtonPress()
    {
        var form = recipeSection.GetNode("VBoxContainer/AddRecipeForm");
        var prompts = form.GetNode("VBoxContainer");
        var titleLineEdit = prompts.GetNode<LineEdit>("TitlePrompt/LineEdit");
        var foodLineEdit = prompts.GetNode<LineEdit>("FoodPrompt/LineEdit");
        var invLineEdit = prompts.GetNode<LineEdit>("InvPrompt/LineEdit");

        root.AddRecipe(titleLineEdit.Text, foodLineEdit.Text, invLineEdit.Text);
    }

    public void AddLocalItemButton()
    {
        var form = itemSetSection.GetNode<Container>("VBoxContainer/AddItemForm");
        var foodFunc = (string text) => root.AddLocalFoodItems(text);
        var invFunc = (string text) => root.AddLocalInvItems(text);
        FormAddItem(form, foodFunc, invFunc);
    }

    public void AddFilterItemButton()
    {
        var form = filterSection.GetNode<Container>("Fold/Content/ItemSet/AddItemForm");
        var foodFunc = (string text) => root.AddFilterFoodItems(text);
        var invFunc = (string text) => root.AddFilterInvItems(text);
        FormAddItem(form, foodFunc, invFunc);
    }

    public void FormAddItem(Container form, Action<string> foodFunc, Action<string> invFunc)
    {
        var lineEdit = form.GetNode<LineEdit>("VBoxContainer/LineEdit");
        var addFood = form.GetNode<CheckBox>("VBoxContainer/HBoxContainer/AddFood");
        var addInv = form.GetNode<CheckBox>("VBoxContainer/HBoxContainer/AddInventory");

        if(addFood.ButtonPressed)
            foodFunc.Invoke(lineEdit.Text);

        if(addInv.ButtonPressed)
            invFunc.Invoke(lineEdit.Text);
    }

    public void OnSearchButtonPress()
    {
        var filters = GetNode("HBoxContainer/SearchSection/Filters");
        var dishType = filters.GetNode<OptionButton>("Fold/Content/DishType/OptionButton");
        var time = filters.GetNode<SpinBox>("Fold/Content/Time/SpinBox");

        root.Compare(time.Value, dishType.Selected);
    }

    public void OnSearchClearButtonPress()
    {
        CompareUpdate(new ItemSet(), new Recipe[0]);
    }
}
