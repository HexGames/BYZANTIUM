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

        // update fleets
        StartTurn_Fleets();

        // update resources
        StartTurn_Resources();

        // update actions
        StartTurn_NewActions();

        // update UI
        Game.GalaxyUI.StartTurn();

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

                ActionBuild.EndTurn(Game, sector); // --- !!! ---
            }

            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];

                ActionMove.EndTurn(Game, fleet);
            }
        }
    }
    // ----------------------------------------------------------------------------------------------
    private void StartTurn_Fleets()
    {
        for (int starIdx = 0; starIdx < Game.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.Map.Data.Stars[starIdx];
            star.Fleets_PerTurn.Clear();
        }

        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.Map.Data.Players[playerIdx];
            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];
                StarData star = Data.GetLinkStarData(fleet.Data, Game.Map.Data);

                star.Fleets_PerTurn.Add(fleet);
                fleet.AtStar_PerTurn = star;
            }
        }
    }

    // ----------------------------------------------------------------------------------------------
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

                    for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
                    {
                        ColonyData colony = system.Colonies[colonyIdx];
                        colony.Resources_PerTurn.Refresh();
                        //colony.ActionsConPerTurn.Refresh();
                        //colony.Resources_PerTurn.AddIncome(baseGrowth.Name, baseGrowth.ValueI);

                        //DataBlock pops = colony.Resources.GetSub("Pops*Stockpile");
                        //colony.Resources_PerTurn.AddTotal("BuildingSlots", pops.ValueI / 1000);

                        Array<DataBlock> buildings = colony.Buildings.GetSubs("Building");
                        //int totalBuildings = 0;
                        for (int buildingIdx = 0; buildingIdx < buildings.Count; buildingIdx++)
                        {
                            DefBuildingWrapper buildingInfo =  Game.Def.GetBuildingInfo(buildings[buildingIdx].ValueS);
                            int buildingCount = buildings[buildingIdx].ValueI;

                            if (buildingInfo == null)
                            {
                                GD.Print("BUILDING NOT FOUND! - " + buildings[buildingIdx].Name);
                                continue;
                            }

                            if (buildingInfo.Benefit != null) colony.Resources_PerTurn.Add(buildingInfo.Benefit);
                            //totalBuildings += buildingCount;
                        }
                        //colony.Resources_PerTurn.Use("BuildingSlots", totalBuildings);

                        int popsControlled = 0;
                        if (colony.Resources_PerTurn.GetPops() != null)
                        {
                            popsControlled = colony.Resources_PerTurn.GetPops().GetCPops();
                            colony.Resources_PerTurn.ProcessGrowth();
                        }

                        colony.Resources_PerTurn.ProcessIncome(); // --- !!! ---
                        system.Resources_PerTurn.Add(colony.Resources_PerTurn, true);
                    }
                    system.Resources_PerTurn.ProcessIncome(); // --- !!! ---
                    sector.Resources_PerTurn.Add(system.Resources_PerTurn);
                }

                DataBlock queue = sector.ActionBuildQueue.GetSub("Queue");
                if (queue.Subs.Count == 0)
                {
                    DataBlock overflow = sector.ActionBuildQueue.GetSub("Overflow");
                    int production = sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal() + overflow.ValueI;

                    sector._Player.Resources_PerTurn.GetIncome("BC").Income += production / 2;
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

                ActionBuild.RefreshAvailableBuildings(Game, sector);

                sector.BuildQueue_PerTurn_ActionChange.Refresh();
            }

            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];

                ActionMove.RefreshAvailableMoves(Game, fleet);
            }
        }
    }
}
