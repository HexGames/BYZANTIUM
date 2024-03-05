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
    public DataBlock Buildings = null;
    [Export]
    public DataBlock Support = null;

    [ExportCategory("ColonyData-Actions")]
    [Export]
    public DataBlock ActionConBuildings = null;
    [Export]
    public DataBlock ActionConColony = null;
    [Export]
    public DataBlock ActionConShipyard = null;

    [ExportCategory("ColonyData-Links")]
    [Export]
    public PlanetData Planet = null;

    // resources
    public ResourcesWrapperTemp ResourcesPerTurn = null;

    // actions
    //public ActionsConWrapper ActionsConPerTurn = null;

    // actions
    //public List<ActionColonyBuild> ActionsBuildPossible = new List<ActionColonyBuild>();
}
