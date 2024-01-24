using Godot;
using Godot.Collections;

// Generated
[Tool]
public partial class PlayerData : Node
{
    [Export]
    public string PlayerName = "";

    [Export]
    public DataBlock Resources = null;
    [Export]
    public DataBlock Status = null;
    [Export]
    public DataBlock Civics = null;
    [Export]
    public DataBlock Bonuses = null;
    [Export]
    public Array<DataBlock> Colonies = new Array<DataBlock>();

    [Export]
    public Array<LocationData> LocationsOwned = new Array<LocationData>();

    [Export]
    public Array<PawnData> PawnsOwned = new Array<PawnData>();

    [Export]
    public bool Human = false;
    [Export]
    public bool TurnFinished = false;

    public DataBlock GetColony(string colony)
    {
        for (int idx = 0; idx < Colonies.Count; idx++)
        {
            if (Colonies[idx].ValueS == colony)
            {
                return Colonies[idx];
            }
        }

        return null;
    }
}
