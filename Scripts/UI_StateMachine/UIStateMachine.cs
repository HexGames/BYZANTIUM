using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIStateMachine
{
    public enum StateID
    {
        MAP_GALAXY,
        MAP_STAR,
        MAP_PLANET,
        MAP_FLEET,
        ACTION_SELECT_PLANET,
        ACTION_SELECT_DISTRICT,
        ACTION_SELECT_CHOICE,
    }
    private System.Collections.Generic.Dictionary<StateID, UIStateBase> States = new System.Collections.Generic.Dictionary<StateID, UIStateBase>();
    private UIStateBase StateCurrent;
    public StateID State;

    public StarData StarSelected;
    public StarData StarHovered;
    public PlanetData PlanetSelected;
    public PlanetData PlanetHovered;
    public FleetData FleetSelected;
    public Array<FleetData> FleetsHovered = new Array<FleetData>();

    public List<ActionBase> PossibleActions = new List<ActionBase>();
    public List<ActionPlanet> PossibleActions_forPlanet = new List<ActionPlanet>();

    public bool ZoomedIn = false;


    // ---------------------------------------------------------------------------------------------------
    public void Init()
    {
        Init_CreateStates();
        Init_States();
        Init_DefaultState();
    }

    private void Init_CreateStates()
    {
        States.Add(StateID.MAP_GALAXY, new UIState_MapGalaxy());
        States.Add(StateID.MAP_STAR, new UIState_MapStar());
        States.Add(StateID.MAP_PLANET, new UIState_MapPlanet());
        States.Add(StateID.MAP_FLEET, new UIState_MapFleet());
        States.Add(StateID.ACTION_SELECT_PLANET, new UIState_ActionSelectPlanet());
        States.Add(StateID.ACTION_SELECT_DISTRICT, new UIState_ActionSelectDistrict());
        States.Add(StateID.ACTION_SELECT_CHOICE, new UIState_ActionSelectChoice());
    }

    private void Init_States()
    {
        foreach (UIStateBase state in States.Values)
        {
            state.Init();
        }
    }

    private void Init_DefaultState()
    {
        StateCurrent = States[StateID.MAP_GALAXY];
    }

    // ---------------------------------------------------------------------------------------------------
    private void ExitState()
    {
        Dehover();
        StateCurrent.Exit();
    }
    private void EnterState(StateID newState)
    {
        State = newState;
        StateCurrent = States[State];
        StateCurrent.Enter();
        Game.self.Input.Raycast();
    }

    // ---------------------------------------------------------------------------------------------------
    private void Select(StarData star)
    {
        if (star == StarSelected) return;
        if (star == null)
        {
            Deselect();
            return;
        }

        ExitState();

        StarSelected = star;
        PlanetSelected = null;
        FleetSelected = null;

        EnterState(StateID.MAP_STAR);
    }
    public void Select(PlanetData planet) // from Select or from UI
    {
        if (planet == PlanetSelected) return;
        if (planet == null)
        {
            Deselect();
            return;
        }

        ExitState();

        StarSelected = null;
        PlanetSelected = planet;
        FleetSelected = null;

        EnterState(StateID.MAP_PLANET);
    }
    public void Select(FleetData fleet) // from SelectNextFleet or from UI
    {
        if (fleet == FleetSelected) return;
        if (fleet == null)
        {
            Deselect();
            return;
        }

        ExitState();

        StarSelected = null;
        PlanetSelected = null;
        FleetSelected = fleet;

        EnterState(StateID.MAP_FLEET);
    }
    private void SelectNextFleet()
    {
        if (FleetsHovered.Count == 0) return;
        
        int idx = FleetsHovered.IndexOf(FleetSelected);
        if (idx >= 0)
        {
            Select(FleetsHovered[(idx + 1) % FleetsHovered.Count]);
        }
        else
        {
            Select(FleetsHovered[0]); // if alredy selected, it will do nothing
        }
    }

    // ---------------------------------------------------------------------------------------------------
    public void Select()
    {
        if (StarHovered != null)
        {
            Select(StarHovered);
            return;
        }
        if (PlanetHovered != null)
        {
            Select(PlanetHovered);
            return;
        }
        if (FleetsHovered.Count > 0)
        {
            SelectNextFleet();
            return;
        }
    }

    public void Deselect()
    {
        if (State == StateID.ACTION_SELECT_CHOICE)
        {
            if (PlanetSelected != null)
            {
                ExitState();

                PossibleActions.Clear();
                PossibleActions_forPlanet.Clear();

                EnterState(StateID.MAP_PLANET);
            }
            else if (StarSelected != null)
            {
                ExitState();

                if (PossibleActions_forPlanet.Count > 0)
                {
                    PossibleActions_forPlanet.Clear();
                    EnterState(StateID.ACTION_SELECT_PLANET);
                }
                else
                {
                    PossibleActions.Clear();
                    EnterState(StateID.MAP_STAR);
                }
            }
            return;
        }
        else if (State == StateID.ACTION_SELECT_PLANET)
        {
            ExitState();
            EnterState(StateID.MAP_STAR);
            return;
        }
        else if (PlanetSelected != null)
        {
            ExitState();

            StarSelected = PlanetSelected._Star;
            PlanetSelected = null;
            FleetSelected = null;

            EnterState(StateID.MAP_STAR);
            return;
        }

        DeselectAll();
    }
    public void DeselectAll()
    {
        ExitState();

        StarSelected = null;
        PlanetSelected = null;
        FleetSelected = null;

        EnterState(StateID.MAP_GALAXY);
    }

    // ---------------------------------------------------------------------------------------------------
    public void Hover(StarData star)
    {
        if (star == null) return;
        if (star == StarHovered) return;
        if (star == StarSelected) Dehover();

        if (PlanetSelected != null && star == PlanetSelected._Star) Dehover();

        StateCurrent.Hover(star);

        StarHovered = star;
        PlanetHovered = null;
        FleetsHovered.Clear();
    }
    public void Hover(PlanetData planet)
    {
        if (planet == null) return;
        if (planet == PlanetHovered) return;
        if (planet == PlanetSelected) return;

        StateCurrent.Hover(planet);

        StarHovered = null;
        PlanetHovered = planet;
        FleetsHovered.Clear();

    }
    public void Hover(Array<FleetData> fleets)
    {
        if (fleets.Count == 0) return;
        if (FleetsHovered.Count > 0 && fleets.Contains(FleetsHovered[0])) return;
        //if (FleetSelected != null && fleets.Contains(FleetSelected)) return;

        StateCurrent.Hover(fleets);

        StarHovered = null;
        PlanetHovered = null;
        FleetsHovered.Clear();
        FleetsHovered.AddRange(fleets);
    }
    public void Dehover()
    {
        if (StarHovered == null && PlanetHovered == null && FleetsHovered.Count == 0) return;

        StateCurrent.Dehover();

        StarHovered = null;
        PlanetHovered = null;
        FleetsHovered.Clear();
    }

    // ---------------------------------------------------------------------------------------------------
    public void ZoomIn()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            StarData star = Game.self.Map.Data.Stars[idx];
            star._Node.GFX.LODClose();
        }

        StateCurrent.ZoomIn();
        ZoomedIn = true;
    }

    public void ZoomOut()
    {
        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODFar();
        }

        StateCurrent.ZoomOut();
        ZoomedIn = false;
    }

    // ---------------------------------------------------------------------------------------------------
    public void Update(double frameTime)
    {
        StateCurrent.Update(frameTime);
    }

    // ---------------------------------------------------------------------------------------------------
    public bool IsSomethingHovered()
    {
        return StarHovered != null || PlanetHovered != null || FleetsHovered.Count > 0;
    }

    public StarData GetSomethingHoveredStar()
    {
        if (PlanetHovered != null) return PlanetHovered._Star;
        if (FleetsHovered.Count > 0) return FleetsHovered[0].StarAt_PerTurn;
        return StarHovered;
    }
    // ---------------------------------------------------------------------------------------------------
    public void SetHoverToken(StarData star)
    {
        star._Node.GFX.GFXHover();
        if (ZoomedIn && star != StarSelected) star._Node.GFX.ShowPlanets3DGUI();
    }
    public void ClearHoverToken(StarData star)
    {
        star._Node.GFX.GFXDehover();
        if (ZoomedIn && star != StarSelected) star._Node.GFX.HidePlanets3DGUI();
    }
    public void SetHoverToken(PlanetData planet)
    {
        Game.self.SelectorsUI3D.PlanetHover(planet);
    }
    public void ClearHoverToken(PlanetData planet)
    {
        Game.self.SelectorsUI3D.PlanetDehover();
    }

    public void SetHoverToken(Array<FleetData> fleets)
    {
        Game.self.SelectorsUI3D.FleetHover(fleets);
    }
    public void ClearHoverToken(Array<FleetData> fleets)
    {
        Game.self.SelectorsUI3D.FleetDehover();
    }

    // ---------------------------------------------------------------------------------------------------
    public void OnDataChanged(StarData star)
    {
        if (star == null) return;
        if (Game.self.UIGalaxy.SystemInfo._Star == star) Game.self.UIGalaxy.SystemInfo.NeedsRefresh();
        if (Game.self.UIGalaxy.DistrictsInfo._Star == star) Game.self.UIGalaxy.DistrictsInfo.NeedsRefresh();
        if (star.System != null) foreach (var empire in Game.self.UIGalaxy.DiplomacyBar.Empires) if (empire._Player == star.System._Player) empire.NeedsRefresh();
        if (Game.self.UIGalaxy.ActionsEconomy._System == star.System || Game.self.UIGalaxy.ActionsEconomy._TextStar == star) Game.self.UIGalaxy.ActionsEconomy.NeedsRefresh();
    }
}

