using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class EventMeet
{
    static public void PlayerMeetPlayer(PlayerData player_1, PlayerData player_2)
    {
        DataBlock reladionsParent = Game.self.Map.Data._Data.GetSub("Relations");
        
        DataBlock relationData = Data.AddData(reladionsParent, "Relation", player_1.PlayerName + ":" + player_2.PlayerName, Game.self.Def);
        Data.AddData(relationData, "Player_1", player_1.PlayerName, Game.self.Def);
        Data.AddData(relationData, "Player_2", player_2.PlayerName, Game.self.Def);
        Data.AddData(relationData, "Value", 0, Game.self.Def);
        //Data.AddData(relationData, "Balance", 0, Game.self.Def);
        Data.AddData(relationData, "Player_1_Favors", 0, Game.self.Def);
        Data.AddData(relationData, "Player_2_Favors", 0, Game.self.Def);

        RelationData relation = new RelationData(relationData);
        Game.self.Map.Data.Relations.Add(relation);
        player_1.Relations.Add(relation);
        player_2.Relations.Add(relation);

        for (int idx = 0; idx < player_2.Systems.Count; idx++)
        {
            player_2.Systems[idx].Star.Visibility_PerTurn.SetAsUncovered(player_1);
        }
        for (int idx = 0; idx < player_1.Systems.Count; idx++)
        {
            player_1.Systems[idx].Star.Visibility_PerTurn.SetAsUncovered(player_2);
        }
    }
}