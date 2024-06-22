using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class SectorData : Node
{
    [ExportCategory("SectorParnt")]
    [Export]
    public PlayerData _Player = null;

    [ExportCategory("SectorRawData")]
    [Export]
    public DataBlock Data = null;

    [ExportCategory("SectorData")]
    [Export]
    public string SectorName = "";

    [Export]
    public DataBlock Resources = null;
    //[Export]
    //public DataBlock Budget = null;
    //[Export]
    //public DataBlock Buildings = null;


    [ExportCategory("ColonyData-Actions")]
    //[Export]
    //public DataBlock ActionCampaign = null;
    [Export]
    public DataBlock ActionBuildQueue = null;

    [ExportCategory("ColonyData-Links")]
    [Export]
    public Array<SystemData> Systems = new Array<SystemData>();

    // resources
    public ResourcesWrapper Resources_PerTurn = null;
    public BuildingQueueWrapper BuildQueue_PerTurn_ActionChange = null;
    // budget
    //public BudgetWrapper BudgetPerTurn = null;

    // actions
    //public List<ActionTargetInfo> BuildQueue_PerTurn = new List<ActionTargetInfo>();
    public List<DefBuildingWrapper> AvailableBuildings_PerTurn = new List<DefBuildingWrapper>();
    public List<DesignData> AvailableDesigns_PerTurn = new List<DesignData>();
    //public List<ActionTargetInfo> UnavailableBuildings_PerTurn = new List<ActionTargetInfo>();

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

    public PlanetData GetPlanet(string planetName)
    {
        for (int systemIdx = 0; systemIdx < Systems.Count; systemIdx++)
        {
            for (int planetIdx = 0; planetIdx < Systems[systemIdx].Star.Planets.Count; planetIdx++)
            {
                if (Systems[systemIdx].Star.Planets[planetIdx].PlanetName == planetName)
                {
                    return Systems[systemIdx].Star.Planets[planetIdx];
                }
            }
        }
        return null;
    }
}
