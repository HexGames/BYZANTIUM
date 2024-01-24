using Godot;
using System.Collections.Generic;

public partial class Data
{
    static public PlayerData GetPlayer(MapData mapData, DataBlock planet)
    {
        DataBlock link = planet.GetLink("Link:Player");
        if (link == null) return null;

        string player = Helper.Split_0(link.ValueS);

        for (int idx = 0; idx < mapData.Players.Count; idx++)
        {
            if (mapData.Players[idx].PlayerName == player)
            {
                return mapData.Players[idx];
            }
        }

        return null;
    }

    static public DataBlock GetPlayerColony(MapData mapData, DataBlock planet, out PlayerData player)
    {
        player = GetPlayer(mapData, planet);

        if (planet == null) return null;

        DataBlock link = planet.GetLink("Link:Player:Colony");
        if (link == null) return null;

        string colony = Helper.Split_1(link.ValueS);

        for (int idx = 0; idx < player.Colonies.Count; idx++)
        {
            if (player.Colonies[idx].ValueS == colony)
            {
                return player.Colonies[idx];
            }
        }

        return null;
    }

    static public DataBlock GetPlayerColony(MapData mapData, DataBlock planet)
    {
        PlayerData playerData;
        return GetPlayerColony(mapData, planet, out playerData);
    }
}
