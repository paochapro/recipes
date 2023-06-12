partial class FileMenu : PopupMenu
{
    #nullable disable
    FileManager fileManager;
    #nullable enable

    public override void _Ready() {
        fileManager = GetNode<FileManager>("FileManager");
    }

    public void ItemPressed(int item) {
        if(item == 0)
            fileManager.SaveItemPressed();
        else if(item == 1)
            fileManager.LoadItemPressed();
    }

    public void FileMenuPopup() => this.Popup();
}