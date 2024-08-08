using Godot;
using Godot.Collections;
using System.IO;

public partial class UIGalaxy : Control
{
    public enum State
    {
        GALAXY,
        // end turn
        GALAXY_H_SECTOR,
        GALAXY_H_STAR,
        GALAXY_H_FLEET,
        GALAXY_FLEET,
        GALAXY_FLEET_H_FLEET,
        GALAXY_FLEET_H_SECTOR,
        GALAXY_FLEET_H_STAR,
        SECTOR,
        SECTOR_H_SECTOR, // ?
        SECTOR_H_STAR, 
        SECTOR_H_FLEET,
        SECTOR_FLEET,
        SECTOR_FLEET_H_FLEET,
        SECTOR_FLEET_H_STAR,
        STAR,
        STAR_H_STAR, // ?
        STAR_H_PLANET,
        STAR_H_FLEET,
        STAR_PLANET,
        STAR_PLANET_H_PLANET,
        STAR_PLANET_H_FLEET,
        STAR_FLEET,
        STAR_FLEET_H_FLEET,
        STAR_FLEET_H_PLANET,
    };

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
    public State UIState;
    [Export]
    public SectorData SelectedSector = null;
    [Export]
    public StarData SelectedStar = null;
    [Export]
    public SystemData SelectedStarSystem = null;
    [Export]
    public PlanetData SelectedPlanet = null;
    [Export]
    public ColonyData SelectedPlanetColony = null;
    [Export]
    public FleetData SelectedFleet = null;

    [Export]
    public SectorData HoverSector = null;
    [Export]
    public StarData HoverStar = null;
    [Export]
    public PlanetData HoverPlanet = null;
    [Export]
    public FleetData HoverFleet = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        //Init();

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

    public void OnHoverSector(SectorData sector)
    {
        HoverSector = sector;

        if (UIState == State.GALAXY) CS_Hover_from_Galaxy_to_GalaxyHoverSector();
        else if (UIState == State.GALAXY_FLEET) CS_Hover_from_GalaxyFleet_to_GalaxyFleetHoverSector();
        else if (UIState == State.SECTOR) CS_Hover_from_Sector_to_SectorHoverSector(); // ?
    }
    public void OnDehoverSector()
    {
        HoverSector = null;

        if (UIState == State.GALAXY_H_SECTOR) CS_Dehover_from_GalaxyHoverSector_to_Galaxy();
        else if (UIState == State.GALAXY_FLEET_H_SECTOR) CS_Dehover_from_GalaxyFleetHoverSector_to_GalaxyFleet();
        else if (UIState == State.SECTOR_H_SECTOR) CS_Refresh_from_SectorHoverSector_to_Sector(); // ?
    }

    public void OnSelectSector(SectorData sector)
    {
        SelectedSector = sector;

        if (UIState == State.GALAXY_H_SECTOR) CS_Select_from_GalaxyHoverSector_to_Sector();
        else if (UIState == State.GALAXY_FLEET_H_SECTOR) CS_Select_from_GalaxyFleetHoverSector_to_SectorFleet();
        else if (UIState == State.SECTOR_H_SECTOR) CS_Refresh_from_SectorHoverSector_to_Sector(); // ?
    }
    public void OnDeselectSector()
    {
        SelectedSector = null;

        if (UIState == State.SECTOR) CS_Deselect_from_Sector_to_Galaxy();
        else if (UIState == State.SECTOR_FLEET) CS_Deselect_from_SectorFleet_to_GalaxyFleet();
    }

