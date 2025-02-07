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
        CLOSE_GALAXY,
        CLOSE_STAR,
        //CLOSE_STAR_H_STAR,
        CLOSE_STAR_H_PLANET,
        CLOSE_STAR_H_FLEETS,
        CLOSE_PLANET,
        CLOSE_PLANET_H_PLANET,
        CLOSE_PLANET_H_FLEETS,
        //CLOSE_PLANET_H_STAR,
        CLOSE_FLEETS,
        CLOSE_FLEETS_H_FLEETS,
        CLOSE_FLEETS_H_PLANET,
        CLOSE_FLEETS_H_STAR,
        LOCKED_DIPLOMACY = 999,
    };

    [ExportCategory("Runtime")]
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
    public FleetData SelectedFleet = null;
    //[Export]
    //public Array<FleetData> SelectedFleetList = new Array<FleetData>();

    [Export]
    public SystemData SelectedStarSystem = null;
    [Export]
    public ColonyData SelectedPlanetColony = null;

    private bool UnhandledInput = false;
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyButtonEvent) // load save
        {
            if (!keyButtonEvent.IsPressed())
            {
                // on key button release
                if (keyButtonEvent.Keycode == Key.S)
                {
                    Game.self.Map.SaveMap_Progressive();
                }
                if (keyButtonEvent.Keycode == Key.D)
                {
                    Game.self.Map.LoadMap();
                    Game.self.Map.Data.GenerateGameFromData(Game.self.Def);
                }
                if (keyButtonEvent.Keycode == Key.B)
                {
                    Game.self.GalaxyUI.DEBUGText.Visible = !Game.self.GalaxyUI.DEBUGText.Visible;
                }
                if (keyButtonEvent.Keycode == Key.V)
                {
                    Game.self.HumanPlayer.DEBUG = !Game.self.HumanPlayer.DEBUG;
                    Game.self.TurnLoop.StartTurn_Visibility();
                    Game.self.TurnLoop.StartTurn_RefreshGUI3D();
                }
            }
        }

        UnhandledInput = false;
    }

    //public InputState OldState = InputState.FAR_GALAXY;
    //public override void _Process(double delta)
    //{
    //    if (State != OldState)
    //    {
    //        OldState = State;
    //        GD.Print("New State " + State);
    //    }
    //}

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if (!mouseButtonEvent.IsPressed())
            {
                Raycast();

                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                {
                    if (HoverPlanet != null)
                    {
                        OnSelectPlanet(HoverPlanet);
                    }
                    else if (HoverFleets.Count > 0)
                    {
                        OnSelectFleets(HoverFleets);
                    }
                    else if (HoverStar != null)
                    {
                        OnSelectStar(HoverStar);
                    }
                }
                else if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    bool moved = false;
                    if (HoverStar != null)
                    {
                        moved = Game.self.Input.TryFleetsMoveToStar(HoverStar);
                    }

                    if (moved == false)
                    {
                        Game.self.Input.DeselectOneStep();
                    }
                }
            }
        }
        else
        {
            UnhandledInput = true;
        }
        //else if (inputEvent is InputEventMouseMotion mouseMotionEvent)
        //{
        //    if (mouseMotionEvent.Velocity.LengthSquared() < 10000)
        //    {
        //        Game.self.GalaxyUI.DEBUGText.Text = "cast";
        //        HandledInput = false;
        //        Raycast();
        //    }
        //    else
        //    {
        //        Game.self.GalaxyUI.DEBUGText.Text = "too fast------------ too fast";
        //    }
        //}
    }

    //private float Raycast_cooldown = 0.0f;
    private Vector2 Raycast_lastMousePos = Vector2.Zero;
    private Node3D Raycast_hitNode = null;
    public void Raycast()
    {
        MapCamera camera =  Game.self.Camera;
        Vector2 mouse_pos = GetViewport().GetMousePosition();
        float ray_length = 1000.0f;
        Vector3 from = camera.ProjectRayOrigin(mouse_pos);
        Vector3 to = from + camera.ProjectRayNormal(mouse_pos) * ray_length;
        var space = camera.GetWorld3D().DirectSpaceState;
        var ray_query = new PhysicsRayQueryParameters3D();
        ray_query.From = from;
        ray_query.To = to;
        ray_query.CollideWithAreas = true;
        ray_query.CollisionMask = 1;

        var raycast_result = space.IntersectRay(ray_query);

        if (raycast_result.Count > 0)
        {
            Node3D hitNode = (Node3D)raycast_result["collider"];

            //if (useCooldown)
            //{
            //    if (Raycast_hitNode == null || Raycast_hitNode != hitNode)
            //    {
            //        Raycast_hitNode = hitNode;
            //        //Raycast_cooldown = 0.1f;
            //        //GD.Print("Hit " + Raycast_cooldown + " " + Raycast_hitNode);
            //    }
            //}
            //else
            //{
                ProcessHitNode(hitNode);
            //}
        }
        else
        {
            //if (useCooldown)
            //{
            //    Raycast_hitNode = null;
            //}

            //GD.Print("Nothing");

            OnDehoverFleets();
            OnDehoverPlanet();
            OnDehoverStar();
        }
    }

    private Vector2 LastMousePosition = Vector2.Zero;
    public override void _Process(double delta)
    {
        Vector2 mousePos = GetViewport().GetMousePosition();
        if (UnhandledInput)
        {
            if ((LastMousePosition - mousePos).LengthSquared() < delta * 1000.0)
            {
                //Game.self.GalaxyUI.DEBUGText.Text = "cast";
                Raycast();
            }
            //    else
            //    {
            //        Game.self.GalaxyUI.DEBUGText.Text = "too fast------------ too fast";
            //    }
        }
        //else
        //{
        //    Game.self.GalaxyUI.DEBUGText.Text = "-- Handled Input --";
        //}
        LastMousePosition = mousePos;

        DEBUG_State();
    }

    private void DEBUG_State()
    { 
        if (State == InputState.CLOSE_STAR || State == InputState.CLOSE_PLANET || State == InputState.CLOSE_GALAXY)
        {
            AutoselectClosestStar(false);
        }
        if (Game.self != null)
        {
            string text = State.ToString();
            if (HoverStar != null)
                text += "\n" + "HoveredStar:" + HoverStar.StarName;
            if (SelectedStar != null)
                text += "\n" + "SelectedStar:" + SelectedStar.StarName;
            if (HoverFleets.Count > 0)
                text += "\n" + "HoveredFleets";
            if (SelectedFleet != null)
                text += "\n" + "SelectedFleet";
            if (HoverPlanet != null)
                text += "\n" + "HoveredPlanet:" + HoverPlanet.PlanetName;
            if (SelectedPlanet != null)
                text += "\n" + "SelectedPlanet:" + SelectedPlanet.PlanetName;
            Game.self.GalaxyUI.DEBUGText.Text = text;
        }
    }

    private void ProcessHitNode(Node3D hitNode)
    {
        if (hitNode.GetParent().Name.ToString() == "Star")
        {
            OnDehoverFleets();
            OnDehoverPlanet();

            GFXStar star = (GFXStar)hitNode.GetParent().GetParent();
            OnHoverStar(star._Star);

            //GD.Print(star.Name);
        }
        else if (hitNode.GetParent().Name.ToString() == "Offset")
        {
            OnDehoverFleets();
            OnDehoverStar();

            GFXStarOrbit starOrbit = (GFXStarOrbit)hitNode.GetParent().GetParent();
            OnHoverPlanet(starOrbit._Planet);

            //GD.Print(starOrbit.Name);
        }
        else if (hitNode.GetParent().Name.ToString().StartsWith("Ship") == true)
        {
            OnDehoverPlanet();
            OnDehoverStar();

            GFXStarShip starShip = (GFXStarShip)hitNode.GetParent();
            OnHoverFleets(starShip._Fleets);

            //GD.Print(starShip.Name);
        }
        else
        {
            //nothing
            OnDehoverFleets();
            OnDehoverPlanet();
            OnDehoverStar();

            // GD.Print("Never");
        }
    }

    // ---------------------------------------------------------------------------------
    public void OnHoverStar(StarData star)
    {
        if (star == HoverStar) return;

        if (HoverStar != null) OnDehoverStar();

        if (star == SelectedStar) return;

        switch (State)
        {
            case InputState.FAR_GALAXY:
                CS_Hover_from_FarGalaxy_to_FarGalaxyHoverStar(star); break;
            case InputState.FAR_STAR:
                CS_Hover_from_FarStar_to_FarStarHoverStar(star); break;
            case InputState.FAR_FLEETS:
                CS_Hover_from_FarFleets_to_FarFleetsHoverStar(star); break;
            //case InputState.CLOSE_STAR:
            //    CS_Hover_from_CloseStar_to_CloseStarHoverStar(star); break;
            //case InputState.CLOSE_PLANET:
            //    CS_Hover_from_ClosePlanet_to_ClosePlanetHoverStar(star); break;
            case InputState.CLOSE_FLEETS:
                CS_Hover_from_CloseFleets_to_CloseFleetsHoverStar(star); break;
        }
    }
    //public void OnDehoverStar(StarData star)
    //{
    //    if (star != HoverStar) return;
    //
    //    OnDehoverStar();
    //}
    public void OnDehoverStar()
    {
        if (HoverStar == null) return;

        switch (State)
        {
            case InputState.FAR_GALAXY_H_STAR:
                CS_Dehover_from_FarGalaxyHoverStar_to_FarGalaxy(); break;
            case InputState.FAR_STAR_H_STAR:
                CS_Dehover_from_FarStarHoverStar_to_FarStar(); break;
            case InputState.FAR_FLEETS_H_STAR:
                CS_Dehover_from_FarFleetsHoverStar_to_FarFleets(); break;
            //case InputState.CLOSE_STAR_H_STAR:
            //    CS_Dehover_from_CloseStarHoverStar_to_CloseStar(); break;
            //case InputState.CLOSE_PLANET_H_STAR:
            //    CS_Dehover_from_ClosePlanetHoverStar_to_ClosePlanet(); break;
            case InputState.CLOSE_FLEETS_H_STAR:
                CS_Dehover_from_CloseFleetsHoverStar_to_CloseFleets(); break;
        }
    }

    public void OnSelectStar(StarData star)
    {
        if (star != HoverStar) return;
        if (HoverStar.Visibility_PerTurn.IsUncoveredBy(Game.self.HumanPlayer) == false) return;

        switch (State)
        {
            case InputState.FAR_GALAXY_H_STAR:
                CS_Select_from_FarGalaxyHoverStar_to_FarStar(); break;
            case InputState.FAR_STAR_H_STAR:
                CS_Select_from_FarStarHoverStar_to_FarStar(); break;
            case InputState.FAR_FLEETS_H_STAR:
                CS_Select_from_FarFleetsHoverStar_to_FarStar(); break;
            //case InputState.CLOSE_STAR_H_STAR:
            //    CS_Select_from_CloseStarHoverStar_to_CloseStar(); break;
            //case InputState.CLOSE_PLANET_H_STAR:
            //    CS_Select_from_ClosePlanetHoverStar_to_CloseStar(); break;
            case InputState.CLOSE_FLEETS_H_STAR:
                CS_Select_from_CloseFleetsHoverStar_to_CloseStar(); break;
        }
    }
    public void OnDeselectStar(bool forced = false)
    {
        switch (State)
        {
            case InputState.FAR_STAR:
                CS_Deselect_from_FarStar_to_FarGalaxy(); break;
            case InputState.FAR_STAR_H_FLEETS:
                CS_Deselect_from_FarStarHoverFleet_to_FarGalaxyHoverFleet(); break;
            case InputState.FAR_STAR_H_STAR:
                CS_Deselect_from_FarStarHoverStar_to_FarGalaxyHoverStar(); break;
            case InputState.CLOSE_STAR:
                if (forced) CS_Deselect_from_CloseStar_to_CloseGalaxy(); break;
        }
    }

    // -------------------------------------
    public void OnHoverFleets(Array<FleetData> fleets)
    {
        if (fleets.Count == 0 || (fleets.Count > 0 && fleets.Count == HoverFleets.Count && fleets[0] == HoverFleets[0])) return;

        if (HoverFleets.Count > 0) OnDehoverFleets();

        if (fleets.Count == 1 && fleets[0] == SelectedFleet) return;

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
        if (HoverFleets.Count == 0) return;

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
                CS_Select_from_FarGalaxyHoverFleets_to_FarFleets(fleets.Count > 1); break;
            case InputState.FAR_STAR_H_FLEETS:
                CS_Select_from_FarStarHoverFleets_to_FarFleets(fleets.Count > 1); break;
            case InputState.FAR_FLEETS_H_FLEETS:
                CS_Select_from_FarFleetsHoverFleets_to_FarFleets(fleets.Count > 1); break;
            case InputState.CLOSE_STAR_H_FLEETS:
                CS_Select_from_CloseStarHoverFleets_to_CloseFleet(fleets.Count > 1); break;
            case InputState.CLOSE_PLANET_H_FLEETS:
                CS_Select_from_ClosePlanetHoverFleets_to_CloseFleets(fleets.Count > 1); break;
            case InputState.CLOSE_FLEETS_H_FLEETS:
                CS_Select_from_CloseFleetsHoverFleets_to_CloseFleets(fleets.Count > 1); break;
        }
    }

    //public void OnDeselectFleet(FleetData fleet)
    //{
    //    //SelectedFleets.Remove(fleet);
    //    //if (SelectedFleets.Count == 0)
    //    //{
    //    //    OnDeselectFleets();
    //    //}
    //    //else
    //    //{
    //    //    Game.self.GalaxyUI.FleetsSelected.Refresh(SelectedFleets);
    //    //}
    //
    //    SelectedFleets.Clear();
    //    SelectedCurrentFleet = null;
    //    OnDeselectFleets();
    //}

    public void OnDeselectFleets()
    {
        //SelectedFleetList.Clear();
        switch (State)
        {
            case InputState.FAR_FLEETS:
                CS_Deselect_from_FarFleets_to_FarGalaxy(); break;
            case InputState.FAR_FLEETS_H_FLEETS:
                CS_Deselect_from_FarFleetsHoverFleet_to_FarGalaxyHoverFleet(); break;
            case InputState.FAR_FLEETS_H_STAR:
                CS_Deselect_from_FarFleetsHoverStar_to_FarGalaxyHoverStar(); break;
            case InputState.CLOSE_FLEETS:
                CS_Deselect_from_CloseFleets_to_CloseStar(); break;
            case InputState.CLOSE_FLEETS_H_FLEETS:
                CS_Deselect_from_CloseFleetsHoverFleets_to_CloseStarHoverFleets(); break;
            case InputState.CLOSE_FLEETS_H_STAR:
                CS_Deselect_from_CloseFleetsHoverStar_to_CloseStar(); break;
            case InputState.CLOSE_FLEETS_H_PLANET:
                CS_Deselect_from_CloseFleetsHoverPlanet_to_CloseStarHoverPlanet(); break;
        }
    }

    // -------------------------------------
    public void OnHoverPlanet(PlanetData planet)
    {
        if (planet == HoverPlanet) return;

        if (HoverPlanet != null) OnDehoverPlanet();

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
    //public void OnDehoverPlanet(PlanetData planet)
    //{
    //    if (planet != HoverPlanet) return;
    //
    //    OnDehoverPlanet();
    //}
    public void OnDehoverPlanet()
    {
        if (HoverPlanet == null) return;

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
        if (planet != HoverPlanet) return;

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
            //case InputState.CLOSE_PLANET_H_STAR:
            //    CS_Deselect_from_ClosePlanetHoverStar_to_CloseStarHoverStar(); break;
            case InputState.CLOSE_PLANET_H_FLEETS:
                CS_Deselect_from_ClosePlanetHoverFleets_to_CloseStarHoverFleets(); break;
        }
    }

    // ---------------------------------------------------------------------------------
    public void DeselectOneStep(bool forced = false)
    {
        if (State == InputState.LOCKED_DIPLOMACY) OnCloseDiplomacy();
        else if (SelectedPlanet != null) OnDeselectPlanet();
        else if (SelectedFleet != null) OnDeselectFleets();
        else if (SelectedStar != null) OnDeselectStar(forced);
    }

    public void DeselectAll(bool forced = false)
    {
        DeselectOneStep(forced);
        DeselectOneStep(forced);
    }

    // -------------------------------------
    private void CS_Hover_from_FarGalaxy_to_FarGalaxyHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        Game.self.GalaxyUI.ShowStarInfo(null, HoverStar);

        State = InputState.FAR_GALAXY_H_STAR;
    }
    private void CS_Hover_from_FarStar_to_FarStarHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, HoverStar);

        State = InputState.FAR_STAR_H_STAR;
    }
    private void CS_Hover_from_FarFleets_to_FarFleetsHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(null, HoverStar);

        State = InputState.FAR_FLEETS_H_STAR;
    }
    //private void CS_Hover_from_CloseStar_to_CloseStarHoverStar(StarData star)
    //{
    //    HoverStar = star;
    //    HoverStar._Node.GFX.GFXHover();
    //    HoverStar._Node.GFX.ShowPlanets3DGUI();
    //    Game.self.GalaxyUI.ShowStarInfo(HoverStar);
    //
    //    State = InputState.CLOSE_STAR_H_STAR;
    //}
    //private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverStar(StarData star)
    //{
    //    HoverStar = star;
    //    HoverStar._Node.GFX.GFXHover();
    //    HoverStar._Node.GFX.ShowPlanets3DGUI();
    //    Game.self.GalaxyUI.ShowStarInfo(HoverStar);
    //
    //    State = InputState.CLOSE_PLANET_H_STAR;
    //}
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverStar(StarData star)
    {
        HoverStar = star;
        HoverStar._Node.GFX.GFXHover();
        HoverStar._Node.GFX.ShowPlanets3DGUI();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(null, HoverStar);
    
        State = InputState.CLOSE_FLEETS_H_STAR;
    }

    // -------------------------------------
    private void CS_Dehover_from_FarGalaxyHoverStar_to_FarGalaxy()
    {
        HoverStar._Node.GFX.GFXDehover();
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.HidePlanetInfo();

        HoverStar = null;
        State = InputState.FAR_GALAXY;
    }
    private void CS_Dehover_from_FarStarHoverStar_to_FarStar()
    {
        HoverStar._Node.GFX.GFXDehover();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);

        HoverStar = null;
        State = InputState.FAR_STAR;
    }
    private void CS_Dehover_from_FarFleetsHoverStar_to_FarFleets()
    {
        HoverStar._Node.GFX.GFXDehover();
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.HidePlanetInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(null, SelectedFleet);

        HoverStar = null;
        State = InputState.FAR_FLEETS;
    }
    //private void CS_Dehover_from_CloseStarHoverStar_to_CloseStar()
    //{
    //    HoverStar._Node.GFX.GFXDehover();
    //    if(HoverStar != SelectedStar) HoverStar._Node.GFX.HidePlanets3DGUI();
    //    Game.self.GalaxyUI.ShowStarInfo(SelectedStar);
    //
    //    HoverStar = null;
    //    State = InputState.CLOSE_STAR;
    //}
    //private void CS_Dehover_from_ClosePlanetHoverStar_to_ClosePlanet()
    //{
    //    HoverStar._Node.GFX.GFXDehover();
    //    HoverStar._Node.GFX.HidePlanets3DGUI();
    //    Game.self.GalaxyUI.ShowStarInfo(SelectedStar);
    //
    //    HoverStar = null;
    //    State = InputState.CLOSE_PLANET;
    //}
    private void CS_Dehover_from_CloseFleetsHoverStar_to_CloseFleets()
    {
        HoverStar._Node.GFX.GFXDehover();
        HoverStar._Node.GFX.HidePlanets3DGUI();
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.HidePlanetInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(null, SelectedFleet);
    
        HoverStar = null;
        State = InputState.CLOSE_FLEETS;
    }

    // -------------------------------------
    private void CS_Select_from_FarGalaxyHoverStar_to_FarStar()
    {
        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.FAR_STAR;
    }
    private void CS_Select_from_FarStarHoverStar_to_FarStar()
    {
        SelectedStar._Node.GFX.GFXDeselect();

        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.FAR_STAR;
    }
    private void CS_Select_from_FarFleetsHoverStar_to_FarStar()
    {
        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();

        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        State = InputState.FAR_STAR;
    }
    //private void CS_Select_from_CloseStarHoverStar_to_CloseStar()
    //{
    //    SelectedStar._Node.GFX.GFXDeselect();
    //    SelectedStar._Node.GFX.HidePlanets3DGUI();
    //
    //    SelectedStar = HoverStar;
    //    SelectedStar._Node.GFX.GFXSelect();
    //    //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);
    //
    //    HoverStar._Node.GFX.GFXDehover();
    //    HoverStar = null;
    //
    //    State = InputState.CLOSE_STAR;
    //}
    //private void CS_Select_from_ClosePlanetHoverStar_to_CloseStar()
    //{
    //    Game.self.SelectorsUI3D.PlanetDeselect();
    //    SelectedPlanet = null;
    //
    //    SelectedStar._Node.GFX.GFXDeselect();
    //    SelectedStar._Node.GFX.HidePlanets3DGUI();
    //
    //    SelectedStar = HoverStar;
    //    SelectedStar._Node.GFX.GFXSelect();
    //    //Game.self.GalaxyUI.ShowStarInfo(SelectedStar);
    //
    //    HoverStar._Node.GFX.GFXDehover();
    //    HoverStar = null;
    //
    //    State = InputState.CLOSE_STAR;
    //}
    private void CS_Select_from_CloseFleetsHoverStar_to_CloseStar()
    {
        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();
    
        //SelectedStar._Node.GFX.GFXDeselect();
        //SelectedStar._Node.GFX.HidePlanets3DGUI();
    
        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);
    
        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;
    
        State = InputState.CLOSE_STAR;
    }

    // -------------------------------------
    private void CS_Deselect_from_FarStar_to_FarGalaxy()
    {
        SelectedStar._Node.GFX.GFXDeselect();
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.HidePlanetInfo();
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

    private void CS_Deselect_from_CloseStar_to_CloseGalaxy()
    {
        SelectedStar._Node.GFX.GFXDeselect();
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.HidePlanetInfo();
        SelectedStar = null;

        State = InputState.CLOSE_GALAXY;
    }

    // -------------------------------------
    private void CS_Hover_from_FarGalaxy_to_FarGalaxyHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        State = InputState.FAR_GALAXY_H_FLEETS;
    }
    private void CS_Hover_from_FarStar_to_FarStarHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.HidePlanetInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);


        State = InputState.FAR_STAR_H_FLEETS;
    }
    private void CS_Hover_from_FarFleets_to_FarFleetsHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        State = InputState.FAR_FLEETS_H_FLEETS;
    }
    private void CS_Hover_from_CloseStar_to_CloseStarHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.HidePlanetInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        State = InputState.CLOSE_STAR_H_FLEETS;
    }
    private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.HideStarInfo();
        Game.self.GalaxyUI.HidePlanetInfo();
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        State = InputState.CLOSE_PLANET_H_FLEETS;
    }
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverFleets(Array<FleetData> fleets)
    {
        HoverFleets.Clear();
        HoverFleets.AddRange(fleets);
        Game.self.SelectorsUI3D.FleetHover(fleets);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

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
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);

       State = InputState.FAR_STAR;
    }
    private void CS_Dehover_from_FarFleetsHoverFleets_to_FarFleets()
    {
        HoverFleets.Clear();
        //SelectedFleetList.Clear();
        //SelectedFleetList.Add(SelectedFleet);
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.ShowFleetsInfo(null, SelectedFleet);

        State = InputState.FAR_FLEETS;
    }
    private void CS_Dehover_from_CloseStarHoverFleets_to_CloseStar()
    {
        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);

        State = InputState.CLOSE_STAR;
    }
    private void CS_Dehover_from_ClosePlanetHoverFleets_to_ClosePlanet()
    {
        HoverFleets.Clear();
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);
        Game.self.GalaxyUI.ShowPlanetInfo(SelectedPlanet);

        State = InputState.CLOSE_PLANET;
    }
    private void CS_Dehover_from_CloseFleetsHoverFleets_to_CloseFleets()
    {
        HoverFleets.Clear();
        //SelectedFleetList.Clear();
        //SelectedFleetList.Add(SelectedFleet);
        Game.self.SelectorsUI3D.FleetDehover();
        Game.self.GalaxyUI.ShowFleetsInfo(null, SelectedFleet);

        State = InputState.CLOSE_FLEETS;
    }

    // -------------------------------------
    private void CS_Select_from_FarGalaxyHoverFleets_to_FarFleets(bool multipleFleets)
    {
        //SelectedFleetList.Clear();
        //SelectedFleetList.AddRange(HoverFleets);
        SelectedFleet = HoverFleets[0];
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleet);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        if (multipleFleets == false)
        {
            HoverFleets.Clear();
            Game.self.SelectorsUI3D.FleetDehover();

            State = InputState.FAR_FLEETS;
        }
        else
        {
            State = InputState.FAR_FLEETS_H_FLEETS;
        }
    }
    private void CS_Select_from_FarStarHoverFleets_to_FarFleets(bool multipleFleets)
    {
        SelectedStar._Node.GFX.GFXDeselect();
        SelectedStar = null;

        //SelectedFleetList.Clear();
        //SelectedFleetList.AddRange(HoverFleets);
        SelectedFleet = HoverFleets[0];
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleet);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        if (multipleFleets == false)
        {
            HoverFleets.Clear();
            Game.self.SelectorsUI3D.FleetDehover();

            State = InputState.FAR_FLEETS;
        }
        else
        {
            State = InputState.FAR_FLEETS_H_FLEETS;
        }
    }
    private void CS_Select_from_FarFleetsHoverFleets_to_FarFleets(bool multipleFleets)
    {
        Game.self.SelectorsUI3D.FleetDeselect();

        //SelectedFleetList.Clear();
        //SelectedFleetList.AddRange(HoverFleets);
        if (HoverFleets.Contains(SelectedFleet))
        {
            int idx = HoverFleets.IndexOf(SelectedFleet);
            SelectedFleet = HoverFleets[(idx + 1) % HoverFleets.Count];
        }
        else
        {
            SelectedFleet = HoverFleets[0];
        }
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleet);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        if (multipleFleets == false)
        {
            HoverFleets.Clear();
            Game.self.SelectorsUI3D.FleetDehover();

            State = InputState.FAR_FLEETS;
        }
        else
        {
            State = InputState.FAR_FLEETS_H_FLEETS;
        }
    }
    private void CS_Select_from_CloseStarHoverFleets_to_CloseFleet(bool multipleFleets)
    {
        SelectedStar._Node.GFX.GFXDeselect();
        SelectedStar = null;

        //SelectedFleetList.Clear();
        //SelectedFleetList.AddRange(HoverFleets);
        SelectedFleet = HoverFleets[0];
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleet);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        if (multipleFleets == false)
        {
            HoverFleets.Clear();
            Game.self.SelectorsUI3D.FleetDehover();

            State = InputState.CLOSE_FLEETS;
        }
        else
        {
            State = InputState.CLOSE_FLEETS_H_FLEETS;
        }
    }
    private void CS_Select_from_ClosePlanetHoverFleets_to_CloseFleets(bool multipleFleets)
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        SelectedStar._Node.GFX.GFXDeselect();
        SelectedStar = null;

        //SelectedFleetList.Clear();
        //SelectedFleetList.AddRange(HoverFleets);
        SelectedFleet = HoverFleets[0];
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleet);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        if (multipleFleets == false)
        {
            HoverFleets.Clear();
            Game.self.SelectorsUI3D.FleetDehover();

            State = InputState.CLOSE_FLEETS;
        }
        else
        {
            State = InputState.CLOSE_FLEETS_H_FLEETS;
        }
    }
    private void CS_Select_from_CloseFleetsHoverFleets_to_CloseFleets(bool multipleFleets)
    {
        Game.self.SelectorsUI3D.FleetDeselect();

        //SelectedFleetList.Clear();
        //SelectedFleetList.AddRange(HoverFleets);
        if (HoverFleets.Contains(SelectedFleet))
        {
            int idx = HoverFleets.IndexOf(SelectedFleet);
            SelectedFleet = HoverFleets[(idx + 1) % HoverFleets.Count];
        }
        else
        {
            SelectedFleet = HoverFleets[0];
        }
        Game.self.SelectorsUI3D.FleetSelect(SelectedFleet);
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        if (multipleFleets == false)
        {
            HoverFleets.Clear();
            Game.self.SelectorsUI3D.FleetDehover();

            State = InputState.CLOSE_FLEETS;
        }
        else
        {
            State = InputState.CLOSE_FLEETS_H_FLEETS;
        }
    }

    // -------------------------------------
    private void CS_Deselect_from_FarFleets_to_FarGalaxy()
    {
        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();
        Game.self.GalaxyUI.HideFleetsInfo();

        State = InputState.FAR_GALAXY;
    }
    private void CS_Deselect_from_FarFleetsHoverStar_to_FarGalaxyHoverStar()
    {
        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.FAR_GALAXY_H_STAR;
    }
    private void CS_Deselect_from_FarFleetsHoverFleet_to_FarGalaxyHoverFleet()
    {
        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        State = InputState.FAR_GALAXY_H_FLEETS;
    }
    private void CS_Deselect_from_CloseFleets_to_CloseStar()
    {
        SelectedStar = SelectedFleet.StarAt_PerTurn;
        SelectedStar._Node.GFX.GFXSelect();
        SelectedStar._Node.GFX.ShowPlanets3DGUI();
        Game.self.GalaxyUI.HideFleetsInfo();
        Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);

        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.CLOSE_STAR;
    }
    private void CS_Deselect_from_CloseFleetsHoverFleets_to_CloseStarHoverFleets()
    {
        SelectedStar = SelectedFleet.StarAt_PerTurn;
        SelectedStar._Node.GFX.GFXSelect();
        SelectedStar._Node.GFX.ShowPlanets3DGUI();

        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();
        Game.self.GalaxyUI.ShowFleetsInfo(HoverFleets, SelectedFleet);

        State = InputState.CLOSE_STAR_H_FLEETS;
    }
    private void CS_Deselect_from_CloseFleetsHoverStar_to_CloseStar()
    {
        //SelectedStar = SelectedFleet.StarAt_PerTurn;
        SelectedStar = HoverStar;
        SelectedStar._Node.GFX.GFXSelect();
        SelectedStar._Node.GFX.ShowPlanets3DGUI();

        HoverStar._Node.GFX.GFXDehover();
        HoverStar = null;

        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();
    
        State = InputState.CLOSE_STAR;
    }
    private void CS_Deselect_from_CloseFleetsHoverPlanet_to_CloseStarHoverPlanet()
    {
        SelectedStar = SelectedFleet.StarAt_PerTurn;
        SelectedStar._Node.GFX.GFXSelect();
        SelectedStar._Node.GFX.ShowPlanets3DGUI();

        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();

        State = InputState.CLOSE_STAR_H_PLANET;
    }

    // -------------------------------------
    private void CS_Hover_from_CloseStar_to_CloseStarHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        Game.self.SelectorsUI3D.PlanetHover(HoverPlanet);
        Game.self.GalaxyUI.ShowPlanetInfo(HoverPlanet);

        if (planet._Star != SelectedStar)
        {
            HoverStar = planet._Star;

            HoverStar._Node.GFX.GFXHover();
            HoverStar._Node.GFX.ShowPlanets3DGUI();
            Game.self.GalaxyUI.ShowStarInfo(SelectedStar, HoverStar);
        }

        State = InputState.CLOSE_STAR_H_PLANET;
    }
    private void CS_Hover_from_ClosePlanet_to_ClosePlanetHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        Game.self.SelectorsUI3D.PlanetHover(HoverPlanet);
        Game.self.GalaxyUI.ShowPlanetInfo(HoverPlanet);

        if (planet._Star != SelectedStar)
        {
            HoverStar = planet._Star;

            HoverStar._Node.GFX.GFXHover();
            HoverStar._Node.GFX.ShowPlanets3DGUI();
            Game.self.GalaxyUI.ShowStarInfo(SelectedStar, HoverStar);
        }

        State = InputState.CLOSE_PLANET_H_PLANET;
    }
    private void CS_Hover_from_CloseFleets_to_CloseFleetsHoverPlanet(PlanetData planet)
    {
        HoverPlanet = planet;
        Game.self.SelectorsUI3D.PlanetHover(HoverPlanet);
        Game.self.GalaxyUI.ShowPlanetInfo(HoverPlanet);

        //if (planet._Star != SelectedStar) // selectedStar should be null and the planet has a star
        {
            HoverStar = planet._Star;

            HoverStar._Node.GFX.GFXHover();
            HoverStar._Node.GFX.ShowPlanets3DGUI();
            Game.self.GalaxyUI.ShowStarInfo(null, HoverStar);
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
            if (HoverStar != SelectedStar) Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);
            HoverStar = null;
        }

        Game.self.GalaxyUI.HidePlanetInfo();
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
            if (HoverStar != SelectedStar) Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);
            HoverStar = null;
        }

        Game.self.GalaxyUI.ShowPlanetInfo(SelectedPlanet);
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
            Game.self.GalaxyUI.HidePlanetInfo();
            Game.self.GalaxyUI.ShowFleetsInfo(null, SelectedFleet);
            HoverStar = null;
        }

        Game.self.GalaxyUI.HidePlanetInfo();
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
        //Game.self.GalaxyUI.SystemInfo.Refresh(SelectedStar);

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
        //Game.self.GalaxyUI.SystemInfo.Refresh(SelectedStar);

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

        //SelectedFleetList.Clear();
        SelectedFleet = null;
        Game.self.SelectorsUI3D.FleetDeselect();

        SelectedPlanet = HoverPlanet;
        Game.self.SelectorsUI3D.PlanetSelect(SelectedPlanet);
        //Game.self.GalaxyUI.SystemInfo.Refresh(SelectedStar);

        Game.self.SelectorsUI3D.PlanetDehover();
        HoverPlanet = null;

        State = InputState.CLOSE_PLANET;
    }
    // -------------------------------------
    private void CS_Deselect_from_ClosePlanet_to_CloseStar()
    {
        Game.self.GalaxyUI.HidePlanetInfo();
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        Game.self.GalaxyUI.SystemInfo.Refresh(SelectedStar);
        Game.self.GalaxyUI.DistrictsInfo.RefreshAll(SelectedStar);

        State = InputState.CLOSE_STAR;
    }
    private void CS_Deselect_from_ClosePlanetHoverPlanet_to_CloseStarHoverPlanet()
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        //Game.self.GalaxyUI.SystemInfo.Refresh(SelectedStar);

        State = InputState.CLOSE_STAR_H_PLANET;
    }
    //private void CS_Deselect_from_ClosePlanetHoverStar_to_CloseStarHoverStar()
    //{
    //    Game.self.SelectorsUI3D.PlanetDeselect();
    //    SelectedPlanet = null;
    //
    //    Game.self.GalaxyUI.SystemInfo.Refresh(SelectedStar);
    //
    //    State = InputState.CLOSE_STAR;
    //}
    private void CS_Deselect_from_ClosePlanetHoverFleets_to_CloseStarHoverFleets()
    {
        Game.self.SelectorsUI3D.PlanetDeselect();
        SelectedPlanet = null;

        //Game.self.GalaxyUI.SystemInfo.Refresh(SelectedStar);

        State = InputState.CLOSE_STAR_H_FLEETS;
    }

    // ---------------------------------------------------------------------------------
    public void CS_Zoom_from_FarStar_to_CloseStar()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            StarData star = Game.self.Map.Data.Stars[idx];
            star._Node.GFX.LODClose();
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

        SelectedStar = SelectedFleet.StarAt_PerTurn;
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

    Plane XOZPlane = new Plane(Vector3.Up);
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
                AutoselectClosestStar(true);
            }
            else
            {
                CS_Zoom_from_FarStar_to_CloseStar();
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

    private void AutoselectClosestStar(bool fromZoom)
    {
        Vector3 focusPoint = Game.self.Camera.CenterCameraXOZ;
        if (fromZoom) focusPoint = Game.self.Camera.MouseXOZ;

        // get closest star
        float minDist = 666.0f;
        StarData star = null;
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            if (Game.self.Map.Data.Stars[idx].Visibility_PerTurn.IsUncoveredBy(Game.self.HumanPlayer) == false) continue;

            float dist = (Game.self.Map.Data.Stars[idx]._Node.GFX.Position - focusPoint).LengthSquared();
            if (dist < minDist)
            {
                star = Game.self.Map.Data.Stars[idx];
                minDist = dist;
            }
        }
        if (star != null && star != SelectedStar)
        {
            if (State == InputState.CLOSE_PLANET)
            {
                OnDeselectPlanet();
            }

            if (State == InputState.CLOSE_STAR || State == InputState.CLOSE_GALAXY || fromZoom)
            {
                if (SelectedStar != null)
                {
                    SelectedStar._Node.GFX.HidePlanets3DGUI();
                    SelectedStar._Node.GFX.GFXDeselect();
                }

                SelectedStar = star;
                SelectedStar._Node.GFX.ShowPlanets3DGUI();
                SelectedStar._Node.GFX.GFXSelect();
                Game.self.GalaxyUI.ShowStarInfo(SelectedStar, null);
                if (fromZoom && (State == InputState.FAR_GALAXY || State == InputState.FAR_STAR))
                {
                    State = InputState.FAR_STAR;
                    CS_Zoom_from_FarStar_to_CloseStar();
                }
                else
                {
                    State = InputState.CLOSE_STAR;
                }
            }
        }
        else if (star == null)
        {
            if (State == InputState.CLOSE_PLANET)
            {
                OnDeselectPlanet();
            }

            if (State == InputState.CLOSE_STAR || fromZoom)
            {
                Game.self.GalaxyUI.HideStarInfo();
                if (SelectedStar != null)
                {
                    SelectedStar._Node.GFX.HidePlanets3DGUI();
                    SelectedStar._Node.GFX.GFXDeselect();
                }
                SelectedStar = null;
                State = InputState.CLOSE_GALAXY;
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
    public void OnOpenDiplomacy(PlayerData player)
    {
        // dehover
        OnDehoverPlanet();
        OnDehoverFleets();
        OnDehoverStar();

        DeselectAll(true);

        //Game.self.GalaxyUI.Diplomacy.Visible = true;
        //Game.self.GalaxyUI.Diplomacy.Refresh(player);

        State = InputState.LOCKED_DIPLOMACY;

        Game.self.Camera.UILock = true;
    }

    public void OnCloseDiplomacy()
    {
        //Game.self.GalaxyUI.Diplomacy.Visible = false;

        State = InputState.FAR_GALAXY;
        if (Game.self.Camera.LOD == 0)
        {
            State = InputState.CLOSE_GALAXY;
            AutoselectClosestStar(true);
        }

        Game.self.Camera.UILock = false;
    }

    // ---------------------------------------------------------------------------------
    public bool TryFleetsMoveToStar(StarData targetStar)
    {
        if (State != InputState.CLOSE_FLEETS_H_STAR && State != InputState.CLOSE_FLEETS_H_PLANET && State != InputState.FAR_FLEETS_H_STAR)
        {
            return false;
        }

        //for (int idx = 0; idx < SelectedFleetList.Count; idx++)
        //{
        //    if (SelectedFleetList[idx]._Player == Game.self.HumanPlayer)
        //    {
        //        if (ActionMove.HasAvailableMove(Game.self, SelectedFleetList[idx], targetStar))
        //        {
        //            ActionMove.CancelMove(Game.self, SelectedFleetList[idx]);
        //            ActionMove.AddMove(Game.self, SelectedFleetList[idx], targetStar);
        //
        //            Game.self.Paths.AddPath(SelectedFleetList[idx], targetStar);
        //        }
        //        else if (SelectedFleetList[idx].StarAt_PerTurn == targetStar)
        //        {
        //            ActionMove.CancelMove(Game.self, SelectedFleetList[idx]);
        //
        //            Game.self.Paths.ClearPathForFleet(SelectedFleetList[idx]);
        //        }
        //
        //        Game.self.GalaxyUI.FleetsSelected.Refresh(SelectedFleetList);
        //    }
        //}

        if (SelectedFleet._Player == Game.self.HumanPlayer)
        {
            if (ActionMove.HasAvailableMove(Game.self, SelectedFleet, targetStar))
            {
                ActionMove.CancelMove(Game.self, SelectedFleet);
                ActionMove.AddMove(Game.self, SelectedFleet, targetStar);
        
                Game.self.Paths.AddPath(SelectedFleet, targetStar);
            }
            else if (SelectedFleet.StarAt_PerTurn == targetStar)
            {
                ActionMove.CancelMove(Game.self, SelectedFleet);
        
                Game.self.Paths.ClearPathForFleet(SelectedFleet);
            }
        
            Game.self.GalaxyUI.FleetsSelected.Refresh(null, SelectedFleet);
        }

        return true;
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
