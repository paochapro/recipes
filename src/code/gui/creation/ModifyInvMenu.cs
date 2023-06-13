partial class ModifyInvMenu : ModifyMenu
{
	public override void _Ready()
	{
        base._Ready();

        var events = GetNode<GlobalEvents>("/root/GlobalEvents");

        var nameComp = form.GetNode<FormLineEditComponent>("Name");
        var categoryComp = form.GetNode<FormCategoryComponent>("FormCategoryComponent");

        events.OpenInvModificationMenu += (InventoryItem invItem) => {
            nameComp.SetValue(invItem.Name);
            categoryComp.SetValue(invItem.Category);
        };
	}
}