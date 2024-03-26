using Godot;
using Godot.Collections;
using System;

// Generated
public partial class TurnLoop : Node
{
    void EndTurn()
    {
        // increment turn number
        Game.Map.Data.Turn = Game.Map.Data.Turn + 1;

        // update actions
        EndTurn_Actions();

        // update resources
        StartTurn_Resources();

        // update actions
        StartTurn_NewActions();

        // update UI
        Game.GalaxyUI.Refresh();

        // reset players states
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            Game.Map.Data.Players[playerIdx].TurnFinished = false;
        }
    }

    // ----------------------------------------------------------------------------------------------
    private void EndTurn_Actions()
    {
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.Map.Data.Players[playerIdx];
            for (int sectorIdx = 0; sectorIdx < player.Sectors.Count; sectorIdx++)
            {
                SectorData sector = player.Sectors[sectorIdx];

                ActionBuild.Update(sector, Game); // --- !!! ---
            }
        }
    }

    // ----------------------------------------------------------------------------------------------
    private class ResourceInfo
    {
        public enum Type
        {
            VALUE,
            VALUE_INCOME,
            INCOME,
            TOTAL_USED
        }

        public Type ResType = Type.VALUE;

        public int Value_1 = 0;
        public int Value_2 = 0;

        public string Name = "Res";
    };

    private void StartTurn_Resources()
    {
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.Map.Data.Players[playerIdx];
            player.Resources_PerTurn.Refresh();

            for (int sectorIdx = 0; sectorIdx < player.Sectors.Count; sectorIdx++)
            {
                SectorData sector = player.Sectors[sectorIdx];
                //sector.BudgetPerTurn.Refresh();
                sector.Resources_PerTurn.Refresh();

                for (int systemIdx = 0; systemIdx < sector.Systems.Count; systemIdx++)
                {
                    SystemData system = sector.Systems[sectorIdx];
                    system.Resources_PerTurn.Refresh();

                    for (int colonyIdx = 0; colonyIdx < sector.Systems.Count; colonyIdx++)
                    {
                        ColonyData colony = system.Colonies[colonyIdx];
                        colony.Resources_PerTurn.Refresh();
                        //colony.ActionsConPerTurn.Refresh();

                        DataBlock baseGrowth = colony.Resources.GetSub("Growth*Income");
                        colony.Resources_PerTurn.AddIncome(baseGrowth.Name, baseGrowth.ValueI);

                        DataBlock pops = colony.Resources.GetSub("Pops*Used");
                        colony.Resources_PerTurn.AddTotal("BuildingSlots", pops.ValueI / 1000);

                        colony.Jobs_PerTurn.Refresh(pops.ValueI);

                        Array<DataBlock> buildings = colony.Buildings.GetSubs();
                        //int totalBuildings = 0;
                        for (int buildingIdx = 0; buildingIdx < buildings.Count; buildingIdx++)
                        {
                            ActionTargetInfo buildingInfo =  Game.Def.GetBuildingInfo(buildings[buildingIdx].Name);
                            int buildingCount = buildings[buildingIdx].ValueI;

                            if (buildingInfo == null)
                            {
                                GD.Print("BUILDING NOT FOUND! - " + buildings[buildingIdx].Name);
                                continue;
                            }

                            colony.Resources_PerTurn.Add(buildingInfo.Benefit, buildingCount);
                            //totalBuildings += buildingCount;
                        }
                        colony.Resources_PerTurn.Add(colony.Jobs_PerTurn);
                        //colony.Resources_PerTurn.Use("BuildingSlots", totalBuildings);

                        colony.Resources_PerTurn.ProcessIncome(); // --- !!! ---
                        system.Resources_PerTurn.Add(colony.Resources_PerTurn);
                    }
                    system.Resources_PerTurn.ProcessIncome(); // --- !!! ---
                    sector.Resources_PerTurn.Add(system.Resources_PerTurn);
                }
                sector.Resources_PerTurn.ProcessIncome(); // --- !!! ---
                player.Resources_PerTurn.Add(sector.Resources_PerTurn);
            }
            player.Resources_PerTurn.ProcessIncome(); // --- !!! ---
        }
    }

    private void StartTurn_NewActions()
    {
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.Map.Data.Players[playerIdx];

            for (int sectorIdx = 0; sectorIdx < player.Sectors.Count; sectorIdx++)
            {
                SectorData sector = player.Sectors[sectorIdx];

                ActionBuild.RefreshAvailableBuildings(sector, Game.Def);
            }
        }
    }
}
