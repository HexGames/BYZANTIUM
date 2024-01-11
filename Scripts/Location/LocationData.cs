using Godot;
using Godot.Collections;

// Generated
public partial class LocationData : Node
{
    [Export]
    public int X;
    [Export]
    public int Y;

    [Export]
    public DataBlock System = null;

    [Export]
    public Array<PawnData> PawnsInLocation = new Array<PawnData>();
}
