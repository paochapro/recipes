partial class InvSubsectionContent : ItemsInspectorContent<InventoryItem>
{
    // [Export] PackedScene? invButton;

    // protected override Control GetControlForItem(InventoryItem item)
    // {
    //     if(invButton == null)
    //         throw new Exception("No inv button packed scene [InvSubsectionContent.cs]");

    //     var button = invButton.Instantiate<InvButton>();
    //     var program = GetNode<Program>("/root/Program");
    //     button.Initialize(item.Name, () => program.RemoveInvItem(item));
    //     return button;
    // }
}