using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionBuild
{
    static public void RefreshAvailableBuildings(SectorData sector, DefLibrary df)
    {
        sector.AvailableBuildings_PerTurn.Clear();

        for (int idx = 0; idx < df.BuildingsInfo.Count; idx++)
        {
            DataBlock require = df.BuildingsInfo[idx]._Data.GetSub("Require");
            if (require != null)
            {
                DataBlock feature = require.GetSub("Planet:Feature");
                if (feature != null)
                {
                    for (int systemIdx = 0; systemIdx < sector.Systems.Count; systemIdx++)
                    {
                        SystemData system = sector.Systems[systemIdx];
                        for (int planetIdx = 0; planetIdx < system.Star.Planets.Count; planetIdx++)
                        {
                            PlanetData planet = system.Star.Planets[planetIdx];
                            if (planet.Data.GetSub(feature.ValueS) != null)
                            {
                                ActionTargetInfo action = new ActionTargetInfo(df.BuildingsInfo[idx]._Data);
                                action._Planet = planet;
                                action._Sector = sector;
                                sector.AvailableBuildings_PerTurn.Add(action);
                            }
                        }
                    }
                }
            }
        }
    }

    static public void AddToQueue(ActionTargetInfo action, Game game)
    {
        DataBlock building = Data.AddData(action._Sector.ActionBuildQueue, "Building", action.Name, game.Def);
        Data.AddLink(building, action._Planet, game.Def);
        Data.AddData(building, "Progress:Max", action.Cost.Get("Production").Value_1, game.Def);
    }

    static public void ReorderInQueue(ActionTargetInfo action, int orderChange, Game game)
    {
        for (int idx = 0; idx < action._Sector.ActionBuildQueue.Subs.Count; idx++)
        {
            if (action._Sector.ActionBuildQueue.Subs[idx].ValueS == action.Name)
            {
                DataBlock data = action._Sector.ActionBuildQueue.Subs[idx];
                action._Sector.ActionBuildQueue.Subs.RemoveAt(idx);
                action._Sector.ActionBuildQueue.Subs.Insert(Mathf.Clamp(idx + orderChange, 0, action._Sector.ActionBuildQueue.Subs.Count - 1), data);
                break;
            }
        }
    }

    static public void DeleteFromQueue(ActionTargetInfo action, Game game, DefLibrary df)
    {
        //SectorData sector = action._Planet?._Star.System?._Sector;
        //if (sector == null) return;
        //
        //DataBlock building = Data.AddData(sector.ActionBuildQueue, "Building", action.Name, df);
        //Data.AddLink(building, action._Planet, df);
        //Data.AddData(building, "Progress:Max", action.Cost.Get("Production").Value_1, df);
    }

    static public void Update(SectorData sector, Game game)
    {
        DataBlock overflow = sector.ActionBuildQueue.GetSub("Overflow");
        int production = sector.Resources_PerTurn.Get("Production").Value_2 + overflow.ValueI;

        Array<DataBlock> buildingQueue = sector.ActionBuildQueue.GetSubs("Building");

        if (buildingQueue.Count == 0)
        {
            var bc = sector._Player.Resources_PerTurn.Get("BC");
            bc.Value_1 += production / 2;
            bc.SaveValue();
            overflow.ValueI = 0;
            return;
        }

        while (production > 0 && buildingQueue.Count > 0)
        {
            PlanetData planet = Data.GetLinkPlanetData(buildingQueue[0], game.Map.Data);

            if (planet.Colony == null)
            {
                SystemData system = planet._Star.System;
                DataBlock colonyList = system.Data.GetSub("Colony_List");

                DataBlock colonyData = Data.AddData(colonyList, "Colony", planet.PlanetName, game.Def);
                Data.AddData(colonyData, "Outpost", game.Def);
                MapGenerator.Create_Colony_Resources(colonyData, game.Def);

                MapGenerator.Create_Colony_Buildings(colonyData, game.Def);

                // ---
                // order maters x3
                Data.AddLink(colonyData, planet, game.Def); // 1
                game.Map.Data.GenerateGameFromData_Player_Sector_System_Colony(colonyData, system); // 2
                Data.AddLink(planet.Data, planet.Colony, game.Def);  // 3
            }

            DataBlock building = planet.Colony.Buildings.GetSub(buildingQueue[0].ValueS);
            bool completed = false;
            if (building == null)
            {
                building = Data.AddData(planet.Colony.Buildings, buildingQueue[0].ValueS, game.Def);

                int progressMax = buildingQueue[0].GetSub("Progress:Max").ValueI;
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
                Data.RemoveData(building, "InConstruction", game.Def);
                sector.ActionBuildQueue.Subs.Remove(buildingQueue[0]);
                buildingQueue.RemoveAt(0);
            }
        }

        overflow.ValueI = production;
    }
}