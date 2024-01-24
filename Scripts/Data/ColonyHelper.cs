using Godot;
using System.Collections.Generic;

public class ColonyHelper
{
    static public PlayerData GetPlayer(Game game, DataBlock colonyData)
    {
        if (colonyData.Name != "Colony")
        {
            GD.PrintErr(colonyData.Name + " " + colonyData.ValueToString() + " is NOT a colony");
            return null;
        }

        for (int idx = 0; idx < game.Map.Data.Players.Count; idx++)
        {
            if (game.Map.Data.Players[idx].Colonies.Contains(colonyData))
            {
                return game.Map.Data.Players[idx];
            }
        }

        GD.PrintErr(colonyData.Name + " " + colonyData.ValueToString() + " - no player found");
        return null;
    }
}
