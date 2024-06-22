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

            if (fleet.AtStar_PerTurn != star && fleet.AtStar_PerTurn.DistanceTo(star) < 4)
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
        fleet.MoveAction = Data.AddData(fleet.Data, "ActionMove", game.Def);

        Data.AddData(fleet.MoveAction, "Progress", 0, game.Def);
        Data.AddData(fleet.MoveAction, "ProgressMax", fleet.AtStar_PerTurn.DistanceTo(star), game.Def);
        Data.AddLink(fleet.MoveAction, star, game.Def);
    }
    static public bool CancelMove(Game game, FleetData fleet)
    {
        if (fleet.MoveAction == null)
        {
            return false;
        }

        int progress = fleet.MoveAction.GetSub("Progress").ValueI;
        if (progress > 0)
        {
            return false;
        }

        Data.RemoveData(fleet.Data, "ActionMove", game.Def);
        fleet.MoveAction = null;
        return true;
    }

    static public void EndTurn(Game game, FleetData fleet)
    {
        if (fleet.MoveAction == null)
        {
            return;
        }

        StarData star = Data.GetLinkStarData(fleet.MoveAction, game.Map.Data);
        int progress = fleet.MoveAction.GetSub("Progress").ValueI;
        int progressMax = fleet.MoveAction.GetSub("ProgressMax").ValueI;

        if (progress < progressMax)
        {
            progress++;
            fleet.MoveAction.GetSub("Progress").ValueI = progress;
        }
        else
        {
            StarData oldStar = Data.GetLinkStarData(fleet.Data, game.Map.Data);
            Data.RemoveData(oldStar._Data, "Link:Player:Fleet", fleet._Player.PlayerName + ":" + fleet.FleetName, game.Def);

            fleet.Data.GetSub("Link:Star").ValueS = fleet.MoveAction.GetSub("Link:Star").ValueS;
            Data.AddLink(star._Data, fleet, game.Def);

            Data.RemoveData(fleet.Data, "ActionMove", game.Def);
            fleet.MoveAction = null;
        }
    }
}