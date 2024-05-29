using Godot;
using Godot.Collections;

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
    public bool Human = false;

    [ExportCategory("PlayerData-Links")]
    [Export]
    public Array<SectorData> Sectors = new Array<SectorData>();

    [ExportCategory("PlayerData-Links")]
    [Export]
    public Array<DesignData> Designs = new Array<DesignData>();

    [ExportCategory("PlayerData-Links")]
    [Export]
    public Array<FleetData> Fleets = new Array<FleetData>();

    [ExportCategory("PlayerData-Runtime")]
    [Export]
    public bool TurnFinished = false;

    // resources
    public ResourcesWrapper Resources_PerTurn = null;


    // --------------------------------------------------------------------------------------------
    public SectorData GetSector(string sector)
    {
        for (int idx = 0; idx < Sectors.Count; idx++)
        {
            if (Sectors[idx].SectorName == sector)
            {
                return Sectors[idx];
            }
        }

        return null;
    }
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
