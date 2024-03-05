using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

// Editor
[Tool]
public partial class MapData : Node
{
    [ExportCategory("MapData-Link")]
    [Export]
    public MapNode _Node;

    [ExportCategory("MapData-LoadedSave")]
    [Export]
    public DataBlock _Data;

    [ExportCategory("MapData-Generated")]
    [Export]
    public DataBlock GameStats = null;
    [Export]
    public Array<DataBlock> Species = new Array<DataBlock>();
    [Export]
    public Array<PlayerData> Players = new Array<PlayerData>();
    [Export]
    public Array<StarData> Stars = new Array<StarData>();
    [Export]
    public Array<PawnData> Fleets = new Array<PawnData>();

    // ---
    private DataBlock TurnData = null;
    public int Turn
    {
        get
        {
            if (GameStats == null) return -1;
            if (TurnData == null)
            {
                TurnData = GameStats.GetSub("Turn");
            }
            return TurnData.ValueI;
        }
        set
        {
            if (GameStats == null) return;
            if (TurnData == null)
            {
                TurnData = GameStats.GetSub("Turn");
            }
            TurnData.ValueI = value;
        }
    }

    private RandomNumberGenerator RNG = new RandomNumberGenerator();

    public void GetTurnData()
    {
    }

    // ---

    public void ClearMap()
    {
        Players.Clear();
        Stars.Clear();
        Fleets.Clear();
    }

    // --------------------------------------------------------------------------------------------
    public void LoadMap(string saveName, DefLibrary defLib)
    {
        _Data = Data.LoadFile( saveName, defLib);

        GenerateGameFromData(defLib);
    }
    public void SaveMap(string saveName, DefLibrary defLib)
    {
        Data.SaveToFile(_Data, saveName, defLib);
    }

    // --------------------------------------------------------------------------------------------
    public void Clear()
    {
        _Node.ClearContainers();
        ClearMap();
    }

    // --------------------------------------------------------------------------------------------
    public void GenerateGameFromData(DefLibrary defLib)
    {
        Clear();

        // Game Stats
        GameStats = _Data.GetSub("GameStats");

        // Stars
        int galaxySize = _Data.GetSub("GalaxySize").ValueI;
        PackedScene gfxScene = GD.Load<PackedScene>("res://3DPrefabs/" + _Node.StarGFXName + ".tscn");

        DataBlock starList = _Data.GetSub("Star_List");
        Array<DataBlock> stars = starList.GetSubs("Star");
        for (int idx = 0; idx < stars.Count; idx++)
        {
            GenerateGameFromData_Star(stars[idx], galaxySize, gfxScene);
        }

        // Link Stars Paths
        PackedScene gfxPathScene = GD.Load<PackedScene>("res://3DPrefabs/" + _Node.PathGFXName + ".tscn");
        GenerateGameFromData_Paths(gfxPathScene);

        // Players
        DataBlock playerList = _Data.GetSub("Player_List");
        Array<DataBlock> players = playerList.GetSubs("Player");
        for (int idx = 0; idx < players.Count; idx++)
        {
            GenerateGameFromData_Player(players[idx], idx, defLib);
        }

        // Refresh system GFX
        for (int idx = 0; idx < Stars.Count; idx++)
        {
            Stars[idx]._Node.GFX.RefreshPlayerColors();
        }
    }

    public void GenerateGameFromData_Star(DataBlock starDataBlock, int galaxySize, PackedScene gfxScene)
    {
        // Node
        StarNode starNode = new StarNode();
        starNode.Name = starDataBlock.ValueS;

        _Node.StarNode.AddChild(starNode);//, true, InternalMode.Back);
        starNode.Owner = GetTree().EditedSceneRoot;

        // Data
        StarData starData = new StarData();
        starData._Data = starDataBlock;
        starData.Name = starDataBlock.ValueS + "_Data";
        starData.StarName = starDataBlock.ValueS;
        starData.X = starDataBlock.GetSub("X").ValueI;
        starData.Y = starDataBlock.GetSub("Y").ValueI;

        starNode.Data = starData;
        starData._Node = starNode;

        starNode.AddChild(starData);//, true, InternalMode.Back);
        starData.Owner = GetTree().EditedSceneRoot;

        Stars.Add(starData);

        DataBlock planetList = starDataBlock.GetSub("Planet_List");
        Array<DataBlock> planets = planetList.GetSubs("Planet");
        for (int idx = 0; idx < planets.Count; idx++)
        {
            GenerateGameFromData_Star_Planet(planets[idx], starData);
        }

        // GFX
        Node gfxNode = gfxScene.Instantiate();
        gfxNode.Name = starDataBlock.ValueS + "_GFX";

        starNode.AddChild(gfxNode);//, true, InternalMode.Back);
        gfxNode.Owner = GetTree().EditedSceneRoot;
        gfxNode.GetParent().SetEditableInstance(gfxNode, true);
        //gfxNode.

        starNode.GFX = gfxNode as StarGFX; // because of this LocationGFX has to be a Tool
        starNode.GFX._Node = starNode;

        starNode.GFX.Position = new Vector3(8.6666f * (2.0f * starData.X - starData.Y), 0.0f, -starData.Y * 15.0f) - new Vector3(8.6666f * galaxySize, 0.0f, -galaxySize * 15.0f);
        starNode.GFX.Position += (0.05f * starDataBlock.GetSub("GFX_RNG_1").ValueI) * Vector3.Right.Rotated(Vector3.Up, 0.01f * Mathf.Pi * starDataBlock.GetSub("GFX_RNG_2").ValueI);
    }

