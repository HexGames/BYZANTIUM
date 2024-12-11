using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionMove
{
    static public void RefreshAvailableMoves(Game game, FleetData fleet)
    {
        fleet.AvailableMoves_PerTurn.Clear();

        for (int starIdx = 0; starIdx < game.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = game.Map.Data.Stars[starIdx];

            if (fleet.StarAt_PerTurn != star && fleet.Data.GetSubValueS("FleetType") != "Defence" && fleet.StarAt_PerTurn.DistanceTo(star) < 4)
            {
                fleet.AvailableMoves_PerTurn.Add(star);
            }
        }
    }

    static public bool HasAvailableMove(Game game, FleetData fleet, StarData star)
    {
        return fleet.AvailableMoves_PerTurn.Contains(star);
    }

    static public void AddMove(Game game, FleetData fleet, StarData star)
    {
        fleet.ActionMoveData = Data.AddData(fleet.Data, "ActionMove", game.Def);

        Data.AddData(fleet.ActionMoveData, "Progress", 0, game.Def);
        Data.AddData(fleet.ActionMoveData, "ProgressMax", fleet.StarAt_PerTurn.DistanceTo(star), game.Def);
        Data.AddLink(fleet.ActionMoveData, star, game.Def);
    }

    static public bool CancelMove(Game game, FleetData fleet)
    {
        if (fleet.ActionMoveData == null)
        {
            return false;
        }

        int progress = fleet.ActionMoveData.GetSub("Progress").ValueI;
        if (progress > 0)
        {
            return false;
        }

        Data.RemoveData(fleet.Data, "ActionMove", game.Def);
        fleet.ActionMoveData = null;
        return true;
    }

    static public void EndTurn(Game game, FleetData fleet)
    {
        if (fleet.ActionMoveData == null)
        {
            return;
        }

        StarData star = Data.GetLinkStarData(fleet.ActionMoveData, game.Map.Data);
        int progress = fleet.ActionMoveData.GetSub("Progress").ValueI;
        int progressMax = fleet.ActionMoveData.GetSub("ProgressMax").ValueI;

        if (progress < progressMax)
        {
            progress++;
            fleet.ActionMoveData.GetSub("Progress").SetValueI(progress, game.Def);
        }
        else
        {
            StarData oldStar = Data.GetLinkStarData(fleet.Data, game.Map.Data);
            Data.RemoveData(oldStar.Data, "Link:Player:Fleet", fleet._Player.PlayerName + ":" + fleet.FleetName, game.Def);

            fleet.Data.GetSub("Link:Star").ValueS = fleet.ActionMoveData.GetSub("Link:Star").ValueS;
            Data.AddLink(star.Data, fleet, game.Def);

            Data.RemoveData(fleet.Data, "ActionMove", game.Def);
            fleet.ActionMoveData = null;
        }
    }
}