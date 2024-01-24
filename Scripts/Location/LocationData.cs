using Godot;
using Godot.Collections;

// Generated
[Tool]
public partial class LocationData : Node
{
    [Export]
    public string LocationName;
    [Export]
    public int X;
    [Export]
    public int Y;

    [Export]
    public DataBlock System = null;

    [Export]
    public Array<PawnData> PawnsInLocation = new Array<PawnData>();

    public LocationNode GetLocationNode()
    {
        return GetParent<LocationNode>();
    }
}
