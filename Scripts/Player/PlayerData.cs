using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class PlayerData : Node
{
    [ExportCategory("PlayerData-Node")]
    [Export]
    public PlayerNode _Node = null;

    [ExportCategory("PlayerRawData")]
    [Export]
    public DataBlock Data = null;

    [ExportCategory("PlayerData")]
    [Export]
    public string PlayerName = "";

    [Export]
    public DataBlock Empire = null;
    [Export]
    public DataBlock Resources = null;
    [Export]
    public DataBlock Status = null;
    [Export]
    public DataBlock Civics = null;
    [Export]
    public DataBlock Bonuses = null;
    [Export]
    public DataBlock DesignsData = null;
    [Export]
    public DataBlock FleetsData = null;
    [Export]
    public int PlayerID = 0;
    [Export]
    public bool Human = false;

    [ExportCategory("PlayerData-Links")]
    [Export]
    public Array<SystemData> Systems = new Array<SystemData>();

    [ExportCategory("PlayerData-Links")]
    [Export]
    public Array<FleetData> Fleets = new Array<FleetData>();

    // shortlist from Game.self.Map.Data.Relations
    public List<RelationData> Relations = new List<RelationData>();

    [ExportCategory("PlayerData-Runtime")]
    [Export]
    public bool TurnFinished = false;

    // stockpiles
    public PlayerStatsWrapper Stats_PerTurn = null;

    // stockpiles
    public PlayerStockpilesWrapper Stockpiles_PerTurn = null;

    // designs
    public List<DesignData> Designs = new List<DesignData>();

    public bool DEBUG = false;


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
    // --------------------------------------------------------------------------------------------
    public FleetData GetFleet(string fleet)
    {
        for (int idx = 0; idx < Fleets.Count; idx++)
        {
            if (Fleets[idx].FleetName == fleet)
            {
                return Fleets[idx];
            }
        }

        return null;
    }

    // --------------------------------------------------------------------------------------------
    public DesignData GetDesign(string design)
    {
        for (int idx = 0; idx < Designs.Count; idx++)
        {
            if (Designs[idx].DesignName == design)
            {
                return Designs[idx];
            }
        }

        return null;
    }

    public DesignData GetDesignAtIdx(int designIdx)
    {
        return Designs[designIdx % Designs.Count];
    }

    public int GetDesignIdx(string design)
    {
        for (int idx = 0; idx < Designs.Count; idx++)
        {
            if (Designs[idx].DesignName == design)
            {
                return idx;
            }
        }

        return -1;
    }

    // --------------------------------------------------------------------------------------------
    public bool IsAtWarWith(PlayerData otherPlayer)
    {
        return false;
    }

    //public ColonyData GetColony(string colony)
    //{
    //    for (int idx = 0; idx < Colonies.Count; idx++)
    //    {
    //        if (Colonies[idx].ColonyName == colony)
    //        {
    //            return Colonies[idx];
    //        }
    //    }
    //
    //    return null;
    //}
}
