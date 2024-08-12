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
        SelectedStar = null;

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
        HoverFleets.Clear();

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
        SelectedFleets.Clear();
        SelectedFleets.AddRange(fleets);

        switch (State)
        {
            case InputState.FAR_GALAXY_H_FLEETS:
                CS_Select_from_FarGalaxyHoverFleets_to_FarStar(); break;
            case InputState.FAR_STAR_H_FLEETS:
                CS_Select_from_FarStarHoverFleets_to_FarStar(); break;
            case InputState.FAR_FLEETS_H_FLEETS:
                CS_Select_from_FarFleetsHoverFleets_to_FarStar(); break;
            case InputState.CLOSE_STAR_H_FLEETS:
                CS_Select_from_CloseStarHoverFleets_to_CloseStar(); break;
            case InputState.CLOSE_PLANET_H_FLEETS:
                CS_Select_from_ClosePlanetHoverFleets_to_CloseStar(); break;
            case InputState.CLOSE_FLEETS_H_FLEETS:
                CS_Select_from_CloseFleetsHoverFleets_to_CloseStar(); break;
        }
    }
    public void OnDeselectFleets()
    {
        SelectedFleets.Clear();

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
        SelectedPlanet = planet;

        switch (State)
        {
            case InputState.CLOSE_STAR_H_PLANET:
                CS_Select_from_CloseStarHoverPlanet_to_CloseStar(); break;
            case InputState.CLOSE_PLANET_H_PLANET:
                CS_Select_from_ClosePlanetHoverPlanet_to_ClosePlanet(); break;
            case InputState.CLOSE_FLEETS_H_PLANET:
                CS_Select_from_CloseFleetsHoverPlanet_to_CloseFleets(); break;
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

        State = InputState.FAR_GALAXY_H_STAR;
    }
    private void CS_Hover_from_FarStar_to_FarStarHoverStar(StarData star)
    {
        HoverStar = star;

        HoverStar._Node.GFX.GFXHover();

        State = InputState.FAR_STAR_H_STAR;
    }
    private void CS_Hover_from_FarFleets_to_FarFleetsHoverStar(StarData star)
    {
        HoverStar = star;

        HoverStar._Node.GFX.GFXHover();

        State = InputState.FAR_FLEETS_H_STAR;
    }
    private void CS_Hover_from_CloseStar_to_CloseStarHoverStar(StarData star)
    {
        HoverStar = star;

        HoverStar._Node.GFX.GFXHover();

        State = InputState.CLOSE_STAR_H_STAR;
    }
    private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverStar(StarData star)
    {
        HoverStar = star;

        HoverStar._Node.GFX.GFXHover();

        State = InputState.CLOSE_PLANET_H_STAR;
    }
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverStar(StarData star)
    {
        HoverStar = star;

        HoverStar._Node.GFX.GFXHover();

        State = InputState.CLOSE_FLEETS_H_STAR;
    }

    // -------------------------------------
    private void CS_Dehover_from_FarGalaxyHoverStar_to_FarGalaxy()
    {
        HoverStar._Node.GFX.GFXDehover();

        HoverStar = null;
        State = InputState.FAR_GALAXY;
    }
    private void CS_Dehover_from_FarStarHoverStar_to_FarStar()
    {
        HoverStar._Node.GFX.GFXDehover();

        HoverStar = null;
        State = InputState.FAR_STAR;
    }
    private void CS_Dehover_from_FarFleetsHoverStar_to_FarFleets()
    {
        HoverStar._Node.GFX.GFXDehover();

        HoverStar = null;
        State = InputState.FAR_FLEETS;
    }
    private void CS_Dehover_from_CloseStarHoverStar_to_CloseStar()
    {
        HoverStar._Node.GFX.GFXDehover();

        HoverStar = null;
        State = InputState.CLOSE_STAR;
    }
    private void CS_Dehover_from_ClosePlanetHoverStar_to_ClosePlanet()
    {
        HoverStar._Node.GFX.GFXDehover();

        HoverStar = null;
        State = InputState.CLOSE_PLANET;
    }
    private void CS_Dehover_from_CloseFleetsHoverStar_to_CloseFleets()
    {
        HoverStar._Node.GFX.GFXDehover();

        HoverStar = null;
        State = InputState.CLOSE_FLEETS;
    }

    // -------------------------------------
    private void CS_Select_from_FarGalaxyHoverStar_to_FarStar()
    {
        SelectedStar = HoverStar;
        HoverStar = null;
        State = InputState.FAR_STAR;
    }
    private void CS_Select_from_FarStarHoverStar_to_FarStar()
    {
        SelectedStar = HoverStar;
        State = InputState.FAR_STAR;
    }
    private void CS_Select_from_FarFleetsHoverStar_to_FarStar()
    {
        SelectedStar = HoverStar;
        State = InputState.FAR_STAR;
    }
    private void CS_Select_from_CloseStarHoverStar_to_CloseStar()
    {
        SelectedStar = HoverStar;
        State = InputState.CLOSE_STAR;
    }
    private void CS_Select_from_ClosePlanetHoverStar_to_CloseStar()
    {
        SelectedStar = HoverStar;
        State = InputState.CLOSE_STAR;
    }
    private void CS_Select_from_CloseFleetsHoverStar_to_CloseStar()
    {
        SelectedStar = HoverStar;
        State = InputState.CLOSE_STAR;
    }

    // -------------------------------------
    private void CS_Deselect_from_FarStar_to_FarGalaxy()
    {
        SelectedStar = HoverStar;
        State = InputState.FAR_GALAXY;
    }
    private void CS_Deselect_from_FarStarHoverStar_to_FarGalaxyHoverStar()
    {
        SelectedStar = HoverStar;
        State = InputState.FAR_GALAXY_H_STAR;
    }
    private void CS_Deselect_from_FarStarHoverFleet_to_FarGalaxyHoverFleet()
    {
        SelectedStar = HoverStar;
        State = InputState.FAR_GALAXY_H_FLEETS;
    }

    // -------------------------------------
    private void CS_Hover_from_FarGalaxy_to_FarGalaxyHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        State = InputState.FAR_GALAXY_H_FLEETS;
    }
    private void CS_Hover_from_FarStar_to_FarStarHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        State = InputState.FAR_STAR_H_FLEETS;
    }
    private void CS_Hover_from_FarFleets_to_FarFleetsHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        State = InputState.FAR_FLEETS_H_FLEETS;
    }
    private void CS_Hover_from_CloseStar_to_CloseStarHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        State = InputState.CLOSE_STAR_H_FLEETS;
    }
    private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        State = InputState.CLOSE_PLANET_H_FLEETS;
    }
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        State = InputState.CLOSE_FLEETS_H_FLEETS;
    }

    // -------------------------------------
    private void CS_Dehover_from_FarGalaxyHoverFleets_to_FarGalaxy()
    {
        HoverStar = null;
        State = InputState.FAR_GALAXY;
    }
    private void CS_Dehover_from_FarStarHoverFleets_to_FarStar()
    {
        HoverStar = null;
        State = InputState.FAR_STAR;
    }
    private void CS_Dehover_from_FarFleetsHoverFleets_to_FarFleets()
    {
        HoverStar = null;
        State = InputState.FAR_FLEETS;
    }
    private void CS_Dehover_from_CloseStarHoverFleets_to_CloseStar()
    {
        HoverStar = null;
        State = InputState.CLOSE_STAR;
    }
    private void CS_Dehover_from_ClosePlanetHoverFleets_to_ClosePlanet()
    {
        HoverStar = null;
        State = InputState.CLOSE_PLANET;
    }
    private void CS_Dehover_from_CloseFleetsHoverFleets_to_CloseFleets()
    {
        HoverStar = null;
        State = InputState.CLOSE_FLEETS;
    }

    // -------------------------------------
    private void CS_Select_from_FarGalaxyHoverFleets_to_FarStar()
    {
        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        State = InputState.FAR_FLEETS;
    }
    private void CS_Select_from_FarStarHoverFleets_to_FarStar()
    {
        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        State = InputState.FAR_FLEETS;
    }
    private void CS_Select_from_FarFleetsHoverFleets_to_FarStar()
    {
        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        State = InputState.FAR_FLEETS;
    }
    private void CS_Select_from_CloseStarHoverFleets_to_CloseStar()
    {
        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        State = InputState.CLOSE_FLEETS;
    }
    private void CS_Select_from_ClosePlanetHoverFleets_to_CloseStar()
    {
        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        State = InputState.CLOSE_FLEETS;
    }
    private void CS_Select_from_CloseFleetsHoverFleets_to_CloseStar()
    {
        SelectedFleets.Clear();
        SelectedFleets.AddRange(HoverFleets);
        State = InputState.CLOSE_FLEETS;
    }

    // -------------------------------------
    private void CS_Deselect_from_FarFleets_to_FarGalaxy()
    {
        SelectedFleets.Clear();
        State = InputState.FAR_GALAXY;
    }
    private void CS_Deselect_from_FarFleetsHoverStar_to_FarGalaxyHoverStar()
    {
        SelectedFleets.Clear();
        State = InputState.FAR_GALAXY_H_STAR;
    }
    private void CS_Deselect_from_FarFleetsHoverFleet_to_FarGalaxyHoverFleet()
    {
        SelectedFleets.Clear();
        State = InputState.FAR_GALAXY_H_FLEETS;
    }
    private void CS_Deselect_from_CloseFleets_to_CloseStar()
    {
        SelectedFleets.Clear();
        State = InputState.CLOSE_STAR;
    }
    private void CS_Deselect_from_CloseFleetsHoverFleets_to_CloseStarHoverFleets()
    {
        SelectedFleets.Clear();
        State = InputState.CLOSE_STAR_H_FLEETS;
    }
    private void CS_Deselect_from_CloseFleetsHoverStar_to_CloseStarHoverStar()
    {
        SelectedFleets.Clear();
        State = InputState.CLOSE_STAR_H_STAR;
    }
    private void CS_Deselect_from_CloseFleetsHoverPlanet_to_CloseStarHoverPlanet()
    {
        SelectedFleets.Clear();
        State = InputState.CLOSE_STAR_H_PLANET;
    }

    // -------------------------------------
    private void CS_Hover_from_CloseStar_to_CloseStarHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        HoverStar = planet._Star;

        HoverPlanet.GFX.GFXHover();
        HoverStar._Node.GFX.GFXHover();

        State = InputState.CLOSE_STAR_H_PLANET;
    }
    private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        HoverStar = planet._Star;

        HoverPlanet.GFX.GFXHover();
        HoverStar._Node.GFX.GFXHover();

        State = InputState.CLOSE_PLANET_H_PLANET;
    }
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        HoverStar = planet._Star;

        HoverPlanet.GFX.GFXHover();
        HoverStar._Node.GFX.GFXHover();

        State = InputState.CLOSE_FLEETS_H_PLANET;
    }
    // -------------------------------------
    private void CS_Dehover_from_CloseStarHoverPlanet_to_CloseStar()
    {
        HoverStar._Node.GFX.GFXDehover();
        HoverPlanet.GFX.GFXDehover();

        HoverStar = null;
        HoverPlanet = null;
        State = InputState.CLOSE_STAR;
    }
    private void CS_Dehover_from_ClosePlanetHoverPlanet_to_ClosePlanet()
    {
        HoverStar._Node.GFX.GFXDehover();
        HoverPlanet.GFX.GFXDehover();

        HoverStar = null;
        HoverPlanet = null;
        State = InputState.CLOSE_PLANET;
    }
    private void CS_Dehover_from_CloseFleetsHoverPlanet_to_CloseFleets()
    {
        HoverStar._Node.GFX.GFXDehover();
        HoverPlanet.GFX.GFXDehover();

        HoverStar = null;
        HoverPlanet = null;
        State = InputState.CLOSE_FLEETS;
    }
    // -------------------------------------
    private void CS_Select_from_CloseStarHoverPlanet_to_CloseStar()
    {
        SelectedPlanet = HoverPlanet;
        State = InputState.CLOSE_PLANET;
    }
    private void CS_Select_from_ClosePlanetHoverPlanet_to_ClosePlanet()
    {
        SelectedPlanet = HoverPlanet;
        State = InputState.CLOSE_PLANET;
    }
    private void CS_Select_from_CloseFleetsHoverPlanet_to_CloseFleets()
    {
        SelectedPlanet = HoverPlanet;
        State = InputState.CLOSE_PLANET;
    }
    // -------------------------------------
    private void CS_Deselect_from_ClosePlanet_to_CloseStar()
    {
        SelectedPlanet = null;
        State = InputState.CLOSE_STAR;
    }
    private void CS_Deselect_from_ClosePlanetHoverPlanet_to_CloseStarHoverPlanet()
    {
        SelectedPlanet = null;
        State = InputState.CLOSE_STAR_H_PLANET;
    }
    private void CS_Deselect_from_ClosePlanetHoverStar_to_CloseStarHoverStar()
    {
        SelectedPlanet = null;
        State = InputState.CLOSE_STAR_H_STAR;
    }
    private void CS_Deselect_from_ClosePlanetHoverFleets_to_CloseStarHoverFleets()
    {
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

        State = InputState.CLOSE_STAR;
    }
    public void CS_Zoom_from_FarFleets_to_CloseFleets()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODClose();
        }

        SelectedStar = SelectedFleets[0].AtStar_PerTurn;
        State = InputState.CLOSE_FLEETS;
    }
    public void CS_Zoom_from_CloseStar_to_FarStar()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODFar();
        }

        State = InputState.FAR_STAR;
    }
    public void CS_Zoom_from_CloseFleets_to_FarFleets()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODFar();
        }

        SelectedStar = null;
        State = InputState.FAR_FLEETS;
    }

    // ---------------------------------------------------------------------------------

    Plane XOYPlane = new Plane(Vector3.Up);
    public void ZoomIn()
    {
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

    //public void SelectSector(SectorData newSelectedSector)
    //{



    //DeselectFleetAll();
    //
    //if (SelectedSector == newSelectedSector)
    //{
    //    // on reselect
    //    GD.Print("PlayerInput - reselecteed " + SelectedSector.SectorName);
    //
    //    if (refreshUI) Game.GalaxyUI.Refresh();
    //
    //    return;
    //}
    //
    //if (SelectedSector != null)
    //{
    //    DeselectSector();
    //}
    //
    //SelectedSector = newSelectedSector;
    //
    //for (int idx = 0; idx < SelectedSector.Systems.Count; idx++)
    //{
    //    SystemData systemFromSector = SelectedSector.Systems[idx];
    //    if (systemFromSector.Star != SelectedStar)
    //    {
    //        //TEMP01 systemFromSector.Star._Node.GFX.Select(false, true, false, false);
    //    }
    //}
    //
    //if (refreshUI) Game.GalaxyUI.Refresh();
    //
    //// on select
    //if (SelectedStar != null) GD.Print("PlayerInput - selected " + SelectedSector.SectorName);
    //}

    //public void SelectStar(StarData newSelectedStar, bool refreshUI = true)
    //{
    //DeselectFleetAll();
    //
    //if (SelectedStar == newSelectedStar)
    //{
    //    // on reselect
    //    GD.Print("PlayerInput - reselecteed " + SelectedStar.StarName);
    //
    //    if (refreshUI) Game.GalaxyUI.Refresh();
    //
    //    return;
    //}
    //
    //if (SelectedSector != null && SelectedSector != newSelectedStar?.System?._Sector)
    //{
    //    DeselectSector();
    //}
    //if (SelectedStar != null)
    //{
    //    if (SelectedPlanet != null)
    //    {
    //        DeselectPlanet();
    //    }
    //    DeselectStar();
    //}
    //SelectedStar = newSelectedStar;
    //
    //SystemData system = SelectedStar.System;
    //if (system != null)
    //{
    //    SelectSector(system._Sector, false);
    //    //TEMP01 SelectedStar._Node.GFX.Select(true, true, false, false);
    //}
    //else
    //{
    //    //TEMP01 SelectedStar._Node.GFX.Select(true, false, false, false);
    //}
    //
    //if (refreshUI) Game.GalaxyUI.Refresh();
    //
    //// on select
    //if (SelectedStar != null) GD.Print("PlayerInput - selected " + SelectedStar.StarName);
    //}
    //public void SelectPlanet(PlanetData newSelectedPlanet, bool buildingsTab, bool populationTab)
    //{
    //DeselectFleetAll();
    //
    //if (SelectedPlanet == newSelectedPlanet)
    //{
    //    // on reselect
    //    GD.Print("PlayerInput - reselecteed " + SelectedPlanet.PlanetName);
    //
    //    Game.GalaxyUI.Refresh(buildingsTab, populationTab);
    //
    //    return;
    //}
    //
    //if (SelectedPlanet != null)
    //{
    //    DeselectPlanet();
    //}
    //SelectedPlanet = newSelectedPlanet;
    //
    //SelectStar(SelectedPlanet._Star, false);
    //
    //Game.GalaxyUI.Refresh(buildingsTab, populationTab);
    //
    //// on select
    //if (SelectedPlanet != null) GD.Print("PlayerInput - selected " + SelectedPlanet.PlanetName);
    //}

    //public void SelectFleet(StarData atStar, bool friendlyToStar)
    //{
    //DeselectAll(false);
    //
    //for (int idx = 0; idx < atStar.Fleets_PerTurn.Count; idx++)
    //{
    //    if (atStar.System == null || atStar.System._Sector._Player == Game.HumanPlayer)
    //    {
    //        if (friendlyToStar)
    //        {
    //            if (atStar.Fleets_PerTurn[idx]._Player == Game.HumanPlayer)
    //            {
    //                SelectedFleets.Add(atStar.Fleets_PerTurn[idx]);
    //            }
    //        }
    //        else
    //        {
    //            if (atStar.Fleets_PerTurn[idx]._Player != Game.HumanPlayer)
    //            {
    //                SelectedFleets.Add(atStar.Fleets_PerTurn[idx]);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (friendlyToStar)
    //        {
    //            if (atStar.Fleets_PerTurn[idx]._Player != Game.HumanPlayer)
    //            {
    //                SelectedFleets.Add(atStar.Fleets_PerTurn[idx]);
    //            }
    //        }
    //        else
    //        {
    //            if (atStar.Fleets_PerTurn[idx]._Player == Game.HumanPlayer)
    //            {
    //                SelectedFleets.Add(atStar.Fleets_PerTurn[idx]);
    //            }
    //        }
    //    }
    //}
    //
    //if (SelectedFleets.Count > 0)
    //{
    //    //TEMP01 SelectedFleets[0].AtStar_PerTurn._Node.GFX.Select(false, false, friendlyToStar, !friendlyToStar, SelectedFleets.Count == 1);
    //}
    //
    //Game.GalaxyUI.Refresh();
    //}

    //public void DeselectOneStep()
    //{
    // if ()

    //if (Game.GalaxyUI.ColonyBuildings.IsUpgradeWindowOpen())
    //{
    //    Game.GalaxyUI.ColonyBuildings.CloseUpgradeWindow();
    //    return;
    //}
    //else if (Game.GalaxyUI.ColonyBuildings.IsUpgradingWindowOpen())
    //{
    //    Game.GalaxyUI.ColonyBuildings.CloseUpgradingWindow();
    //    return;
    //}
    //else if (SelectedFleets.Count > 0)
    //{
    //    DeselectFleetAll();
    //}
    //if (SelectedPlanet != null)
    //{
    //    DeselectPlanet();
    //}
    //else if (SelectedStar != null)
    //{
    //    DeselectStar();
    //}
    //else
    //{
    //    DeselectSector();
    //}
    //
    //Game.GalaxyUI.Refresh();
    //}

    //public void DeselectAll(bool refresh = true)
    //{
    //Game.GalaxyUI.ColonyBuildings.CloseUpgradeWindow();
    //Game.GalaxyUI.ColonyBuildings.CloseUpgradingWindow();
    //
    //DeselectSector();
    //DeselectStar();
    //DeselectPlanet();
    //DeselectFleetAll();
    //
    //if (refresh)
    //    Game.GalaxyUI.Refresh();
    //}
    //public void DeselectAllButSector()
    //{
    //DeselectStar();
    //DeselectPlanet(); 
    //
    //Game.GalaxyUI.Refresh();
    //}

    //public void DeselectSector()
    //{
    // on deselect
    //if (SelectedSector != null) GD.Print("PlayerInput - deselected " + SelectedSector.SectorName);
    //
    //if (SelectedSector != null)
    //{
    //    for (int idx = 0; idx < SelectedSector.Systems.Count; idx++)
    //    {
    //        SystemData systemFromSector = SelectedSector.Systems[idx];
    //        if (systemFromSector.Star != SelectedStar)
    //        {
    //            //TEMP01 systemFromSector.Star._Node.GFX.Deselect();
    //        }
    //    }
    //
    //    SelectedSector = null;
    //}
    //}

    //public void DeselectStar()
    //{
    // on deselect
    //if (SelectedStar != null) GD.Print("PlayerInput - deselected " + SelectedStar.StarName);
    //
    //if (SelectedStar != null)
    //{
    //    if (SelectedSector != null)
    //    {
    //        //TEMP01 SelectedStar._Node.GFX.Select(false, true, false, false);
    //    }
    //    else
    //    {
    //        //TEMP01 SelectedStar._Node.GFX.Deselect();
    //    }
    //    SelectedStar = null;
    //}
    //}

    //public void DeselectPlanet()
    //{
    // on deselect
    //if (SelectedPlanet != null) GD.Print("PlayerInput - deselected " + SelectedPlanet.PlanetName);
    //
    //if (SelectedPlanet != null)
    //{
    //    SelectedPlanet = null;
    //}
    //}

    //public void DeselectFleetAll()
    //{
    //if (SelectedFleets.Count > 0)
    //{
    //    //TEMP01 SelectedFleets[0].AtStar_PerTurn._Node.GFX.Deselect();
    //}
    //
    //SelectedFleets.Clear();
    //}

    //public void DeselectFleet(FleetData fleet)
    //{
    //if (SelectedFleets.Count == 1)
    //{
    //    //TEMP01 SelectedFleets[0].AtStar_PerTurn._Node.GFX.Deselect();
    //}
    //SelectedFleets.Remove(fleet); 
    //
    //Game.GalaxyUI.RefreshPawnsUI();
    //}

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
