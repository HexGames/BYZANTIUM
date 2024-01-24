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

    [ExportCategory("Runtime - Colony")]
    [Export]
    public int CurrentColonyIdx = -1;
    [Export]
    public DataBlock CurrentColonyData = null;
    [Export]
    public LocationNode CurrentColonyLocation = null;
    [Export]
    public DataBlock CurrentColonyAction = null;

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

    public override void _Process(double delta)
    {
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

        CurrentColonyData = GetNextColony(CurrentPlayerData, out CurrentColonyIdx, out CurrentColonyLocation, out CurrentColonyAction);

        if (CurrentColonyData != null)
        {
            if (CurrentPlayerData.Human == true)
            {
                // HUMAN
                WaitingForHuman = true;
            }
            else
            {
                // AI
                CurrentColonyAction.ValueS = "Infinite_AI";
            }
        }
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

    void EndTurn()
    {
        Game.Map.Data.Turn = Game.Map.Data.Turn + 1;
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            Game.Map.Data.Players[playerIdx].TurnFinished = true;
        }
    }

    DataBlock GetNextColony(PlayerData playerData, out int colonyIdx, out LocationNode colonyLocation, out DataBlock colonyAction)
    {
        for (int idx = 0; idx < playerData.Colonies.Count; idx++)
        {
            DataBlock action = playerData.Colonies[idx].GetSub("Action");
            if (action.ValueS == "None")
            {
                colonyIdx = idx;
                colonyAction = action;
                colonyLocation = GetLocation(playerData.Colonies[idx].GetSub("System").ValueS);
                return playerData.Colonies[idx];
            }
        }

        colonyIdx = -1;
        colonyAction = null;
        colonyLocation = null;
        return null;
    }

    LocationNode GetLocation( string location )
    {
        string system = Helper.Split_0(location);

        for (int idx = 0; idx < Game.Map.Data.Systems.Count; idx++)
        {
            if (Game.Map.Data.Systems[idx].System.ValueS == system)
            {
                return Game.Map.Data.Systems[idx].GetLocationNode();
            }
        }

        GD.PrintErr("Location " + location + " not found!");
        return null;
    }
}
