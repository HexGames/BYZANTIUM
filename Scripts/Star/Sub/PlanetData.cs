using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class PlanetData : Node
{
    [ExportCategory("GFX")]
    [Export]
    public GFXStarOrbit GFX = null;
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
    public int Angle;
    //[Export]
    //public DataBlock Resources = null;

    [Export]
    public DataBlock Empire = null;

    [ExportCategory("PlanetData-Links")]
    [Export]
    public ColonyData Colony = null;

    // [--- ("PlanetData-Session") ---]
    // features
    public List<FeatureData> Features = new List<FeatureData>();

    public bool IsHabitable()
    {
        return PlanetRaw.GetBaseMaxPops(Data, Game.self.Def) > 0;
    }

    // --------------------------------------------------------------------------------------------
    public bool IsNotStar()
    {
        return Data.HasSub("Star_Type");
    }    
}
