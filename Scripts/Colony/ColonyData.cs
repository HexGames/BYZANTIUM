using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class ColonyData : Node
{
    [ExportCategory("ColonyData-Node")]
    [Export]
    public ColonyNode _Node = null;

    [ExportCategory("ColonyData-Node")]
    [Export]
    public DataBlock DataBlock = null;

    [ExportCategory("ColonyData")]
    [Export]
    public string ColonyName = "";

    [Export]
    public DataBlock Resources = null;
    [Export]
    public DataBlock Buildings = null;
    [Export]
    public DataBlock Support = null;
    [Export]
    public DataBlock Bonuses = null;

    [ExportCategory("ColonyData-Actions")]
    [Export]
    public DataBlock ActionBuild = null;

    [ExportCategory("ColonyData-Links")]
    [Export]
    public SystemData System = null;
    [Export]
    public DataBlock Planet = null;

    // actions
    public List<ActionColonyBuild> ActionsBuildPossible = new List<ActionColonyBuild>();
}
