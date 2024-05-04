using Godot;
using Godot.Collections;

public partial class AssetLibrary : Node
{
    [Export]
    private Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

    public override void _Ready()
	{
        Load2DTextures("res://Assets/Flags/");
    }

    private void Load2DTextures(string dirPath)
    {
        string[] filePaths = DirAccess.GetFilesAt(dirPath);
        for (int idx = 0; idx < filePaths.Length; idx++)
        {
            if (filePaths[idx].EndsWith("png"))
            {
                Textures.Add(dirPath + filePaths[idx], GD.Load<Texture2D>(dirPath + filePaths[idx]));
            }
        } 
    }

    public Texture2D GetTexture2D(string path)
    {
        if (Textures.ContainsKey(path) == false)
        {
            GD.PrintErr("Unable to find texture2D " + path);
            return null;
        }
        return Textures[path];
    }
}
