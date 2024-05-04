using Godot;
using Godot.Collections;

public partial class UIGalaxy : Control
{
    [ExportCategory("Links")]
    //[Export]
    //public
    [Export]
    public Array<UIGalaxySystem> Systems = new Array<UIGalaxySystem>();
    [Export]
    public UIEconomyBar Resources = null;
    [Export]
    public UIGalaxyBarList GalaxyBar = null;
    [Export]
    public UIBuildings ColonyBuildings = null;
    [Export]
    public UIEconomyInfo EconomyInfo = null;
    [Export]
    public UIConstruction SectorConstruction = null;
    [Export]
    public UIFocusList ColonyFocus = null;
    [Export]
    public UIItemList PlanetInfo = null;
    [Export]
    public Label CurrentTurn = null;

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;
    [Export]
    public SectorData SelectedSector = null;
    [Export]
    public SystemData SelectedSystem = null;
    [Export]
    public StarData SelectedStar = null;
    [Export]
    public ColonyData SelectedColony = null;
    [Export]
    public PlanetData SelectedPlanet = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Init();

        EconomyInfo.Visible = false;
        ColonyBuildings.Visible = false;
        SectorConstruction.Visible = false;
        ColonyFocus.Visible = false;
        PlanetInfo.Visible = false;
    }

    public void Init()
    {
        // delete surplus
        while (Systems.Count > Mathf.Max(Game.Map.Data.Stars.Count, 1))
        {
            Node node = Systems[Systems.Count - 1];
            Systems[0].GetParent().RemoveChild(node);
            node.Free();
        }

        // grow
        while (Systems.Count < Game.Map.Data.Stars.Count)
        {
            UIGalaxySystem newSys = Systems[0].Duplicate(7) as UIGalaxySystem;
            Systems[0].GetParent().AddChild(newSys);
            Systems.Add(newSys);
        }

        // update
        for (int idx = 0; idx < Systems.Count; idx++)
        {
            if (idx < Game.Map.Data.Stars.Count)
            {
                Systems[idx].Refresh(Game.Map.Data.Stars[idx]._Node);
            }
        }
    }

    public void Refresh()
    {
        bool refreshEconomy = false;
        bool refreshGameBar = false;
        bool refreshSelectedPlanet = false;

        // planet
        if (SelectedPlanet != Game.Input.SelectedPlanet)
        {
            if (SelectedPlanet != null && Game.Input.SelectedPlanet == null)
            {
                // deselect planet
                PlanetInfo.Visible = false;
                refreshSelectedPlanet = true;
            }
            SelectedPlanet = Game.Input.SelectedPlanet;

            if (SelectedPlanet != null)
            {
                // select planet
                PlanetInfo.Refresh(SelectedPlanet.Data);
                refreshSelectedPlanet = true;
            }
        }

        // colony
        if (SelectedColony != Game.Input.SelectedPlanet?.Colony)
        {
            if (SelectedColony != null && Game.Input.SelectedPlanet?.Colony == null)
            {
                // deselect colony
                //ColonyBuildings.Visible = false;
                ColonyFocus.Visible = false;
                refreshEconomy = true;
            }
            SelectedColony = Game.Input.SelectedPlanet?.Colony;

            if (SelectedColony != null)
            {
                // select colony
                //ColonyBuildings.Refresh(SelectedColony);
                //ColonyBuildings.Visible = true;
                ColonyFocus.Refresh(SelectedColony);
                refreshEconomy = true;
            }
        }

        // star
        if (SelectedStar != Game.Input.SelectedStar)
        {
            SelectedStar = Game.Input.SelectedStar;
            refreshGameBar = true;
        }

        // system
        if (SelectedSystem != Game.Input.SelectedStar?.System)
        {
            if (SelectedSystem != null && Game.Input.SelectedStar == null)
            {
                // deselect system
                if (SelectedColony == null) refreshEconomy = true;
                refreshGameBar = true;
            }
            SelectedSystem = Game.Input.SelectedStar?.System;

            if (SelectedSystem != null)
            {
                // select system
                if (SelectedColony == null) refreshEconomy = true;
                refreshGameBar = true;
            }
        }

        // sector
        if (SelectedSector != Game.Input.SelectedSector)
        {
            if (SelectedSector != null && Game.Input.SelectedSector == null)
            {
                // deselect sector
                SectorConstruction.Visible = false;
                if (SelectedSystem == null && SelectedColony == null) refreshEconomy = true;
                if (SelectedSystem == null && SelectedStar == null) refreshGameBar = true;
            }
            SelectedSector = Game.Input.SelectedSector;

            if (SelectedSector != null)
            {
                // select sector
                SectorConstruction.Refresh(SelectedSector);
                if (SelectedSystem == null && SelectedColony == null) refreshEconomy = true;
                if (SelectedSystem == null && SelectedStar == null) refreshGameBar = true;
            }
        }

        if (refreshEconomy)
        {
            if (SelectedColony != null) EconomyInfo.Refresh(SelectedColony);
            else if (SelectedSystem != null) EconomyInfo.Refresh(SelectedSystem);
            else if (SelectedSector != null) EconomyInfo.Refresh(SelectedSector);
            else EconomyInfo.Visible = false;
        }

        if (refreshGameBar)
        {
            if (SelectedSystem != null) GalaxyBar.Refresh(SelectedSystem);
            else if (SelectedStar != null) GalaxyBar.Refresh(SelectedStar);
            else if (SelectedSector != null) GalaxyBar.Refresh(SelectedSector);
            else GalaxyBar.Refresh(Game.HumanPlayer);
        }

        if ((SelectedSystem != null || SelectedStar != null) && refreshSelectedPlanet)
        {
            GalaxyBar.Select(SelectedPlanet);
            if (SelectedPlanet != null)
            {
                ColonyBuildings.Refresh(SelectedPlanet);
                ColonyBuildings.Visible = true;
            }
            else
            {
                ColonyBuildings.Visible = false;
            }
        }
    }


    public void StartTurn()
    {
        CurrentTurn.Text = "Current Turn: " + Game.Map.Data.Turn.ToString();

        Resources.Refresh(Game.TurnLoop.CurrentHumanPlayerData);
        GalaxyBar.Refresh(Game.TurnLoop.CurrentHumanPlayerData);
    }

    public void OnEndTurn()
    {
        Game.TurnLoop.CurrentPlayerData.TurnFinished = true;
        Game.TurnLoop.WaitingForHuman = false;
    }
}