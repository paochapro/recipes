using System.IO;

static class ImageLoader
{
    static readonly string MISSING_TEXTURE_PATH = "res://content/missing.png";

    public static Texture2D GetImage(string path)
    {
        Texture2D texture;

        try  {
            texture = GetTextureFromPath(path);
        }
        catch(Exception ex) {
            GD.PrintErr($"Failed to load image {path}. Exception: {ex.Message}");
            texture = GD.Load<Texture2D>(MISSING_TEXTURE_PATH);
        }

        return texture;
    }

    static Texture2D GetTextureFromPath(string path) 
    {
        if(path.StartsWith("res")) {
            return GD.Load<Texture2D>(path);
        }

        var image = Image.LoadFromFile(path);
        return ImageTexture.CreateFromImage(image);
    }
}