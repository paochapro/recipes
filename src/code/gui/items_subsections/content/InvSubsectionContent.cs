partial class InvSubsectionContent : ItemsSubsectionContent<InventoryItem>
{
    static PackedScene invButton = GD.Load<PackedScene>("res://src/tscn/item_buttons/inv_button.tscn");

    protected override Control GetControlForItem(InventoryItem item)
    {
        var button = invButton.Instantiate<InvButton>();
        var program = GetNode<Program>("/root/Program");
        button.Initialize(item.Name, () => program.RemoveInvItem(item));
        return button;
    }
}