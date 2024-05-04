using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class PlanetData : Node
{
    [ExportCategory("PlanetParent")]
    [Export]
    public StarData _Star = null;

    [ExportCategory("PlanetRawData")]
    [Export]
    public DataBlock Data = null;

    [ExportCategory("PlanetData")]
    [Export]
    public string PlanetName = "";

    [Export]
    public DataBlock Empire = null;

    [ExportCategory("PlanetData-Links")]
    [Export]
    public ColonyData Colony = null;
}
