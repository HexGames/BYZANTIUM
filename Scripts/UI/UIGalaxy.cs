using Godot;
using Godot.Collections;
using System.IO;

public partial class UIGalaxy : Control
{
    [ExportCategory("Links")]
    //[Export]
    //public
    [Export]
    public Array<UIGalaxySystem> Systems = new Array<UIGalaxySystem>();
    [Export]
    public Array<UIGalaxyPath> Paths = new Array<UIGalaxyPath>();

    [Export]
    public UIGeneral General = null;

    [Export]
    public UIEconomyBar Resources = null;
    [Export]
    public UIGalaxyBarList GalaxyBar = null;
    [Export]
    public UIBuildings ColonyBuildings = null;
    [Export]
    public UIConstruction SectorConstruction = null;
    [Export]
    public UIShipbuilding SectorShipbuilding = null;
    [Export]
    public UIPops PopsInfo = null;
    [Export]
    public UIPlanetInfo PlanetInfo = null;
    [Export]
    public UIPopsControl ControlInfo = null;
    [Export]
    public UIPopsFactions FactionsInfo = null;
    [Export]
    public UISelectedFleets FleetsSelected = null;
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
    [Export]
    public bool SelectedTabBuildings = false;
    [Export]
    public bool SelectedTabPopulation = false;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Init();

        General.Visible = false;

        ColonyBuildings.Visible = false;
        SectorConstruction.Visible = false;
        SectorShipbuilding.Visible = false;
        PopsInfo.Visible = false;
        PlanetInfo.Visible = false;

        ControlInfo.Visible = false;
        FactionsInfo.Visible = false;

        FleetsSelected.Visible = false;
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

    public void Refresh(bool buildingsTab, bool populationTab)
    {
        RefreshLocationUI();

        if (buildingsTab) ShowBuildingsTab();
        else if (populationTab) ShowPopulationTab();
        //else HideAllTabs();
    }

    public void ShowBuildingsTab()
    {
        SelectedTabBuildings = true;
        SelectedTabPopulation = false;

        GalaxyBar.ShowBuildingsTab();

        ColonyBuildings.Visible = true;
        SectorConstruction.Visible = true;
        SectorShipbuilding.Visible = true;
        PopsInfo.Visible = SelectedColony != null;
        PlanetInfo.Visible = true;

        ControlInfo.Visible = false;
        FactionsInfo.Visible = false;
    }

    public void ShowPopulationTab()
    {
        SelectedTabBuildings = false;
        SelectedTabPopulation = true;

        GalaxyBar.ShowPopulationTab();

        ColonyBuildings.Visible = false;
        SectorConstruction.Visible = false;
        SectorShipbuilding.Visible = false;
        PopsInfo.Visible = SelectedColony != null;
        PlanetInfo.Visible = false;

        ControlInfo.Visible = SelectedColony != null;
        FactionsInfo.Visible = SelectedColony != null;
    }

    public void HideAllTabs()
    {
        SelectedTabBuildings = false;
        SelectedTabPopulation = false;

        GalaxyBar.HideAllTabs();
    }

    public void Refresh()
    {
        RefreshLocationUI();
        RefreshPawnsUI();
    }

    public void RefreshLocationUI()
    {
        bool refreshEconomy = false;
        bool refreshPopInfo = false;
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
                PlanetInfo.Visible = GalaxyBar.Tabs_BuildingsSelected.Visible;
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
                refreshEconomy = true;
                refreshPopInfo = true;
            }
            SelectedColony = Game.Input.SelectedPlanet?.Colony;

            if (SelectedColony != null)
            {
                // select colony
                //ColonyBuildings.Refresh(SelectedColony);
                //ColonyBuildings.Visible = true;
                refreshEconomy = true;
                refreshPopInfo = true;
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
                SectorShipbuilding.Visible = false;
                if (SelectedSystem == null && SelectedColony == null) refreshEconomy = true;
                if (SelectedColony == null) refreshPopInfo = true;
                if (SelectedSystem == null && SelectedStar == null) refreshGameBar = true;
            }
            SelectedSector = Game.Input.SelectedSector;

