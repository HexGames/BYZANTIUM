using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class ShipData : Node
{
    [ExportCategory("FleetParent")]
    [Export]
    public FleetData _Fleet = null;

    [ExportCategory("ShipRawData")]
    [Export]
    public DataBlock Data = null;

    [ExportCategory("ShipData")]
    [Export]
    public string ShipName = "";

    [Export]
    public DataBlock HP = null;

    [Export]
    public DataBlock Design = null;

    //[ExportCategory("SystemData-Actions")]
    //[Export]
    //public DataBlock ActionBuild = null;

    // actions
    //public List<ActionSectorBuild> ActionsBuildPossible = new List<ActionSectorBuild>();
}
