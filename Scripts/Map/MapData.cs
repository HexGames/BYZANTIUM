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

    //Game __Game;
    //Game Game
    //{
    //    get
    //    {
    //        if (__Game != null)
    //        {
    //            return __Game;
    //        }
    //        else
    //        {
    //            __Game = GetNode<Game>("/root/Main/Game");
    //            return __Game;
    //        }
    //    }
    //}

    private RandomNumberGenerator RNG = new RandomNumberGenerator();

    public void GetTurnData()
    {
    }

    // ---

    public void ClearMap()
    {
        Players.Clear();
        Stars.Clear();
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
        //Node prefabGFX = gfxScene.Instantiate();
        //prefabGFX.
        //prefabGFX.Owner = GetTree().EditedSceneRoot;
        //AddChild(prefabGFX);
        //prefabGFX.GetParent().SetEditableInstance(prefabGFX, true);
        //prefabGFX.SceneFilePath = "";
        for (int idx = 0; idx < stars.Count; idx++)
        {
            GenerateGameFromData_Star(stars[idx], galaxySize, gfxScene); 
        }
        //prefabGFX.QueueFree();

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
            //TEMP01 Stars[idx]._Node.GFX.RefreshPlayerColors();
            //TEMP01 Stars[idx]._Node.GFX.RefreshShips();
        }
    }

    public void GenerateGameFromData_Star(DataBlock starDataBlock, int galaxySize, PackedScene gfxScene)
    {
        // Node
        StarNode starNode = new StarNode();
        starNode.Name = starDataBlock.ValueS;

        _Node.StarsNode.AddChild(starNode);//, true, InternalMode.Back);
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

        _Node.StarsGFX.AddChild(gfxNode);//, true, InternalMode.Back);
        gfxNode.Owner = GetTree().EditedSceneRoot;
        gfxNode.GetParent().SetEditableInstance(gfxNode, true);


        starNode.GFX = gfxNode as GFXStar; // because of this LocationGFX has to be a Tool
        starNode.GFX.Init();
        int angleSeed = starDataBlock.GetSub("GFX_RNG_1").ValueI + starDataBlock.GetSub("GFX_RNG_2").ValueI;
        starNode.GFX.Refresh(starData, angleSeed); 
        //TEMP01
        //starNode.GFX._Node = starNode;

        starNode.GFX.Position = new Vector3(8.6666f * (2.0f * starData.X + starData.Y), 0.0f, starData.Y * 15.0f) - new Vector3(8.6666f * galaxySize, 0.0f, -galaxySize * 15.0f);
        starNode.GFX.Position += (0.035f * starDataBlock.GetSub("GFX_RNG_1").ValueI) * Vector3.Right.Rotated(Vector3.Up, 0.01f * Mathf.Pi * starDataBlock.GetSub("GFX_RNG_2").ValueI);
    }

    public void GenerateGameFromData_Star_Planet(DataBlock planetDataBlock, StarData starData)
    {
        // Data
        PlanetData planetData = new PlanetData();
        planetData.Name = planetDataBlock.ValueS + "_Data";
        planetData.PlanetName = planetDataBlock.ValueS;
        //planetData.Resources = planetDataBlock.GetSub("Resources");

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
        playerData.Empire = playerDataBlock.GetSub("Empire");
        playerData.Resources = playerDataBlock.GetSub("Resources");
        playerData.Status = playerDataBlock.GetSub("Status");
        playerData.Civics = playerDataBlock.GetSub("Civics");
        playerData.Bonuses = playerDataBlock.GetSub("Bonuses");
        playerData.PlayerID = Players.Count;
        playerData.Human = playerDataBlock.GetSub("Human", false) != null;

        playerData.Data = playerDataBlock;

        playerNode.Data = playerData;

        playerNode.AddChild(playerData);//, true, InternalMode.Back);
        playerData.Owner = GetTree().EditedSceneRoot;

        Players.Add(playerData);

        DataBlock systemList = playerDataBlock.GetSub("Systems_List");
        Array<DataBlock> systems = systemList.GetSubs("System");
        for (int idx = 0; idx < systems.Count; idx++)
        {
            GenerateGameFromData_Player_System(systems[idx], playerData);
        }

        DataBlock shipDesignList = playerDataBlock.GetSub("Designs");
        Array<DataBlock> designs = shipDesignList.GetSubs("Design");
        for (int idx = 0; idx < designs.Count; idx++)
        {
            GenerateGameFromData_Player_Design(designs[idx], playerData);
        }

        DataBlock fleetsList = playerDataBlock.GetSub("Fleets");
        Array<DataBlock> fleets = fleetsList.GetSubs("Fleet");
        for (int idx = 0; idx < fleets.Count; idx++)
        {
            GenerateGameFromData_Player_Fleet(fleets[idx], playerData);
        }
    }

    public void GenerateGameFromData_Player_System(DataBlock systemDataBlock, PlayerData playerData)
    {
        // Data
        SystemData system = new SystemData();
        system.Name = systemDataBlock.ValueS + "_Data";
        system.Capital = systemDataBlock.HasSub("Capital");
        system.Resources = systemDataBlock.GetSub("Resources");

        system.Data = systemDataBlock;

        system._Player = playerData;

        system.Star = Data.GetLinkStarData(systemDataBlock, this);
        system.Star.System = system;

        playerData.AddChild(system);//, true, InternalMode.Back);
        system.Owner = GetTree().EditedSceneRoot;

        playerData.Systems.Add(system);

        DataBlock colonyList = systemDataBlock.GetSub("Colony_List");
        Array<DataBlock> colonies = colonyList.GetSubs("Colony");
        for (int idx = 0; idx < colonies.Count; idx++)
        {
            ColonyData colony = GenerateGameFromData_Player_System_Colony(colonies[idx], system);
            system.Colonies.Add(colony);
        }
    }

    public ColonyData GenerateGameFromData_Player_System_Colony(DataBlock colonyDataBlock, SystemData systemData)
    {
        // Data
        ColonyData colony = new ColonyData();
        colony.Name = colonyDataBlock.ValueS + "_Data";
        colony.ColonyName = colonyDataBlock.ValueS;
        colony.Type = colonyDataBlock.GetSub("Type");
        //colony.Resources = colonyDataBlock.GetSub("Resources");
        //colony.Buildings = colonyDataBlock.GetSub("Buildings");

        colony.Data = colonyDataBlock;

        colony._System = systemData;

        colony.Planet = Data.GetLinkPlanetData(colonyDataBlock, this);
        colony.Planet.Colony = colony;

        systemData.AddChild(colony);//, true, InternalMode.Back);
        colony.Owner = GetTree().EditedSceneRoot;

        return colony;
    }

    public void GenerateGameFromData_Player_Design(DataBlock designDataBlock, PlayerData playerData)
    {
        // Data
        DesignData design = new DesignData();
        design.Name = designDataBlock.ValueS + "_Data";
        design.DesignName = designDataBlock.ValueS;
        //design.Resources = sectorDataBlock.GetSub("Resources");
        //design.ActionBuildQueue = sectorDataBlock.GetSub("ActionBuildQueue");

        design.Data = designDataBlock;

        design._Player = playerData;

        playerData.AddChild(design);//, true, InternalMode.Back);
        design.Owner = GetTree().EditedSceneRoot;

        playerData.Designs.Add(design);
    }

    public void GenerateGameFromData_Player_Fleet(DataBlock fleetDataBlock, PlayerData playerData)
    {
        // Data
        FleetData fleet = new FleetData();
        fleet.Name = fleetDataBlock.ValueS + "_Data";
        fleet.FleetName = fleetDataBlock.ValueS;
        //fleet.MoveAction = fleetDataBlock.GetSub("ActionMove");
        //fleet.ShipsData = fleetDataBlock.GetSub("Resources");
        //design.ActionBuildQueue = sectorDataBlock.GetSub("ActionBuildQueue");

        fleet.Data = fleetDataBlock;

        fleet._Player = playerData;

        playerData.AddChild(fleet);//, true, InternalMode.Back);
        fleet.Owner = GetTree().EditedSceneRoot;

        playerData.Fleets.Add(fleet);

        Array<DataBlock> ships = fleetDataBlock.GetSubs("Ship");
        for (int idx = 0; idx < ships.Count; idx++)
        {
            GenerateGameFromData_Player_Fleet_Ship(ships[idx], fleet);
        }
    }

    public void GenerateGameFromData_Player_Fleet_Ship(DataBlock shipDataBlock, FleetData fleetData)
    {
        // Data
        ShipData ship = new ShipData();
        ship.Name = shipDataBlock.ValueS + "_Data";
        ship.ShipName = shipDataBlock.ValueS;
        //design.Resources = sectorDataBlock.GetSub("Resources");
        //design.ActionBuildQueue = sectorDataBlock.GetSub("ActionBuildQueue");

        ship.Data = shipDataBlock;

        ship._Fleet = fleetData;

        fleetData.AddChild(ship);//, true, InternalMode.Back);
        ship.Owner = GetTree().EditedSceneRoot;

        fleetData.Ships.Add(ship);
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
