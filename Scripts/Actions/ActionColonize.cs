using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionColonize
{
    static public bool CanColonize(Game game, PlayerData player, StarData star)
    {
        return GetColonyShip(game, player, star) != null;
    }

    static public ShipData GetColonyShip(Game game, PlayerData player, StarData star)
    {
        for (int fleetIdx = 0; fleetIdx < star.Fleets_PerTurn.Count; fleetIdx++)
        {
            if (star.Fleets_PerTurn[fleetIdx]._Player == player)
            {
                for (int shipIdx = 0; shipIdx < star.Fleets_PerTurn[fleetIdx].Ships.Count; shipIdx++)
                {
                    if (star.Fleets_PerTurn[fleetIdx].Ships[shipIdx].Data.HasSub("CanColonize"))
                    {
                        return star.Fleets_PerTurn[fleetIdx].Ships[shipIdx];
                    }
                }
            }
        }
        return null;
    }

    static public void Colonize(Game game, PlayerData player, StarData star, PlanetData planet)
    {
        ShipData colonyShip = GetColonyShip(game, player, star);

        DataBlock systemData;
        DataBlock colonyList;
        if (star.System == null)
        {
            systemData = Data.AddData(player.Data.GetSub("Systems_List"), "System", star.StarName, Game.self.Def);
            Game.self.MapGen.GenerateNewMapSave_Players_StartingColony_SystemResources(systemData, 0, false);

            Data.AddData(systemData, "Link:Star", star.StarName, Game.self.Def);
            Data.AddData(star.Data, "Link:Player:System", player.PlayerName + ":" + star.StarName, Game.self.Def);

            colonyList = Data.AddData(systemData, "Colony_List", Game.self.Def);

            Game.self.Map.Data.GenerateGameFromData_Player_System(systemData, player);
        }
        else
        {
            systemData = star.System.Data;
            colonyList = systemData.GetSub("Colony_List");
        }

        DataBlock colonyData = Game.self.MapGen.GenerateNewMapSave_Players_Colony(star.Data, planet.Data, player.Data, systemData, 0);
        Game.self.Map.Data.GenerateGameFromData_Player_System_Colony(colonyData, star.System);

        for (int idx = 0; idx < star.Fleets_PerTurn.Count; idx++)
        {
            FleetData fleet = star.Fleets_PerTurn[idx];
            if (star.Fleets_PerTurn[idx] == colonyShip._Fleet)
            {
                star.Fleets_PerTurn[idx].Ships.Remove(colonyShip);
                star.Fleets_PerTurn[idx].Data.Subs.Remove(colonyShip.Data);

                if (star.Fleets_PerTurn[idx].Data.HasSub("Fleet") == false)
                {
                    star.Fleets_PerTurn.Remove(fleet);
                    player.Fleets.Remove(fleet);
                    player.Data.GetSub("Fleets").Subs.Remove(fleet.Data);
                }
                break;
            }
        }

        star._Node.GFX.RefreshPlayerColor();
        star._Node.GFX.RefreshShips();
        planet.GFX.GUI3D.Refresh();
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