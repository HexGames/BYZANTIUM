using Godot;
using Godot.Collections;
using System.Linq;

[Tool]
public partial class AssetLibrary : Node
{
    [Export]
    private Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
    [Export]
    private Dictionary<string, Material> Materials = new Dictionary<string, Material>();
    [Export]
    private Dictionary<string, Material> MaterialsPlanets = new Dictionary<string, Material>();
    [Export]
    private Dictionary<string, Material> MaterialsPlanetsAlpha = new Dictionary<string, Material>();

    [Export]
    private Material FOG_1;
    [Export]
    private Material StarHover;
    [Export]
    private Material StarSelected;

    [ExportCategory("Loader")]
    [Export]
    public bool LoadAllAssets
    {
        get => false;
        set
        {
            if (value)
            {
                LoadAll();
            }
        }
    }
    //public override void _Ready()
    //{
    //}

    private void LoadAll()
    {
        Load2DTextures("res://Assets/Flags/");
        LoadMaterials(Materials, "res://Assets//3D/Materials/");
        LoadMaterials(MaterialsPlanets, "res://Assets//3D/MaterialsPlanets/");
        LoadMaterials(MaterialsPlanetsAlpha, "res://Assets//3D/MaterialsPlanetsAlpha/");
    }

    private void Load2DTextures(string dirPath)
    {
        Textures.Clear();
        string[] filePaths = DirAccess.GetFilesAt(dirPath);
        for (int idx = 0; idx < filePaths.Length; idx++)
        {
            if (filePaths[idx].EndsWith("png"))
            {
                Textures.Add(filePaths[idx], GD.Load<Texture2D>(dirPath + filePaths[idx]));
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

    private void LoadMaterials(Dictionary<string, Material> materials, string dirPath)
    {
        materials.Clear();
        string[] filePaths = DirAccess.GetFilesAt(dirPath);
        for (int idx = 0; idx < filePaths.Length; idx++)
        {
            if (filePaths[idx].EndsWith("tres"))
            {
                materials.Add(filePaths[idx], GD.Load<Material>(dirPath + filePaths[idx]));
            }
        }
    }

    public Material GetMaterial(string path)
    {
        if (Materials.ContainsKey(path) == false)
        {
            GD.Print("Unable to find material " + path);
            return null;
        }
        return Materials[path];
    }

    public Material GetMaterialPlanet(string path)
    {
        if (MaterialsPlanets.ContainsKey(path) == false)
        {
            //GD.Print("Unable to find material " + path);
            return null;
        }
        return MaterialsPlanets[path];
    }

    //public void LODPlanetsShow()
    //{
    //    foreach (var item in MaterialsPlanets)
    //    {
    //        Material mat = item.Value;
    //        if (mat is StandardMaterial3D stdMat)
    //        {
    //            stdMat.AlbedoColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    //        }
    //    }
    //}
    //
    //public void LODPlanetsHide()
    //{
    //    foreach (var item in MaterialsPlanets)
    //    {
    //        Material mat = item.Value;
    //        if (mat is StandardMaterial3D stdMat)
    //        {
    //            stdMat.AlbedoColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    //        }
    //    }
    //}

    public void LODPlanetsAlpha(float alpha)
    {
        foreach (var item in MaterialsPlanets)
        {
            Material mat = item.Value;
            if (mat is StandardMaterial3D stdMat)
            {
                stdMat.AlbedoColor = new Color(1.0f, 1.0f, 1.0f, alpha);
                if (alpha >= 0.975f)
                    stdMat.Transparency = BaseMaterial3D.TransparencyEnum.Disabled;
                else
                    stdMat.Transparency = BaseMaterial3D.TransparencyEnum.Alpha;
            }
        }

        foreach (var item in MaterialsPlanetsAlpha)
        {
            Material mat = item.Value;
            if (mat is StandardMaterial3D stdMat)
            {
                stdMat.AlbedoColor = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
        }

        // fog
        {
            if (FOG_1 is StandardMaterial3D stdMat)
            {
                stdMat.AlbedoColor = new Color(1.0f, 0.7f, 0.75f, 0.25f * (1.0f - alpha));
            }
        }
        // star hover
        {
            if (StarHover is StandardMaterial3D stdMat)
            {
                stdMat.AlbedoColor = new Color(1.0f, 1.0f, 1.0f, 1.0f - alpha);
            }
        }
        // star selected
        {
            if (StarSelected is StandardMaterial3D stdMat)
            {
                stdMat.AlbedoColor = new Color(1.0f, 1.0f, 1.0f, 1.0f - alpha);
            }
        }
    }
}