    public void OnHoverStar(StarData star)
    {
        HoverStar = star;

        if (UIState == State.GALAXY) CS_Hover_from_Galaxy_to_GalaxyHoverStar();
        else if (UIState == State.SECTOR) CS_Hover_from_Sector_to_SectorHoverStar();
        else if (UIState == State.GALAXY_FLEET) CS_Hover_from_GalaxyFleet_to_GalaxyFleetHoverStar();
        else if (UIState == State.SECTOR_FLEET) CS_Hover_from_SectorFleet_to_SectorFleetHoverStar();
        else if (UIState == State.STAR) CS_Hover_from_Star_to_StarHoverStar();
    }
    public void OnDehoverStar()
    {
        HoverStar = null;

        if (UIState == State.GALAXY_H_STAR) CS_Dehover_from_GalaxyHoverStar_to_Galaxy();
        else if (UIState == State.SECTOR_H_STAR) CS_Dehover_from_SectorHoverStar_to_Sector();
        else if (UIState == State.GALAXY_FLEET_H_STAR) CS_Dehover_from_GalaxyFleetHoverStar_to_GalaxyFleet();
        else if (UIState == State.SECTOR_FLEET_H_STAR) CS_Dehover_from_SectorFleetHoverStar_to_SectorFleet();
        else if (UIState == State.STAR_H_STAR) CS_Refresh_from_StarHoverStar_to_Star();
    }

    public void OnSelectStar(StarData star)
    {
        SelectedStar = star;

        if (UIState == State.GALAXY_H_STAR) CS_Select_from_GalaxyHoverStar_to_Star();
        else if (UIState == State.SECTOR_H_STAR) CS_Select_from_SectorHoverStar_to_Star();
        else if (UIState == State.GALAXY_FLEET_H_STAR) CS_Select_from_GalaxyFleetHoverStar_to_StarFleet();
        else if (UIState == State.SECTOR_FLEET_H_STAR) CS_Select_from_SectorFleetHoverStar_to_StarFleet();
        else if (UIState == State.STAR_H_STAR) CS_Refresh_from_StarHoverStar_to_Star();
    }
    public void OnDeselectStar()
    {
        SelectedStar = null;

        if (UIState == State.STAR) CS_Deselect_from_Star_to_Galaxy();
        else if (UIState == State.STAR_FLEET) CS_Deselect_from_StarFleet_to_GalaxyFleet();
    }

    public void OnHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;

