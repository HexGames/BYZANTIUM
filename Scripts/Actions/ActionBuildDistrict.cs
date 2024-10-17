using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionBuildDistrict
{
    static public void RefreshAvailableDistricts(Game game, SystemData system)
    {
        system.ActionsBuildPossible_PerTurn.Clear();

        for (int planetIdx = 0; planetIdx < system.Star.Planets.Count; planetIdx++)
        {
            PlanetData planet = system.Star.Planets[planetIdx];
            if (planet.Data.HasSub("SlotType"))
            {
                string districtType = planet.Data.GetSub("SlotType").ValueS;
                if (planet.Colony != null && planet.Data.HasSub("Districts"))
                {
                    Array<DataBlock> districtSlots = planet.Data.GetSub("Districts").GetSubs("District");
                    for (int districtIdx = 0; districtIdx < districtSlots.Count; districtIdx++)
                    {
                        if (districtSlots[districtIdx].ValueS == null)
                        {
                            bool hasPops = true;
                            if (districtSlots[districtIdx].HasSub("RequiredPops"))
                            {
                                hasPops = districtSlots[districtIdx].GetSub("RequiredPops").ValueI <= planet.Colony.Pops_PerTurn.GetPops();
                            }
                            if (hasPops)
                            {
                                for (int defIdx = 0; defIdx < game.Def.DistrictsInfo.Count; defIdx++)
                                {
                                    if (game.Def.DistrictsInfo[defIdx].Type == districtType)
                                    {
                                        system.ActionsBuildPossible_PerTurn.Add(new DistrictData(game.Def.DistrictsInfo[defIdx], districtSlots[districtIdx], planet));
                                    }
                                }
                            }
                        }
                    }
                }
                else if (planet.Data.HasSub("SlotType"))
                {
                    for (int defIdx = 0; defIdx < game.Def.DistrictsInfo.Count; defIdx++)
                    {
                        if (game.Def.DistrictsInfo[defIdx].Type == districtType)
                        {
                            system.ActionsBuildPossible_PerTurn.Add(new DistrictData(game.Def.DistrictsInfo[defIdx], planet));
                        }
                    }
                }
            }
        }
    }

    static public void AddToQueue(Game game, DistrictData chosenDistrict)
    {
        PlanetData planet = chosenDistrict._Planet;
        SystemData system = planet._Star.System;
        if (system == null)
            return;

        DataBlock queue = system.Data.GetSub("ActionBuildDistrict").GetSub("Queue");
        int placeInQueue = queue.Subs.Count; // GetSubs("District").Count;
        DataBlock district = Data.AddData(queue, "District", chosenDistrict.DistrictDef.Name, game.Def);
        Data.AddLink(district, chosenDistrict._Planet, game.Def);
        Data.AddData(district, "Progress", 0, game.Def);

        if (planet.Colony == null)
        {
            // new colony
            DataBlock colonyData = MapGenerator.CreateNewColony(planet._Star.Data, planet.Data, system._Player.Data, system.Data, -1, Game.self.Def, false);

            // ---
            ColonyData colony = game.Map.Data.GenerateGameFromData_Player_System_Colony(colonyData, system);

            colony.Districts.Clear();
            Array<DataBlock> districts = colony.Data.GetSub("Districts").GetSubs("District");
            for (int districtIdx = 0; districtIdx < districts.Count; districtIdx++)
            {
                colony.Districts.Add(new DistrictData(districts[districtIdx], colony)); // 5
            }

            //system.Colonies.Add(colony);

            colony.Resources_PerTurn = new ResourcesWrapper(colony.Resources, ResourcesWrapper.ParentType.COLONY);
            colony.Pops_PerTurn = new PopsWrapper(colony);
            colony.Buildings_PerTurn = new BuildingsWrapper(colony);
        }

        DistrictData colonyDistrict = null;
        for (int districtIdx = 0; districtIdx < planet.Colony.Districts.Count; districtIdx++)
        {
            if (planet.Colony.Districts[districtIdx].Data.ValueS == "")
            {
                colonyDistrict = planet.Colony.Districts[districtIdx];
                break;
            }
        }

        colonyDistrict.Data.SetValueS(chosenDistrict.DistrictDef.Name, game.Def);
        colonyDistrict.DistrictDef = chosenDistrict.DistrictDef;
        Data.AddData(colonyDistrict.Data, "InQueue", placeInQueue, game.Def);
        Data.DeleteDataSub(colonyDistrict.Data, "RequiredPops");

        system.DistrictsQueue_PerTurn.Refresh();

        planet.GFX.GUI3D.Refresh();
        Game.self.GalaxyUI.SystemInfo.ProductionInfo.Refresh(system);
    }

    static public void EndTurn(Game game, SystemData system)
    {
        DataBlock queue = system.Data.GetSub("ActionBuildDistrict").GetSub("Queue");
        int overflow = system.Data.GetSub("ActionBuildDistrict").GetSub("Overflow").ValueI;
        int production = system.Resources_PerTurn.GetIncome("Districts").IncomeAllTotal(system);

        Array<DataBlock> districtsInQueue = queue.Subs; // GetSubs("District");
        if (production > 0 && districtsInQueue.Count > 0)
        {
            PlanetData planet = Data.GetLinkPlanetData(queue.Subs[0], game.Map.Data);
            ColonyData colony = planet.Colony;
            if (colony == null)
            {
                GD.PrintErr("NO COLONY FOR QUEUE ?!?");
                return;
            }

            DistrictData district = null;
            for (int colDisIdx = 0; colDisIdx < colony.Districts.Count; colDisIdx++)
            {
                if (colony.Districts[colDisIdx].Data.HasSub("InQueue") && colony.Districts[colDisIdx].Data.GetSub("InQueue").ValueI == 0)
                {
                    district = colony.Districts[colDisIdx];
                    break;
                }
            }

            int progress = districtsInQueue[0].GetSub("Progress").ValueI;
            int maxProgress = district.DistrictDef.Cost;

            if (progress + production + overflow >= maxProgress)
            {
                queue.Subs.RemoveAt(0);
                Data.DeleteDataSub(district.Data, "InQueue");

                system.Data.GetSub("ActionBuildDistrict").GetSub("Overflow").SetValueI(progress + production + overflow - maxProgress, game.Def);
            }
            else
            {
                districtsInQueue[0].GetSub("Progress").SetValueI(progress + production + overflow, game.Def); // <-- add production to progress
                system.Data.GetSub("ActionBuildDistrict").GetSub("Overflow").SetValueI(0, game.Def);
            }
        }
    }

    //static public DefDistrictWrapper GetAvailableBuilding(Game game, SectorData sector, string buildingName, PlanetData planet)
    //{
    //    DataBlock buildingDef = game.Def.GetBuilding(buildingName);
    //    for (int idx = 0; idx < sector.AvailableBuildings_PerTurn.Count; idx++)
    //    {
    //        if (sector.AvailableBuildings_PerTurn[idx]._Data == buildingDef && sector.AvailableBuildings_PerTurn[idx]._Planet == planet)
    //        {
    //            return sector.AvailableBuildings_PerTurn[idx];
    //        }
    //    }
    //    return null;
    //}

    //static public void AddToQueue(Game game, SectorData sector, string buildingName, PlanetData planet)
    //{
    //    AddToQueue(game, GetAvailableBuilding(game, sector, buildingName, planet));
    //}
    //
    //static public void AddToQueue(Game game, DefDistrictWrapper possibleBuilding)
    //{
    //    DataBlock queue = possibleBuilding._Sector.ActionBuildQueue.GetSub("Queue");
    //    DataBlock building = Data.AddData(queue, "Building", possibleBuilding.Name, game.Def);
    //    Data.AddLink(building, possibleBuilding._Planet, game.Def);
    //    Data.AddData(building, "Progress:Max", possibleBuilding.Cost, game.Def);
    //
    //    if (possibleBuilding._BuildingOld != null)
    //    {
    //        Data.AddData(building, "OldBuilding", possibleBuilding._BuildingOld.ValueS, game.Def);
    //        Data.AddData(possibleBuilding._BuildingOld, "NewBuilding", possibleBuilding.Name, game.Def);
    //    }
    //    else if (possibleBuilding._BuildingPlanet != null)
    //    {
    //        Data.AddData(building, "PlanetBuilding", possibleBuilding._BuildingPlanet.ValueS, game.Def);
    //    }
    //
    //    possibleBuilding._Sector.BuildQueue_PerTurn_ActionChange.Refresh();
    //}

    /*static public DataBlock GetBuildingInQueue(Game game, PlayerData player, string buildingName, PlanetData planet, out int positionInQueue, out int queueSize)
    {
        if (planet._Star.System != null)
        {
            SectorData sector = planet._Star.System._Sector;
            for (int idx = 0; idx < sector.ActionBuildQueue.Subs.Count; idx++)
            {
                if (sector.ActionBuildQueue.Subs[idx].ValueS == buildingName)
                {
                    queueSize = sector.ActionBuildQueue.Subs.Count;
                    positionInQueue = idx;
                    return sector.ActionBuildQueue.Subs[idx];
                }
            }
        }
        else
        {
            for (int sectorIdx = 0; sectorIdx < player.Sectors.Count; sectorIdx++)
            {
                SectorData sector = player.Sectors[sectorIdx];
                for (int idx = 0; idx < sector.ActionBuildQueue.Subs.Count; idx++)
                {
                    if (sector.ActionBuildQueue.Subs[idx].ValueS == buildingName)
                    {
                        queueSize = sector.ActionBuildQueue.Subs.Count;
                        positionInQueue = idx;
                        return sector.ActionBuildQueue.Subs[idx];
                    }
                }
            }
        }
        queueSize = 0;
        positionInQueue = 0;
        return null;
    }*/

    //static public void ReorderInQueue(Game game, SectorData sector, string buildingName, PlanetData planet, int orderChange)
    //{
    //    DataBlock queue = sector.ActionBuildQueue.GetSub("Queue");
    //    for (int idx = 0; idx < queue.Subs.Count; idx++)
    //    {
    //        string queueName = queue.Subs[idx].ValueS;
    //        PlanetData queuePlanet = Data.GetLinkPlanetData(queue.Subs[idx], game.Map.Data);
    //        if (queue.Subs[idx].Name == "Building" && buildingName == queueName && planet == queuePlanet)
    //        {
    //            DataBlock data = queue.Subs[idx];
    //            queue.Subs.RemoveAt(idx);
    //            queue.Subs.Insert(Mathf.Clamp(idx + orderChange, 0, queue.Subs.Count - 1), data);
    //            break;
    //        }
    //    }
    //
    //    sector.BuildQueue_PerTurn_ActionChange.Refresh();
    //}
    //
    //static public void DeleteFromQueue(Game game, SectorData sector, string buildingName, PlanetData planet)
    //{
    //    DataBlock queue = sector.ActionBuildQueue.GetSub("Queue");
    //    for (int idx = 0; idx < queue.Subs.Count; idx++)
    //    {
    //        string queueName = queue.Subs[idx].ValueS;
    //        PlanetData queuePlanet = Data.GetLinkPlanetData(queue.Subs[idx], game.Map.Data);
    //        if (queue.Subs[idx].Name == "Building" && buildingName == queueName && planet == queuePlanet)
    //        {
    //            DataBlock data = queue.Subs[idx];
    //            queue.Subs.RemoveAt(idx);
    //            break;
    //        }
    //    }
    //
    //    sector.BuildQueue_PerTurn_ActionChange.Refresh();
    //}

    //static public void EndTurn(Game game, SectorData sector)
    //{
    //    DataBlock overflow = sector.ActionBuildQueue.GetSub("Overflow");
    //    int production = sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal() + overflow.ValueI;
    //
    //    DataBlock queue = sector.ActionBuildQueue.GetSub("Queue");
    //
    //    //if (queue.Subs.Count == 0)
    //    //{
    //    //    var bc = sector._Player.Resources_PerTurn.Get("BC");
    //    //    bc.Value_1 += production / 2;
    //    //    bc.SaveValue();
    //    //    overflow.ValueI = 0;
    //    //    return;
    //    //}
    //
    //    while (production > 0 && queue.Subs.Count > 0)
    //    {
    //        PlanetData planet = Data.GetLinkPlanetData(queue.Subs[0], game.Map.Data);
    //
    //        if (planet.Colony == null)
    //        {
    //            SystemData system = planet._Star.System;
    //            DataBlock colonyList = system.Data.GetSub("Colony_List");
    //
    //            DataBlock colonyData = Data.AddData(colonyList, "Colony", planet.PlanetName, game.Def);
    //            Data.AddData(colonyData, "Type", "Outpost", game.Def);
    //            MapGenerator.Create_Colony_Resources(colonyData, game.Def);
    //            MapGenerator.Create_Colony_Buildings(colonyData, game.Def);
    //
    //            // ---
    //            // order maters x3
    //            Data.AddLink(colonyData, planet, game.Def); // 1
    //            ColonyData colony = game.Map.Data.GenerateGameFromData_Player_System_Colony(colonyData, system); // 2
    //            Data.AddLink(planet.Data, planet.Colony, game.Def);  // 3
    //
    //            system.Colonies.Add(colony);
    //            //colony.Resources_PerTurn = new ResourcesWrapper(colony.Resources, ResourcesWrapper.ParentType.Colony);
    //        }
    //
    //        //DataBlock building = planet.Colony.Buildings.GetSub("Building", queue.Subs[0].ValueS);
    //        //bool completed = false;
    //        //if (building == null)
    //        //{
    //        //    building = Data.AddData(planet.Colony.Buildings, "Building", queue.Subs[0].ValueS, game.Def);
    //        //
    //        //    int progressMax = queue.Subs[0].GetSub("Progress:Max").ValueI;
    //        //    DataBlock inConstruction = Data.AddData(building, "InConstruction", game.Def);
    //        //    Data.AddData(inConstruction, "Progress:Max", progressMax, game.Def);
    //        //    if (production >= progressMax)
    //        //    {
    //        //        production -= progressMax;
    //        //        completed = true;
    //        //    }
    //        //    else
    //        //    {
    //        //        Data.AddData(inConstruction, "Progress", production, game.Def);
    //        //        production = 0;
    //        //    }
    //        //}
    //        //else
    //        //{
    //        //    DataBlock inConstruction = building.GetSub("InConstruction");
    //        //    DataBlock progressData = inConstruction.GetSub("Progress");
    //        //    int progressCurrent = progressData.ValueI;
    //        //    int progressMax = inConstruction.GetSub("Progress:Max").ValueI;
    //        //    int remaining = progressMax - progressCurrent;
    //        //    if (production >= remaining)
    //        //    {
    //        //        production -= remaining;
    //        //        completed = true;
    //        //    }
    //        //    else
    //        //    {
    //        //        progressData.ValueI += production;
    //        //        production = 0;
    //        //    }
    //        //}
    //
    //        //if (completed)
    //        //{
    //        //    if (queue.Subs[0].GetSub("OldBuilding") != null)
    //        //    {
    //        //        Data.RemoveData(planet.Colony.Buildings, "Building", queue.Subs[0].GetSub("OldBuilding").ValueS, game.Def);
    //        //    }
    //        //    Data.RemoveData(building, "InConstruction", game.Def);
    //        //    sector.ActionBuildQueue.Subs.Remove(queue.Subs[0]);
    //        //    queue.Subs.RemoveAt(0);
    //        //}
    //    }
    //
    //    overflow.ValueI = production;
    //}
}