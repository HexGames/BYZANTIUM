using Godot;
using System;

// Editor
[Tool]
public partial class DefGenerator : Node
{
    [Export]
    private Resource DefLibraryRes = null;
    private DefLibrary DefLibrary
    {
        get
        {
            return (DefLibrary)DefLibraryRes;
        }
    }

    [Export]
    public bool GenerateCityDef
    {
        get => false;
        set
        {
            if (value)
            {
                GenerateCityDefFunc();
            }
        }
    }

    public void ClearCityDef()
    {
        if (DefLibrary == null)
        {
            GD.PrintErr("HEX - No Def Library linked!");
            return;
        }

        for (int idx = 0; idx < DefLibrary.Cities.Count; idx++)
        {
            string dirString = DefLibrary.Cities[idx].FilePath.GetBaseDir();
            DirAccess dir = DirAccess.Open(dirString);
            string fileString = DefLibrary.Cities[idx].FilePath.GetFile();
            Error err = dir.Remove(fileString);
            if (err != Error.Ok)
            {
                GD.PrintErr("HEX - Error deleting file: " + err);
            }
        }

        DefLibrary.Cities.Clear();
        ResourceSaver.Save(DefLibrary, DefLibrary.ResourcePath);
    }

    public void GenerateCityDefFunc()
    {
        ClearCityDef();

        // ?HEX? Godot.EditorInterface.scan 

        CityDef cityInfo = new CityDef();
        cityInfo.CityName = "Atlantis";
        cityInfo.Province = "Pegasus";
        cityInfo.Population = 1000;
        cityInfo.Positon = new Vector2( 10, 10 );
        
        string path = "res://Def/" + cityInfo.CityName + ".tres";
        cityInfo.FilePath = path;
        ResourceSaver.Save(cityInfo, path);

        DefLibrary.Cities.Add(cityInfo);

        CityDef cityInfo_2 = new CityDef();
        cityInfo_2.CityName = "Avalon";
        cityInfo_2.Province = "Nova";
        cityInfo_2.Population = 100;
        cityInfo_2.Positon = new Vector2(-10, -10);
        
        path = "res://Def/" + cityInfo_2.CityName + ".tres";
        cityInfo_2.FilePath = path;
        ResourceSaver.Save(cityInfo_2, path);

        DefLibrary.Cities.Add(cityInfo_2);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
