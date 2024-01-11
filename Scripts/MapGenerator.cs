using Godot;
using Godot.Collections;
using System;

// Editor
[Tool]
public partial class MapGenerator : Node
{
    [Export]
    public DefLibrary DefLibrary;
    [Export]
    public MapNode MapData;
    [Export]
    public string MapTypeFile = "";
    [Export]
    public string StarGFXName = "";
    [Export]
    public Node LocationsNode = null;
    [Export]
    public Node PawnsNode = null;

    [Export]
    public bool GenerateMap
    {
        get => false;
        set
        {
            if (value)
            {
                GenerateMapFunc();
            }
        }
    }

    private RandomNumberGenerator RNG = new RandomNumberGenerator();

    private int FromFile_Size = 0;
    private char PaddingCharacter = ' ';
    private char EmptyCharacter = ' ';
    private int FromFile_SectorsCount = 0;
    private int FromFile_SectorGroupsCount = 0;
    private Array<string> FromFile_SectorGroups = new Array<string>();
    private Array<string> FromFile_Stars = new Array<string>();


    // Called when the node enters the scene tree for the first time.
    //public override void _Ready()
    //{
    //}

    public void ClearMap()
    {
        while (LocationsNode.GetChildCount(true) > 0)
        {
            Node child = LocationsNode.GetChild(0, true);
            LocationsNode.RemoveChild(child);
            child.Free();
        }
    }

    public void GenerateMapFunc()
    {
        ClearMap();
        MapData.Systems.Clear();

        LoadMapFile();

        if (DefLibrary.Locations.Count == 0)
        {
            GD.PrintErr("Map gen error 01");
            return;
        }

        for ( int idx = 0; idx < FromFile_Stars.Count; idx++ )
        {
            if (FromFile_Stars[idx].Length < 0)
            {
                GD.PrintErr("Map gen error 02");
                return;
            }
            if (FromFile_Stars[idx][0] != PaddingCharacter && FromFile_Stars[idx][0] != EmptyCharacter)
            {
                int x = idx % (1 + 2 * FromFile_Size);
                int y = idx / (1 + 2 * FromFile_Size);

                DataBlock system = GenerateSolarSystem();

                LocationData locationData = new LocationData();
                locationData.System = system;
                locationData.Name = system.ValueS + "_Data";
                locationData.X = x;
                locationData.Y = y;

                MapData.Systems.Add(locationData);

                //node.AddChild(node.Data);
                //node.Data.Owner = GetTree().EditedSceneRoot;

                CreateLocationNode(x, y, locationData);
            }
        }

        // set camera
        MapCamera Camera = GetTree().EditedSceneRoot.GetNode<MapCamera>("Camera3D");
        Camera.MoveLimitX = 8.6666f * 2.0f * 2 * FromFile_Size;
        Camera.MoveLimitY = 15.0f * 2 * FromFile_Size;

        //for ( int idx = 0; idx < DefLibrary.Locations.Count; idx++ )
        //{
        //    LocationNode node = new LocationNode();
        //    node.Def = DefLibrary.Locations[idx];
        //    node.Name = node.Def.LocationName;
        //
        //    LocationsNode.AddChild(node, true, InternalMode.Back);
        //    node.Owner = GetTree().EditedSceneRoot;
        //
        //    node.Data = new LocationData();
        //    node.Data.Name = node.Name + "Data";
        //    node.Data.Population = node.Def.Population;
        //    node.Data.Prosperity = 15;
        //    node.AddChild(node.Data);
        //    node.Data.Owner = GetTree().EditedSceneRoot;
        //
        //    PackedScene gfxScene = GD.Load<PackedScene>("res:///3DPrefabs/" + SarGFXName + ".tscn");
        //    Node gfxNode = gfxScene.Instantiate();
        //    gfxNode.Name = node.Name + "GFX";
        //    node.AddChild(gfxNode);
        //    gfxNode.Owner = GetTree().EditedSceneRoot;
        //    gfxNode.GetParent().SetEditableInstance(gfxNode, true);
        //
        //    node.GFX = gfxNode as LocationGFX;
        //    node.GFX.Position = new Vector3( node.Def.Positon.X, 0.0f, node.Def.Positon.Y);
        //    node.GFX.SetLocationName(node.Name);
        //}

        //for ( int idx = 0; idx < DefLibrary.Pawns.Count; idx++ )
        //{
        //    PawnNode node = new PawnNode();
        //    node.Def = DefLibrary.Pawns[idx];
        //    node.Name = node.Def.PawnName;
        //
        //    PawnsNode.AddChild(node, true, InternalMode.Back);
        //    node.Owner = GetTree().EditedSceneRoot;
        //
        //    node.Data = new PawnData();
        //    node.Data.Name = node.Name + "Data";
        //    node.Data.Power = node.Def.Power;
        //    node.Data.Age = node.Def.Age;
        //    node.AddChild(node.Data);
        //    node.Data.Owner = GetTree().EditedSceneRoot;
        //
        //    PackedScene gfxScene = GD.Load<PackedScene>("res:///3DPrefabs/Pawn.tscn");
        //    Node gfxNode = gfxScene.Instantiate();
        //    gfxNode.Name = node.Name + "GFX";
        //    node.AddChild(gfxNode);
        //    gfxNode.Owner = GetTree().EditedSceneRoot;
        //    gfxNode.GetParent().SetEditableInstance(gfxNode, true);
        //
        //    node.GFX = gfxNode as PawnGFX;
        //
        //    // set in Location
        //    // node.Def.StartingLocation == somewhere in map
        //}
    }

