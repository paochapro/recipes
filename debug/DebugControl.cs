using Godot;
using System;

partial class DebugControl : Control
{
    bool showCategories = false;

    const string sectionContentPath = "VBoxContainer/PanelContainer/ScrollContainer/Content";
    readonly PackedScene recipePackedScene = GD.Load<PackedScene>("uid://cnqfeh5fpq4ua");
    readonly Color noMatchColor = Color.Color8(255,255,255);
    readonly Color foundMatchColor = Color.Color8(70,70,255);

    #nullable disable
    Debug root;
    Container itemSetSection;
    Container recipeSection;
    Container compareSection;
    #nullable restore

    public override void _Ready() => GetNodes();

    void GetNodes()
    {
        root = GetParent<Debug>();

        var sectionRoot = GetNode("HBoxContainer");

        itemSetSection = sectionRoot.GetNode<Container>("ItemSetSection");
        recipeSection = sectionRoot.GetNode<Container>("RecipeSection");
        compareSection = sectionRoot.GetNode<Container>("CompareSection");
    }

    void AddLabelChild(Node parent, string text) => AddItemLabelColored(parent, text, new Color(255,255,255));
    
    void AddItemLabelColored(Node parent, string text, Color color)
    {    
        Label label = new();
        label.Text = text;
        label.LabelSettings = new() {
            FontColor = color
        };

        parent.AddChild(label);
    }

    void AddRecipeScene(Node parent, Recipe recipe)
    {
        var recipeScene = recipePackedScene.Instantiate();

        var titleLabel = recipeScene.GetNode<Label>("Title");
        var content1 = recipeScene.GetNode<Container>("MarginContainer/HBoxContainer/Content1");
        var content2 = recipeScene.GetNode<Container>("MarginContainer/HBoxContainer/Content2");

        titleLabel.Text = recipe.Title;

        //problem
        ContentItemsUpdate(content1, GenerateFoodText(recipe.SearchData.ItemSet.Food));
        ContentItemsUpdate(content2, GenerateInvText(recipe.SearchData.ItemSet.Inventory));

        parent.AddChild(recipeScene);
    }

    void AddCompareItemLabels<TItem>(IEnumerable<TItem> items, IEnumerable<TItem> compareColl, Container content) where TItem : Item 
    {
        foreach(TItem item in items)
        {
            bool match = compareColl.Contains(item);
            AddItemLabelColored(content, item.Name, match ? foundMatchColor : noMatchColor);
        }
    }

    void ContentItemsUpdate(Container container, IEnumerable<string> texts)
    {
        container.RemoveChildren();

        foreach(var text in texts)
            AddLabelChild(container, text);
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

        foreach(Recipe recipe in recipes)
            AddRecipeScene(recipesContent, recipe);
    }

    public void CompareUpdate(ReadonlyItemSet itemSet, IEnumerable<Recipe> recipes) 
    {
        var compareContent = compareSection.GetNode<Container>(sectionContentPath);

        compareContent.RemoveChildren();

        foreach(Recipe recipe in recipes)
        {
            var recipeScene = recipePackedScene.Instantiate();

            var titleLabel = recipeScene.GetNode<Label>("Title");
            var content1 = recipeScene.GetNode<Container>("MarginContainer/HBoxContainer/Content1");
            var content2 = recipeScene.GetNode<Container>("MarginContainer/HBoxContainer/Content2");

            titleLabel.Text = recipe.Title;

            AddCompareItemLabels(recipe.SearchData.ItemSet.FoodItems, itemSet.FoodItems, content1);
            AddCompareItemLabels(recipe.SearchData.ItemSet.Inventory, itemSet.Inventory, content2);

            compareContent.AddChild(recipeScene);
        }
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

    public void OnAddItemButtonPress()
    {
        var form = itemSetSection.GetNode("VBoxContainer/AddItemForm");
        var lineEdit = form.GetNode<LineEdit>("VBoxContainer/LineEdit");
        var addFood = form.GetNode<CheckBox>("VBoxContainer/HBoxContainer/AddFood");
        var addInv = form.GetNode<CheckBox>("VBoxContainer/HBoxContainer/AddInventory");

        if(addFood.ButtonPressed)
            root.AddFoodItem(lineEdit.Text);

        if(addInv.ButtonPressed)
            root.AddInventoryItem(lineEdit.Text);
    }
}