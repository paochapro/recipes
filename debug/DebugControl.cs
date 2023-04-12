using Godot;
using System;

partial class DebugControl : Control
{
	const string sectionContentPath = "VBoxContainer/PanelContainer/ScrollContainer/Content";
	readonly PackedScene recipePackedScene = GD.Load<PackedScene>("uid://cnqfeh5fpq4ua");
	readonly Color noMatchColor = Color.Color8(255,255,255);
	readonly Color foundMatchColor = Color.Color8(70,70,255);

	#nullable disable
	Container itemSetSection;
	Container recipeSection;
	Container compareSection;

	Debug root;
	#nullable restore

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
		UpdateItemSetLabels(content1, content2, recipe.SearchData.ItemSet);

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

	void UpdateItemSetLabels(Container foodContent, Container invContent, ReadonlyItemSet itemSet)
	{
		bool showCategory = false;

		foodContent.RemoveChildren();
		invContent.RemoveChildren();

		void AddLabels<TItem>(IEnumerable<TItem> items, Container content)
			where TItem : Item
		{
			foreach(TItem item in items)
			{
				string category = (showCategory ? $"({item.Category})" : "");
				AddLabelChild(content, item.Name + category);
			}
		}

		AddLabels(itemSet.Food, foodContent);
		AddLabels(itemSet.Inventory, invContent);
	}

	void RecipeSectionUpdate(Container container, IEnumerable<Recipe> recipes)
	{
		container.RemoveChildren();

		foreach(Recipe recipe in recipes)
			AddRecipeScene(container, recipe);
	}

	public override void _Ready() => GetNodes();

	public void LocalItemsUpdate(ReadonlyItemSet itemSet)  
	{
		var itemSetContent = itemSetSection.GetNode(sectionContentPath);
		var foodSubsection = itemSetContent.GetNode<Container>("FoodSubsection/MarginContainer/Content");
		var invSubsection = itemSetContent.GetNode<Container>("InvSubsection/MarginContainer/Content");

		UpdateItemSetLabels(foodSubsection, invSubsection, itemSet); 
	}

	public void RecipesUpdate(IEnumerable<Recipe> recipes) 
	{
		var recipesContent = recipeSection.GetNode<Container>(sectionContentPath);
		RecipeSectionUpdate(recipesContent, recipes);
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

			AddCompareItemLabels(recipe.SearchData.ItemSet.Food, itemSet.Food, content1);
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
			root.AddFoodItem(lineEdit.Text, "category_placeholder");

		if(addInv.ButtonPressed)
			root.AddInventoryItem(lineEdit.Text, "category_placeholder");
	}
}