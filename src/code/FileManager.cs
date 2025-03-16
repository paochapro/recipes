using System.IO;
using Newtonsoft.Json;

partial class FileManager : Node
{
    #nullable disable
    PackedScene openFileDialogScene;
    PackedScene saveFileDialogScene;
    AcceptDialog errorDialog;
    #nullable restore

    public override void _Ready(){
        openFileDialogScene = GD.Load<PackedScene>("res://src/tscn/open_table_file_dialog.tscn");
        saveFileDialogScene = GD.Load<PackedScene>("res://src/tscn/save_table_file_dialog.tscn");
        errorDialog = GetNode<AcceptDialog>("ErrorDialog");
    }

    void FileDialogInitialize(PackedScene fileDialogScene, Godot.FileDialog.FileSelectedEventHandler fileSelected) 
    {
        if(fileDialogScene == null)
            throw new Exception("No fileDialog scene");

        var fileDialog = fileDialogScene.Instantiate<FileDialog>();
        AddChild(fileDialog);
        fileDialog.FileSelected += fileSelected;
        fileDialog.PopupCentered();
    }

    public void SaveItemPressed() {
        FileDialogInitialize(saveFileDialogScene, SaveFile);
    }   

    public void LoadItemPressed() {
        FileDialogInitialize(openFileDialogScene, LoadFile);
    }

    public void SaveFile(string path) 
    {
        var program = GetNode<Program>("/root/Program");

        FileLocalFood[] localFoodArray = program.LocalItems.Food
            .Select(f => new FileLocalFood(f.Item, f.Count))
            .ToArray();

        FileTable fileTable = new(
            program.RecipeBank.ToArray(), 
            program.ItemsBank.Food.ToArray(),
            program.ItemsBank.Inventory.ToArray(),
            localFoodArray,
            program.LocalItems.Inventory.ToArray()
        );

        try {
            string json = TableToJson(fileTable);

            using(FileStream fs = new(path, FileMode.OpenOrCreate))
            using(StreamWriter sw = new(fs)) {
                sw.Write(json);
            }
        }
        catch(Exception ex) {
            GD.PrintErr($"Unable to save file \"{path}\". Exception in Save::FileManager.cs: " + ex.Message);
            ShowErrorWindow($"Не удалось сохранить файл \"{path}\".");
        }
    }

    public void LoadFile(string path) 
    {
        var program = GetNode<Program>("/root/Program");
        FileTable? table = Load(path);

        if(table != null)
            program.LoadFileTable((FileTable)table);
    }

    FileTable? Load(string path) 
    {
        if(!File.Exists(path)) {
            GD.PrintErr($"No file \"{path}\" was found");
            return null;
        }

        FileTable? fileTable = null;

        try {
            string json = "";

            using(StreamReader sr = new(path))
                json = sr.ReadToEnd();

            fileTable = JsonToTable(json);
        }
        catch (Exception ex) {
            GD.PrintErr($"Unable to load file \"{path}\". Exception in Load::FileManager.cs: " + ex.Message);
            ShowErrorWindow($"Не удалось загрузить файл \"{path}\".");
        }

        return fileTable;
    }

    string TableToJson(FileTable fileTable) {
        return JsonConvert.SerializeObject(fileTable);
    }

    FileTable JsonToTable(string json) {
        return JsonConvert.DeserializeObject<FileTable>(json);
    }

    void ShowErrorWindow(string message) {
        errorDialog.DialogText = message;
        errorDialog.PopupCentered();
    }
}