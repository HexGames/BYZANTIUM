using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

public partial class UIGalaxy : Control
{
    [ExportCategory("Links")]
    //[Export]
    //public
    //[Export]
    //public Array<UIGalaxySystem> Systems = new Array<UIGalaxySystem>();
    [Export]
    public UI3DManager UI3DManager = null;
    [Export]
    public Array<UIGalaxyPath> Incoming = new Array<UIGalaxyPath>();
    [Export]
    public Array<UIGalaxyPath> Paths = new Array<UIGalaxyPath>();

    [Export]
    public UIGeneral General = null;

    [Export]
    public UIEconomyBar Stockpiles = null;
    //[Export]
    //public UIGalaxyBarList GalaxyBar = null;
    //[Export]
    //public UIBuildings ColonyBuildings = null;
    //[Export]
    //public UIConstruction SectorConstruction = null;
    //[Export]
    //public UIShipbuilding SectorShipbuilding = null;
    //[Export]
    //public UIPops PopsInfo = null;
    [Export]
    public UIDiplomacyBar DiplomacyBar = null;
    [Export]
    public UIDiplomacyWindow Diplomacy = null;
    [Export]
    public UIStarInfo SystemInfo = null;
    //[Export]
    //public UIPlanetInfo PlanetInfo = null;
    [Export]
    public UIDistricts DistrictsInfo = null;
    //[Export]
    //public UIPlanetInfo PlanetInfo = null;
    //[Export]
    //public UIPopsControl ControlInfo = null;
    //[Export]
    //public UIPopsFactions FactionsInfo = null;
    [Export]
    public UISelectedFleets FleetsSelected = null;

    [Export]
    public UIActionsEconomy ActionsEconomy = null;

    [Export]
    public UIActionsChoice ActionsChoice = null;

    [Export]
    public Control EndTurnBg = null;
    [Export]
    public Label CurrentTurn = null;
    [Export]
    public RichTextLabel DEBUGText = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        //Init();

        General.Visible = false;

        //ColonyBuildings.Visible = false;
        //SectorConstruction.Visible = false;
        //SectorShipbuilding.Visible = false;
        //PopsInfo.Visible = false;
        Stockpiles.Visible = false;
        DiplomacyBar.Visible = false;
        //Diplomacy.Visible = false;
        SystemInfo.Visible = false;
        //PlanetInfo.Visible = false;
        DistrictsInfo.Visible = false;
        //ControlInfo.Visible = false;
        //FactionsInfo.Visible = false;
        FleetsSelected.Visible = false;

