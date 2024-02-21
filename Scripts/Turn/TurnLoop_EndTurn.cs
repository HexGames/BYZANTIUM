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

        // update resources
        EndTurn_Resources();

        // update UI
        Game.GalaxyUI.Refresh();

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
            for (int colonyIdx = 0; colonyIdx < player.Colonies.Count; colonyIdx++)
            {
                ColonyData colony = player.Colonies[colonyIdx];

                if (colony.ActionBuild != null)
                {
                    ActionColonyBuild.Update(colony, Game.Def);
                }
            }
        }
    }

    // ----------------------------------------------------------------------------------------------
    private class ResourceInfo
    {
        public enum Type
        {
            VALUE,
            VALUE_INCOME,
            INCOME,
            TOTAL_USED
        }

        public Type ResType = Type.VALUE;

        public int Value_1 = 0;
        public int Value_2 = 0;

        public string Name = "Res";
    };

    private void EndTurn_Resources()
    {
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.Map.Data.Players[playerIdx];

            ResourcesWrapperTemp playerRes = new ResourcesWrapperTemp(player.Resources);

            playerRes.Clear();

            for (int colonyIdx = 0; colonyIdx < player.Colonies.Count; colonyIdx++)
            {
                ColonyData colony = player.Colonies[colonyIdx];
                Array<DataBlock> buildings = colony.Buildings.GetSubs();
                for (int buildingIdx = 0; buildingIdx < buildings.Count; buildingIdx++)
                {
                    ActionTargetInfo buildingInfo =  Game.Def.GetBuildingInfo(buildings[buildingIdx].Name);
                    int buildingCount = buildings[buildingIdx].ValueI;

                    if (buildingInfo == null)
                    {
                        GD.Print("BUILDING NOT FOUND! - " + buildings[buildingIdx].Name);
                        continue;
                    }

                    playerRes.Add(buildingInfo.Benefit, buildingCount);
                }

                if (colony.ActionBuild != null)
                {
                    ActionTargetInfo buildingInfo = Game.Def.GetBuildingInfo(colony.ActionBuild.GetSub("Building").ValueS);

                    if (buildingInfo == null)
                    {
                        GD.Print("BUILDING NOT FOUND! - " + colony.ActionBuild.GetSub("Building").ValueS);
                        continue;
                    }

                    playerRes.Use(buildingInfo.Cost);
                }
            }

            playerRes.AddIncome();

            playerRes.Save();
        }
    }
}
