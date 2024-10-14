using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class DistrictQueueWrapper
{
    public class Info
    {
        public PlanetData Planet;
        public DistrictData District;
        public DataBlock QueueData;

        public int Progress = 0;

        public int EstimatedProgressNextTurn = 0;
        public int EstimatedTurns = 0;
    };

    public SystemData _System = null;
    public List<Info> DistrictsInQueue = new List<Info>();

    public DistrictQueueWrapper(SystemData system)
    {
        _System = system;
    
        //Refresh();
    }

    public void Refresh()
    {
        DistrictsInQueue.Clear();
        DataBlock queue = _System.Data.GetSub("ActionBuildDistrict").GetSub("Queue");
        int production = _System.Resources_PerTurn.GetIncome("Districts").IncomeAllTotal(_System);
        
        int overflow = _System.Data.GetSub("ActionBuildDistrict").GetSub("Overflow").ValueI;
        int turns = 0;

        Array<DataBlock> districtsInQueue = queue.Subs; // GetSubs("District");
        for (int idx = 0; idx < districtsInQueue.Count; idx++)
        {
            PlanetData planet = Data.GetLinkPlanetData( districtsInQueue[idx], Game.self.Map.Data);
            ColonyData colony = planet.Colony;
            if (colony == null)
            {
                GD.PrintErr("NO COLONY FOR QUEUE ?!?");
                continue;
            }

            DistrictData district = null;
            for (int colDisIdx = 0; colDisIdx < colony.Districts.Count; colDisIdx++) 
            { 
               if (colony.Districts[colDisIdx].Data.HasSub("InQueue") && colony.Districts[colDisIdx].Data.GetSub("InQueue").ValueI == idx)
                {
                    district = colony.Districts[colDisIdx];
                    break;
                }
            }

            Info info = new Info();
            info.QueueData = districtsInQueue[idx];
            info.District = district;
            info.Planet = planet;
            info.Progress = info.QueueData.GetSub("Progress").ValueI;

            int maxProgress = info.District.DistrictDef.Cost;

            int remaining = maxProgress - info.Progress;
            info.EstimatedProgressNextTurn = Mathf.Min(maxProgress, info.Progress + production + overflow);

            int t = Mathf.CeilToInt(1.0f * (remaining - overflow) / production);
            turns += t;
            overflow = t * production - (remaining - overflow);

            info.EstimatedTurns = turns;
            //info.Position = idx;

            DistrictsInQueue.Add(info);
        }
        //{
        //    DefDistrictWrapper buildingInfo = Game.Def.GetBuildingInfo(queue.Subs[idx].ValueS);
        //
        //    Info info = new Info();
        //    info.Sector = _Sector;
        //    info.Planet = Data.GetLinkPlanetData(queue.Subs[idx], Game.Map.Data);
        //info.BuildingOld = queue.Subs[idx].GetSub("OldBuilding");
        //info.BuildingPlanet = queue.Subs[idx].GetSub("PlanetBuilding");
        //info.BuildingDef = buildingInfo._Data;
        //if (info.Planet.Colony != null)
        //{
        //    info.BuildingReal = info.Planet.Colony.Buildings.GetSub("Building", info.BuildingDef.ValueS);
        //}
        //if (info.BuildingReal != null)
        //{ 
        //    DataBlock inConstruction = info.BuildingReal.GetSub("InConstruction");
        //    info.Progress = inConstruction.GetSub("Progress").ValueI;
        //    info.ProgressMax = inConstruction.GetSub("Progress:Max").ValueI;
        //    int progressNextTurn = 0;
        //    if (idx == 0) progressNextTurn = Mathf.Min(info.ProgressMax, info.Progress + production);
        //
        //    int remaining = info.ProgressMax - info.Progress;
        //    int t = Mathf.CeilToInt(1.0f * (remaining - overflow) / production);
        //    turns += t;
        //    overflow = t * production - (remaining - overflow);
        //}
        //else
        //{
        //    info.Progress = 0; 
        //    info.ProgressMax = queue.Subs[idx].GetSub("Progress:Max").ValueI;
        //    int progressNextTurn = 0;
        //    if (idx == 0) progressNextTurn = Mathf.Min(info.ProgressMax, production);
        //
        //    int remaining = info.ProgressMax;
        //    int t = Mathf.CeilToInt(1.0f * (remaining - overflow) / production);
        //    turns += t;
        //    overflow = t * production - (remaining - overflow);
        //}
        //info.Turns = turns;
        //info.Position = idx;
        //Buildings.Add(info);
        //}
    }

    public Info Get(string name, PlanetData planet)
    {
        //for (int idx = 0; idx < DistrictsInQueue.Count; idx++)
        //{
        //    if (DistrictsInQueue[idx].District.DistrictDef.Name == name && planet == DistrictsInQueue[idx].Planet)
        //    {
        //        return DistrictsInQueue[idx];
        //    }
        //}
        return null;
    }
}
