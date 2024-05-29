using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class BuildingQueueWrapper
{
    public class Info
    {
        public SectorData Sector;
        public PlanetData Planet;
        public DataBlock BuildingDef;
        public DataBlock BuildingReal;
        public DataBlock BuildingOld;
        public DataBlock BuildingPlanet;

        public int Position = 0;
        public int Progress = 0;
        public int ProgressNextTurn = 0;
        public int ProgressMax = 0;
        public int Turns = 0;
    };

    private SectorData _Sector = null;
    public List<Info> Buildings = new List<Info>();

    Game Game = null;

    public BuildingQueueWrapper(SectorData sector, Game game)
    {
        _Sector = sector;
        Game = game;

        //Refresh();
    }

    public void Refresh()
    {
        Buildings.Clear();
        DataBlock queue = _Sector.ActionBuildQueue.GetSub("Queue");
        int production = _Sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal();

        int overflow = _Sector.ActionBuildQueue.GetSub("Overflow").ValueI;
        int turns = 0;

        for (int idx = 0; idx < queue.Subs.Count; idx++)
        {
            DefBuildingWrapper buildingInfo = Game.Def.GetBuildingInfo(queue.Subs[idx].ValueS);

            Info info = new Info();
            info.Sector = _Sector;
            info.Planet = Data.GetLinkPlanetData(queue.Subs[idx], Game.Map.Data);
            info.BuildingOld = queue.Subs[idx].GetSub("OldBuilding");
            info.BuildingPlanet = queue.Subs[idx].GetSub("PlanetBuilding");
            info.BuildingDef = buildingInfo._Data;
            if (info.Planet.Colony != null)
            {
                info.BuildingReal = info.Planet.Colony.Buildings.GetSub("Building", info.BuildingDef.ValueS);
            }
            if (info.BuildingReal != null)
            { 
                DataBlock inConstruction = info.BuildingReal.GetSub("InConstruction");
                info.Progress = inConstruction.GetSub("Progress").ValueI;
                info.ProgressMax = inConstruction.GetSub("Progress:Max").ValueI;
                int progressNextTurn = 0;
                if (idx == 0) progressNextTurn = Mathf.Min(info.ProgressMax, info.Progress + production);

                int remaining = info.ProgressMax - info.Progress;
                int t = Mathf.CeilToInt(1.0f * (remaining - overflow) / production);
                turns += t;
                overflow = t * production - (remaining - overflow);
            }
            else
            {
                info.Progress = 0; 
                info.ProgressMax = queue.Subs[idx].GetSub("Progress:Max").ValueI;
                int progressNextTurn = 0;
                if (idx == 0) progressNextTurn = Mathf.Min(info.ProgressMax, production);

                int remaining = info.ProgressMax;
                int t = Mathf.CeilToInt(1.0f * (remaining - overflow) / production);
                turns += t;
                overflow = t * production - (remaining - overflow);
            }
            info.Turns = turns;
            info.Position = idx;
            Buildings.Add(info);
        }
    }

    public Info Get(string name, PlanetData planet)
    {
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            if (Buildings[idx].BuildingDef.ValueS == name && planet == Buildings[idx].Planet)
            {
                return Buildings[idx];
            }
        }
        return null;
    }
    public Info GetUpgrade(string name, PlanetData planet)
    {
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            //Buildings[idx].
            if ((Buildings[idx].BuildingOld != null && Buildings[idx].BuildingOld.ValueS == name) || (Buildings[idx].BuildingPlanet != null && Buildings[idx].BuildingPlanet.ValueS == name) && planet == Buildings[idx].Planet)
            {
                return Buildings[idx];
            }
        }
        return null;
    }
}
