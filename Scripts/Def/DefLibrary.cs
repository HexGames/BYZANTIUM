using Godot;
using Godot.Collections;
using System.Linq;

// Editor
[Tool]
public partial class DefLibrary : Node
{
    [Export]
    public Dictionary<int, string> DB_Types_S = new Dictionary<int, string>();
    [Export]
    public Dictionary<string, int> DB_Types_I = new Dictionary<string, int>();

    public string LocationsDir = "res:///Def/Locations/";
    //[Export]
    //public Array<LocationDef> Locations = new Array<LocationDef>();

    public string PawnsDir = "res:///Def/Pawns/";
    [Export]
    public Array<PawnDef> Pawns = new Array<PawnDef>();

    [Export]
    public DefUIPlanets UIPlanets = null;
    [Export]
    public DefGFXPlayerColors GFXPlayerColors = null;

    //[ExportCategory("Def Generators")]
    /*[Export]
    public bool GenerateBuidingsDef
    {
        get => false;
        set
        {
            if (value)
            {
                GenerateBuildingsDefFunc();
            }
        }
    }*/

    [ExportCategory("Def Loaders")]
    [Export]
    public bool LoadBuildingsDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadBuildingsDefFunc();
            }
        }
    }
    [Export]
    public bool LoadShipPartsDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadShipPartsDefFunc();
            }
        }
    }
    /*[Export]
    public bool SaveShipPartsDefButton
    {
        get => false;
        set
        {
            if (value)
            {
                SaveShipPartsDef();
            }
        }
    }*/

    public override void _Ready()
    {
        _Ready_Buildings();
    }
    //    DirAccess folder = null;
    //    string[] files = null;
    //
    //    // Locations
    //    //Locations.Clear();
    //
    //    folder = DirAccess.Open(LocationsDir);
    //    files = folder.GetFiles();
    //
    //    //for ( int idx = 0; idx < files.Length; idx++ )
    //    //{
    //    //    Resource res = GD.Load<Resource>(LocationsDir + files[idx]);
    //    //    Locations.Add(res as LocationDef);
    //    //}
    //
    //    // Pawns
    //    Pawns.Clear();
    //
    //    folder = DirAccess.Open(PawnsDir);
    //    files = folder.GetFiles();
    //
    //    for (int idx = 0; idx < files.Length; idx++)
    //    {
    //        Resource res = GD.Load<Resource>(PawnsDir + files[idx]);
    //        Pawns.Add(res as PawnDef);
    //    }
    //}

    public int GetDBType(string name, Data.BaseType baseType)
    {

        int type;
        if (DB_Types_I.TryGetValue(name, out type) == false)
        {
            int newType = 0 + ((int)baseType) * 10000;
            while (DB_Types_S.ContainsKey(newType)) newType++;

            DB_Types_S.Add(newType, name);
            DB_Types_I.Add(name, newType);
            GD.Print("Def new Data Block Type: " + name + " - " + newType);

            return newType;
        }

        return type;
    }

    public int GetDBType(string name)
    {

        int type;
        if (DB_Types_I.TryGetValue(name, out type) == false)
        {
            GD.PrintErr("Def Data Block Type  " + name + " not found!");
            return -1;
        }

        return type;
    }

    public string GetDBValue(int type)
    {
        string name;
        if (DB_Types_S.TryGetValue(type, out name) == false)
        {
            GD.PrintErr("Def Data Block Type  " + type + " not found!");
            return "";
        }

        return name;
    }
}
