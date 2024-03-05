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
    public DataBlock Resources = null;
    [Export]
    public DataBlock Status = null;
    [Export]
    public DataBlock Civics = null;
    [Export]
    public DataBlock Bonuses = null;
    [Export]
    public bool Human = false;

    [ExportCategory("PlayerData-Links")]
    [Export]
    public Array<SectorData> Sectors = new Array<SectorData>();

    [ExportCategory("PlayerData-Runtime")]
    [Export]
    public bool TurnFinished = false;

    // resources
    public ResourcesWrapperTemp ResourcesPerTurn = null;


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
