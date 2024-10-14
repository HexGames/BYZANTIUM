using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class DesignData : Node
{
    [ExportCategory("PlayerParent")]
    [Export]
    public PlayerData _Player = null;

    [ExportCategory("DesignRawData")]
    [Export]
    public DataBlock Data = null;

    [ExportCategory("DesignData")]
    [Export]
    public string DesignName = "";

    //[Export]
    //public DataBlock ShipsData = null;

    //[ExportCategory("SystemData-Actions")]
    //[Export]
    //public DataBlock ActionBuild = null;

    // actions
    //public List<ActionSectorBuild> ActionsBuildPossible = new List<ActionSectorBuild>();
}