    public void CreateLocationNode(int x, int y, LocationData locationData)
    {
        LocationNode node = new LocationNode();
        node.Def = DefLibrary.Locations[0];
        node.Name = "Loc_" + locationData.System.ValueS;
        node.Data = locationData;

        LocationsNode.AddChild(node, true, InternalMode.Back);
        node.Owner = GetTree().EditedSceneRoot;

        node.AddChild(locationData);
        locationData.Owner = GetTree().EditedSceneRoot;

        PackedScene gfxScene = GD.Load<PackedScene>("res:///3DPrefabs/" + StarGFXName + ".tscn");
        Node gfxNode = gfxScene.Instantiate();
        gfxNode.Name = locationData.System.ValueS + "_GFX";
        node.AddChild(gfxNode);
        gfxNode.Owner = GetTree().EditedSceneRoot;
        gfxNode.GetParent().SetEditableInstance(gfxNode, true);

        node.GFX = gfxNode as LocationGFX;
        node.GFX.Position = new Vector3(8.6666f * (2.0f * x - y), 0.0f, -y * 15.0f) - new Vector3(8.6666f * FromFile_Size, 0.0f, -FromFile_Size * 15.0f);
        node.GFX.SetLocationName(node.Name);
    }

    public void LoadMapFile()
    {
        using var file = FileAccess.Open("res:///Mod/" + MapTypeFile + ".map", FileAccess.ModeFlags.Read);
        string content = file.GetAsText(); 
        char[] delimiters = { '\n', '\r' ,'\t' };
        string[] rows = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        int rowIdx = -1;

        string[] firstRow = GetWordsFromNextValidRow(rows, ref rowIdx);
        if (firstRow == null)
        {
            return;
        }
        if (firstRow.Length <= 4)
        {
            GD.PrintErr("Load map file error 02");
            return;
        }
        FromFile_Size = firstRow[0].ToInt();
        PaddingCharacter = firstRow[1][0];
        EmptyCharacter = firstRow[2][0];
        FromFile_SectorsCount = firstRow[3].ToInt();
        FromFile_SectorGroupsCount = firstRow[4].ToInt();
        if (firstRow.Length < 5 + FromFile_SectorGroupsCount)
        {
            GD.PrintErr("Load map file error 03");
            return;
        }
        FromFile_SectorGroups.Clear();
        for (int idx = 5; idx < 5 + FromFile_SectorGroupsCount; idx++)
        {
            FromFile_SectorGroups.Add(firstRow[idx]);
        }

        FromFile_Stars.Clear();
        for (int n = 0; n < 1 + 2 * FromFile_Size; n++)
        {
            string[] row = GetWordsFromNextValidRow(rows, ref rowIdx);
            if (row == null)
            {
                return;
            }
            if (row.Length < 1 + 2 * FromFile_Size)
            {
                GD.PrintErr("Load map file error 04");
                return;
            }
            for (int idx = 0; idx < 1 + 2 * FromFile_Size; idx++)
            {
                FromFile_Stars.Insert(idx, row[idx]);
            }
        }
    }

    string[] GetWordsFromNextValidRow(string[] rows, ref int rowIdx)
    {
        rowIdx++;
        if (rowIdx >= rows.Length)
        {
            GD.PrintErr("Load map file error 01");
            return null;
        }
        string[] words = rows[rowIdx].Split("//")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if ( words.Length == 0 )
        {
            return GetWordsFromNextValidRow(rows, ref rowIdx);
        }
        return words;
    }

    private DataBlock GenerateSolarSystem()
    {
        DataBlock system = new DataBlock();

        system.Type = DefLibrary.GetDBType("System", Data.BaseType.STRING);
        system.ValueS = "System_" + RNG.RandiRange(1, 99);

        DataBlock star = Data.AddData(system, "Planet", system.ValueS + "_Star", DefLibrary);
        Data.AddData(star, "StarSize", RNG.RandiRange(3, 7), DefLibrary);
        Data.AddData(star, "StarType", "M_Class", DefLibrary);

        int noOfPlanes = RNG.RandiRange(1, 9);
        for (int n = 0; n < noOfPlanes; n++)
        {
            DataBlock planet = Data.AddData(system, "Planet", "Planet_" + RNG.RandiRange(1, 99), DefLibrary);
            Data.AddData(planet, "PlanetSize", RNG.RandiRange(1, 9), DefLibrary);
            Data.AddData(planet, "PlanetType", "Barren", DefLibrary);
        }

        return system;
    }
}
