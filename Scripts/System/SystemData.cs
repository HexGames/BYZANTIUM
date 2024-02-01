using Godot;
using Godot.Collections;

// Generated
[Tool]
public partial class SystemData : Node
{
    [Export]
    public SystemNode _Node = null;
    [Export]
    public string SystemName;
    [Export]
    public int X;
    [Export]
    public int Y;

    [Export]
    public Array<DataBlock> Planets = new Array<DataBlock>();
    [Export]
    public Array<ColonyData> Colonies = new Array<ColonyData>();

    //[Export]
    //public Array<PawnData> PawnsInLocation = new Array<PawnData>();

    public ColonyData GetColony(DataBlock planet)
    {
        for (int idx = 0; idx < Colonies.Count; idx++)
        {
            if (Colonies[idx].Planet == planet)
            {
                return Colonies[idx];
            }
        }
        return null;
    }
}
