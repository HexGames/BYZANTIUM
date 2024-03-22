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
    static public DataBlock AddLink(DataBlock parent, SectorData sector, DefLibrary df)
    {
        string name = "Link:Player:Sector";
        return AddData(parent, name, sector._Player.PlayerName + ":" + sector.SectorName, df);
    }
    static public DataBlock AddLink(DataBlock parent, SystemData system, DefLibrary df)
    {
        string name = "Link:Player:Sector:System";
        return AddData(parent, name, system._Sector._Player.PlayerName + ":" + system._Sector.SectorName + ":" + system.SystemName, df);
    }
    static public DataBlock AddLink(DataBlock parent, ColonyData colony, DefLibrary df)
    {
        string name = "Link:Player:Sector:System:Colony";
        return AddData(parent, name, colony._System._Sector._Player.PlayerName + ":" + colony._System._Sector.SectorName + ":" + colony._System.SystemName + ":" + colony.ColonyName, df);
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
    static public SectorData GetLinkSectorData(DataBlock parent, MapData map)
    {
        DataBlock link = parent.GetLink("Link:Player:Sector");
        string[] split = link.ValueS.Split(":");
        return map.GetPlayer(split[0]).GetSector(split[1]);
    }
    static public SystemData GetLinkSystemData(DataBlock parent, MapData map)
    {
        DataBlock link = parent.GetLink("Link:Player:Sector:System");
        string[] split = link.ValueS.Split(":");
        return map.GetPlayer(split[0]).GetSector(split[1]).GetSystem(split[2]);
    }
    static public ColonyData GetLinkColonyData(DataBlock parent, MapData map)
    {
        DataBlock link = parent.GetLink("Link:Player:Sector:System:Colony");
        string[] split = link.ValueS.Split(":");
        return map.GetPlayer(split[0]).GetSector(split[1]).GetSystem(split[2]).GetColony(split[3]);
    }
}