    public void GenerateGameFromData_Star_Planet(DataBlock planetDataBlock, StarData starData)
    {
        // Data
        PlanetData planetData = new PlanetData();
        planetData.Name = planetDataBlock.ValueS + "_Data";
        planetData.PlanetName = planetDataBlock.ValueS;

        planetData.Data = planetDataBlock;

        planetData._Star = starData;

        starData.AddChild(planetData);//, true, InternalMode.Back);
        planetData.Owner = GetTree().EditedSceneRoot;

        starData.Planets.Add(planetData);
    }

    public void GenerateGameFromData_Paths(PackedScene gfxScene)
    {
        for (int fromIdx = 0; fromIdx < Stars.Count; fromIdx++)
        {
            Array<DataBlock> paths = Stars[fromIdx]._Data.GetSubs("PathTo");
            for (int pathIdx = 0; pathIdx < paths.Count; pathIdx++)
            {
                for (int toIdx = 0; toIdx < Stars.Count; toIdx++)
                {
                    if (Stars[toIdx]._Data.GetSub("ID").ValueI == paths[pathIdx].ValueI)
                    {
                        Stars[fromIdx].PathsTo.Add(Stars[toIdx]);
                        break;
                    }
                }
            }
        }

        // GFX
        for (int fromIdx = 0; fromIdx < Stars.Count; fromIdx++)
        {
            for (int toIdx = 0; toIdx < Stars[fromIdx].PathsTo.Count; toIdx++)
            {
                if (Stars[fromIdx]._Data.GetSub("ID").ValueI < Stars[fromIdx].PathsTo[toIdx]._Data.GetSub("ID").ValueI)
                {
                    Node gfxNode = gfxScene.Instantiate();
                    gfxNode.Name = "Path_" + Stars[fromIdx].StarName + "_" + Stars[fromIdx].PathsTo[toIdx].StarName;

                    _Node.PathsNode.AddChild(gfxNode);//, true, InternalMode.Back);
                    gfxNode.Owner = GetTree().EditedSceneRoot;
                    gfxNode.GetParent().SetEditableInstance(gfxNode, true);
                    //gfxNode.

                    Node3D node3D = gfxNode as Node3D;
                    node3D.Position = (Stars[fromIdx]._Node.GFX.Position + Stars[fromIdx].PathsTo[toIdx]._Node.GFX.Position) / 2;
                    node3D.Rotation = new Vector3(0.0f, -(Stars[fromIdx]._Node.GFX.Position - Stars[fromIdx].PathsTo[toIdx]._Node.GFX.Position).SignedAngleTo(Vector3.Forward, Vector3.Up), 0.0f);
                }
            }
        }
    }

    public void GenerateGameFromData_Player(DataBlock playerDataBlock, int id, DefLibrary defLib)
    {
        // Node
        PlayerNode playerNode = new PlayerNode();
        playerNode.Name = playerDataBlock.ValueS;
        playerNode.GFX = defLib.GFXPlayerColors.PrimaryColors[id % defLib.GFXPlayerColors.PrimaryColors.Count];

        _Node.PlayersNode.AddChild(playerNode);//, true, InternalMode.Back);
        playerNode.Owner = GetTree().EditedSceneRoot;

        // Data
        PlayerData playerData = new PlayerData();
        playerData._Node = playerNode;
        playerData.Name = playerDataBlock.ValueS + "_Data";
        playerData.PlayerName = playerDataBlock.ValueS;
        playerData.Resources = playerDataBlock.GetSub("Resources");
        playerData.Status = playerDataBlock.GetSub("Status");
        playerData.Civics = playerDataBlock.GetSub("Civics");
        playerData.Bonuses = playerDataBlock.GetSub("Bonuses");
        playerData.Human = playerDataBlock.GetSub("Human") != null;

        playerData.Data = playerDataBlock;

        playerNode.Data = playerData;

        playerNode.AddChild(playerData);//, true, InternalMode.Back);
        playerData.Owner = GetTree().EditedSceneRoot;

        Players.Add(playerData);

        DataBlock sectorList = playerDataBlock.GetSub("Sector_List");
        Array<DataBlock> sectors = sectorList.GetSubs("Sector");
        for (int idx = 0; idx < sectors.Count; idx++)
        {
            GenerateGameFromData_Player_Sector(sectors[idx], playerData);
        }
    }

