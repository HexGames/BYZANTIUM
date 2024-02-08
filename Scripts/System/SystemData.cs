using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class SystemData : Node
{
    [Export]
    public DataBlock _Data = null;
    [Export]
    public SystemNode _Node = null;
    [Export]
    public string SystemName;
    [Export]
    public int X;
    [Export]
    public int Y;

    [Export]
    public Array<SystemData> PathsTo = new Array<SystemData>();
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

    public List<PlayerData> GetPlayersPresentinSystem()
    {
        List<PlayerData> players = new List<PlayerData>();
        for (int idx = 0; idx < Colonies.Count; idx++)
        {
            if (players.Contains(Colonies[idx].Player) == false)
            {
                players.Add(Colonies[idx].Player);
            }
        }
        return players;
    }
}
