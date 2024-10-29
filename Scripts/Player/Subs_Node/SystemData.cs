using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class SystemData : Node
{
    [ExportCategory("SystemParent")]
    [Export]
    public PlayerData _Player = null;

    [ExportCategory("SystemRawData")]
    [Export]
    public DataBlock Data = null;

    public string SystemName { get { return Star.StarName; } }

    [Export]
    public bool Capital = false;
    [Export]
    public DataBlock Resources = null;

    //[ExportCategory("SystemData-Actions")]
    //[Export]
    //public DataBlock ActionBuild = null;

    [ExportCategory("SystemData-Links")]
    [Export]
    public Array<ColonyData> Colonies = new Array<ColonyData>();
    [Export]
    public StarData Star = null;

    // resources
    public ResourcesWrapper Resources_PerTurn = null;
    // pops
    public PopsWrapper Pops_PerTurn = null;
    // control
    public ControlWrapper Control_PerTurn = null;
    // buildings
    public BuildingsWrapper Buildings_PerTurn = null;
    // actions
    public List<DistrictData> ActionsBuildPossible_PerTurn = new List<DistrictData>();
    // district queue
    public DistrictQueueWrapper DistrictsQueue_PerTurn = null;
    // shipbuilding
    public ShipbuildingWrapper Shipbuilding_PerTurn = null;

    // --------------------------------------------------------------------------------------------
    public ColonyData GetColony(string colony)
    {
        for (int idx = 0; idx < Colonies.Count; idx++)
        {
            if (Colonies[idx].ColonyName == colony)
            {
                return Colonies[idx];
            }
        }

        return null;
    }
}
