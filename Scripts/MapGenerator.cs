using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Transactions;

// Editor
[Tool]
public partial class MapGenerator : Node
{
    [Export]
    public DefLibrary DefLibrary;
    [Export]
    public MapNode Map;
    [Export]
    public string MapTypeFile = "";
    [Export]
    public string StarGFXName = "";
    [Export]
    public Node PlayersNode = null;
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

    public void ClearContainers()
    {
        while (LocationsNode.GetChildCount(true) > 0)
        {
            Node child = LocationsNode.GetChild(0, true);
            LocationsNode.RemoveChild(child);
            child.Free();
        }

        while (PlayersNode.GetChildCount(true) > 0)
        {
            Node child = PlayersNode.GetChild(0, true);
            PlayersNode.RemoveChild(child);
            child.Free();
        }
    }

    public void GenerateMapFunc()
    {
        Map.Data.ClearMap();
        ClearContainers();

        GenerateGameStats();

        GenerateMapLocations();

        GenerateMapPlayers();

        // set camera
        MapCamera Camera = GetTree().EditedSceneRoot.GetNode<MapCamera>("Camera3D");
        Camera.MoveLimitX = 8.6666f * 2.0f * 2 * FromFile_Size;
        Camera.MoveLimitY = 15.0f * 2 * FromFile_Size;
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateGameStats()
    {
        Map.Data.GameStats = new DataBlock();
        
        Map.Data.GameStats.Type = DefLibrary.GetDBType("GameStats", Data.BaseType.NONE);
        
        Data.AddData(Map.Data.GameStats, "Turn", 0, DefLibrary);
        Data.AddData(Map.Data.GameStats, "Human", 0, DefLibrary);
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateMapLocations()
    {
        LoadMapFile();

        //if (DefLibrary.Locations.Count == 0)
        //{
        //    GD.PrintErr("Map gen error 01");
        //    return;
        //}

        for (int idx = 0; idx < FromFile_Stars.Count; idx++)
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

                DataBlock system = null;
                if (idx == 22) system = GenerateSolarSystemCustom_Sol();
                else system = GenerateSolarSystem();

                LocationData locationData = new LocationData();
                locationData.Name = system.ValueS + "_Loc";
                locationData.System = system;
                locationData.LocationName = system.ValueS + "_Loc";
                locationData.X = x;
                locationData.Y = y;

                Map.Data.Systems.Add(locationData);

                CreateLocationNode(x, y, locationData);
            }
        }
    }

    public void CreateLocationNode(int x, int y, LocationData locationData)
    {
        LocationNode node = new LocationNode();
        //node.Def = DefLibrary.Locations[0];
        node.Name = "Loc_" + locationData.System.ValueS;
        node.Data = locationData;

        LocationsNode.AddChild(node);//, true, InternalMode.Back);
        node.Owner = GetTree().EditedSceneRoot;

        node.AddChild(locationData);
        locationData.Owner = GetTree().EditedSceneRoot;

        PackedScene gfxScene = GD.Load<PackedScene>("res://3DPrefabs/" + StarGFXName + ".tscn");
        Node gfxNode = gfxScene.Instantiate();
        gfxNode.Name = locationData.System.ValueS + "_GFX";
        node.AddChild(gfxNode);//, true, InternalMode.Back);
        gfxNode.Owner = GetTree().EditedSceneRoot;
        gfxNode.GetParent().SetEditableInstance(gfxNode, true);

        node.GFX = gfxNode as LocationGFX; // because of this LocationGFX has to be a Tool
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
        if (words.Length == 0)
        {
            return GetWordsFromNextValidRow(rows, ref rowIdx);
        }
        return words;
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateMapPlayers()
    {
        for (int n = 0; n < 10; n++)
        {
            bool human = false;
            string startingPlanetCustom = "";
            string startingPlanetType = "";
            if (n == 0)
            {
                startingPlanetCustom = "Terra";
                startingPlanetType = "Temperate";
                human = true;
            }
            else
            {
                startingPlanetType = "Temperate";
            }

            LocationData startingSystem;
            DataBlock startingPlanet = GetPlayerStartingPlanet(startingPlanetCustom, startingPlanetType, out startingSystem);
            if (startingPlanet == null) continue;

            DataBlock playerResources = GeneratePlayerResources();
            DataBlock playerStatus = GeneratePlayerStatus();
            DataBlock playerCivics = GeneratePlayerCivics();
            DataBlock playerBonuses = GeneratePlayerBonuses();

            PlayerData playerData = new PlayerData();
            playerData.PlayerName = "Player_" + n.ToString();
            playerData.Name = "Player_" + n.ToString() + "_Data";
            playerData.Resources = playerResources;
            playerData.Status = playerStatus;
            playerData.Civics = playerCivics;
            playerData.Bonuses = playerBonuses;
            playerData.Human = human;

            DataBlock startingColony = GeneratePlayerColony(startingSystem, startingPlanet);
            playerData.Colonies.Add(startingColony);

            Data.AddData(startingColony, "Link:Location:Planet", startingSystem.LocationName + ":" + startingPlanet.ValueS, DefLibrary);
            Data.AddData(startingPlanet, "Link:Player:Colony", playerData.PlayerName + ":" + startingColony.ValueS, DefLibrary);

            Map.Data.Players.Add(playerData);

            CreatePlayerNode(playerData);
        }
    }

    public DataBlock GetPlayerStartingPlanet(string customName, string type, out LocationData system)
    {
        for (int systemIdx = 0; systemIdx < Map.Data.Systems.Count; systemIdx++)
        {
            DataBlock systemData = Map.Data.Systems[systemIdx].System;
            Array<DataBlock> planets = systemData.GetSubs("Planet");
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                DataBlock colony = planets[planetIdx].GetSub("Colony");
                if (colony == null)
                {
                    if (planets[planetIdx].ValueS == customName)
                    {
                        system = Map.Data.Systems[systemIdx];
                        return planets[planetIdx];
                    }
                }
            }
        }
        for (int systemIdx = 0; systemIdx < Map.Data.Systems.Count; systemIdx++)
        {
            DataBlock systemData = Map.Data.Systems[systemIdx].System;
            Array<DataBlock> planets = systemData.GetSubs("Planet");
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                DataBlock colony = planets[planetIdx].GetSub("Colony");
                if (colony == null)
                {
                    DataBlock planetType = planets[planetIdx].GetSub("Type");
                    DataBlock player = planets[planetIdx].GetLink("Link:Player");
                    if (planetType != null && planetType.ValueS == type && player == null)
                    {
                        system = Map.Data.Systems[systemIdx];
                        return planets[planetIdx];
                    }
                }
            }
        }
        system = null;
        return null;
    }

    public void CreatePlayerNode(PlayerData playerData)
    {
        PlayerNode node = new PlayerNode();
        //node.Def = ...
        node.Name = playerData.PlayerName;
        node.Data = playerData;

        PlayersNode.AddChild(node);//, true, InternalMode.Back);
        node.Owner = GetTree().EditedSceneRoot;

        node.AddChild(playerData);
        playerData.Owner = GetTree().EditedSceneRoot;

        //node.GFX = ...
    }
}
