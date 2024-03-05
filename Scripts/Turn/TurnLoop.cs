using Godot;
using System;

// Generated
public partial class TurnLoop : Node
{
    [ExportCategory("Runtime")]
    [Export]
    public bool WaitingForHuman = false; 

    [ExportCategory("Runtime - Player")]
    [Export]
    public int CurrentPlayerIdx = -1;
    [Export]
    public PlayerData CurrentPlayerData = null;
    [Export]
    public PlayerData CurrentHumanPlayerData = null;

    //[ExportCategory("Runtime - Colony")]
    //[Export]
    //public int CurrentColonyIdx = -1;
    //[Export]
    //public DataBlock CurrentColonyData = null;
    //[Export]
    //public SystemNode CurrentColonyLocation = null;
    //[Export]
    //public DataBlock CurrentColonyAction = null;

    [ExportCategory("Runtime - Links")]
    [Export]
    public Game Game = null;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
        }
    }

    public void Init()
    {
        CurrentHumanPlayerData = GetHumanPlayer();
        Init_Resources();
        EndTurn_Resources();
        Game.GalaxyUI.Refresh();
    }

    public void Init_Resources()
    {
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.Map.Data.Players[playerIdx];
            player.ResourcesPerTurn = new ResourcesWrapperTemp(player.Resources);

            for (int sectorIdx = 0; sectorIdx < player.Sectors.Count; sectorIdx++)
            {
                SectorData sector = player.Sectors[sectorIdx];
                sector.ResourcesPerTurn = new ResourcesWrapperTemp(sector.Resources);
                //sector.BudgetPerTurn = new BudgetWrapper(sector.Budget);

                for (int systemIdx = 0; systemIdx < sector.Systems.Count; systemIdx++)
                {
                    SystemData system = sector.Systems[sectorIdx];
                    system.ResourcesPerTurn = new ResourcesWrapperTemp(system.Resources);

                    for (int colonyIdx = 0; colonyIdx < sector.Systems.Count; colonyIdx++)
                    {
                        ColonyData colony = system.Colonies[colonyIdx];
                        colony.ResourcesPerTurn = new ResourcesWrapperTemp(colony.Resources);
                        //colony.ActionsConPerTurn = new ActionsConWrapper(colony.ActionConBuildings, colony.ActionConColony, colony.ActionConShipyard, sector.ActionConTreasury);
                    }
                }
            }
        }
    }

    public override void _Process(double delta)
    {
        if (CurrentHumanPlayerData == null)
        {
            Init();
        }

        if (WaitingForHuman == true)
        {
            return;
        }

        if (CurrentPlayerData == null || CurrentPlayerData.TurnFinished == true)
        {
            CurrentPlayerData = GetNextPlayer(out CurrentPlayerIdx);
        }

        if (CurrentPlayerData == null)
        {
            EndTurn();
            return;
        }

        if (CurrentPlayerData.Human == true)
        {
            // HUMAN
            WaitingForHuman = true;
        }
        else
        {
            // AI
            CurrentPlayerData.TurnFinished = true;
        }

        //CurrentColonyData = GetNextColony(CurrentPlayerData, out CurrentColonyIdx, out CurrentColonyLocation, out CurrentColonyAction);
        //
        //if (CurrentColonyData != null)
        //{
        //    if (CurrentPlayerData.Human == true)
        //    {
        //        // HUMAN
        //        WaitingForHuman = true;
        //    }
        //    else
        //    {
        //        // AI
        //        CurrentColonyAction.ValueS = "Infinite_AI";
        //    }
        //}
    }

    PlayerData GetNextPlayer(out int playeridx)
    {
        for (int idx = 0; idx < Game.Map.Data.Players.Count; idx++)
        {
            if (Game.Map.Data.Players[idx].TurnFinished == false)
            {
                playeridx = idx;
                return Game.Map.Data.Players[idx];
            }
        }
        playeridx = -1;
        return null;
    }

    PlayerData GetHumanPlayer()
    {
        for (int idx = 0; idx < Game.Map.Data.Players.Count; idx++)
        {
            if (Game.Map.Data.Players[idx].Human)
            {
                return Game.Map.Data.Players[idx];
            }
        }
        return null;
    }

    //DataBlock GetNextColony(PlayerData playerData, out int colonyIdx, out SystemNode colonyLocation, out DataBlock colonyAction)
    //{
    //    for (int idx = 0; idx < playerData.Colonies.Count; idx++)
    //    {
    //        //DataBlock action = playerData.Colonies[idx].GetSub("Action");
    //        //if (action.ValueS == "None")
    //        //{
    //        //    colonyIdx = idx;
    //        //    colonyAction = action;
    //        //    colonyLocation = GetLocation(playerData.Colonies[idx].GetSub("System").ValueS);
    //        //    return playerData.Colonies[idx];
    //        //}
    //    }
    //
    //    colonyIdx = -1;
    //    colonyAction = null;
    //    colonyLocation = null;
    //    return null;
    //}

    //SystemNode GetLocation( string location )
    //{
    //    string system = Helper.Split_0(location);
    //
    //    for (int idx = 0; idx < Game.Map.Data.Systems.Count; idx++)
    //    {
    //        //if (Game.Map.Data.Systems[idx].System.ValueS == system)
    //        //{
    //        //    return Game.Map.Data.Systems[idx].GetLocationNode();
    //        //}
    //    }
    //
    //    GD.PrintErr("Location " + location + " not found!");
    //    return null;
    //}
}
