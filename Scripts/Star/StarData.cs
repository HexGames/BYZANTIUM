using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class StarData : Node
{
    [Export]
    public DataBlock Data = null;
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
    [Export]
    public DataBlock ActionData = null;

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
    public Array<FleetData> GetFriendlyFleets(PlayerData currentPlayer, bool includeHidden = false)
    {
        Fleets_Friendly.Clear();

        for (int idx = 0; idx < Fleets_PerTurn.Count; idx++)
        {
            if (Fleets_PerTurn[idx]._Player == currentPlayer)
            {
                if (includeHidden || Fleets_PerTurn[idx].Data.HasSub("ActionColonize") == false)
                {
                    Fleets_Friendly.Add(Fleets_PerTurn[idx]);
                }
            }
        }

        return Fleets_Friendly;
    }

    private Array<FleetData> Fleets_Neutral = new Array<FleetData>();
    public Array<FleetData> GetNeutralFleets(PlayerData currentPlayer, bool includeHidden = false)
    {
        Fleets_Neutral.Clear();

        for (int idx = 0; idx < Fleets_PerTurn.Count; idx++)
        {
            if (Fleets_PerTurn[idx]._Player != currentPlayer && Fleets_PerTurn[idx]._Player.IsAtWarWith(currentPlayer) == false)
            {
                if (includeHidden || Fleets_PerTurn[idx].Data.HasSub("ActionColonize") == false)
                {
                    Fleets_Neutral.Add(Fleets_PerTurn[idx]);
                }
            }
        }

        return Fleets_Neutral;
    }

    private Array<FleetData> Fleets_Enemy = new Array<FleetData>();
    public Array<FleetData> GetEnemyFleets(PlayerData currentPlayer, bool includeHidden = false)
    {
        Fleets_Enemy.Clear();

        for (int idx = 0; idx < Fleets_PerTurn.Count; idx++)
        {
            if (Fleets_PerTurn[idx]._Player != currentPlayer && Fleets_PerTurn[idx]._Player.IsAtWarWith(currentPlayer) == true)
            {
                if (includeHidden || Fleets_PerTurn[idx].Data.HasSub("ActionColonize") == false)
                {
                    Fleets_Enemy.Add(Fleets_PerTurn[idx]);
                }
            }
        }

        return Fleets_Enemy;
    }
    // --------------------------------------------------------------------------------------------
    public List<PlanetData> GetHabitablePlanets()
    {
        List<PlanetData> habitables = new List<PlanetData>();
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx].IsHabitable())
            {
                habitables.Add(Planets[idx]);
            }
        }
        return habitables;
    }

    // --------------------------------------------------------------------------------------------
    public int GetPopsMax()
    {
        int pops = 0;
        for (int planetIdx = 0; planetIdx < Planets.Count; planetIdx++)
        {
            pops += Planets[planetIdx].Data.GetSubValueI("PopsMax");
        }

        return pops;
    }


    //public int GetFirendlySupply(PlayerData currentPlayer)
    //{
    //    Array<FleetData> fleetsFriendly = GetFriendlyFleets(currentPlayer);
    //
    //    int supply = 0;
    //    for (int idx = 0; idx < fleetsFriendly.Count; idx++)
    //    {
    //        supply += fleetsFriendly[idx].Stats_PerTurn.Supply;
    //    }
    //
    //    return supply;
    //}

    //public void UseFirendlySupply(PlayerData currentPlayer, int value)
    //{
    //    Array<FleetData> fleetsFriendly = GetFriendlyFleets(currentPlayer);
    //
    //    if (fleetsFriendly.Count == 0)
    //        return;
    //
    //    int supply = GetFirendlySupply(currentPlayer);
    //
    //    for (int idx = 0; idx < fleetsFriendly.Count; idx++)
    //    {
    //        int toSubstract = Mathf.Min(fleetsFriendly[idx].Stats_PerTurn.Supply, value * fleetsFriendly[idx].Stats_PerTurn.Supply / supply);
    //        fleetsFriendly[idx].Stats_PerTurn.Supply -= toSubstract;
    //        supply -= toSubstract;
    //    }
    //
    //    int maxTrys = 1000;
    //    while (supply > 0 && maxTrys > 0)
    //    {
    //        int randIdx = Game.self.RNG.RandiRange(0, fleetsFriendly.Count);
    //        if (fleetsFriendly[randIdx].Stats_PerTurn.Supply > 0)
    //        {
    //            fleetsFriendly[randIdx].Stats_PerTurn.Supply--;
    //            supply--;
    //        }
    //        maxTrys--;
    //    }
    //
    //    if (maxTrys == 0) GD.PrintErr("Use supply overflow");
    //}

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
