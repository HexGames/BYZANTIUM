using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionColonizePlanet
{
    // target planet

    static public bool CanColonizePlanet(Game game, PlayerData player, StarData star, PlanetData planet)
    {
        if (planet.Colony != null) return false;
        if (star.System == null) return false;
        if (star.System._Player != player) return false;

        return true;
    }

    static public FleetData GetColonyFleet(Game game, PlayerData player, StarData star)
    {
        for (int fleetIdx = 0; fleetIdx < star.Fleets_PerTurn.Count; fleetIdx++)
        {
            if (star.Fleets_PerTurn[fleetIdx]._Player == player)
            {
                for (int shipIdx = 0; shipIdx < star.Fleets_PerTurn[fleetIdx].Ships.Count; shipIdx++)
                {
                    if (star.Fleets_PerTurn[fleetIdx].Data.GetSubValueS("FleetType") == "Colony")
                    {
                        return star.Fleets_PerTurn[fleetIdx];
                    }
                }
            }
        }
        return null;
    }

    static public void Colonize(Game game, PlayerData player, StarData star, PlanetData planet)
    {
        FleetData fleet = GetColonyFleet(game, player, star);

        fleet.ActionColonizeData = Data.AddData(fleet.Data, "ActionColonize", game.Def);
        Data.AddData(fleet.ActionColonizeData, "Planet", planet.PlanetName, game.Def);

        //Data.AddData(fleet.ActionData, "Progress", 0, game.Def);
        //Data.AddData(fleet.ActionData, "ProgressMax", fleet.StarAt_PerTurn.DistanceTo(star), game.Def);

        Game.self.UIGalaxy.SystemInfo.Refresh(star);
        Game.self.UIGalaxy.DistrictsInfo.RefreshStar(star);
        //star._Node.GFX.RefreshPlayerColor();
        star._Node.GFX.RefreshShips();
    }

    static public void Cancel(Game game, PlayerData player, StarData star)
    {
        FleetData fleet = GetColonyFleet(game, player, star);

        Data.RemoveData(fleet.Data, "ActionColonize", game.Def);
        fleet.ActionColonizeData = null;

        Game.self.UIGalaxy.SystemInfo.Refresh(star);
        Game.self.UIGalaxy.DistrictsInfo.RefreshStar(star);
        //star._Node.GFX.RefreshPlayerColor();
        star._Node.GFX.RefreshShips();
    }

    static public void EndTurn(Game game, FleetData fleet)
    {
        if (fleet.StarAt_PerTurn.System != null)
            return;

        PlayerData player = fleet._Player;
        StarData star = fleet.StarAt_PerTurn;

        FleetData colonyFleet = GetColonyFleet(game, player, star);

        if (colonyFleet != null && colonyFleet.Data.HasSub("ActionColonize"))
        {
            PlanetData planet = star.GetPlanet(colonyFleet.Data.GetSubValueS("ActionColonize", "Planet"));
            DataBlock systemData = SystemRaw.CreateNewSystem(player.Data, star.Data, Game.self.Def, false, planet.Data);
            SystemData system = Game.self.Map.Data.GenerateGameFromData_Player_System(systemData, player);

            system.Init_DistrictData();
            system.Init_Resources();

            SystemData fromSystem = player.GetSystem(colonyFleet.Data.GetSubValueS("FromSystem"));
            if (fromSystem != null)
            {
                DataBlock tradeIn = Data.AddData(system.Trades, "Trade", Game.self.Def);
                Data.AddData(tradeIn, "Incoming", Game.self.Def);
                Data.AddData(tradeIn, "Resource", "Growth", Game.self.Def);
                Data.AddData(tradeIn, "OtherSystem", fromSystem.SystemName, Game.self.Def);

                DataBlock tradeOut = Data.AddData(fromSystem.Trades, "Trade", Game.self.Def);
                Data.AddData(tradeOut, "Outgoing", Game.self.Def);
                Data.AddData(tradeOut, "Resource", "Growth", Game.self.Def);
                Data.AddData(tradeOut, "OtherSystem", system.SystemName, Game.self.Def);
            }

            //DataBlock colonyData = Game.self.MapGen.GenerateNewMapSave_Players_Colony(star.Data, planet.Data, player.Data, systemData, 0);
            //Game.self.Map.Data.GenerateGameFromData_Player_System_Colony(colonyData, star.System);

            FleetRaw.RemoveFleet(player.Data, colonyFleet.Data, Game.self.Def);

            star._Node.GFX.RefreshPlayerColor();
            star._Node.GFX.RefreshShips();
            if (planet.GFX.GUI3D != null) planet.GFX.GUI3D.Refresh();
        }
    }

    /*static public void AddColonization(Game game, StarData star, Array<FleetData> selectedFleets)
    {
        star.ActionData = Data.AddData(star._Data, "ActionColonize", game.Def);

        //Data.AddData(fleet.ActionData, "Progress", 0, game.Def);
        //Data.AddData(fleet.ActionData, "ProgressMax", 3, game.Def);
        //Data.AddLink(fleet.ActionData, star, game.Def);
    }

    static public bool CancelColonization(Game game, FleetData fleet)
    {
        if (fleet.ActionData == null)
        {
            return false;
        }

        int progress = fleet.ActionData.GetSub("Progress").ValueI;
        if (progress > 0)
        {
            return false;
        }

        Data.RemoveData(fleet.Data, "ActionMove", game.Def);
        fleet.ActionData = null;
        return true;
    }*/

    /*static public void EndTurn(Game game, FleetData fleet)
    {
        if (fleet.ActionData == null)
        {
            return;
        }

        StarData star = Data.GetLinkStarData(fleet.ActionData, game.Map.Data);
        int progress = fleet.ActionData.GetSub("Progress").ValueI;
        int progressMax = fleet.ActionData.GetSub("ProgressMax").ValueI;

        if (progress < progressMax)
        {
            progress++;
            fleet.ActionData.GetSub("Progress").ValueI = progress;
        }
        else
        {
            StarData oldStar = Data.GetLinkStarData(fleet.Data, game.Map.Data);
            Data.RemoveData(oldStar._Data, "Link:Player:Fleet", fleet._Player.PlayerName + ":" + fleet.FleetName, game.Def);

            fleet.Data.GetSub("Link:Star").ValueS = fleet.ActionData.GetSub("Link:Star").ValueS;
            Data.AddLink(star._Data, fleet, game.Def);

            Data.RemoveData(fleet.Data, "ActionMove", game.Def);
            fleet.ActionData = null;
        }
    }*/
}