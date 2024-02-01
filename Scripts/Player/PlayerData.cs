using Godot;
using Godot.Collections;

// Generated
[Tool]
public partial class PlayerData : Node
{
    [ExportCategory("PlayerData-Node")]
    [Export]
    public PlayerNode _Node = null;

    [ExportCategory("PlayerData")]
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
    public bool Human = false;

    [ExportCategory("PlayerData-Links")]
    [Export]
    public Array<ColonyData> Colonies = new Array<ColonyData>();
    //[Export]
    //public Array<SystemData> LocationsOwned = new Array<SystemData>();
    //[Export]
    //public Array<PawnData> PawnsOwned = new Array<PawnData>();

    [ExportCategory("PlayerData-Runtime")]
    [Export]
    public bool TurnFinished = false;

    public ColonyData GetColony(string colony)
    {
        for (int idx = 0; idx < Colonies.Count; idx++)
        {
            if (Colonies[idx].ColonyName == colony)
            {
                return Colonies[idx];
            }
        }

        return null;
    }
}
