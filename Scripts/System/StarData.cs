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

    [Export]
    public Array<StarData> PathsTo = new Array<StarData>(); // not used yet
    [Export]
    public Array<PlanetData> Planets = new Array<PlanetData>();
    [Export]
    public SystemData System = null;


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
