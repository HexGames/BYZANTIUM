﻿using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionShipbuilding
{
    static public void EndTurn(Game game, SystemData system)
    {
        DesignData design = system._Player.GetDesign(system.Data.GetSub("ActionBuildShip").GetSub("Design").ValueS);
        DataBlock actionBuildShip = system.Data.GetSub("ActionBuildShip");
        int progress = actionBuildShip.GetSub("Progress").ValueI;
        int overflow = actionBuildShip.GetSub("Overflow").ValueI;
        actionBuildShip.GetSub("Overflow").ValueI = 0;
        int production = system.Resources_PerTurn.GetIncome("Shipbuilding").IncomeAllTotal(system);

        if (design != null)
        {
            int maxProgress = 1000; // design.DistrictDef.Cost;
            int totalProgress = progress + production + overflow;
            int trys = 1000;
            while (totalProgress >= maxProgress && trys > 0)
            {
                FleetData ownedFleeet = null;
                FleetData bestFleeet = null;
                for (int fleetIdx = 0; fleetIdx < system.Star.Fleets_PerTurn.Count; fleetIdx++)
                {
                    if (system.Star.Fleets_PerTurn[fleetIdx]._Player == system._Player)
                    {
                        ownedFleeet = system.Star.Fleets_PerTurn[fleetIdx];
                        for (int shipIdx = 0; shipIdx < ownedFleeet.Ships.Count; shipIdx++)
                        {
                            if (ownedFleeet.Ships[shipIdx].Design == design)
                            {
                                bestFleeet = ownedFleeet;
                                break;
                            }
                        }
                    }
                }
                if (bestFleeet == null) bestFleeet = ownedFleeet;
                if (bestFleeet == null)
                {
                    // new fleet
                    DataBlock fleetData = Data.AddData(system._Player.Data.GetSub("Fleets"), "Fleet", Helper.IntToRoman(system._Player.Fleets.Count + 1), game.Def);
                    Data.AddData(fleetData, "Name", "Fury_" + Helper.IntToRoman(system._Player.Fleets.Count + 1), game.Def);

                    Data.AddData(fleetData, "Link:Star", system.Star.StarName, game.Def); // no StarData yet
                    Data.AddData(system.Star.Data, "Link:Player:Fleet", system._Player.PlayerName + ":" + fleetData.ValueS, game.Def); // no StarData yet

                    // ---
                    bestFleeet = game.Map.Data.GenerateGameFromData_Player_Fleet(fleetData, system._Player);
                    bestFleeet.Ships.Clear();

                    bestFleeet.Stats_PerTurn = new FleetStatsWrapper(bestFleeet);
                    bestFleeet.StarAt_PerTurn = system.Star;
                }

                DataBlock shipData = MapGenerator.CreateNewShip(bestFleeet.Data, "ShipName", design.DesignName, game.Def);
                bestFleeet.Ships.Add(new ShipData(shipData, bestFleeet));

                totalProgress = totalProgress - maxProgress;
                trys--;
            }

            actionBuildShip.GetSub("Progress").SetValueI(totalProgress, game.Def); // <-- add production to progress
        }
    }

    //static public void RefreshAvailableDesigns(Game game, SectorData sector)
    //{
    //    sector.AvailableDesigns_PerTurn.Clear();
    //
    //    for (int designIdx = 0; designIdx < sector._Player.Designs.Count; designIdx++)
    //    {
    //        DesignData design = sector._Player.Designs[designIdx];
    //        sector.AvailableDesigns_PerTurn.Add(design);
    //    }
    //}
    //
    //static public void SwitchDesign(Game game, SectorData sector, DesignData newDesign)
    //{
    //    DataBlock Past = sector.ActionBuildQueue.GetSub("Past");
    //    DataBlock current = sector.ActionBuildQueue.GetSub("Current");
    //
    //    DataBlock oldShipDesign = Data.AddData(current, "ShipDesign", newDesign.Name, game.Def);
    //
    //    DataBlock shipDesign = Data.AddData(current, "ShipDesign", newDesign.Name, game.Def);
    //    Data.AddData(shipDesign, "Progress", 0, game.Def);
    //    Data.AddData(shipDesign, "Progress:Max", 1000, game.Def);
    //
    //    //if (possibleBuilding._BuildingOld != null)
    //    //{
    //    //    Data.AddData(building, "OldBuilding", possibleBuilding._BuildingOld.ValueS, game.Def);
    //    //    Data.AddData(possibleBuilding._BuildingOld, "NewBuilding", possibleBuilding.Name, game.Def);
    //    //}
    //    //else if (possibleBuilding._BuildingPlanet != null)
    //    //{
    //    //    Data.AddData(building, "PlanetBuilding", possibleBuilding._BuildingPlanet.ValueS, game.Def);
    //    //}
    //    //
    //    //possibleBuilding._Sector.BuildQueue_PerTurn_ActionChange.Refresh();
    //}
    //
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