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
    public DataBlock Pops = null;
    [Export]
    public DataBlock Buildings = null;
    [Export]
    public DataBlock Resources = null;

    [ExportCategory("ColonyData-Links")]
    [Export]
    public PlanetData Planet = null;

    // [--- ("ColonyData-Session") ---]
    // districts
    public List<DistrictData> Districts = new List<DistrictData>();

    // [--- ("ColonyData-Per_Turn") ---]
    // resources
    public ResourcesWrapper Resources_PerTurn = null;
    // pops
    public PopsWrapper Pops_PerTurn = null;
    // buildings
    public BuildingsWrapper Buildings_PerTurn = null;

    public bool IsWorld()
    {
        return Type.ValueS == "World";
    }
}
