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
    public DataBlock Type = null;
    [Export]
    public DataBlock Resources = null;
    [Export]
    public DataBlock Buildings = null;

    [ExportCategory("ColonyData-Links")]
    [Export]
    public PlanetData Planet = null;

    // resources
    public ResourcesWrapper Resources_PerTurn = null;

    public bool IsWorld()
    {
        return Type.ValueS == "World";
    }
}
