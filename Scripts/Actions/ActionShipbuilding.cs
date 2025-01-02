using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionShipbuilding
{
    static public void ChangeShipTo(SystemData system, DesignData chosenDesign)
    {
        DataBlock actionBuildShip = system.Data.GetSub("ActionBuildShip");
        actionBuildShip.GetSub("Design").SetValueS(chosenDesign.DesignName, Game.self.Def);

        system.Shipbuilding_PerTurn.Refresh();
    }

    static public void EndTurn(SystemData system)
    {
        int shipbuilding = system.Shipbuilding_PerTurn.Shipbuilding;
        system.Shipbuilding_PerTurn.ProgressCurrent += shipbuilding;
        //if (system.Pops_PerTurn.GrowthProgress >= system.Pops_PerTurn.GrowthProgressMax)

        system.Data.SetSubValueI("ActionBuildShip", "Progress", system.Shipbuilding_PerTurn.ProgressCurrent, Game.self.Def);

        //DataBlock actionBuildShip = system.Data.GetSub("ActionBuildShip");
        //DesignData design = system._Player.GetDesign(actionBuildShip.GetSub("Design").ValueS);
        //DesignData lastDesign = system._Player.GetDesign(actionBuildShip.GetSub("LastDesign").ValueS);
        //int progress = actionBuildShip.GetSub("Progress").ValueI;
        //int overflow = actionBuildShip.GetSub("Overflow").ValueI;
        //if (lastDesign.DesignName != design.DesignName)
        //{
        //    if (progress > lastDesign.Cost / 2)
        //    {
        //        progress = lastDesign.Cost / 2 + (progress - lastDesign.Cost / 2) / 2;
        //    }
        //    actionBuildShip.GetSub("LastDesign").SetValueS(design.DesignName, Game.self.Def);
        //}
        //actionBuildShip.GetSub("Overflow").ValueI = 0;
        //
        //int production = system.Shipbuilding_PerTurn.P;

        //int production = system.Resources_PerTurn.GetIncome("Shipbuilding").IncomeAllTotal();

        //if (design != null)
        //{
        //    int maxProgress = 1000; // design.DistrictDef.Cost;
        //    int totalProgress = progress + production + overflow;
        //    int trys = 1000;
        //    while (totalProgress >= maxProgress && trys > 0)
        //    {
        //        FleetData ownedFleeet = null;
        //        FleetData bestFleeet = null;
        //        for (int fleetIdx = 0; fleetIdx < system.Star.Fleets_PerTurn.Count; fleetIdx++)
        //        {
        //            if (system.Star.Fleets_PerTurn[fleetIdx]._Player == system._Player)
        //            {
        //                ownedFleeet = system.Star.Fleets_PerTurn[fleetIdx];
        //                for (int shipIdx = 0; shipIdx < ownedFleeet.Ships.Count; shipIdx++)
        //                {
        //                    if (ownedFleeet.Ships[shipIdx].Design == design)
        //                    {
        //                        bestFleeet = ownedFleeet;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        if (bestFleeet == null) bestFleeet = ownedFleeet;
        //        if (bestFleeet == null)
        //        {
        //            // new fleet
        //            DataBlock fleetData = Data.AddData(system._Player.Data.GetSub("Fleets"), "Fleet", Helper.IntToRoman(system._Player.Fleets.Count + 1), Game.self.Def);
        //            Data.AddData(fleetData, "Name", "Fury_" + Helper.IntToRoman(system._Player.Fleets.Count + 1), Game.self.Def);
        //
        //            Data.AddData(fleetData, "Link:Star", system.Star.StarName, Game.self.Def); // no StarData yet
        //            Data.AddData(system.Star.Data, "Link:Player:Fleet", system._Player.PlayerName + ":" + fleetData.ValueS, Game.self.Def); // no StarData yet
        //
        //            // ---
        //            bestFleeet = Game.self.Map.Data.GenerateGameFromData_Player_Fleet(fleetData, system._Player);
        //            bestFleeet.Ships.Clear();
        //
        //            bestFleeet.Stats_PerTurn = new FleetStatsWrapper(bestFleeet);
        //            bestFleeet.StarAt_PerTurn = system.Star;
        //        }
        //
        //        DataBlock shipData = MapGenerator.CreateNewShip(bestFleeet.Data, "ShipName", design.DesignName, Game.self.Def);
        //        bestFleeet.Ships.Add(new ShipData(shipData, bestFleeet));
        //
        //        totalProgress = totalProgress - maxProgress;
        //        trys--;
        //    }
        //
        //    actionBuildShip.GetSub("Progress").SetValueI(totalProgress, Game.self.Def); // <-- add production to progress
        //}
    }
}