    public void GenerateGameFromData_Player_Sector(DataBlock sectorDataBlock, PlayerData playerData)
    {
        // Data
        SectorData sectorData = new SectorData();
        sectorData.Name = sectorDataBlock.ValueS + "_Data";
        sectorData.SectorName = sectorDataBlock.ValueS;
        sectorData.Resources = sectorDataBlock.GetSub("Resources");
        sectorData.Budget = sectorDataBlock.GetSub("Budget");
        sectorData.Buildings = sectorDataBlock.GetSub("Buildings");
        sectorData.ActionConTreasury = sectorDataBlock.GetSub("ActionConTreasury");
        //sectorData.ActionCampaign = sectorDataBlock.GetSub("Campaign");
        //sectorData.ActionBuild = sectorDataBlock.GetSub("Construction_1");

        sectorData.Data = sectorDataBlock;

        sectorData._Player = playerData;

        playerData.AddChild(sectorData);//, true, InternalMode.Back);
        sectorData.Owner = GetTree().EditedSceneRoot;

        playerData.Sectors.Add(sectorData);

        DataBlock systemList = sectorDataBlock.GetSub("System_List");
        Array<DataBlock> systems = systemList.GetSubs("System");
        for (int idx = 0; idx < systems.Count; idx++)
        {
            GenerateGameFromData_Player_Sector_System(systems[idx], sectorData);
        }
    }

    public void GenerateGameFromData_Player_Sector_System(DataBlock systemDataBlock, SectorData sectorData)
    {

        // Data
        SystemData systemData = new SystemData();
        systemData.Name = systemDataBlock.ValueS + "_Data";
        systemData.Resources = systemDataBlock.GetSub("Resources");
        systemData.Budget = systemDataBlock.GetSub("Budget");
        systemData.Buildings = systemDataBlock.GetSub("Buildings");

        systemData.Data = systemDataBlock;

        systemData._Sector = sectorData;

        DataBlock link = systemDataBlock.GetLink("Link:Star");
        string star = link.ValueS;
        for (int systemIdx = 0; systemIdx < Stars.Count; systemIdx++)
        {
            if (Stars[systemIdx].StarName == star)
            {
                systemData.Star = Stars[systemIdx];
                Stars[systemIdx].System = systemData;
                break;
            }
        }

        sectorData.AddChild(systemData);//, true, InternalMode.Back);
        systemData.Owner = GetTree().EditedSceneRoot;

        sectorData.Systems.Add(systemData);

        DataBlock colonyList = systemDataBlock.GetSub("Colony_List");
        Array<DataBlock> colonies = colonyList.GetSubs("Colony");
        for (int idx = 0; idx < colonies.Count; idx++)
        {
            GenerateGameFromData_Player_Sector_System_Colony(colonies[idx], systemData);
        }
    }

    public void GenerateGameFromData_Player_Sector_System_Colony(DataBlock colonyDataBlock, SystemData systemData)
    {
        // Data
        ColonyData colonyData = new ColonyData();
        colonyData.Name = colonyDataBlock.ValueS + "_Data";
        colonyData.ColonyName = colonyDataBlock.ValueS;
        colonyData.Resources = colonyDataBlock.GetSub("Resources");
        //colonyData.Budget = colonyDataBlock.GetSub("Budget");
        colonyData.Buildings = colonyDataBlock.GetSub("Buildings");
        colonyData.Support = colonyDataBlock.GetSub("Support");

        colonyData.ActionConBuildings = colonyDataBlock.GetSub("ActionConBuildings");
        colonyData.ActionConColony = colonyDataBlock.GetSub("ActionConColony");
        colonyData.ActionConShipyard = colonyDataBlock.GetSub("ActionConShipyard");
        //colonyData.ActionConTreasury = colonyDataBlock.GetSub("ActionConTreasury");

        colonyData.Data = colonyDataBlock;

        colonyData._System = systemData;

        DataBlock link = colonyDataBlock.GetLink("Link:Star:Planet");
        string star = Helper.Split_0(link.ValueS);
        string planet = Helper.Split_1(link.ValueS);
        for (int systemIdx = 0; systemIdx < Stars.Count; systemIdx++)
        {
            if (Stars[systemIdx].StarName == star)
            {
                for (int planetIdx = 0; planetIdx < Stars[systemIdx].Planets.Count; planetIdx++)
                {
                    if (Stars[systemIdx].Planets[planetIdx].PlanetName == planet)
                    {
                        colonyData.Planet = Stars[systemIdx].Planets[planetIdx];
                        Stars[systemIdx].Planets[planetIdx].Colony = colonyData;
                        break;
                    }
                }
                break;
            }
        }

        systemData.AddChild(colonyData);//, true, InternalMode.Back);
        colonyData.Owner = GetTree().EditedSceneRoot;

        systemData.Colonies.Add(colonyData);
    }

    // --------------------------------------------------------------------------------------------
    public StarData GetStar(string star)
    {
        for (int idx = 0; idx < Stars.Count; idx++)
        {
            if (Stars[idx].StarName == star)
            {
                return Stars[idx];
            }
        }

        return null;
    }

    public PlayerData GetPlayer(string player)
    {
        for (int idx = 0; idx < Players.Count; idx++)
        {
            if (Players[idx].PlayerName == player)
            {
                return Players[idx];
            }
        }

        return null;
    }
}
