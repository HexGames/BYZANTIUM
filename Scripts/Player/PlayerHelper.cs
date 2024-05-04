using Godot;
using System.Windows.Markup;

public class PlayerHelper
{
    static public int GetNewStarbaseCost(Game game, PlayerData player, PlanetData planet, out SectorData sector)
    {
        int cost = game.Def.GetBuildingInfo("Starport").Cost.Get("Production").Value_1;

        // find sector
        sector = player.Sectors[0];

        return cost;
    }

    static public int GetNewColonyCost(Game game, SectorData sector, PlanetData planet)
    {
        int cost = game.Def.GetBuildingInfo("Outpost").Cost.Get("Production").Value_1;

        return cost;
    }

    static public int GetBuildTime(Game game, SectorData sector, int cost)
    {
        if (sector.ActionBuildQueue.GetSubs("Building").Count == 0)
        {
            cost -= sector.ActionBuildQueue.GetSub("Overflow").ValueI;
        }

        int turns = Mathf.CeilToInt(1.0f * cost / sector.Resources_PerTurn.Get("Production").Value_2);

        return turns;
    }
}