        ActionsEconomy.Visible = false;
    }
    /*
    public void ShowStarInfo(StarData selectedStar, StarData hoveredStar)
    {
        SystemInfo.Visible = true;
        SystemInfo.Refresh(hoveredStar != null ? hoveredStar : selectedStar);
        
        DistrictsInfo.Visible = true;
        DistrictsInfo.RefreshStar(hoveredStar != null ? hoveredStar : selectedStar);
        
        DiplomacyBar.Visible = false;

        ActionsEconomy.Visible = true;
        //ActionsEconomy.Refresh(selectedStar, hoveredStar);

        ActionsChoice.Visible = false;
    }

    public void ShowStarInfo_ActionSelectPlanet<T>(StarData selectedStar, List<T> possibleActions) where T : ActionBase
    {
        SystemInfo.Visible = false;

        Game.self.UIGalaxy.DistrictsInfo.RefreshButtons(possibleActions);

        //Game.self.UIGalaxy.ActionsEconomy.RefreshSelectPlanet();
    }

    public void ShowStarInfo_ShowMakeChoice<T>(StarData selectedStar, List<T> possibleActions) where T : ActionBase
    {
        Game.self.UIGalaxy.DistrictsInfo.Visible = false;
        Game.self.UIGalaxy.ActionsEconomy.Visible = false;

        ActionsChoice.Visible = true;
        ActionsChoice.Refresh(possibleActions);
    }

    public void ShowStarInfo_CancelMakeChoice()
    {
        ActionsChoice.Visible = false;
        Game.self.UIGalaxy.DistrictsInfo.Visible = true;
        Game.self.UIGalaxy.ActionsEconomy.Visible = true;
    }

    public void ShowStarInfo_CancelAction(StarData selectedStar)
    {
        SystemInfo.Visible = true;

        Game.self.UIGalaxy.DistrictsInfo.HideButtons();

        //Game.self.UIGalaxy.ActionsEconomy.Refresh(selectedStar, null);
    }
    public void HideStarInfo()
    {
        SystemInfo.Visible = false;
        DistrictsInfo.Visible = false;
    
        DiplomacyBar.Visible = true;

        ActionsEconomy.Visible = false;
    }
    
    */
    /*
    public void ShowFleetsInfo(Array<FleetData> selectedFleetList, FleetData selectedFleet)
    {
        FleetsSelected.Visible = true;
        FleetsSelected.Refresh(selectedFleetList, selectedFleet);
    }

    public void HideFleetsInfo()
    {
        FleetsSelected.Visible = false;
    }

    public void ShowPlanetInfo(PlanetData selectedPlanet)
    {
        //PlanetInfo.Visible = true;
        DistrictsInfo.RefreshPlanet(selectedPlanet);
        //DiplomacyBar.Visible = false;
    }

    public void HidePlanetInfo()
    {
        //DistrictsInfo.RefreshAll();
        //PlanetInfo.Visible = false;
        //DiplomacyBar.Visible = true;
    }*/

    public void AddPathLabel(GFXPathsItem pathGFX)
    {
        UIGalaxyPath newPath = Paths[0].Duplicate(7) as UIGalaxyPath;
        Paths[0].GetParent().AddChild(newPath);
        Paths.Add(newPath);
    
        newPath._PathGFX = pathGFX;
        pathGFX.HUD = newPath;
    }

    public void RefreshAllPathsLabels()
    {
        for (int idx = 0; idx < Paths.Count; idx++)
        {
            Paths[idx].Refresh();
        }
    }

    public void StartTurn()
    {
        Stockpiles.Visible = true;
        DiplomacyBar.Visible = true;

        Stockpiles.Refresh();
        DiplomacyBar.Refresh();
    }

    //public void AddIncomingLabel(GFXIncomingsItem incomingGFX)
    //{
    //    UIGalaxyPath newIncoming = Incoming[0].Duplicate(7) as UIGalaxyPath;
    //    Incoming[0].GetParent().AddChild(newIncoming);
    //    Incoming.Add(newIncoming);
    //
    //    newIncoming._IncomingGFX = incomingGFX;
    //    incomingGFX.HUD = newIncoming;
    //}

    //public void Refresh(bool buildingsTab, bool populationTab)
    //{
    //    RefreshLocationUI();
    //
    //    //else HideAllTabs();
    //}
    //
    //public void Refresh()
    //{
    //    RefreshLocationUI();
    //    RefreshPawnsUI();
    //}
    //
    //public void RefreshLocationUI()
    //{
    //    bool refreshEconomy = false;
    //    bool refreshPopInfo = false;
    //    bool refreshGameBar = false;
    //    bool refreshSelectedPlanet = false;
    //
    //    // planet
    //    if (SelectedPlanet != Game.Input.SelectedPlanet)
    //    {
    //        if (SelectedPlanet != null && Game.Input.SelectedPlanet == null)
    //        {
    //            // deselect planet
    //            PlanetInfo.Visible = false;
    //            refreshSelectedPlanet = true;
    //        }
    //        SelectedPlanet = Game.Input.SelectedPlanet;
    //
    //        if (SelectedPlanet != null)
    //        {
    //            // select planet
    //            PlanetInfo.Refresh(SelectedPlanet);
    //            PlanetInfo.Visible = GalaxyBar.Tabs_BuildingsSelected.Visible;
    //            refreshSelectedPlanet = true;
    //        }
    //    }
    //
    //    // colony
    //    if (SelectedColony != Game.Input.SelectedPlanet?.Colony)
    //    {
    //        if (SelectedColony != null && Game.Input.SelectedPlanet?.Colony == null)
    //        {
    //            // deselect colony
    //            //ColonyBuildings.Visible = false;
    //            refreshEconomy = true;
    //            refreshPopInfo = true;
    //        }
    //        SelectedColony = Game.Input.SelectedPlanet?.Colony;
    //
    //        if (SelectedColony != null)
    //        {
    //            // select colony
    //            //ColonyBuildings.Refresh(SelectedColony);
    //            //ColonyBuildings.Visible = true;
    //            refreshEconomy = true;
    //            refreshPopInfo = true;
    //        }
    //    }
    //
    //    // star
    //    if (SelectedStar != Game.Input.SelectedStar)
    //    {
    //        SelectedStar = Game.Input.SelectedStar;
    //        refreshGameBar = true;
    //    }
    //
    //    // system
    //    if (SelectedSystem != Game.Input.SelectedStar?.System)
    //    {
    //        if (SelectedSystem != null && Game.Input.SelectedStar == null)
    //        {
    //            // deselect system
    //            if (SelectedColony == null) refreshEconomy = true;
    //            refreshGameBar = true;
    //        }
    //        SelectedSystem = Game.Input.SelectedStar?.System;
    //
    //        if (SelectedSystem != null)
    //        {
    //            // select system
    //            if (SelectedColony == null) refreshEconomy = true;
    //            refreshGameBar = true;
    //        }
    //    }
    //
    //    // sector
    //    if (SelectedSector != Game.Input.SelectedSector)
    //    {
    //        if (SelectedSector != null && Game.Input.SelectedSector == null)
    //        {
    //            // deselect sector
    //            SectorConstruction.Visible = false;
    //            SectorShipbuilding.Visible = false;
    //            if (SelectedSystem == null && SelectedColony == null) refreshEconomy = true;
    //            if (SelectedColony == null) refreshPopInfo = true;
    //            if (SelectedSystem == null && SelectedStar == null) refreshGameBar = true;
    //        }
    //        SelectedSector = Game.Input.SelectedSector;
    //
    //        if (SelectedSector != null)
    //        {
    //            // select sector
    //            SectorConstruction.Refresh(SelectedSector);
    //            SectorConstruction.Visible = GalaxyBar.Tabs_BuildingsSelected.Visible || SelectedPlanet == null;
    //            SectorShipbuilding.Refresh(SelectedSector);
    //            SectorShipbuilding.Visible = GalaxyBar.Tabs_BuildingsSelected.Visible || SelectedPlanet == null;
    //            if (SelectedSystem == null && SelectedColony == null) refreshEconomy = true;
    //            if (SelectedColony == null) refreshPopInfo = true;
    //            if (SelectedSystem == null && SelectedStar == null) refreshGameBar = true;
    //        }
    //    }
    //
    //    if (refreshSelectedPlanet)
    //    {
    //        if (Game.GalaxyUI.ColonyBuildings.IsUpgradeWindowOpen()) Game.GalaxyUI.ColonyBuildings.CloseUpgradeWindow();
    //        if (Game.GalaxyUI.ColonyBuildings.IsUpgradingWindowOpen()) Game.GalaxyUI.ColonyBuildings.CloseUpgradingWindow();
    //        else
    //        {
    //            Game.GalaxyUI.ColonyBuildings.Visible = false;
    //            Game.GalaxyUI.GalaxyBar.Deselect();
    //
    //            ControlInfo.Visible = false;
    //            FactionsInfo.Visible = false;
    //        }
    //    }
    //
    //    if (refreshEconomy || refreshSelectedPlanet)
    //    {
    //        if (SelectedPlanet != null) Resources.Refresh(Game.HumanPlayer, SelectedPlanet);
    //        else if (SelectedStar != null) Resources.Refresh(Game.HumanPlayer,SelectedStar);
    //        else if (SelectedSector != null) Resources.Refresh(SelectedSector);
    //        else Resources.Refresh(Game.HumanPlayer);
    //    }
    //
    //    if (refreshPopInfo)
    //    {
    //        if (SelectedColony != null && SelectedColony.Resources_PerTurn.GetPops() != null) 
    //        { 
    //            PopsInfo.Refresh(SelectedColony); 
    //            PopsInfo.Visible = true;
    //
    //            ControlInfo.Refresh(SelectedColony);
    //            ControlInfo.Visible = GalaxyBar.Tabs_PopulationSelected.Visible;
    //
    //            FactionsInfo.Refresh(SelectedColony);
    //            FactionsInfo.Visible = GalaxyBar.Tabs_PopulationSelected.Visible;
    //        }
    //        else if (SelectedSector != null) { /*PopsInfo.Refresh(SelectedSector);*/ PopsInfo.Visible = false; }
    //        else PopsInfo.Visible = false;
    //    }
    //
    //    if (refreshGameBar)
    //    {
    //        if (SelectedSystem != null) GalaxyBar.Refresh(SelectedSystem);
    //        else if (SelectedStar != null) GalaxyBar.Refresh(SelectedStar);
    //        else if (SelectedSector != null) GalaxyBar.Refresh(SelectedSector);
    //        else GalaxyBar.Refresh(Game.HumanPlayer);
    //    }
    //
    //    if ((SelectedSystem != null || SelectedStar != null) && refreshSelectedPlanet)
    //    {
    //        GalaxyBar.Select(SelectedPlanet);
    //        if (SelectedPlanet != null)
    //        {
    //            ColonyBuildings.Refresh(SelectedPlanet);
    //            ColonyBuildings.Visible = GalaxyBar.Tabs_BuildingsSelected.Visible;
    //
    //            bool hasPops = SelectedPlanet.Colony != null && SelectedPlanet.Colony.IsWorld();
    //            GalaxyBar.ShowTabsSelector(true, hasPops);
    //            //if (SelectedTabPopulation && hasPops == false /*&& ColonyBuildings.BuildingNone.Visible == false*/)
    //            //{
    //            //    ShowBuildingsTab();
    //            //}
    //        }
    //        else
    //        {
    //            GalaxyBar.HideTabsSelector();
    //            ColonyBuildings.Visible = false;
    //
    //            GalaxyBar.HideTabsSelector();
    //
    //            SectorConstruction.Visible = SelectedSector != null && (GalaxyBar.Tabs_BuildingsSelected.Visible || SelectedPlanet == null);
    //            SectorShipbuilding.Visible = SelectedSector != null && (GalaxyBar.Tabs_BuildingsSelected.Visible || SelectedPlanet == null);
    //        }
    //    }
    //}

    //public void RefreshPawnsUI()
    //{
    //    if (Game.Input.SelectedFleets.Count > 0)
    //    {
    //        FleetsSelected.Refresh(Game.Input.SelectedFleets);
    //        FleetsSelected.Visible = true;
    //
    //        //Array<DataBlock> datas = new Array<DataBlock>();
    //        //datas.Add(Game.Input.SelectedFleets[0].Data);
    //        //General.Refresh(datas);
    //        //General.Visible = true;
    //
    //        GalaxyBar.Visible = false;
    //    }
    //    else
    //    {
    //        FleetsSelected.Visible = false;
    //        //General.Visible = false;
    //
    //        GalaxyBar.Visible = true;
    //    }
    //}

    //public void HoverPlanet(PlanetData planet)
    //{
    //    PlanetInfo.Refresh(planet);
    //    PlanetInfo.Visible = true;
    //}
    //
    //public void UnhoverPlanet()
    //{
    //    if (SelectedPlanet != null)
    //    {
    //        PlanetInfo.Refresh(SelectedPlanet);
    //        PlanetInfo.Visible = true;
    //    }
    //    else
    //    {
    //        PlanetInfo.Visible = false;
    //    }
    //}
    //
    //public void AddIncomingLabel(GFXIncomingsItem incomingGFX)
    //{
    //    UIGalaxyPath newIncoming = Incoming[0].Duplicate(7) as UIGalaxyPath;
    //    Incoming[0].GetParent().AddChild(newIncoming);
    //    Incoming.Add(newIncoming);
    //
    //    newIncoming._IncomingGFX = incomingGFX;
    //    incomingGFX.HUD = newIncoming;
    //}
    //
    //public void StartTurn()
    //{
    //
    //    Game.Paths.ClearAllPaths();
    //
    //    // Refresh system GFX
    //    for (int idx = 0; idx < Game.Map.Data.Stars.Count; idx++)
    //    {
    //        StarData star = Game.Map.Data.Stars[idx];
    //        //TEMP01 star._Node.GFX.RefreshPlayerColors();
    //        //TEMP01 star._Node.GFX.RefreshShips();
    //    }
    //
    //    // refhresh paths GFX
    //    for (int playerIdx = 0; playerIdx < Game.Map.Data.Players.Count; playerIdx++)
    //    {
    //        PlayerData player = Game.Map.Data.Players[playerIdx];
    //        for (int fleetIdx = 0; fleetIdx < player.Fleets.Count; fleetIdx++)
    //        {
    //            FleetData fleet = player.Fleets[fleetIdx];
    //            if (fleet.MoveAction != null)
    //            {
    //                StarData toStar = Data.GetLinkStarData(fleet.MoveAction, Game.Map.Data);
    //                Game.Paths.AddPath(fleet, toStar);
    //            }
    //        }
    //    }
    //
    //    Resources.Refresh(Game.TurnLoop.CurrentHumanPlayerData);
    //    GalaxyBar.Refresh(Game.TurnLoop.CurrentHumanPlayerData);
    //
    //    CurrentTurn.Text = "Current Turn: " + Game.Map.Data.Turn.ToString();
    //}
    //
    public void OnEndTurn()
    {
        if (Game.self.TurnLoop.CurrentPlayerData != null)
        {
            //Game.self.Input.ZoomOut();
            Game.self.UI.DeselectAll();

            Game.self.TurnLoop.CurrentPlayerData.TurnFinished = true;
            Game.self.TurnLoop.WaitingForHuman = false;
        }
    }
}