        if (UIState == State.STAR) CS_Hover_from_Star_to_StarHoverPlanet();
        else if (UIState == State.STAR_FLEET) CS_Hover_from_StarFleet_to_StarFleetHoverFleet();
        else if (UIState == State.STAR_PLANET) CS_Hover_from_StarPlanet_to_StarPlanetHoverPlanet();
    }
    public void OnDehoverPlanet()
    {
        HoverPlanet = null;

        if (UIState == State.STAR_H_PLANET) CS_Dehover_from_StarHoverPlanet_to_Star();
        else if (UIState == State.STAR_FLEET_H_PLANET) CS_Dehover_from_StarFleetHoverPlanet_to_StarFleet();
        else if (UIState == State.STAR_PLANET_H_PLANET) CS_Refresh_from_StarPlanetHoverPlanet_to_StarPlanet();
    }

    public void OnSelectPlanet(PlanetData planet)
    {
        SelectedPlanet = planet;

        if (UIState == State.STAR_H_PLANET) CS_Select_from_StarHoverPlanet_to_StarPlanet();
        else if (UIState == State.STAR_FLEET_H_PLANET) CS_Select_from_StarFleetHoverPlanet_to_StarPlanet();
        else if (UIState == State.STAR_PLANET_H_PLANET) CS_Refresh_from_StarPlanetHoverPlanet_to_StarPlanet();
    }
    public void OnDeselectPlanet()
    {
        SelectedPlanet = null;

        if (UIState == State.STAR_PLANET) CS_Deselect_from_StarPlanet_to_Star();
    }

    public void OnHoverFleet(FleetData fleet)
    {
        HoverFleet = fleet;

        if (UIState == State.GALAXY) CS_Hover_from_Galaxy_to_GalaxyHoverFleet();
        else if (UIState == State.GALAXY_FLEET) CS_Hover_from_GalaxyFleet_to_GalaxyFleetHoverFleet();
        else if (UIState == State.SECTOR) CS_Hover_from_Sector_to_SectorHoverFleet();
        else if (UIState == State.SECTOR_FLEET) CS_Hover_from_SectorFleet_to_SectorFleetHoverFleet();
        else if (UIState == State.STAR) CS_Hover_from_Star_to_StarHoverFleet();
        else if (UIState == State.STAR_PLANET) CS_Hover_from_StarPlanet_to_StarPlanetHoverFleet();
        else if (UIState == State.STAR_FLEET) CS_Hover_from_StarFleet_to_StarFleetHoverFleet();
    }
    public void OnDehoverFleet()
    {
        HoverFleet = null;

        if (UIState == State.GALAXY_H_FLEET) CS_Dehover_from_GalaxyHoverFleet_to_Galaxy();
        else if (UIState == State.GALAXY_FLEET_H_FLEET) CS_Refresh_from_GalaxyFleetHoverFleet_to_GalaxyFleet();
        else if (UIState == State.SECTOR_H_FLEET) CS_Dehover_from_SectorHoverFleet_to_Sector();
        else if (UIState == State.SECTOR_FLEET_H_FLEET) CS_Refresh_from_SectorFleetHoverFleet_to_SectorFleet();
        else if (UIState == State.STAR_H_FLEET) CS_Dehover_from_StarHoverFleet_to_Star();
        else if (UIState == State.STAR_PLANET_H_FLEET) CS_Dehover_from_StarPlanetHoverFleet_to_StarPlanet();
        else if (UIState == State.STAR_FLEET_H_FLEET) CS_Refresh_from_StarFleetHoverFleet_to_StarFleet();
    }

    public void OnSelectFleet(FleetData fleet)
    {
        SelectedFleet = fleet;

        if (UIState == State.GALAXY_H_FLEET) CS_Select_from_GalaxyHoverFleet_to_GalaxyFleet();
        else if (UIState == State.GALAXY_FLEET_H_FLEET) CS_Refresh_from_GalaxyFleetHoverFleet_to_GalaxyFleet();
        else if (UIState == State.SECTOR_H_FLEET) CS_Select_from_SectorHoverFleet_to_SectorFleet();
        else if (UIState == State.SECTOR_FLEET_H_FLEET) CS_Refresh_from_SectorFleetHoverFleet_to_SectorFleet();
        else if (UIState == State.STAR_H_FLEET) CS_Select_from_StarHoverFleet_to_StarFleet();
        else if (UIState == State.STAR_PLANET_H_FLEET) CS_Select_from_StarPlanetHoverFleet_to_StarFleet();
        else if (UIState == State.STAR_FLEET_H_FLEET) CS_Refresh_from_StarFleetHoverFleet_to_StarFleet();
    }
    public void OnDeselectFleet()
    {
        SelectedFleet = null;

        if (UIState == State.GALAXY_FLEET) CS_Deselect_from_GalaxyFleet_to_Galaxy();
        else if (UIState == State.SECTOR_FLEET) CS_Deselect_from_SectorFleet_to_Sector();
        else if (UIState == State.STAR_FLEET) CS_Deselect_from_StarFleet_to_Star();
    }

    // galaxy to sector
    private void CS_Hover_from_Galaxy_to_GalaxyHoverSector()
    {
        UIState = State.GALAXY_H_SECTOR;
    }
    private void CS_Dehover_from_GalaxyHoverSector_to_Galaxy()
    {
        UIState = State.GALAXY;
    }
    private void CS_Select_from_GalaxyHoverSector_to_Sector()
    {
        UIState = State.SECTOR;
    }
    private void CS_Deselect_from_Sector_to_Galaxy()
    {
        UIState = State.GALAXY;
    }

    // galaxy to star
    private void CS_Hover_from_Galaxy_to_GalaxyHoverStar()
    {
        UIState = State.GALAXY_H_STAR;
    }
    private void CS_Dehover_from_GalaxyHoverStar_to_Galaxy()
    {
        UIState = State.GALAXY;
    }
    private void CS_Select_from_GalaxyHoverStar_to_Star()
    {
        UIState = State.STAR;
    }
    private void CS_Deselect_from_Star_to_Galaxy()
    {
        UIState = State.GALAXY;
    }

    // sector to sector
    private void CS_Hover_from_Sector_to_SectorHoverSector()
    {
        UIState = State.SECTOR_H_SECTOR;
    }
    private void CS_Refresh_from_SectorHoverSector_to_Sector() // dehover & change
    {
        UIState = State.SECTOR;
    }

    // sector to star
    private void CS_Hover_from_Sector_to_SectorHoverStar()
    {
        UIState = State.SECTOR_H_STAR;
    }
    private void CS_Dehover_from_SectorHoverStar_to_Sector()
    {
        UIState = State.SECTOR;
    }
    private void CS_Select_from_SectorHoverStar_to_Star()
    {
        UIState = State.STAR;
    }
    private void CS_Deselect_from_Star_to_Sector()
    {
        UIState = State.SECTOR;
    }

    // star to star
    private void CS_Hover_from_Star_to_StarHoverStar()
    {
        UIState = State.STAR_H_STAR;
    }
    private void CS_Refresh_from_StarHoverStar_to_Star() // dehover & change
    {
        UIState = State.STAR;
    }

    // star to planet
    private void CS_Hover_from_Star_to_StarHoverPlanet()
    {
        UIState = State.STAR_H_PLANET;
    }
    private void CS_Dehover_from_StarHoverPlanet_to_Star()
    {
        UIState = State.STAR;
    }
    private void CS_Select_from_StarHoverPlanet_to_StarPlanet()
    {
        UIState = State.STAR_PLANET;
    }
    private void CS_Deselect_from_StarPlanet_to_Star()
    {
        UIState = State.STAR;
    }

    // star to planet
    private void CS_Hover_from_StarPlanet_to_StarPlanetHoverPlanet()
    {
        UIState = State.STAR_PLANET_H_FLEET;
    }
    private void CS_Refresh_from_StarPlanetHoverPlanet_to_StarPlanet() // dehover & change
    {
        UIState = State.STAR_PLANET;
    }

    // galaxy to galaxy fleet
    private void CS_Hover_from_Galaxy_to_GalaxyHoverFleet()
    {
        UIState = State.GALAXY_H_FLEET;
    }
    private void CS_Dehover_from_GalaxyHoverFleet_to_Galaxy()
    {
        UIState = State.GALAXY;
    }
    private void CS_Select_from_GalaxyHoverFleet_to_GalaxyFleet()
    {
        UIState = State.GALAXY_FLEET;
    }
    private void CS_Deselect_from_GalaxyFleet_to_Galaxy()
    {
        UIState = State.GALAXY;
    }

    // galaxy fleet to galaxy fleet
    private void CS_Hover_from_GalaxyFleet_to_GalaxyFleetHoverFleet()
    {
        UIState = State.GALAXY_FLEET_H_FLEET;
    }
    private void CS_Refresh_from_GalaxyFleetHoverFleet_to_GalaxyFleet() // dehover & change
    {
        UIState = State.GALAXY_FLEET;
    }

    // sector to sector fleet
    private void CS_Hover_from_Sector_to_SectorHoverFleet()
    {
        UIState = State.SECTOR_H_FLEET;
    }
    private void CS_Dehover_from_SectorHoverFleet_to_Sector()
    {
        UIState = State.SECTOR;
    }
    private void CS_Select_from_SectorHoverFleet_to_SectorFleet()
    {
        UIState = State.SECTOR_FLEET;
    }
    private void CS_Deselect_from_SectorFleet_to_Sector()
    {
        UIState = State.SECTOR;
    }

    // sector fleet to sector fleet
    private void CS_Hover_from_SectorFleet_to_SectorFleetHoverFleet()
    {
        UIState = State.SECTOR_FLEET_H_FLEET;
    }
    private void CS_Refresh_from_SectorFleetHoverFleet_to_SectorFleet() // dehover & change
    {
        UIState = State.SECTOR_FLEET;
    }

    // star to star fleet
    private void CS_Hover_from_Star_to_StarHoverFleet()
    {
        UIState = State.STAR_H_FLEET;
    }
    private void CS_Dehover_from_StarHoverFleet_to_Star()
    {
        UIState = State.STAR;
    }
    private void CS_Select_from_StarHoverFleet_to_StarFleet()
    {
        UIState = State.STAR_FLEET;
    }
    private void CS_Deselect_from_StarFleet_to_Star()
    {
        UIState = State.STAR;
    }

    // star fleet to star fleet
    private void CS_Hover_from_StarFleet_to_StarFleetHoverFleet()
    {
        UIState = State.STAR_FLEET_H_FLEET;
    }
    private void CS_Refresh_from_StarFleetHoverFleet_to_StarFleet() // dehover & change
    {
        UIState = State.STAR_FLEET;
    }

    // planet to star fleet - 3
    private void CS_Hover_from_StarPlanet_to_StarPlanetHoverFleet()
    {
        UIState = State.STAR_PLANET_H_FLEET;
    }
    private void CS_Dehover_from_StarPlanetHoverFleet_to_StarPlanet()
    {
        UIState = State.STAR_PLANET;
    }
    private void CS_Select_from_StarPlanetHoverFleet_to_StarFleet()
    {
        UIState = State.STAR_FLEET;
    }

    // star fleet to planet - 3
    private void CS_Hover_from_StarFleet_to_StarFleetHoverPlanet()
    {
        UIState = State.STAR_FLEET_H_PLANET;
    }

    private void CS_Dehover_from_StarFleetHoverPlanet_to_StarFleet()
    {
        UIState = State.STAR_FLEET;
    }

    private void CS_Select_from_StarFleetHoverPlanet_to_StarPlanet()
    {
        UIState = State.STAR_PLANET;
    }

    // galaxy fleet to sector fleet
    private void CS_Hover_from_GalaxyFleet_to_GalaxyFleetHoverSector()
    {
        UIState = State.GALAXY_FLEET_H_SECTOR;
    }
    private void CS_Dehover_from_GalaxyFleetHoverSector_to_GalaxyFleet()
    {
        UIState = State.GALAXY_FLEET;
    }
    private void CS_Select_from_GalaxyFleetHoverSector_to_SectorFleet()
    {
        UIState = State.SECTOR_FLEET;
    }
    private void CS_Deselect_from_SectorFleet_to_GalaxyFleet()
    {
        UIState = State.GALAXY_FLEET;
    }

    // galaxy fleet to star fleet
    private void CS_Hover_from_GalaxyFleet_to_GalaxyFleetHoverStar()
    {
        UIState = State.GALAXY_FLEET_H_STAR;
    }
    private void CS_Dehover_from_GalaxyFleetHoverStar_to_GalaxyFleet()
    {
        UIState = State.GALAXY_FLEET;
    }
    private void CS_Select_from_GalaxyFleetHoverStar_to_StarFleet()
    {
        UIState = State.STAR_FLEET;
    }
    private void CS_Deselect_from_StarFleet_to_GalaxyFleet()
    {
        UIState = State.GALAXY_FLEET;
    }

    // sector fleet to star fleet
    private void CS_Hover_from_SectorFleet_to_SectorFleetHoverStar()
    {
        UIState = State.SECTOR_FLEET_H_STAR;
    }
    private void CS_Dehover_from_SectorFleetHoverStar_to_SectorFleet()
    {
        UIState = State.SECTOR_FLEET;
    }

    private void CS_Select_from_SectorFleetHoverStar_to_StarFleet()
    {
        UIState = State.STAR_FLEET;
    }

    private void CS_Deselect_from_StarFleet_to_SectorFleet()
    {
        UIState = State.SECTOR_FLEET;
    }

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

    //public void AddPathLabel(GFXPathsItem pathGFX)
    //{
    //    UIGalaxyPath newPath = Paths[0].Duplicate(7) as UIGalaxyPath;
    //    Paths[0].GetParent().AddChild(newPath);
    //    Paths.Add(newPath);
    //
    //    newPath._PathGFX = pathGFX;
    //    pathGFX.HUD = newPath;
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
    //public void OnEndTurn()
    //{
    //    Game.Input.DeselectAll();
    //
    //    Game.TurnLoop.CurrentPlayerData.TurnFinished = true;
    //    Game.TurnLoop.WaitingForHuman = false;
    //}
}