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
                                sector.AvailableBuildings_PerTurn.Add(action);
                            }
                        }
                    }
                }
            }
        }
    }

    static public void AddToQueue(ActionTargetInfo action, Game game, DefLibrary df)
    {
        SectorData sector = action._Planet?._Star.System?._Sector;
        if (sector == null) return;

        DataBlock building = Data.AddData(sector.ActionBuildQueue, "Building", action.Name, df);
        Data.AddLink(building, action._Planet, df);
        Data.AddData(building, "Progress:Max", action.Cost.Get("Production").Value_1, df);
    }

    static public void ReorderInQueue(ActionTargetInfo action, Game game, DefLibrary df)
    {
        //SectorData sector = action._Planet?._Star.System?._Sector;
        //if (sector == null) return;
        //
        //DataBlock building = Data.AddData(sector.ActionBuildQueue, "Building", action.Name, df);
        //Data.AddLink(building, action._Planet, df);
        //Data.AddData(building, "Progress:Max", action.Cost.Get("Production").Value_1, df);
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

    static public void Update(SectorData sector, Game game, DefLibrary df)
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

                DataBlock colonyData = Data.AddData(colonyList, "Colony", planet.PlanetName, df);
                Data.AddData(colonyData, "Outpost", df);
                MapGenerator.Create_Colony_Resources(colonyData, df);

                MapGenerator.Create_Colony_Buildings(colonyData, df);

                // ---
                // order maters x3
                Data.AddLink(colonyData, planet, df); // 1
                game.Map.Data.GenerateGameFromData_Player_Sector_System_Colony(colonyData, system); // 2
                Data.AddLink(planet.Data, planet.Colony, df);  // 3
            }

            DataBlock building = planet.Colony.Buildings.GetSub(buildingQueue[0].ValueS);
            bool completed = false;
            if (building == null)
            {
                building = Data.AddData(planet.Colony.Buildings, buildingQueue[0].ValueS, df);

                int progressMax = buildingQueue[0].GetSub("Progress:Max").ValueI;
                DataBlock inConstruction = Data.AddData(building, "InConstruction", df);
                Data.AddData(inConstruction, "Progress:Max", progressMax, df);
                if (production >= progressMax)
                {
                    production -= progressMax;
                    completed = true;
                }
                else
                {
                    Data.AddData(inConstruction, "Progress", production, df);
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
                }
            }

            if (completed)
            {
                Data.RemoveData(building, "InConstruction", df);
            }
        }

        overflow.ValueI = production;
    }
}