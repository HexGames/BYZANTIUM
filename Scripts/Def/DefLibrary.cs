using Godot;
using Godot.Collections;
using System.Linq;

// Editor
[Tool]
public partial class DefLibrary : Node
{
    [ExportCategory("Links")]
    [Export]
    public AssetLibrary AssetLib = null;

    [ExportCategory("DefLibrary")]
    [Export]
    public Dictionary<int, string> DB_Types_S = new Dictionary<int, string>();
    [Export]
    public Dictionary<string, int> DB_Types_I = new Dictionary<string, int>();

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

    static DefLibrary _self;
    public static DefLibrary self
    {
        get
        {
            if (_self != null)
            {
                return _self;
            }
            else
            {
                _self = ((SceneTree)Engine.GetMainLoop()).Root.GetNode<DefLibrary>("/root/Main/Library/DefLibrary");
                return _self;
            }
        }
        set
        {
            _self = value;
        }
    }

    [ExportCategory("Def Loaders")]
    [Export]
    public bool LoadBuildingsDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadDistrictsDefFunc();
            }
        }
    }
    [Export]
    public bool SaveBuildingsDefButton
    {
        get => false;
        set
        {
            if (value)
            {
                SaveDistrictsDef();
            }
        }
    }
    [Export]
    public bool LoadEmpireDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadEmpiresDefFunc();
            }
        }
    }
    [Export]
    public bool LoadEthicsDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadEthicsDefFunc();
            }
        }
    }
    [Export]
    public bool LoadFactionsDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadFactionsDefFunc();
            }
        }
    }
    [Export]
    public bool SaveFactionsDefButton
    {
        get => false;
        set
        {
            if (value)
            {
                SaveFactionsDef();
            }
        }
    }
    [Export]
    public bool LoadPlanetsDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadPlanetsDefFunc();
            }
        }
    }
    [Export]
    public bool LoadPlanetsNames
    {
        get => false;
        set
        {
            if (value)
            {
                LoadPlanetNamesFunc();
            }
        }
    }
    [Export]
    public bool LoadSpeciesDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadSpeciesDefFunc();
            }
        }
    }
    [Export]
    public bool LoadEmpiresDef
    {
        get => false;
        set
        {
            if (value)
            {
                LoadEmpiresDefFunc();
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
        if (Engine.IsEditorHint())
            return;

        //_Ready_Districts();
        //_Ready_Empires();
        //_Ready_Features();
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
            //GD.Print("Def new Data Block Type: " + name + " - " + newType);

            return newType;
        }

        return type;
    }

    //public int GetDBType(string name) // not used
    //{
    //
    //    int type;
    //    if (DB_Types_I.TryGetValue(name, out type) == false)
    //    {
    //        GD.PrintErr("Def Data Block Type  " + name + " not found!");
    //        return -1;
    //    }
    //
    //    return type;
    //}

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

    static public string GetIcon(string resource)
    {
        switch(resource)
        {
            case "Energy": return "Assets/UI/Symbols/Energy.png";
            case "Minerals": return "Assets/UI/Symbols/Minerals.png";
            case "Production": return "Assets/UI/Symbols/Prod.png";
            case "Shipbuilding": return "Assets/UI/Symbols/Shipbuilding.png";
            case "Trade": return "Assets/UI/Symbols/Trade.png";
            case "TechPoints": return "Assets/UI/Symbols/Research.png";
            case "CulturePoints": return "Assets/UI/Symbols/Culture.png";
            case "Authority": return "Assets/UI/Symbols/Authority.png";
            case "Influence": return "Assets/UI/Symbols/Influence.png";
            case "BC": return "Assets/UI/Symbols/BC.png";
        }
        return "";
    }
}
