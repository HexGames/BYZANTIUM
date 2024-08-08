using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionBuild
{
    static public void RefreshAvailableBuildings(Game game, SectorData sector)
    {
        sector.AvailableBuildings_PerTurn.Clear();

        for (int systemIdx = 0; systemIdx < sector.Systems.Count; systemIdx++)
        {
            SystemData system = sector.Systems[systemIdx];
            for (int planetIdx = 0; planetIdx < system.Star.Planets.Count; planetIdx++)
            {
                PlanetData planet = system.Star.Planets[planetIdx];
                if (planet.Colony != null)
                {
                    Array<DataBlock> existing = planet.Colony.Buildings.GetSubs("Building");
                    for (int existingIdx = 0; existingIdx < existing.Count; existingIdx++)
                    {
                        sector.AvailableBuildings_PerTurn.AddRange(RefreshAvailableBuildings_AddAllUpgrades(game, sector, existing[existingIdx], planet));
                    }
                }
                else
                {
                    Array<DataBlock> existing = planet.Data.GetSubs("Building");
                    for (int existingIdx = 0; existingIdx < existing.Count; existingIdx++)
                    {
                        sector.AvailableBuildings_PerTurn.AddRange(RefreshAvailableBuildings_AddAllUpgrades(game, sector, existing[existingIdx], planet));
                    }
                }
            }
        }

        //for (int idx = 0; idx < df.BuildingsInfo.Count; idx++)
        //{
        //    DataBlock require = df.BuildingsInfo[idx]._Data.GetSub("Require");
        //    if (require != null)
        //    {
        //        DataBlock feature = require.GetSub("Planet:Feature");
        //        if (feature != null)
        //        {
        //            for (int systemIdx = 0; systemIdx < sector.Systems.Count; systemIdx++)
        //            {
        //                SystemData system = sector.Systems[systemIdx];
        //                for (int planetIdx = 0; planetIdx < system.Star.Planets.Count; planetIdx++)
        //                {
        //                    PlanetData planet = system.Star.Planets[planetIdx];
        //                    if (planet.Data.GetSub(feature.ValueS) != null)
        //                    {
        //                        ActionTargetInfo action = new ActionTargetInfo(df.BuildingsInfo[idx]._Data);
        //                        action._Planet = planet;
        //                        action._Sector = sector;
        //                        sector.AvailableBuildings_PerTurn.Add(action);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
    static private List<DefBuildingWrapper> RefreshAvailableBuildings_AddAllUpgrades(Game game, SectorData sector, DataBlock buildingOld, PlanetData planet)
    {
        List<DefBuildingWrapper> buildings = new List<DefBuildingWrapper>();

        DataBlock buildingDef = game.Def.GetBuilding(buildingOld.ValueS);
        if (buildingDef != null)
        {
            Array<DataBlock> upgrades = buildingDef.GetSubs("Upgrade");
            for (int idx = 0; idx < upgrades.Count; idx++)
            {
                DataBlock upgradeDef = game.Def.GetBuilding(upgrades[idx].ValueS);
                bool hasRequiredFeatures = true;
                if (upgradeDef.GetSub("RequiredFeature", false) != null)
                {
                    hasRequiredFeatures = false;
                    string[] features = upgradeDef.GetSub("RequiredFeature").ValueS.Split('|');
                    for (int featureIdx = 0; featureIdx < features.Length; featureIdx++)
                    {
                        if (planet.Data.GetSub(features[featureIdx], false) != null)
                        {
                            hasRequiredFeatures = true;
                            break;
                        }
                    }
                }

                if (hasRequiredFeatures == false)
                    continue;

                DefBuildingWrapper action = new DefBuildingWrapper(upgradeDef);
                action._Planet = planet;
                action._Sector = sector;
                if (planet.Colony != null)
                    action._BuildingOld = buildingOld;
                else
                    action._BuildingPlanet = buildingOld;
                buildings.Add(action);
            }
        }
        else
        {
            GD.PrintErr("Building Def not found: " + buildingOld.ValueS);
        }

        return buildings;
    }

    static public DefBuildingWrapper GetAvailableBuilding(Game game, SectorData sector, string buildingName, PlanetData planet)
    {
        DataBlock buildingDef = game.Def.GetBuilding(buildingName);
        for (int idx = 0; idx < sector.AvailableBuildings_PerTurn.Count; idx++)
        {
            if (sector.AvailableBuildings_PerTurn[idx]._Data == buildingDef && sector.AvailableBuildings_PerTurn[idx]._Planet == planet)
            {
                return sector.AvailableBuildings_PerTurn[idx];
            }
        }
        return null;
    }

    static public void AddToQueue(Game game, SectorData sector, string buildingName, PlanetData planet)
    {
        AddToQueue(game, GetAvailableBuilding(game, sector, buildingName, planet));
    }
    static public void AddToQueue(Game game, DefBuildingWrapper possibleBuilding)
    {
        DataBlock queue = possibleBuilding._Sector.ActionBuildQueue.GetSub("Queue");
        DataBlock building = Data.AddData(queue, "Building", possibleBuilding.Name, game.Def);
        Data.AddLink(building, possibleBuilding._Planet, game.Def);
        Data.AddData(building, "Progress:Max", possibleBuilding.Cost, game.Def);

        if (possibleBuilding._BuildingOld != null)
        {
            Data.AddData(building, "OldBuilding", possibleBuilding._BuildingOld.ValueS, game.Def);
            Data.AddData(possibleBuilding._BuildingOld, "NewBuilding", possibleBuilding.Name, game.Def);
        }
        else if (possibleBuilding._BuildingPlanet != null)
        {
            Data.AddData(building, "PlanetBuilding", possibleBuilding._BuildingPlanet.ValueS, game.Def);
        }

        possibleBuilding._Sector.BuildQueue_PerTurn_ActionChange.Refresh();
    }

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

    static public void ReorderInQueue(Game game, SectorData sector, string buildingName, PlanetData planet, int orderChange)
    {
        DataBlock queue = sector.ActionBuildQueue.GetSub("Queue");
        for (int idx = 0; idx < queue.Subs.Count; idx++)
        {
            string queueName = queue.Subs[idx].ValueS;
            PlanetData queuePlanet = Data.GetLinkPlanetData(queue.Subs[idx], game.Map.Data);
            if (queue.Subs[idx].Name == "Building" && buildingName == queueName && planet == queuePlanet)
            {
                DataBlock data = queue.Subs[idx];
                queue.Subs.RemoveAt(idx);
                queue.Subs.Insert(Mathf.Clamp(idx + orderChange, 0, queue.Subs.Count - 1), data);
                break;
            }
        }

        sector.BuildQueue_PerTurn_ActionChange.Refresh();
    }

    static public void DeleteFromQueue(Game game, SectorData sector, string buildingName, PlanetData planet)
    {
        DataBlock queue = sector.ActionBuildQueue.GetSub("Queue");
        for (int idx = 0; idx < queue.Subs.Count; idx++)
        {
            string queueName = queue.Subs[idx].ValueS;
            PlanetData queuePlanet = Data.GetLinkPlanetData(queue.Subs[idx], game.Map.Data);
            if (queue.Subs[idx].Name == "Building" && buildingName == queueName && planet == queuePlanet)
            {
                DataBlock data = queue.Subs[idx];
                queue.Subs.RemoveAt(idx);
                break;
            }
        }

        sector.BuildQueue_PerTurn_ActionChange.Refresh();
    }

    static public void EndTurn(Game game, SectorData sector)
    {
        DataBlock overflow = sector.ActionBuildQueue.GetSub("Overflow");
        int production = sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal() + overflow.ValueI;

        DataBlock queue = sector.ActionBuildQueue.GetSub("Queue");

        //if (queue.Subs.Count == 0)
        //{
        //    var bc = sector._Player.Resources_PerTurn.Get("BC");
        //    bc.Value_1 += production / 2;
        //    bc.SaveValue();
        //    overflow.ValueI = 0;
        //    return;
        //}

        while (production > 0 && queue.Subs.Count > 0)
        {
            PlanetData planet = Data.GetLinkPlanetData(queue.Subs[0], game.Map.Data);

            if (planet.Colony == null)
            {
                SystemData system = planet._Star.System;
                DataBlock colonyList = system.Data.GetSub("Colony_List");

                DataBlock colonyData = Data.AddData(colonyList, "Colony", planet.PlanetName, game.Def);
                Data.AddData(colonyData, "Type", "Outpost", game.Def);
                MapGenerator.Create_Colony_Resources(colonyData, game.Def);
                MapGenerator.Create_Colony_Buildings(colonyData, game.Def);

                // ---
                // order maters x3
                Data.AddLink(colonyData, planet, game.Def); // 1
                ColonyData colony = game.Map.Data.GenerateGameFromData_Player_Sector_System_Colony(colonyData, system); // 2
                Data.AddLink(planet.Data, planet.Colony, game.Def);  // 3

                system.Colonies.Add(colony);
                colony.Resources_PerTurn = new ResourcesWrapper(colony.Resources, ResourcesWrapper.ParentType.Colony);
            }

            DataBlock building = planet.Colony.Buildings.GetSub("Building", queue.Subs[0].ValueS);
            bool completed = false;
            if (building == null)
            {
                building = Data.AddData(planet.Colony.Buildings, "Building", queue.Subs[0].ValueS, game.Def);

                int progressMax = queue.Subs[0].GetSub("Progress:Max").ValueI;
                DataBlock inConstruction = Data.AddData(building, "InConstruction", game.Def);
                Data.AddData(inConstruction, "Progress:Max", progressMax, game.Def);
                if (production >= progressMax)
                {
                    production -= progressMax;
                    completed = true;
                }
                else
                {
                    Data.AddData(inConstruction, "Progress", production, game.Def);
                    production = 0;
                }
            }
            else
            {
                DataBlock inConstruction = building.GetSub("InConstruction");
                DataBlock progressData = inConstruction.GetSub("Progress");
                int progressCurrent = progressData.ValueI;
                int progressMax = inConstruction.GetSub("Progress:Max").ValueI;
                int remaining = progressMax - progressCurrent;
                if (production >= remaining)
                {
                    production -= remaining;
                    completed = true;
                }
                else
                {
                    progressData.ValueI += production;
                    production = 0;
                }
            }

            if (completed)
            {
                if (queue.Subs[0].GetSub("OldBuilding") != null)
                {
                    Data.RemoveData(planet.Colony.Buildings, "Building", queue.Subs[0].GetSub("OldBuilding").ValueS, game.Def);
                }
                Data.RemoveData(building, "InConstruction", game.Def);
                sector.ActionBuildQueue.Subs.Remove(queue.Subs[0]);
                queue.Subs.RemoveAt(0);
            }
        }

        overflow.ValueI = production;
    }
}