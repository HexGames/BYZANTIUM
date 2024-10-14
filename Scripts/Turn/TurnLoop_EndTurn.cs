using Godot;
using Godot.Collections;
using MEC;
using System;
using System.Collections.Generic;

// Generated
public partial class TurnLoop : Node
{
    Array<FleetData> Fleets_Friendly = new Array<FleetData>();
    Array<FleetData> Fleets_Other = new Array<FleetData>();
    private IEnumerator<double> EndTurn()
    {
        // increment turn number
        Game.self.Map.Data.Turn = Game.self.Map.Data.Turn + 1;

        // update actions
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(EndTurn_ActionsBuild()));
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(EndTurn_ActionsMove()));

        // update fleets
        StartTurn_Fleets();

        // update resources
        StartTurn_Resources();

        // update actions
        StartTurn_NewActions();

        // update UI
        // Game.GalaxyUI.StartTurn();//TEMP02
        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            star._Node.GFX.RefreshPlayerColor();
        }

        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            star._Node.GFX.RefreshShips();
        }

        // reset players states
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            Game.self.Map.Data.Players[playerIdx].TurnFinished = false;
        }

        WaitingForEndTurn = false;

        yield return Timing.WaitForOneFrame;
    }

    // ----------------------------------------------------------------------------------------------
    private IEnumerator<double> EndTurn_ActionsBuild()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];
        
                ActionBuildDistrict.EndTurn(Game.self, system); // --- !!! ---
            }
        }

        yield return Timing.WaitForOneFrame;
    }

    private IEnumerator<double> EndTurn_ActionsMove()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];

            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];
                ActionMove.EndTurn(Game.self, fleet);

                if (fleet.ActionData == null)
                {
                    Game.self.Paths.ClearPathForFleet(fleet);
                }

                //Game.self.Paths.ClearPathForFleet(fleet);
                //Game.self.Incomings.ClearIncomingForFleet(fleet);
                //
                //if (fleet.GetMoveActionTurns() >= 0)
                //{
                //    StarData star = Data.GetLinkStarData(fleet.MoveAction, Game.self.Map.Data);
                //    Game.self.Incomings.AddIncoming(fleet, star);
                //}
            }
        }

        Game.self.GalaxyUI.RefreshAllPathsLabels();

        yield return Timing.WaitForOneFrame;
    }
    // ----------------------------------------------------------------------------------------------
    private void StartTurn_Fleets()
    {
        // moves
        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            star.Fleets_PerTurn.Clear();
        }

        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];
                StarData star = Data.GetLinkStarData(fleet.Data, Game.self.Map.Data);

                star.Fleets_PerTurn.Add(fleet);
                fleet.StarAt_PerTurn = star;
            }
        }

        // stats
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];
                fleet.Stats_PerTurn.Refresh();
            }
        }
    }

    // ----------------------------------------------------------------------------------------------
    private void StartTurn_Resources()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            //player.Resources_PerTurn.Refresh();

            //for (int sectorIdx = 0; sectorIdx < player.Sectors.Count; sectorIdx++)
            //{
            //    SectorData sector = player.Sectors[sectorIdx];
            //    //sector.BudgetPerTurn.Refresh();
            //    sector.Resources_PerTurn.Refresh();
            //
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];
                system.Resources_PerTurn.Refresh();
                system.Pops_PerTurn.Refresh();
                system.Buildings_PerTurn.Refresh();
                system.QueueDistricts_PerTurn.Refresh();

                for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
                {
                    ColonyData colony = system.Colonies[colonyIdx];
                    colony.Resources_PerTurn.Refresh();
                    colony.Pops_PerTurn.Refresh();
                    colony.Buildings_PerTurn.Refresh();
                    //colony.ActionsConPerTurn.Refresh();

                    // Add Features
                    PlanetData planet = colony.Planet;
                    for (int featureIdx = 0; featureIdx < planet.Features.Count; featureIdx++)
                    {
                        colony.Resources_PerTurn.AddResources_Features(planet.Features[featureIdx].FeatureDef.Res_PerSession);
                    }

                    // Add Districts
                    for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
                    {
                        if (colony.Districts[districtIdx].IsFinishedDistrict())
                        {
                            colony.Resources_PerTurn.AddResources_Districts(colony.Districts[districtIdx], colony.Districts[districtIdx].DistrictDef.Res_PerSession);
                        }
                    }
                }

                // system income propagation
                {
                    // add up system income
                    for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
                    {
                        ColonyData colony = system.Colonies[colonyIdx];
                        system.Resources_PerTurn.AddSystemIncome(colony.Resources_PerTurn);
                    }

                    // add system income to each colony income
                    for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
                    {
                        ColonyData colony = system.Colonies[colonyIdx];
                        colony.Resources_PerTurn.AddSystemIncomeToColonies(system.Resources_PerTurn);
                    }
                }

                // add colonies to system
                for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
                {
                    ColonyData colony = system.Colonies[colonyIdx];
                    system.Resources_PerTurn.AddColonyResources(colony);
                }

                //{
                //DataBlock pops = colony.Resources.GetSub("Pops*Stockpile");
                //colony.Resources_PerTurn.AddTotal("BuildingSlots", pops.ValueI / 1000);

                //Array<DataBlock> buildings = colony.Buildings.GetSubs("Building");
                //int totalBuildings = 0;
                //for (int buildingIdx = 0; buildingIdx < buildings.Count; buildingIdx++)
                //{
                //    DefBuildingWrapper buildingInfo =  Game.Def.GetBuildingInfo(buildings[buildingIdx].ValueS);
                //    int buildingCount = buildings[buildingIdx].ValueI;
                //
                //    if (buildingInfo == null)
                //    {
                //        GD.Print("BUILDING NOT FOUND! - " + buildings[buildingIdx].Name);
                //        continue;
                //    }
                //
                //    if (buildingInfo.Benefit != null) colony.Resources_PerTurn.Add(buildingInfo.Benefit);
                //    //totalBuildings += buildingCount;
                //}
                //colony.Resources_PerTurn.Use("BuildingSlots", totalBuildings);

                //int popsControlled = 0;
                //if (colony.Resources_PerTurn.GetPops() != null)
                //{
                //    popsControlled = colony.Resources_PerTurn.GetPops().GetCPops();
                //    colony.Resources_PerTurn.ProcessGrowth();
                //}
                //
                //colony.Resources_PerTurn.ProcessIncome(); // --- !!! ---
                //system.Resources_PerTurn.Add(colony.Resources_PerTurn, true);
                //}
                //system.Resources_PerTurn.ProcessIncome(); // --- !!! ---
                //player.Resources_PerTurn.Add(system.Resources_PerTurn);
            }

            //DataBlock queue = sector.ActionBuildQueue.GetSub("Queue");
            //if (queue.Subs.Count == 0)
            //{
            //    DataBlock overflow = sector.ActionBuildQueue.GetSub("Overflow");
            //    int production = sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal() + overflow.ValueI;
            //
            //    sector._Player.Resources_PerTurn.GetIncome("BC").Income += production / 2;
            //}

            //sector.Resources_PerTurn.ProcessIncome(); // --- !!! ---
            //player.Resources_PerTurn.Add(System.Resources_PerTurn);
            //}
            //player.Resources_PerTurn.ProcessIncome(); // --- !!! ---
        }
    }

    private void StartTurn_NewActions()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];

            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];
                ActionBuildDistrict.RefreshAvailableDistricts(Game.self, system);
            }

            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];

                ActionMove.RefreshAvailableMoves(Game.self, fleet);
            }
        }
    }
}
