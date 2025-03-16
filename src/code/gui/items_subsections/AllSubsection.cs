partial class AllSubsection : VBoxContainer
{
	LineEdit? foodLineEdit;
	LineEdit? invLineEdit;

	public override void _Ready() {
		//If we add metadata "foodLineEdit" or "invLineEdit" then we search for these search lineedits
		//We need that because subsection content must update when search lineedits are changed
		//Also form_item_set_component (the one that is used inside recipe creation menu) also uses all_subsection, so it has these search lineedits
		//But they are hidden, basically they dont have a function and they dont serve a purpose
		//Thats why there are no metadatas "foodLineEdit" or "invLineEdit", it doesnt search for them

		if(this.HasMeta("foodLineEdit")) 
		{
			foodLineEdit = (GetNode("ScrollContainer/Content/FoodTab") as Fold)
				.MainContainer
				.GetNode<LineEdit>("MarginContainer/FoodInspector/ControlPanel/LineEdit");
		}

		if(this.HasMeta("invLineEdit")) {
			invLineEdit = (GetNode("ScrollContainer/Content/InvTab") as Fold)
				.MainContainer
				.GetNode<LineEdit>("MarginContainer/InvInspector/ControlPanel/LineEdit");
		}

		var lineedit = GetNode<LineEdit>("ControlPanel/LineEdit");
		lineedit.TextChanged += OnSearchTextChanged;
	}

	void OnSearchTextChanged(string text) {
		if(foodLineEdit != null)
			foodLineEdit.EmitSignal("text_changed", text);

		if(invLineEdit != null)
			invLineEdit.EmitSignal("text_changed", text);
	}
}
