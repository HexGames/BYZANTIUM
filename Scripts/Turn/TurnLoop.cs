using Godot;
using Godot.Collections;
using System;
using static MEC.Timing;

// Generated
public partial class TurnLoop : Node
{
    [ExportCategory("Runtime")]
    [Export]
    public bool WaitingForHuman = false;
    [Export]
    public bool WaitingForEndTurn = false;

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

    //[ExportCategory("Runtime - Links")]
    //[Export]
    //public Game Game = null;

    //public override void _Ready()
    //{
    //    if (!Engine.IsEditorHint())
    //    {
    //        Game = GetNode<Game>("/root/Main/Game");
    //    }
    //}

    public void Init()
    {
        // session
        Init_FeatureData();
        Init_DistrictData();

        CurrentHumanPlayerData = GetHumanPlayer();
        Init_Resources();
        Init_Fleets();
        StartTurn_Fleets();
        StartTurn_Resources();
        StartTurn_NewActions();

        // update UI
        //Game.GalaxyUI.StartTurn();//TEMP02
        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            star._Node.GFX.RefreshPlayerColor();
        }

        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            star._Node.GFX.RefreshShips();
        }
    }
    public void Init_FeatureData()
    {
        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            for (int planetIdx = 0; planetIdx < star.Planets.Count; planetIdx++)
            {
                PlanetData planet = star.Planets[planetIdx];
                planet.Features.Clear();
                Array<DataBlock> features = planet.Data.GetSub("Features").GetSubs();
                for (int featureIdx = 0; featureIdx < features.Count; featureIdx++)
                {
                    planet.Features.Add(new FeatureData(features[featureIdx], planet));
                }
            }
        }
    }

    public void Init_DistrictData()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int systemIdx = 0; systemIdx < player.Systems.Count; systemIdx++)
            {
                SystemData system = player.Systems[systemIdx];
                for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
                {
                    ColonyData colony = system.Colonies[colonyIdx];
                    colony.Districts.Clear();
                    Array<DataBlock> districts = colony.Data.GetSub("Districts").GetSubs("District");
                    for (int districtIdx = 0; districtIdx < districts.Count; districtIdx++)
                    {
                        colony.Districts.Add(new DistrictData(districts[districtIdx], colony));
                    }
                }
            }
        }
    }

    public void Init_Resources()
    {
        for (int starIdx = 0; starIdx < Game.self.Map.Data.Stars.Count; starIdx++)
        {
            StarData star = Game.self.Map.Data.Stars[starIdx];
            for (int planetIdx = 0; planetIdx < star.Planets.Count; planetIdx++)
            {
                PlanetData planet = star.Planets[planetIdx];
                //planet.BaseResources_PerTurn = new ResourcesWrapper(planet.Resources, ResourcesWrapper.ParentType.Planet);
            }
        }

        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            //player.Resources_PerTurn = new ResourcesWrapper(player.Resources, ResourcesWrapper.ParentType.Player);

            for (int sectorIdx = 0; sectorIdx < player.Systems.Count; sectorIdx++)
            {
                SystemData system = player.Systems[sectorIdx];
                system.Resources_PerTurn = new ResourcesWrapper(system.Resources, ResourcesWrapper.ParentType.SYSTEM);
                system.Pops_PerTurn = new PopsWrapper(system);
                system.Buildings_PerTurn = new BuildingsWrapper(system);
                system.QueueDistricts_PerTurn = new DistrictQueueWrapper(system);
                //sector.BuildQueue_PerTurn_ActionChange = new BuildingQueueWrapper(sector, Game);
                //sector.BudgetPerTurn = new BudgetWrapper(sector.Budget);
                for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
                {
                    ColonyData colony = system.Colonies[colonyIdx];
                    colony.Resources_PerTurn = new ResourcesWrapper(colony.Resources, ResourcesWrapper.ParentType.COLONY);
                    colony.Pops_PerTurn = new PopsWrapper(colony);
                    colony.Buildings_PerTurn = new BuildingsWrapper(colony);
                }
            }
        }
    }

    public void Init_Fleets()
    {
        for (int playerIdx = 0; playerIdx < Game.self.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.self.Map.Data.Players[playerIdx];
            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];
                fleet.Stats_PerTurn = new FleetStatsWrapper(fleet);
            }
        }
    }

    public override void _Process(double delta)
    {
        if (WaitingForEndTurn)
        {
            return;
        }

        if (CurrentHumanPlayerData == null)
        {
            Init();
        }

        if (WaitingForHuman)
        {
            return;
        }

        if (CurrentPlayerData == null || CurrentPlayerData.TurnFinished == true)
        {
            CurrentPlayerData = GetNextPlayer(out CurrentPlayerIdx);
        }

        if (CurrentPlayerData == null)
        {
            WaitingForEndTurn = true;
            RunCoroutine(EndTurn().CancelWith(this));
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
    }

    PlayerData GetNextPlayer(out int playeridx)
    {
        for (int idx = 0; idx < Game.self.Map.Data.Players.Count; idx++)
        {
            if (Game.self.Map.Data.Players[idx].TurnFinished == false)
            {
                playeridx = idx;
                return Game.self.Map.Data.Players[idx];
            }
        }
        playeridx = -1;
        return null;
    }

    PlayerData GetHumanPlayer()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Players.Count; idx++)
        {
            if (Game.self.Map.Data.Players[idx].Human)
            {
                return Game.self.Map.Data.Players[idx];
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
