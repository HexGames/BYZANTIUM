using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class ColonyData : Node
{
    [ExportCategory("ColonyData-Node")]
    [Export]
    public SystemData _System = null;

    [ExportCategory("ColonyRawData")]
    [Export]
    public DataBlock Data = null;

    [ExportCategory("ColonyData")]
    [Export]
    public string ColonyName = "";

    [Export]
    public DataBlock Resources = null;
    [Export]
    public DataBlock Jobs = null;
    [Export]
    public DataBlock Buildings = null;
    [Export]
    public DataBlock Support = null;

    [ExportCategory("ColonyData-Actions")]
    [Export]
    public DataBlock ActionConstruction = null;
    [Export]
    public DataBlock ActionShipbuilding = null;

    [ExportCategory("ColonyData-Links")]
    [Export]
    public PlanetData Planet = null;

    // resources
    public ResourcesWrapper Resources_PerTurn = null;

    // jobs
    public JobsWrapper Jobs_PerTurn = null;

    // actions
    public List<ActionColonyConBuildings> ActionsConBuildingsPossible = new List<ActionColonyConBuildings>();
}