            if (SelectedSector != null)
            {
                // select sector
                SectorConstruction.Refresh(SelectedSector);
                SectorConstruction.Visible = GalaxyBar.Tabs_BuildingsSelected.Visible || SelectedPlanet == null;
                SectorShipbuilding.Refresh(SelectedSector);
                SectorShipbuilding.Visible = GalaxyBar.Tabs_BuildingsSelected.Visible || SelectedPlanet == null;
                if (SelectedSystem == null && SelectedColony == null) refreshEconomy = true;
                if (SelectedColony == null) refreshPopInfo = true;
                if (SelectedSystem == null && SelectedStar == null) refreshGameBar = true;
            }
        }

        if (refreshSelectedPlanet)
        {
            if (Game.GalaxyUI.ColonyBuildings.IsUpgradeWindowOpen()) Game.GalaxyUI.ColonyBuildings.CloseUpgradeWindow();
            if (Game.GalaxyUI.ColonyBuildings.IsUpgradingWindowOpen()) Game.GalaxyUI.ColonyBuildings.CloseUpgradingWindow();
            else
            {
                Game.GalaxyUI.ColonyBuildings.Visible = false;
                Game.GalaxyUI.GalaxyBar.Deselect();

                ControlInfo.Visible = false;
                FactionsInfo.Visible = false;
            }
        }

        if (refreshEconomy || refreshSelectedPlanet)
        {
            if (SelectedPlanet != null) Resources.Refresh(Game.HumanPlayer, SelectedPlanet);
            else if (SelectedStar != null) Resources.Refresh(Game.HumanPlayer,SelectedStar);
            else if (SelectedSector != null) Resources.Refresh(SelectedSector);
            else Resources.Refresh(Game.HumanPlayer);
        }

        if (refreshPopInfo)
        {
            if (SelectedColony != null && SelectedColony.Resources_PerTurn.GetPops() != null) 
            { 
                PopsInfo.Refresh(SelectedColony); 
                PopsInfo.Visible = true;

                ControlInfo.Refresh(SelectedColony);
                ControlInfo.Visible = GalaxyBar.Tabs_PopulationSelected.Visible;

                FactionsInfo.Refresh(SelectedColony);
                FactionsInfo.Visible = GalaxyBar.Tabs_PopulationSelected.Visible;
            }
            else if (SelectedSector != null) { /*PopsInfo.Refresh(SelectedSector);*/ PopsInfo.Visible = false; }
            else PopsInfo.Visible = false;
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
                ColonyBuildings.Visible = GalaxyBar.Tabs_BuildingsSelected.Visible;

                bool hasPops = SelectedPlanet.Colony != null && SelectedPlanet.Colony.IsWorld();
                GalaxyBar.ShowTabsSelector(true, hasPops);
                if (SelectedTabPopulation && hasPops == false /*&& ColonyBuildings.BuildingNone.Visible == false*/)
                {
                    ShowBuildingsTab();
                }
            }
            else
            {
                GalaxyBar.HideTabsSelector();
                ColonyBuildings.Visible = false;

                GalaxyBar.HideTabsSelector();

                SectorConstruction.Visible = SelectedSector != null && (GalaxyBar.Tabs_BuildingsSelected.Visible || SelectedPlanet == null);
                SectorShipbuilding.Visible = SelectedSector != null && (GalaxyBar.Tabs_BuildingsSelected.Visible || SelectedPlanet == null);
            }
        }
    }

    public void RefreshPawnsUI()
    {
        if (Game.Input.SelectedFleets.Count > 0)
        {
            //FleetsSelected.Refresh(Game.Input.SelectedFleets);
            //FleetsSelected.Visible = true;

            Array<DataBlock> datas = new Array<DataBlock>();
            datas.Add(Game.Input.SelectedFleets[0].Data);
            General.Refresh(datas);
            General.Visible = true;

            GalaxyBar.Visible = false;
        }
        else
        {
            FleetsSelected.Visible = false;
            General.Visible = false;

            GalaxyBar.Visible = true;
        }
    }

    public void AddPathLabel(GFXPathsItem pathGFX)
    {
        UIGalaxyPath newPath = Paths[0].Duplicate(7) as UIGalaxyPath;
        Paths[0].GetParent().AddChild(newPath);
        Paths.Add(newPath);

        newPath._PathGFX = pathGFX;
        pathGFX.HUD = newPath;
    }

    public void StartTurn()
    {

        Game.Paths.ClearAllPaths();

        // Refresh system GFX
        for (int idx = 0; idx < Game.Map.Data.Stars.Count; idx++)
        {
            StarData star = Game.Map.Data.Stars[idx];
            star._Node.GFX.RefreshPlayerColors();
            star._Node.GFX.RefreshShips();
        }

        // refhresh paths GFX
        for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
        {
            PlayerData player = Game.Map.Data.Players[playerIdx];
            for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
            {
                FleetData fleet = player.Fleets[fleetIdx];
                if (fleet.MoveAction != null)
                {
                    StarData toStar = Data.GetLinkStarData(fleet.MoveAction, Game.Map.Data);
                    Game.Paths.AddPath(fleet, toStar);
                }
            }
        }

        Resources.Refresh(Game.TurnLoop.CurrentHumanPlayerData);
        GalaxyBar.Refresh(Game.TurnLoop.CurrentHumanPlayerData);

        CurrentTurn.Text = "Current Turn: " + Game.Map.Data.Turn.ToString();
    }

    public void OnEndTurn()
    {
        Game.Input.DeselectAll();

        Game.TurnLoop.CurrentPlayerData.TurnFinished = true;
        Game.TurnLoop.WaitingForHuman = false;
    }
}