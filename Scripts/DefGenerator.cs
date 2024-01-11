using Godot;
using System;

// Editor
[Tool]
public partial class DefGenerator : Node
{
    [Export]
    public DefLibrary DefLibrary;
    //private Resource DefLibraryRes = null;
    //private DefLibrary DefLibrary
    //{
    //    get
    //    {
    //        return (DefLibrary)DefLibraryRes;
    //    }
    //}

    [Export]
    public bool ClearDefDBTypes
    {
        get => false;
        set
        {
            if (value)
            {
                ClearDefDBTypesFunc();
            }
        }
    }
    [Export]
    public bool GenerateLocationDef
    {
        get => false;
        set
        {
            if (value)
            {
                GenerateLocationDefFunc();
            }
        }
    }
    [Export]
    public bool GeneratePawnDef
    {
        get => false;
        set
        {
            if (value)
            {
                GeneratePawnDefFunc();
            }
        }
    }

    public void ClearDefDBTypesFunc()
    {
        if (DefLibrary == null)
        {
            GD.PrintErr("HEX - No Def Library linked!");
            return;
        }

        DefLibrary.DB_Types_I.Clear();
        DefLibrary.DB_Types_S.Clear();
    }

    public void ClearLocationDef()
    {
        if (DefLibrary == null)
        {
            GD.PrintErr("HEX - No Def Library linked!");
            return;
        }

        for (int idx = 0; idx < DefLibrary.Locations.Count; idx++)
        {
            string dirString = DefLibrary.Locations[idx].FilePath.GetBaseDir();
            DirAccess dir = DirAccess.Open(dirString);
            string fileString = DefLibrary.Locations[idx].FilePath.GetFile();
            Error err = dir.Remove(fileString);
            if (err != Error.Ok)
            {
                GD.PrintErr("HEX - Error deleting file: " + err);
            }
        }

        DefLibrary.Locations.Clear();
        //ResourceSaver.Save(DefLibrary, DefLibrary.ResourcePath);
    }

    public void GenerateLocationDefFunc()
    {
        ClearLocationDef();

        // ?HEX? Godot.EditorInterface.scan 

        LocationDef LocationInfo = new LocationDef();
        LocationInfo.LocationName = "Atlantis";
        LocationInfo.Province = "Pegasus";
        LocationInfo.Population = 1000;
        LocationInfo.Positon = new Vector2( 10, 10 );
        
        string path = DefLibrary.LocationsDir + LocationInfo.LocationName + ".tres";
        LocationInfo.FilePath = path;
        ResourceSaver.Save(LocationInfo, path);

        DefLibrary.Locations.Add(LocationInfo);

        LocationDef LocationInfo_2 = new LocationDef();
        LocationInfo_2.LocationName = "Avalon";
        LocationInfo_2.Province = "Nova";
        LocationInfo_2.Population = 100;
        LocationInfo_2.Positon = new Vector2(-10, -10);
        
        path = DefLibrary.LocationsDir + LocationInfo_2.LocationName + ".tres";
        LocationInfo_2.FilePath = path;
        ResourceSaver.Save(LocationInfo_2, path);

        DefLibrary.Locations.Add(LocationInfo_2);

        LocationDef LocationInfo_3 = new LocationDef();
        LocationInfo_3.LocationName = "Camelot";
        LocationInfo_3.Province = "Britain";
        LocationInfo_3.Population = 100;
        LocationInfo_3.Positon = new Vector2(-10, 10);

        path = DefLibrary.LocationsDir + LocationInfo_3.LocationName + ".tres";
        LocationInfo_3.FilePath = path;
        ResourceSaver.Save(LocationInfo_3, path);

        DefLibrary.Locations.Add(LocationInfo_3);

        LocationDef LocationInfo_4 = new LocationDef();
        LocationInfo_4.LocationName = "Carcassonne";
        LocationInfo_4.Province = "France";
        LocationInfo_4.Population = 100;
        LocationInfo_4.Positon = new Vector2(-10, 0);

        path = DefLibrary.LocationsDir + LocationInfo_4.LocationName + ".tres";
        LocationInfo_4.FilePath = path;
        ResourceSaver.Save(LocationInfo_4, path);

        DefLibrary.Locations.Add(LocationInfo_4);

        LocationDef LocationInfo_5 = new LocationDef();
        LocationInfo_5.LocationName = "Constantinopolis";
        LocationInfo_5.Province = "Byzantium";
        LocationInfo_5.Population = 100;
        LocationInfo_5.Positon = new Vector2(0, 10);

        path = DefLibrary.LocationsDir + LocationInfo_5.LocationName + ".tres";
        LocationInfo_5.FilePath = path;
        ResourceSaver.Save(LocationInfo_5, path);

        DefLibrary.Locations.Add(LocationInfo_5);

        //ResourceSaver.Save(DefLibrary, DefLibrary.ResourcePath);
    }

    public void ClearPawnDef()
    {
        if (DefLibrary == null)
        {
            GD.PrintErr("HEX - No Def Library linked!");
            return;
        }

        for (int idx = 0; idx < DefLibrary.Pawns.Count; idx++)
        {
            string dirString = DefLibrary.Pawns[idx].FilePath.GetBaseDir();
            DirAccess dir = DirAccess.Open(dirString);
            string fileString = DefLibrary.Pawns[idx].FilePath.GetFile();
            Error err = dir.Remove(fileString);
            if (err != Error.Ok)
            {
                GD.PrintErr("HEX - Error deleting file: " + err);
            }
        }

        DefLibrary.Pawns.Clear();
        //ResourceSaver.Save(DefLibrary, DefLibrary.ResourcePath);
    }

    public void GeneratePawnDefFunc()
    {
        ClearPawnDef();

        PawnDef pawnInfo = new PawnDef();
        pawnInfo.PawnName = "Alexios";
        pawnInfo.Power = 20;
        pawnInfo.Age = 25;
        pawnInfo.StartingLocation = "Atlantis";

        string path = DefLibrary.PawnsDir + pawnInfo.PawnName + ".tres";
        pawnInfo.FilePath = path;
        ResourceSaver.Save(pawnInfo, path);

        DefLibrary.Pawns.Add(pawnInfo);

        PawnDef pawnInfo_2 = new PawnDef();
        pawnInfo_2.PawnName = "Bohemond";
        pawnInfo_2.Power = 18;
        pawnInfo_2.Age = 22;
        pawnInfo_2.StartingLocation = "Avalon";

        path = DefLibrary.PawnsDir + pawnInfo_2.PawnName + ".tres";
        pawnInfo_2.FilePath = path;
        ResourceSaver.Save(pawnInfo_2, path);

        DefLibrary.Pawns.Add(pawnInfo_2);
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
