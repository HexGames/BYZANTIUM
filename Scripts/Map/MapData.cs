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
    public Array<SystemData> Systems = new Array<SystemData>();
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
        Systems.Clear();
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

        // Systems
        int galaxySize = _Data.GetSub("GalaxySize").ValueI;
        PackedScene gfxScene = GD.Load<PackedScene>("res://3DPrefabs/" + _Node.StarGFXName + ".tscn");

        DataBlock systemList = _Data.GetSub("System_List");
        Array<DataBlock> systems = systemList.GetSubs("System");
        for (int idx = 0; idx < systems.Count; idx++)
        {
            GenerateGameFromData_System(systems[idx], galaxySize, gfxScene);
        }

        // Link Systems Paths
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
        for (int idx = 0; idx < Systems.Count; idx++)
        {
            Systems[idx]._Node.GFX.RefreshPlayerColors();
        }
    }

    public void GenerateGameFromData_System(DataBlock systemDataBlock, int galaxySize, PackedScene gfxScene)
    {
        // Node
        SystemNode systemNode = new SystemNode();
        systemNode.Name = systemDataBlock.ValueS;

        _Node.SystemsNode.AddChild(systemNode);//, true, InternalMode.Back);
        systemNode.Owner = GetTree().EditedSceneRoot;

        // Data
        SystemData systemData = new SystemData();
        systemData._Data = systemDataBlock;
        systemData.Name = systemDataBlock.ValueS + "_Data";
        systemData.SystemName = systemDataBlock.ValueS;
        systemData.X = systemDataBlock.GetSub("X").ValueI;
        systemData.Y = systemDataBlock.GetSub("Y").ValueI;

        DataBlock planetList = systemDataBlock.GetSub("Planet_List");
        systemData.Planets.AddRange(planetList.GetSubs("Planet"));

        systemNode.Data = systemData;
        systemData._Node = systemNode;

        systemNode.AddChild(systemData);//, true, InternalMode.Back);
        systemData.Owner = GetTree().EditedSceneRoot;

        Systems.Add(systemData);

        // GFX
        Node gfxNode = gfxScene.Instantiate();
        gfxNode.Name = systemDataBlock.ValueS + "_GFX";

        systemNode.AddChild(gfxNode);//, true, InternalMode.Back);
        gfxNode.Owner = GetTree().EditedSceneRoot;
        gfxNode.GetParent().SetEditableInstance(gfxNode, true);
        //gfxNode.

        systemNode.GFX = gfxNode as SystemGFX; // because of this LocationGFX has to be a Tool
        systemNode.GFX._Node = systemNode;

        systemNode.GFX.Position = new Vector3(8.6666f * (2.0f * systemData.X - systemData.Y), 0.0f, -systemData.Y * 15.0f) - new Vector3(8.6666f * galaxySize, 0.0f, -galaxySize * 15.0f);
        systemNode.GFX.Position += (0.05f * systemDataBlock.GetSub("GFX_RNG_1").ValueI) * Vector3.Right.Rotated(Vector3.Up, 0.01f * Mathf.Pi * systemDataBlock.GetSub("GFX_RNG_2").ValueI);
    }

    public void GenerateGameFromData_Paths(PackedScene gfxScene)
    {
        for (int fromIdx = 0; fromIdx < Systems.Count; fromIdx++)
        {
            Array<DataBlock> paths = Systems[fromIdx]._Data.GetSubs("PathTo");
            for (int pathIdx = 0; pathIdx < paths.Count; pathIdx++)
            {
                for (int toIdx = 0; toIdx < Systems.Count; toIdx++)
                {
                    if (Systems[toIdx]._Data.GetSub("ID").ValueI == paths[pathIdx].ValueI)
                    {
                        Systems[fromIdx].PathsTo.Add(Systems[toIdx]);
                        break;
                    }
                }
            }
        }

        // GFX
        for (int fromIdx = 0; fromIdx < Systems.Count; fromIdx++)
        {
            for (int toIdx = 0; toIdx < Systems[fromIdx].PathsTo.Count; toIdx++)
            {
                if (Systems[fromIdx]._Data.GetSub("ID").ValueI < Systems[fromIdx].PathsTo[toIdx]._Data.GetSub("ID").ValueI)
                {
                    Node gfxNode = gfxScene.Instantiate();
                    gfxNode.Name = "Path_" + Systems[fromIdx].SystemName + "_" + Systems[fromIdx].PathsTo[toIdx].SystemName;

                    _Node.PathsNode.AddChild(gfxNode);//, true, InternalMode.Back);
                    gfxNode.Owner = GetTree().EditedSceneRoot;
                    gfxNode.GetParent().SetEditableInstance(gfxNode, true);
                    //gfxNode.

                    Node3D node3D = gfxNode as Node3D;
                    node3D.Position = (Systems[fromIdx]._Node.GFX.Position + Systems[fromIdx].PathsTo[toIdx]._Node.GFX.Position) / 2;
                    node3D.Rotation = new Vector3(0.0f, -(Systems[fromIdx]._Node.GFX.Position - Systems[fromIdx].PathsTo[toIdx]._Node.GFX.Position).SignedAngleTo(Vector3.Forward, Vector3.Up), 0.0f);
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

        DataBlock colonyList = playerDataBlock.GetSub("Colony_List");
        Array<DataBlock> colonies = colonyList.GetSubs("Colony");
        for (int idx = 0; idx < colonies.Count; idx++)
        {
            GenerateGameFromData_Player_Colony(colonies[idx], playerData);
        }

        playerNode.Data = playerData;

        playerNode.AddChild(playerData);//, true, InternalMode.Back);
        playerData.Owner = GetTree().EditedSceneRoot;

        Players.Add(playerData);
    }

    public void GenerateGameFromData_Player_Colony(DataBlock colonyDataBlock, PlayerData playerData)
    {
        // Node
        ColonyNode colonyNode = new ColonyNode();
        colonyNode.Name = colonyDataBlock.ValueS;

        playerData._Node.AddChild(colonyNode);//, true, InternalMode.Back);
        colonyNode.Owner = GetTree().EditedSceneRoot;

        // Data
        ColonyData colonyData = new ColonyData();
        colonyData.Name = colonyDataBlock.ValueS + "_Data";
        colonyData.ColonyName = colonyDataBlock.ValueS;
        colonyData.DataBlock = colonyDataBlock;
        colonyData.Resources = colonyDataBlock.GetSub("Resources");
        colonyData.Budget = colonyDataBlock.GetSub("Budget");
        colonyData.Buildings = colonyDataBlock.GetSub("Buildings");
        colonyData.Support = colonyDataBlock.GetSub("Support");
        colonyData.Bonuses = colonyDataBlock.GetSub("Bonuses");

        colonyNode.Data = colonyData;
        colonyData._Node = colonyNode;

        colonyNode.AddChild(colonyData);//, true, InternalMode.Back);
        colonyData.Owner = GetTree().EditedSceneRoot;

        // links
        colonyData.Player = playerData;

        DataBlock link = colonyDataBlock.GetLink("Link:System:Planet");
        string system = Helper.Split_0(link.ValueS);
        string planet = Helper.Split_1(link.ValueS);
        for (int systemIdx = 0; systemIdx < Systems.Count; systemIdx++)
        {
            if (Systems[systemIdx].SystemName == system)
            {
                for (int planetIdx = 0; planetIdx < Systems[systemIdx].Planets.Count; planetIdx++)
                {
                    if (Systems[systemIdx].Planets[planetIdx].ValueS == planet)
                    {
                        colonyData.System = Systems[systemIdx];
                        colonyData.Planet = Systems[systemIdx].Planets[planetIdx];
                        Systems[systemIdx].Colonies.Add(colonyData);
                        break;
                    }
                }
                break;
            }
        }

        playerData.Colonies.Add(colonyData);
    }

    // --------------------------------------------------------------------------------------------
    public SystemData GetSystem(string system)
    {
        for (int idx = 0; idx < Systems.Count; idx++)
        {
            if (Systems[idx].SystemName == system)
            {
                return Systems[idx];
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
