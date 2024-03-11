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
    [Export]
    public DataBlock Budget = null;
    [Export]
    public DataBlock Buildings = null;


    [ExportCategory("ColonyData-Actions")]
    [Export]
    public DataBlock ActionConTreasury = null;
    //[Export]
    //public DataBlock ActionCampaign = null;
    //[Export]
    //public DataBlock ActionBuild = null;

    [ExportCategory("ColonyData-Links")]
    [Export]
    public Array<SystemData> Systems = new Array<SystemData>();

    // resources
    public ResourcesWrapper Resources_PerTurn = null;
    // budget
    //public BudgetWrapper BudgetPerTurn = null;

    // actions
    public List<ActionColonyConBuildings> ActionsCampaignPossible = new List<ActionColonyConBuildings>();


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
