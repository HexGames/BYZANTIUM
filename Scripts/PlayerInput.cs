using Godot;
using Godot.Collections;

// Generated
public partial class PlayerInput : Node
{
    public enum InputState
    {
        FAR_GALAXY,
        FAR_GALAXY_H_STAR,
        FAR_GALAXY_H_FLEETS,
        FAR_STAR,
        FAR_STAR_H_STAR,
        FAR_STAR_H_FLEETS,
        FAR_FLEETS,
        FAR_FLEETS_H_FLEETS,
        FAR_FLEETS_H_STAR,
        CLOSE_STAR,
        CLOSE_STAR_H_STAR,
        CLOSE_STAR_H_PLANET,
        CLOSE_STAR_H_FLEETS,
        CLOSE_PLANET,
        CLOSE_PLANET_H_PLANET,
        CLOSE_PLANET_H_FLEETS,
        CLOSE_PLANET_H_STAR,
        CLOSE_FLEETS,
        CLOSE_FLEETS_H_FLEETS,
        CLOSE_FLEETS_H_PLANET,
        CLOSE_FLEETS_H_STAR,
    };

    [ExportCategory("Runtime")]
    [Export]
    public Game Game = null;
    [Export]
    public InputState State = InputState.FAR_GALAXY;

    [Export]
    public StarData HoverStar = null;
    [Export]
    public PlanetData HoverPlanet = null;
    [Export]
    public Array<FleetData> HoverFleets = new Array<FleetData>();

    [Export]
    public StarData SelectedStar = null;
    [Export]
    public PlanetData SelectedPlanet = null;
    [Export]
    public Array<FleetData> SelectedFleets = new Array<FleetData>();

