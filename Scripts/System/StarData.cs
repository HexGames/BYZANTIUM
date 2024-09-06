using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class StarData : Node
{
    [Export]
    public DataBlock _Data = null;
    [Export]
    public StarNode _Node = null;
    [Export]
    public string StarName;
    [Export]
    public int X;
    [Export]
    public int Y;
    public int Z
    {
        get { return -X - Y; }
    }

    [Export]
    public Array<StarData> PathsTo = new Array<StarData>(); // not used yet
    [Export]
    public Array<PlanetData> Planets = new Array<PlanetData>();
    [Export]
    public SystemData System = null;

    [ExportCategory("Runtime")]
    [Export]
    public Array<FleetData> Fleets_PerTurn = new Array<FleetData>();


    // --------------------------------------------------------------------------------------------
    public PlanetData GetPlanet(string planet)
    {
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx].PlanetName == planet)
            {
                return Planets[idx];
            }
        }

        return null;
    }

    // --------------------------------------------------------------------------------------------
    public int DistanceTo(StarData otherStar)
    {
        return Mathf.Max(Mathf.Max(Mathf.Abs(X - otherStar.X), Mathf.Abs(Y - otherStar.Y)), Mathf.Abs(Z - otherStar.Z));
    }

    // --------------------------------------------------------------------------------------------
    private Array<FleetData> Fleets_Friendly = new Array<FleetData>();
    public Array<FleetData> GetFriendlyFleets(PlayerData currentPlayer)
    {
        Fleets_Friendly.Clear();

        for (int idx = 0; idx < Fleets_PerTurn.Count; idx++)
        {
            if (Fleets_PerTurn[idx]._Player == currentPlayer)
            {
                Fleets_Friendly.Add(Fleets_PerTurn[idx]);
            }
        }

        return Fleets_Friendly;
    }

    private Array<FleetData> Fleets_Neutral = new Array<FleetData>();
    public Array<FleetData> GetNeutralFleets(PlayerData currentPlayer)
    {
        Fleets_Neutral.Clear();

        for (int idx = 0; idx < Fleets_PerTurn.Count; idx++)
        {
            if (Fleets_PerTurn[idx]._Player != currentPlayer && Fleets_PerTurn[idx]._Player.IsAtWarWith(currentPlayer) == false)
            {
                Fleets_Neutral.Add(Fleets_PerTurn[idx]);
            }
        }

        return Fleets_Neutral;
    }

    private Array<FleetData> Fleets_Enemy = new Array<FleetData>();
    public Array<FleetData> GetEnemyFleets(PlayerData currentPlayer)
    {
        Fleets_Enemy.Clear();

        for (int idx = 0; idx < Fleets_PerTurn.Count; idx++)
        {
            if (Fleets_PerTurn[idx]._Player != currentPlayer && Fleets_PerTurn[idx]._Player.IsAtWarWith(currentPlayer) == true)
            {
                Fleets_Enemy.Add(Fleets_PerTurn[idx]);
            }
        }

        return Fleets_Enemy;
    }

    // --------------------------------------------------------------------------------------------
    //[Export]
    //public Array<PawnData> PawnsInLocation = new Array<PawnData>();

    //public ColonyData GetColony(DataBlock planet)
    //{
    //    for (int idx = 0; idx < Colonies.Count; idx++)
    //    {
    //        if (Colonies[idx].Planet == planet)
    //        {
    //            return Colonies[idx];
    //        }
    //    }
    //    return null;
    //}

    //public List<PlayerData> GetPlayersPresentinSystem()
    //{
    //    List<PlayerData> players = new List<PlayerData>();
    //    for (int idx = 0; idx < Colonies.Count; idx++)
    //    {
    //        if (players.Contains(Colonies[idx].Player) == false)
    //        {
    //            players.Add(Colonies[idx].Player);
    //        }
    //    }
    //    return players;
    //}    
}
