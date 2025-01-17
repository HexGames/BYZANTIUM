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
    //
    public List<RelationData> Relations = new List<RelationData>();

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
    public void SaveMap_Progressive(string saveName, DefLibrary defLib)
    {
        Data.SaveToFile_Progressive(_Data, saveName, defLib);
    }
    

    // --------------------------------------------------------------------------------------------
    public void Clear()
    {
        _Node.ClearContainers();
        ClearMap();
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

    // --------------------------------------------------------------------------------------------
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
    
    // --------------------------------------------------------------------------------------------
    public RelationData GetRelation(PlayerData player_1, PlayerData player_2)
    {
        for (int idx = 0; idx < Relations.Count; idx++)
        {
            if ((Relations[idx]._Player_1 == player_1 && Relations[idx]._Player_2 == player_2)
                || (Relations[idx]._Player_1 == player_2 && Relations[idx]._Player_2 == player_1))
            {
                return Relations[idx];
            }
        }
        return null;
    }
}