    [Export]
    public SectorData SelectedSector = null;
    [Export]
    public SystemData SelectedStarSystem = null;
    [Export]
    public ColonyData SelectedPlanetColony = null;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
            //OnSelect += PlayerInput.SelectLocation;
        }
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyButtonEvent) // load save
        {
            if (!keyButtonEvent.IsPressed())
            {
                // on key button release
                if (keyButtonEvent.Keycode == Key.S)
                {
                    Game.Map.SaveMap();
                }
                if (keyButtonEvent.Keycode == Key.D)
                {
                    Game.Map.LoadMap();
                    Game.Map.Data.GenerateGameFromData(Game.Def);
                }
                if (keyButtonEvent.Keycode == Key.B)
                {
                    Game.self.GalaxyUI.DEBUGText.Visible = !Game.self.GalaxyUI.DEBUGText.Visible;
                }
            }
        }
    }

    //public override void _Process(double delta)
    //{
    //    var space_state = Game.self.Camera.GetWorld3D().DirectSpaceState;
    //    var raycast = PhysicsRayQueryParameters3D.Create(Game.self.Camera.Position, Game.self.Camera.ProjectPosition(GetViewport().GetMousePosition(), 1000.0f), 2);
    //    var collision = space_state.IntersectRay(raycast);
    //    Vector3 point = (Vector3)collision["collider"];
    //}

    // ---------------------------------------------------------------------------------
    public void OnHoverStar(StarData star)
    {
        if (star == SelectedStar) return;

        switch (State)
        {
            case InputState.FAR_GALAXY:
                CS_Hover_from_FarGalaxy_to_FarGalaxyHoverStar(star); break;
            case InputState.FAR_STAR:
                CS_Hover_from_FarStar_to_FarStarHoverStar(star); break;
            case InputState.FAR_FLEETS:
                CS_Hover_from_FarFleets_to_FarFleetsHoverStar(star); break;
            case InputState.CLOSE_STAR:
                CS_Hover_from_CloseStar_to_CloseStarHoverStar(star); break;
            case InputState.CLOSE_PLANET:
                CS_Hover_from_ClosePlanet_to_ClosePlanetHoverStar(star); break;
            case InputState.CLOSE_FLEETS:
                CS_Hover_from_CloseFleets_to_CloseFleetsHoverStar(star); break;
        }
    }
    public void OnDehoverStar(StarData star)
    {
        if (star != HoverStar) return;

        OnDehoverStar();
    }
    public void OnDehoverStar()
    {
        switch (State)
        {
            case InputState.FAR_GALAXY_H_STAR:
                CS_Dehover_from_FarGalaxyHoverStar_to_FarGalaxy(); break;
            case InputState.FAR_STAR_H_STAR:
                CS_Dehover_from_FarStarHoverStar_to_FarStar(); break;
            case InputState.FAR_FLEETS_H_STAR:
                CS_Dehover_from_FarFleetsHoverStar_to_FarFleets(); break;
            case InputState.CLOSE_STAR_H_STAR:
                CS_Dehover_from_CloseStarHoverStar_to_CloseStar(); break;
            case InputState.CLOSE_PLANET_H_STAR:
                CS_Dehover_from_ClosePlanetHoverStar_to_ClosePlanet(); break;
            case InputState.CLOSE_FLEETS_H_STAR:
                CS_Dehover_from_CloseFleetsHoverStar_to_CloseFleets(); break;
        }
    }

    public void OnSelectStar(StarData star)
    {
        if (star != HoverStar) return;

        switch (State)
        {
            case InputState.FAR_GALAXY_H_STAR:
                CS_Select_from_FarGalaxyHoverStar_to_FarStar(); break;
            case InputState.FAR_STAR_H_STAR:
                CS_Select_from_FarStarHoverStar_to_FarStar(); break;
            case InputState.FAR_FLEETS_H_STAR:
                CS_Select_from_FarFleetsHoverStar_to_FarStar(); break;
            case InputState.CLOSE_STAR_H_STAR:
                CS_Select_from_CloseStarHoverStar_to_CloseStar(); break;
            case InputState.CLOSE_PLANET_H_STAR:
                CS_Select_from_ClosePlanetHoverStar_to_CloseStar(); break;
            case InputState.CLOSE_FLEETS_H_STAR:
                CS_Select_from_CloseFleetsHoverStar_to_CloseStar(); break;
        }
    }
    public void OnDeselectStar()
    {
        switch (State)
        {
            case InputState.FAR_STAR:
                CS_Deselect_from_FarStar_to_FarGalaxy(); break;
            case InputState.FAR_STAR_H_FLEETS:
                CS_Deselect_from_FarStarHoverStar_to_FarGalaxyHoverStar(); break;
            case InputState.FAR_STAR_H_STAR:
                CS_Deselect_from_FarStarHoverFleet_to_FarGalaxyHoverFleet(); break;
        }
    }

    // -------------------------------------
    public void OnHoverFleets(Array<FleetData> fleets)
    {
        if (fleets.Count == 0) return;

        switch (State)
        {
            case InputState.FAR_GALAXY:
                CS_Hover_from_FarGalaxy_to_FarGalaxyHoverFleets(fleets); break;
            case InputState.FAR_STAR:
                CS_Hover_from_FarStar_to_FarStarHoverFleets(fleets); break;
            case InputState.FAR_FLEETS:
                CS_Hover_from_FarFleets_to_FarFleetsHoverFleets(fleets); break;
            case InputState.CLOSE_STAR:
                CS_Hover_from_CloseStar_to_CloseStarHoverFleets(fleets); break;
            case InputState.CLOSE_PLANET:
                CS_Hover_from_ClosePlanet_to_ClosePlanetHoverFleets(fleets); break;
            case InputState.CLOSE_FLEETS:
                CS_Hover_from_CloseFleets_to_CloseFleetsHoverFleets(fleets); break;
        }
    }
    public void OnDehoverFleets()
    {
        switch (State)
        {
            case InputState.FAR_GALAXY_H_FLEETS:
                CS_Dehover_from_FarGalaxyHoverFleets_to_FarGalaxy(); break;
            case InputState.FAR_STAR_H_FLEETS:
                CS_Dehover_from_FarStarHoverFleets_to_FarStar(); break;
            case InputState.FAR_FLEETS_H_FLEETS:
                CS_Dehover_from_FarFleetsHoverFleets_to_FarFleets(); break;
            case InputState.CLOSE_STAR_H_FLEETS:
                CS_Dehover_from_CloseStarHoverFleets_to_CloseStar(); break;
            case InputState.CLOSE_PLANET_H_FLEETS:
                CS_Dehover_from_ClosePlanetHoverFleets_to_ClosePlanet(); break;
            case InputState.CLOSE_FLEETS_H_FLEETS:
                CS_Dehover_from_CloseFleetsHoverFleets_to_CloseFleets(); break;
        }
    }

    public void OnSelectFleets(Array<FleetData> fleets)
    {
        switch (State)
        {
            case InputState.FAR_GALAXY_H_FLEETS:
                CS_Select_from_FarGalaxyHoverFleets_to_FarFleets(); break;
            case InputState.FAR_STAR_H_FLEETS:
                CS_Select_from_FarStarHoverFleets_to_FarFleets(); break;
            case InputState.FAR_FLEETS_H_FLEETS:
                CS_Select_from_FarFleetsHoverFleets_to_FarFleets(); break;
            case InputState.CLOSE_STAR_H_FLEETS:
                CS_Select_from_CloseStarHoverFleets_to_CloseFleet(); break;
            case InputState.CLOSE_PLANET_H_FLEETS:
                CS_Select_from_ClosePlanetHoverFleets_to_CloseFleets(); break;
            case InputState.CLOSE_FLEETS_H_FLEETS:
                CS_Select_from_CloseFleetsHoverFleets_to_CloseFleets(); break;
        }
    }
    public void OnDeselectFleets()
    {
        switch (State)
        {
            case InputState.FAR_FLEETS:
                CS_Deselect_from_FarFleets_to_FarGalaxy(); break;
            case InputState.FAR_FLEETS_H_FLEETS:
                CS_Deselect_from_FarFleetsHoverStar_to_FarGalaxyHoverStar(); break;
            case InputState.FAR_FLEETS_H_STAR:
                CS_Deselect_from_FarFleetsHoverFleet_to_FarGalaxyHoverFleet(); break;
            case InputState.CLOSE_FLEETS:
                CS_Deselect_from_CloseFleets_to_CloseStar(); break;
            case InputState.CLOSE_FLEETS_H_FLEETS:
                CS_Deselect_from_CloseFleetsHoverFleets_to_CloseStarHoverFleets(); break;
            case InputState.CLOSE_FLEETS_H_STAR:
                CS_Deselect_from_CloseFleetsHoverStar_to_CloseStarHoverStar(); break;
            case InputState.CLOSE_FLEETS_H_PLANET:
                CS_Deselect_from_CloseFleetsHoverPlanet_to_CloseStarHoverPlanet(); break;
        }
    }

    // -------------------------------------
    public void OnHoverPlanet(PlanetData planet)
    {
        if (planet == SelectedPlanet) return;

        switch (State)
        {
            case InputState.CLOSE_STAR:
                CS_Hover_from_CloseStar_to_CloseStarHoverPlanet(planet); break;
            case InputState.CLOSE_PLANET:
                CS_Hover_from_ClosePlanet_to_ClosePlanetHoverPlanet(planet); break;
            case InputState.CLOSE_FLEETS:
                CS_Hover_from_CloseFleets_to_CloseFleetsHoverPlanet(planet); break;
        }
    }
    public void OnDehoverPlanet(PlanetData planet)
    {
        if (planet != HoverPlanet) return;

        OnDehoverPlanet();
    }
    public void OnDehoverPlanet()
    {
        switch (State)
        {
            case InputState.CLOSE_STAR_H_PLANET:
                CS_Dehover_from_CloseStarHoverPlanet_to_CloseStar(); break;
            case InputState.CLOSE_PLANET_H_PLANET:
                CS_Dehover_from_ClosePlanetHoverPlanet_to_ClosePlanet(); break;
            case InputState.CLOSE_FLEETS_H_PLANET:
                CS_Dehover_from_CloseFleetsHoverPlanet_to_CloseFleets(); break;
        }
    }

    public void OnSelectPlanet(PlanetData planet)
    {
        switch (State)
        {
            case InputState.CLOSE_STAR_H_PLANET:
                CS_Select_from_CloseStarHoverPlanet_to_ClosePlanet(); break;
            case InputState.CLOSE_PLANET_H_PLANET:
                CS_Select_from_ClosePlanetHoverPlanet_to_ClosePlanet(); break;
            case InputState.CLOSE_FLEETS_H_PLANET:
                CS_Select_from_CloseFleetsHoverPlanet_to_ClosePlanet(); break;
        }
    }
    public void OnDeselectPlanet()
    {
        switch (State)
        {
            case InputState.CLOSE_PLANET:
                CS_Deselect_from_ClosePlanet_to_CloseStar(); break;
            case InputState.CLOSE_PLANET_H_PLANET:
                CS_Deselect_from_ClosePlanetHoverPlanet_to_CloseStarHoverPlanet(); break;
            case InputState.CLOSE_FLEETS_H_STAR:
                CS_Deselect_from_ClosePlanetHoverStar_to_CloseStarHoverStar(); break;
            case InputState.CLOSE_FLEETS_H_FLEETS:
                CS_Deselect_from_ClosePlanetHoverFleets_to_CloseStarHoverFleets(); break;
        }
    }

    // ---------------------------------------------------------------------------------
    public void DeselectOneStep()
    {
        if (SelectedPlanet != null) OnDeselectPlanet();
        else if (SelectedFleets.Count > 0) OnDeselectFleets();
        else if (SelectedStar != null) OnDeselectStar();
    }

    // -------------------------------------
    private void CS_Hover_from_FarGalaxy_to_FarGalaxyHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        Game.self.GalaxyUI.ShowStarInfo(HoverStar);

        State = InputState.FAR_GALAXY_H_STAR;
    }
    private void CS_Hover_from_FarStar_to_FarStarHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        Game.self.GalaxyUI.ShowStarInfo(HoverStar);

        State = InputState.FAR_STAR_H_STAR;
    }
    private void CS_Hover_from_FarFleets_to_FarFleetsHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(HoverStar);

        State = InputState.FAR_FLEETS_H_STAR;
    }
    private void CS_Hover_from_CloseStar_to_CloseStarHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        HoverStar._Node.GFX.ShowPlanets3DGUI();
        Game.self.GalaxyUI.ShowStarInfo(HoverStar);

        State = InputState.CLOSE_STAR_H_STAR;
    }
    private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        HoverStar._Node.GFX.ShowPlanets3DGUI();
        Game.self.GalaxyUI.ShowStarInfo(HoverStar);

        State = InputState.CLOSE_PLANET_H_STAR;
    }
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        HoverStar._Node.GFX.ShowPlanets3DGUI();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(HoverStar);

        State = InputState.CLOSE_FLEETS_H_STAR;
    }

    // -------------------------------------
    private void CS_Dehover_from_FarGalaxyHoverStar_to_FarGalaxy()
    {
        HoverStar._Node.GFX.GFXDehover();
        Game.self.GalaxyUI.HideStarInfo();

        HoverStar = null;
        State = InputState.FAR_GALAXY;
    }
    private void CS_Dehover_from_FarStarHoverStar_to_FarStar()
    {
        HoverStar._Node.GFX.GFXDehover();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar = null;
        State = InputState.FAR_STAR;
    }
    private void CS_Dehover_from_FarFleetsHoverStar_to_FarFleets()
    {
        HoverStar._Node.GFX.GFXDehover();
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        HoverStar = null;
        State = InputState.FAR_FLEETS;
    }
    private void CS_Dehover_from_CloseStarHoverStar_to_CloseStar()
    {
        HoverStar._Node.GFX.GFXDehover();
        if(HoverStar != SelectedStar) HoverStar._Node.GFX.HidePlanets3DGUI();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar = null;
        State = InputState.CLOSE_STAR;
    }
    private void CS_Dehover_from_ClosePlanetHoverStar_to_ClosePlanet()
    {
        HoverStar._Node.GFX.GFXDehover();
        HoverStar._Node.GFX.HidePlanets3DGUI();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar = null;
        State = InputState.CLOSE_PLANET;
    }
    private void CS_Dehover_from_CloseFleetsHoverStar_to_CloseFleets()
    {
        HoverStar._Node.GFX.GFXDehover();
        HoverStar._Node.GFX.HidePlanets3DGUI();
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        HoverStar = null;
        State = InputState.CLOSE_FLEETS;
    }

    // -------------------------------------
    private void CS_Select_from_FarGalaxyHoverStar_to_FarStar()
    {
        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.FAR_STAR;
    }
    private void CS_Select_from_FarStarHoverStar_to_FarStar()
    {
        SelectedStar._Node.GFX.GFXDeselect();

        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.FAR_STAR;
    }
    private void CS_Select_from_FarFleetsHoverStar_to_FarStar()
    {
        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.FAR_STAR;
    }
    private void CS_Select_from_CloseStarHoverStar_to_CloseStar()
    {
        SelectedStar._Node.GFX.GFXDeselect();
        SelectedStar._Node.GFX.HidePlanets3DGUI();

        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.CLOSE_STAR;
    }
    private void CS_Select_from_ClosePlanetHoverStar_to_CloseStar()
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        SelectedStar._Node.GFX.GFXDeselect();
        SelectedStar._Node.GFX.HidePlanets3DGUI();

        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.CLOSE_STAR;
    }
    private void CS_Select_from_CloseFleetsHoverStar_to_CloseStar()
    {
        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        //SelectedStar._Node.GFX.GFXDeselect();
        //SelectedStar._Node.GFX.HidePlanets3DGUI();

        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.CLOSE_STAR;
    }

    // -------------------------------------
    private void CS_Deselect_from_FarStar_to_FarGalaxy()
    {
        SelectedStar._Node.GFX.GFXDeselect();
        Game.self.GalaxyUI.HideStarInfo();
        SelectedStar = null;

        State = InputState.FAR_GALAXY;
    }
    private void CS_Deselect_from_FarStarHoverStar_to_FarGalaxyHoverStar()
    {
        SelectedStar._Node.GFX.GFXDeselect();
        //Game.self.GalaxyUI.ShowStarInfo(HoverStar);
        SelectedStar = null;

        State = InputState.FAR_GALAXY_H_STAR;
    }
    private void CS_Deselect_from_FarStarHoverFleet_to_FarGalaxyHoverFleet()
    {
        SelectedStar._Node.GFX.GFXDeselect();
        //Game.self.GalaxyUI.HideStarInfo();
        SelectedStar = null;

        State = InputState.FAR_GALAXY_H_FLEETS;
    }

    // -------------------------------------
    private void CS_Hover_from_FarGalaxy_to_FarGalaxyHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets);

        State = InputState.FAR_GALAXY_H_FLEETS;
    }
    private void CS_Hover_from_FarStar_to_FarStarHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets);


        State = InputState.FAR_STAR_H_FLEETS;
    }
    private void CS_Hover_from_FarFleets_to_FarFleetsHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets);

        State = InputState.FAR_FLEETS_H_FLEETS;
    }
    private void CS_Hover_from_CloseStar_to_CloseStarHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets);

        State = InputState.CLOSE_STAR_H_FLEETS;
    }
    private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets);

        State = InputState.CLOSE_PLANET_H_FLEETS;
    }
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets);

        State = InputState.CLOSE_FLEETS_H_FLEETS;
    }

    // -------------------------------------
    private void CS_Dehover_from_FarGalaxyHoverFleets_to_FarGalaxy()
    {
        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.HideFleetsInfo();

        State = InputState.FAR_GALAXY;
    }
    private void CS_Dehover_from_FarStarHoverFleets_to_FarStar()
    {
        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

       State = InputState.FAR_STAR;
    }
    private void CS_Dehover_from_FarFleetsHoverFleets_to_FarFleets()
    {
        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        State = InputState.FAR_FLEETS;
    }
    private void CS_Dehover_from_CloseStarHoverFleets_to_CloseStar()
    {
        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        State = InputState.CLOSE_STAR;
    }
    private void CS_Dehover_from_ClosePlanetHoverFleets_to_ClosePlanet()
    {
        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        State = InputState.CLOSE_PLANET;
    }
    private void CS_Dehover_from_CloseFleetsHoverFleets_to_CloseFleets()
    {
        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        State = InputState.CLOSE_FLEETS;
    }

    // -------------------------------------
    private void CS_Select_from_FarGalaxyHoverFleets_to_FarFleets()
    {
        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleets);
        //Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();

        State = InputState.FAR_FLEETS;
    }
    private void CS_Select_from_FarStarHoverFleets_to_FarFleets()
    {
        SelectedStar._Node.GFX.GFXDeselect();
        SelectedStar = null;

        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleets);
        //Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();

        State = InputState.FAR_FLEETS;
    }
    private void CS_Select_from_FarFleetsHoverFleets_to_FarFleets()
    {
        Game.self.SelectorsUI3D.FleetDeselect();

        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleets);
        //Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();

        State = InputState.FAR_FLEETS;
    }
    private void CS_Select_from_CloseStarHoverFleets_to_CloseFleet()
    {
        SelectedStar._Node.GFX.GFXDeselect();
        SelectedStar = null;

        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleets);
        //Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();

        State = InputState.CLOSE_FLEETS;
    }
    private void CS_Select_from_ClosePlanetHoverFleets_to_CloseFleets()
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        SelectedStar._Node.GFX.GFXDeselect();
        SelectedStar = null;

        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleets);
        //Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();

        State = InputState.CLOSE_FLEETS;
    }
    private void CS_Select_from_CloseFleetsHoverFleets_to_CloseFleets()
    {
        Game.self.SelectorsUI3D.FleetDeselect();

        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleets);
        //Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);

        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();

        State = InputState.CLOSE_FLEETS;
    }

    // -------------------------------------
    private void CS_Deselect_from_FarFleets_to_FarGalaxy()
    {
        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();
        Game.self.GalaxyUI.HideFleetsInfo();

        State = InputState.FAR_GALAXY;
    }
    private void CS_Deselect_from_FarFleetsHoverStar_to_FarGalaxyHoverStar()
    {
        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.FAR_GALAXY_H_STAR;
    }
    private void CS_Deselect_from_FarFleetsHoverFleet_to_FarGalaxyHoverFleet()
    {
        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.FAR_GALAXY_H_FLEETS;
    }
    private void CS_Deselect_from_CloseFleets_to_CloseStar()
    {
        SelectedStar = SelectedFleets[0].Star_At_PerTurn;
        SelectedStar._Node.GFX.GFXSelect();
        SelectedStar._Node.GFX.ShowPlanets3DGUI();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.CLOSE_STAR;
    }
    private void CS_Deselect_from_CloseFleetsHoverFleets_to_CloseStarHoverFleets()
    {
        SelectedStar = SelectedFleets[0].Star_At_PerTurn;
        SelectedStar._Node.GFX.GFXSelect();
        SelectedStar._Node.GFX.ShowPlanets3DGUI();

        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.CLOSE_STAR_H_FLEETS;
    }
    private void CS_Deselect_from_CloseFleetsHoverStar_to_CloseStarHoverStar()
    {
        SelectedStar = SelectedFleets[0].Star_At_PerTurn;
        SelectedStar._Node.GFX.GFXSelect();
        SelectedStar._Node.GFX.ShowPlanets3DGUI();

        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.CLOSE_STAR_H_STAR;
    }
    private void CS_Deselect_from_CloseFleetsHoverPlanet_to_CloseStarHoverPlanet()
    {
        SelectedStar = SelectedFleets[0].Star_At_PerTurn;
        SelectedStar._Node.GFX.GFXSelect();
        SelectedStar._Node.GFX.ShowPlanets3DGUI();

        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.CLOSE_STAR_H_PLANET;
    }

    // -------------------------------------
    private void CS_Hover_from_CloseStar_to_CloseStarHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        Game.self.SelectorsUI3D.PlanetHover(HoverPlanet);

        if (planet._Star != SelectedStar)
        {
            HoverStar = planet._Star;

            HoverStar._Node.GFX.GFXHover();
            HoverStar._Node.GFX.ShowPlanets3DGUI();
            Game.self.GalaxyUI.ShowStarInfo(HoverStar);
        }

        State = InputState.CLOSE_STAR_H_PLANET;
    }
    private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        Game.self.SelectorsUI3D.PlanetHover(HoverPlanet);

        if (planet._Star != SelectedStar)
        {
            HoverStar = planet._Star;

            HoverStar._Node.GFX.GFXHover();
            HoverStar._Node.GFX.ShowPlanets3DGUI();
            Game.self.GalaxyUI.ShowStarInfo(HoverStar);
        }

        State = InputState.CLOSE_PLANET_H_PLANET;
    }
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        Game.self.SelectorsUI3D.PlanetHover(HoverPlanet);

        if (planet._Star != SelectedStar)
        {
            HoverStar = planet._Star;

            HoverStar._Node.GFX.GFXHover();
            HoverStar._Node.GFX.ShowPlanets3DGUI();
            Game.self.GalaxyUI.ShowStarInfo(HoverStar);
        }

        State = InputState.CLOSE_FLEETS_H_PLANET;
    }
    // -------------------------------------
    private void CS_Dehover_from_CloseStarHoverPlanet_to_CloseStar()
    {
        if (HoverStar != null)
        {
            HoverStar._Node.GFX.GFXDehover();
            if (HoverStar != SelectedStar) HoverStar._Node.GFX.HidePlanets3DGUI();
            if (HoverStar != SelectedStar) Game.self.GalaxyUI.ShowStarInfo(SelectedStar);
            HoverStar = null;
        }
        
        Game.self.SelectorsUI3D.PlanetDehover();
        HoverPlanet = null;

        State = InputState.CLOSE_STAR;
    }
    private void CS_Dehover_from_ClosePlanetHoverPlanet_to_ClosePlanet()
    {
        if (HoverStar != null)
        {
            HoverStar._Node.GFX.GFXDehover();
            if (HoverStar != SelectedStar) HoverStar._Node.GFX.HidePlanets3DGUI();
            if (HoverStar != SelectedStar) Game.self.GalaxyUI.ShowStarInfo(SelectedStar);
            HoverStar = null;
        }

        Game.self.SelectorsUI3D.PlanetDehover();
        HoverPlanet = null;

        State = InputState.CLOSE_PLANET;
    }
    private void CS_Dehover_from_CloseFleetsHoverPlanet_to_CloseFleets()
    {
        if (HoverStar != null)
        {
            HoverStar._Node.GFX.GFXDehover();
            if (HoverStar != SelectedStar) HoverStar._Node.GFX.HidePlanets3DGUI();
            Game.self.GalaxyUI.HideStarInfo();
            Game.self.GalaxyUI.ShowFleetsInfo(SelectedFleets);
            HoverStar = null;
        }

        Game.self.SelectorsUI3D.PlanetDehover();
        HoverPlanet = null;

        State = InputState.CLOSE_FLEETS;
    }
    // -------------------------------------
    private void CS_Select_from_CloseStarHoverPlanet_to_ClosePlanet()
    {
        if (HoverStar!= null && HoverStar != SelectedStar)
        {
            SelectedStar._Node.GFX.GFXDeselect();

            SelectedStar = HoverStar;
            SelectedStar._Node.GFX.GFXSelect();
            //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

            HoverStar._Node.GFX.GFXDehover();
            HoverStar = null;
        }

        SelectedPlanet = HoverPlanet;
        Game.self.SelectorsUI3D.PlanetSelect(SelectedPlanet);

        Game.self.SelectorsUI3D.PlanetDehover();
        HoverPlanet = null;

        State = InputState.CLOSE_PLANET;
    }
    private void CS_Select_from_ClosePlanetHoverPlanet_to_ClosePlanet()
    {
        if (HoverStar != null && HoverStar != SelectedStar)
        {
            SelectedStar._Node.GFX.GFXDeselect();

            SelectedStar = HoverStar;
            SelectedStar._Node.GFX.GFXSelect();
            //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);

            HoverStar._Node.GFX.GFXDehover();
            HoverStar = null;
        }

        Game.self.SelectorsUI3D.PlanetDeselect();

        SelectedPlanet = HoverPlanet;
        Game.self.SelectorsUI3D.PlanetSelect(SelectedPlanet);

        Game.self.SelectorsUI3D.PlanetDehover();
        HoverPlanet = null;

        State = InputState.CLOSE_PLANET;
    }
    private void CS_Select_from_CloseFleetsHoverPlanet_to_ClosePlanet()
    {
        if (HoverStar != null/* && HoverStar != SelectedStar*/)
        {
            //SelectedStar._Node.GFX.GFXDeselect();

            SelectedStar = HoverStar;
            SelectedStar._Node.GFX.GFXSelect();

            HoverStar._Node.GFX.GFXDehover();
            HoverStar = null;
        }

        SelectedFleets.Clear();
        Game.self.SelectorsUI3D.FleetDeselect();

        SelectedPlanet = HoverPlanet;
        Game.self.SelectorsUI3D.PlanetSelect(SelectedPlanet);

        Game.self.SelectorsUI3D.PlanetDehover();
        HoverPlanet = null;

        State = InputState.CLOSE_PLANET;
    }
    // -------------------------------------
    private void CS_Deselect_from_ClosePlanet_to_CloseStar()
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        State = InputState.CLOSE_STAR;
    }
    private void CS_Deselect_from_ClosePlanetHoverPlanet_to_CloseStarHoverPlanet()
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        State = InputState.CLOSE_STAR_H_PLANET;
    }
    private void CS_Deselect_from_ClosePlanetHoverStar_to_CloseStarHoverStar()
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        State = InputState.CLOSE_STAR_H_STAR;
    }
    private void CS_Deselect_from_ClosePlanetHoverFleets_to_CloseStarHoverFleets()
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        State = InputState.CLOSE_STAR_H_FLEETS;
    }

    // ---------------------------------------------------------------------------------
    public void CS_Zoom_from_FarStar_to_CloseStar()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODClose();
        }

        SelectedStar._Node.GFX.ShowPlanets3DGUI();

        State = InputState.CLOSE_STAR;
    }
    public void CS_Zoom_from_FarFleets_to_CloseFleets()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODClose();
        }

        SelectedStar = SelectedFleets[0].Star_At_PerTurn;
        SelectedStar._Node.GFX.ShowPlanets3DGUI();

        State = InputState.CLOSE_FLEETS;
    }
    public void CS_Zoom_from_CloseStar_to_FarStar()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODFar();
        }

        SelectedStar._Node.GFX.HidePlanets3DGUI();

        State = InputState.FAR_STAR;
    }
    public void CS_Zoom_from_CloseFleets_to_FarFleets()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODFar();
        }

        if (SelectedStar != null)
        {
            SelectedStar._Node.GFX.HidePlanets3DGUI();
            SelectedStar = null;
        }

        State = InputState.FAR_FLEETS;
    }

    // ---------------------------------------------------------------------------------

    Plane XOYPlane = new Plane(Vector3.Up);
    public void ZoomIn()
    {
        if ((int)State >= (int)InputState.CLOSE_STAR)
            return;

        StarData hStar = null;
        if (HoverStar != null)
        {
            hStar = HoverStar;
            OnDehoverStar();
        }

        Array<FleetData> hFleets = new Array<FleetData>();
        if (HoverFleets != null)
        {
            hFleets.AddRange(HoverFleets);
            OnDehoverFleets();
        }

        if (State == InputState.FAR_FLEETS)
        {
            CS_Zoom_from_FarFleets_to_CloseFleets();
        }
        else
        {
            if (SelectedStar == null)
            {
                if (hStar != null)
                {
                    OnHoverStar(hStar);
                    OnSelectStar(hStar);
                    OnDehoverStar();
                }
                else
                {
                    Vector3 cameraMiddlePoint = XOYPlane.IntersectsRay(Game.self.Camera.Position, Game.self.Camera.ProjectPosition(GetViewport().GetVisibleRect().Size / 2, 1.0f) - Game.self.Camera.Position).Value;
                    // get closest star
                    float minDist = float.MaxValue;
                    StarData star = Game.self.Map.Data.Stars[0];
                    for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
                    {
                        float dist = (Game.self.Map.Data.Stars[idx]._Node.GFX.Position - cameraMiddlePoint).LengthSquared();
                        if (dist < minDist)
                        {
                            star = Game.self.Map.Data.Stars[idx];
                            minDist = dist;
                        }
                    }
                    OnHoverStar(star);
                    OnSelectStar(star);
                    OnDehoverStar();
                }
            }

            if (State == InputState.FAR_STAR)
            {
                CS_Zoom_from_FarStar_to_CloseStar();
            }
            else
            {
                GD.PrintErr("Zoom out of state");
            }

            if (hFleets.Count > 0)
            {
                OnHoverFleets(hFleets);
            }
            else if (hStar != null)
            {
                OnHoverStar(hStar);
            }
        }
    }

    public void ZoomOut()
    {
        if ((int)State < (int)InputState.CLOSE_STAR)
            return;

        StarData hStar = null;
        if (HoverStar != null)
        {
            hStar = HoverStar;
            OnDehoverStar();
        }

        Array<FleetData> hFleets = new Array<FleetData>();
        if (HoverFleets != null)
        {
            hFleets.AddRange(HoverFleets);
            OnDehoverFleets();
        }

        if (HoverPlanet != null)
        {
            OnDehoverPlanet();
        }

        if (State == InputState.CLOSE_PLANET)
        {
            CS_Deselect_from_ClosePlanet_to_CloseStar();
        }

        if (State == InputState.CLOSE_STAR)
        {
            CS_Zoom_from_CloseStar_to_FarStar();
        }
        else if (State == InputState.CLOSE_FLEETS)
        {
            CS_Zoom_from_CloseFleets_to_FarFleets();
        }
        else
        {
            GD.PrintErr("Zoom out of state");
        }

        if (hFleets.Count > 0)
        {
            OnHoverFleets(hFleets);
        }
        else if (hStar != null)
        {
            OnHoverStar(hStar);
        }
    }

    // ---------------------------------------------------------------------------------
    //public void MoveTo(StarData toStar)
    //{
    //for (int idx = 0; idx < SelectedFleets.Count; idx++)
    //{
    //    if (SelectedFleets[idx]._Player == Game.HumanPlayer)
    //    {
    //        if (ActionMove.HasAvailableMove(Game, SelectedFleets[idx], toStar))
    //        {
    //            ActionMove.CancelMove(Game, SelectedFleets[idx]);
    //            ActionMove.AddMove(Game, SelectedFleets[idx], toStar);
    //
    //            Game.Paths.AddPath(SelectedFleets[idx], toStar);
    //        }
    //        else if (SelectedFleets[idx].AtStar_PerTurn == toStar)
    //        {
    //            ActionMove.CancelMove(Game, SelectedFleets[idx]);
    //
    //            Game.Paths.ClearPathForFleet(SelectedFleets[idx]);
    //        }
    //
    //        Game.GalaxyUI.FleetsSelected.Refresh(Game.Input.SelectedFleets);
    //    }
    //}
    //}
}
