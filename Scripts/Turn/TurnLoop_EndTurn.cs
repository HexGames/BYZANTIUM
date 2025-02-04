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
        Game.self.GalaxyUI.EndTurnBg.Visible = false;

        // increment turn number
        Game.self.Map.Data.Turn = Game.self.Map.Data.Turn + 1;

        // update resources
        EndTurn_PlayerStockpiles();

        // update actions
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(EndTurn_ActionsBuild()));
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(EndTurn_ActionsPops()));
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(EndTurn_ActionsShipbuilding()));
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(EndTurn_ActionsMove()));
        //yield return Timing.WaitUntilDone(Timing.RunCoroutine(EndTurn_Control()));
        //yield return Timing.WaitUntilDone(Timing.RunCoroutine(EndTurn_Visibility()));

        // update fleets
        StartTurn_Fleets();

        // update visibility
        StartTurn_Visibility();

        // update resources
        StartTurn_Resources();

        // update actions
        StartTurn_NewActions();

        // update UI
        Game.self.GalaxyUI.StartTurn();

        StartTurn_RefreshGUI3D();

        // reset players states
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            Game.self.Map.Data.Players[playerIdx].TurnFinished = false;
        }

        WaitingForEndTurn = false;

        yield return Timing.WaitForOneFrame;

        Game.self.GalaxyUI.EndTurnBg.Visible = true;
        Game.self.Camera.LOD = 2;
    }

    // ----------------------------------------------------------------------------------------------
    private void EndTurn_PlayerStockpiles()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            player.Stockpiles_PerTurn.EndTurn();
        }
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

                ActionDistrict.EndTurn(system); // --- !!! ---
            }
        }

        yield return Timing.WaitForOneFrame;
    }

    // ----------------------------------------------------------------------------------------------
    private IEnumerator<double> EndTurn_ActionsPops()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];

                AutoActionPops.EndTurn(system); // --- !!! ---
            }
        }

        yield return Timing.WaitForOneFrame;
    }

    // ----------------------------------------------------------------------------------------------
    private IEnumerator<double> EndTurn_ActionsShipbuilding()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];

                ActionShipbuilding.EndTurn(system); // --- !!! ---
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
                ActionColonize.EndTurn(Game.self, fleet);
                ActionMove.EndTurn(Game.self, fleet);

                if (fleet.ActionMoveData == null)
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


    //private IEnumerator<double> EndTurn_Control()
    //{
    //    for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
    //    {
    //        PlayerData player = Game.self.Map.Data.Players[playerIdx];
    //        for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
    //        {
    //            SystemData system = player.Systems[systemIdx];
    //            //DataBlock controlData = system.Data.GetSub("Control");
    //
    //            //controlData.GetSub("Control").SetValueI(Mathf.Clamp(system.Control_PerTurn.Control + system.Resources_PerTurn.SpecialIncome.Control, 10, 1000), Game.self.Def); // --- !!! ---
    //            //controlData.GetSub("Corruption").SetValueI(Mathf.Clamp(system.Control_PerTurn.Corruption + system.Resources_PerTurn.SpecialIncome.Corruption, 10, 1000), Game.self.Def); // --- !!! ---
    //            //controlData.GetSub("Happiness").SetValueI(Mathf.Clamp(system.Control_PerTurn.Happiness + system.Resources_PerTurn.SpecialIncome.Happiness, 10, 1000), Game.self.Def); // --- !!! ---
    //            //controlData.GetSub("Wealth").SetValueI(Mathf.Clamp(system.Control_PerTurn.Wealth + system.Resources_PerTurn.SpecialIncome.Wealth, 10, 1000), Game.self.Def); // --- !!! ---
    //            //controlData.GetSub("Inequality").SetValueI(Mathf.Clamp(system.Control_PerTurn.Inequality + system.Resources_PerTurn.SpecialIncome.Inequality, 10, 1000), Game.self.Def); // --- !!! ---
    //        }
    //    }
    //
    //    Game.self.GalaxyUI.RefreshAllPathsLabels();
    //
    //    yield return Timing.WaitForOneFrame;
    //}

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
    public void StartTurn_Visibility()
    {
        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            star.Visibility_PerTurn.Refresh();
        }

        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];

            // DEBUG
            if (player.DEBUG)
            {
                for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
                {
                    StarData star = Game.self.Map.Data.Stars[starIdx];
                    star.Visibility_PerTurn.UncoveredBy.Add(player);
                    star.Visibility_PerTurn.VisibleBy.Add(player);
                }
            }

            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
                {
                    StarData star = Game.self.Map.Data.Stars[starIdx];
                    if (star == player.Fleets[fleetIdx].StarAt_PerTurn)
                    {
                        star.Visibility_PerTurn.SetAsVisibleForThisTurn(player);
                        StartTurn_Visibility_CheckPlayerMeet(player, star);
                    }
                    else if (star.DistanceTo(player.Fleets[fleetIdx].StarAt_PerTurn) <= 1)
                    {
                        star.Visibility_PerTurn.SetAsUncovered(player);
                    }
                }
            }
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
                {
                    StarData star = Game.self.Map.Data.Stars[starIdx];
                    if (star == player.Systems[systemIdx].Star)
                    {
                        star.Visibility_PerTurn.SetAsVisibleForThisTurn(player);
                        StartTurn_Visibility_CheckPlayerMeet(player, star);
                    }
                    else if (star.DistanceTo(player.Systems[systemIdx].Star) <= 1)
                    {
                        star.Visibility_PerTurn.SetAsUncovered(player);
                    }
                }
            }
        }
    }

    private void StartTurn_Visibility_CheckPlayerMeet(PlayerData player, StarData withAllAtStar)
    {
        if (withAllAtStar.System != null && withAllAtStar.System._Player != player)
        {
            StartTurn_Visibility_CheckPlayerMeet(player, withAllAtStar.System._Player);
        }
    }

    private void StartTurn_Visibility_CheckPlayerMeet(PlayerData player_1, PlayerData player_2)
    {
        RelationData relation = Game.self.Map.Data.GetRelation(player_1, player_2);

        if (relation == null)
        {
            // player meets player
            EventMeet.PlayerMeetPlayer(player_1, player_2);
        }
    }

    // ----------------------------------------------------------------------------------------------
    private void StartTurn_Resources()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];

                system.Economy_PerTurn.Refresh_1();

                system.RefreshSystemDistricts_1();

                system.Pops_PerTurn.RefreshBase_1();
            }
        }

        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];
                system.RefreshInvest_2();
                system.Pops_PerTurn.RefreshOutgoingTrade_2();
            }
        }

        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];
                system.Pops_PerTurn.RefreshIncomingTrade_3();
            }
        }

        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];

                system.Pops_PerTurn.RefreshHappiness_4();
                system.Control_PerTurn.Refresh();
                //system.Infrastructure_PerTurn.Refresh();
                system.Economy_PerTurn.Refresh_4();
                system.Shipbuilding_PerTurn.Refresh();
            }

            player.Stockpiles_PerTurn.Refresh_5();
            player.Stats_PerTurn.Refresh_6();
        }
    }

    private void StartTurn_NewActions()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];

            //for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            //{
            //    SystemData system = player.Systems[systemIdx];
            //    ActionChangeDistrict.RefreshAvailableDistricts(system);
            //}

            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];

                ActionMove.RefreshAvailableMoves(Game.self, fleet);
            }
        }
    }

    public void StartTurn_RefreshGUI3D()
    {
        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            star._Node.GFX.RefreshPlayerColor();
            if (star._Node.GFX.GUI3D != null)
            {
                star._Node.GFX.GUI3D.Refresh();
            }
        }

        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            star._Node.GFX.RefreshShips();
        }
    }
}
