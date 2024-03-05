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
        EndTurn_Resources();

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
            //for (int colonyIdx = 0; colonyIdx < player.Colonies.Count; colonyIdx++)
            //{
             //   ColonyData colony = player.Colonies[colonyIdx];

                //if (colony.ActionBuild != null)
                //{
                //    ActionSectorBuild.Update(colony, Game.Def);
                //}
            //}
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

    private void EndTurn_Resources()
    {
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.Map.Data.Players[playerIdx];
            player.ResourcesPerTurn.Refresh();

            for (int sectorIdx = 0; sectorIdx < player.Sectors.Count; sectorIdx++)
            {
                SectorData sector = player.Sectors[sectorIdx];
                //sector.BudgetPerTurn.Refresh();
                sector.ResourcesPerTurn.Refresh();

                for (int systemIdx = 0; systemIdx < sector.Systems.Count; systemIdx++)
                {
                    SystemData system = sector.Systems[sectorIdx];
                    system.ResourcesPerTurn.Refresh();

                    for (int colonyIdx = 0; colonyIdx < sector.Systems.Count; colonyIdx++)
                    {
                        ColonyData colony = system.Colonies[colonyIdx];
                        colony.ResourcesPerTurn.Refresh();
                        //colony.ActionsConPerTurn.Refresh();

                        DataBlock baseGrowth = colony.Resources.GetSub("Growth");
                        colony.ResourcesPerTurn.Add(baseGrowth.Name, baseGrowth.ValueI);

                        DataBlock pops = colony.Resources.GetSub("Pops*Used");
                        colony.ResourcesPerTurn.Add("BuildingSlots", pops.ValueI / 1000);

                        Array<DataBlock> buildings = colony.Buildings.GetSubs();
                        int totalBuildings = 0;
                        for (int buildingIdx = 0; buildingIdx < buildings.Count; buildingIdx++)
                        {
                            ActionTargetInfo buildingInfo =  Game.Def.GetBuildingInfo(buildings[buildingIdx].Name);
                            int buildingCount = buildings[buildingIdx].ValueI;

                            if (buildingInfo == null)
                            {
                                GD.Print("BUILDING NOT FOUND! - " + buildings[buildingIdx].Name);
                                continue;
                            }

                            colony.ResourcesPerTurn.Add(buildingInfo.Benefit, buildingCount);
                            totalBuildings += buildingCount;
                        }
                        colony.ResourcesPerTurn.Use("BuildingSlots", totalBuildings);
                        colony.ResourcesPerTurn.AddIncome();
                        colony.ResourcesPerTurn.Save();
                        system.ResourcesPerTurn.Add(colony.ResourcesPerTurn);
                    }
                    system.ResourcesPerTurn.AddIncome();
                    system.ResourcesPerTurn.Save();
                    sector.ResourcesPerTurn.Add(system.ResourcesPerTurn);
                }
                sector.ResourcesPerTurn.AddIncome();
                sector.ResourcesPerTurn.Save();
                player.ResourcesPerTurn.Add(sector.ResourcesPerTurn);
            }
            player.ResourcesPerTurn.AddIncome();
            player.ResourcesPerTurn.Save();
        }
        /*for (int colonyIdx = 0; colonyIdx < player.Colonies.Count; colonyIdx++)
        {
            ColonyData colony = player.Colonies[colonyIdx];
            Array<DataBlock> buildings = colony.Buildings.GetSubs();
            for (int buildingIdx = 0; buildingIdx < buildings.Count; buildingIdx++)
            {
                ActionTargetInfo buildingInfo =  Game.Def.GetBuildingInfo(buildings[buildingIdx].Name);
                int buildingCount = buildings[buildingIdx].ValueI;

                if (buildingInfo == null)
                {
                    GD.Print("BUILDING NOT FOUND! - " + buildings[buildingIdx].Name);
                    continue;
                }

                playerRes.Add(buildingInfo.Benefit, buildingCount);
            }

            //if (colony.ActionBuild != null)
            //{
            //    ActionTargetInfo buildingInfo = Game.Def.GetBuildingInfo(colony.ActionBuild.GetSub("Building").ValueS);
            //
            //    if (buildingInfo == null)
            //    {
            //        GD.Print("BUILDING NOT FOUND! - " + colony.ActionBuild.GetSub("Building").ValueS);
            //        continue;
            //    }
            //
            //    playerRes.Use(buildingInfo.Cost);
            //}
        }*/

        //    playerRes.AddIncome();
        //
        //    playerRes.Save();
        //}
    }
}
