using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Data
{
    static public DataBlock AddLink(DataBlock parent, StarData star, DefLibrary df)
    {
        string name = "Link:Star";
        return AddData(parent, name, star.StarName, df);
    }
    static public DataBlock AddLink(DataBlock parent, PlanetData planet, DefLibrary df)
    {
        string name = "Link:Star:Planet";
        return AddData(parent, name, planet._Star.StarName + ":" + planet.PlanetName, df);
    }
    static public DataBlock AddLink(DataBlock parent, PlayerData player, DefLibrary df)
    {
        string name = "Link:Player";
        return AddData(parent, name, player.PlayerName, df);
    }
    static public DataBlock AddLink(DataBlock parent, SystemData system, DefLibrary df)
    {
        string name = "Link:Player:Sector:System";
        return AddData(parent, name, system._Player.PlayerName + ":" + system.SystemName, df);
    }
    static public DataBlock AddLink(DataBlock parent, ColonyData colony, DefLibrary df)
    {
        string name = "Link:Player:Sector:System:Colony";
        return AddData(parent, name, colony._System._Player.PlayerName + ":" + colony._System.SystemName + ":" + colony.ColonyName, df);
    }
    static public DataBlock AddLink(DataBlock parent, FleetData fleet, DefLibrary df)
    {
        string name = "Link:Player:Fleet";
        return AddData(parent, name, fleet._Player.PlayerName + ":" + fleet.FleetName, df);
    }

    static public StarData GetLinkStarData(DataBlock parent, MapData map)
    {
        DataBlock link = parent.GetLink("Link:Star");
        return map.GetStar(link.ValueS);
    }
    static public PlanetData GetLinkPlanetData(DataBlock parent, MapData map)
    {
        DataBlock link = parent.GetLink("Link:Star:Planet");
        string[] split = link.ValueS.Split(":");
        return map.GetStar(split[0]).GetPlanet(split[1]);
    }
    static public PlayerData GetLinkPlayerData(DataBlock parent, MapData map)
    {
        DataBlock link = parent.GetLink("Link:Player");
        return map.GetPlayer(link.ValueS);
    }
    static public SystemData GetLinkSystemData(DataBlock parent, MapData map)
    {
        DataBlock link = parent.GetLink("Link:Player:Sector:System");
        string[] split = link.ValueS.Split(":");
        return map.GetPlayer(split[0]).GetSystem(split[1]);
    }
    static public ColonyData GetLinkColonyData(DataBlock parent, MapData map)
    {
        DataBlock link = parent.GetLink("Link:Player:Sector:System:Colony");
        string[] split = link.ValueS.Split(":");
        return map.GetPlayer(split[0]).GetSystem(split[1]).GetColony(split[2]);
    }

    static public Array<FleetData> GetLinkFleetsData(DataBlock parent, MapData map)
    {
        Array<FleetData> fleets = new Array<FleetData>();
        Array<DataBlock> links = parent.GetLinks("Link:Player:Fleet");
        for (int idx = 0; idx < links.Count; idx++)
        {
            string[] split = links[idx].ValueS.Split(":");
            fleets.Add(map.GetPlayer(split[0]).GetFleet(split[1]));
        }

        return fleets;
    }
}
