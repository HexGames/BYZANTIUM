using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDistricts : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control Selected;
    [Export]
    public UIDistrictsGroupPlanet SelectedPlanet;
    [Export]
    public Control All;
    [Export]
    public Array<UIDistrictsGroup> AllPlanets = new Array<UIDistrictsGroup>();

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;
    [Export]
    public PlanetData _Planet = null;
    [Export]
    public ColonyData _Colony = null;

    // planets in groups
    public List<List<PlanetData>> _Planets = new List<List<PlanetData>>();


    private bool Refreshed = false;
    public void NeedsRefresh()
    {
        Refreshed = false;
        if (Visible && All.Visible) RefreshStar(_Star);
        else if (Visible && Selected.Visible) RefreshPlanet(_Planet);
    }

    public override void _Ready()
    {
        _Planets.Clear();
        for (int i = 0; i < 12; i++) // (3 habitable + gas + asteroids + nonhabitable) x 2 = 12 
        {
            _Planets.Add(new List<PlanetData>());
        }
    }

    //public void RefreshAll()
    //{
    //    RefreshStar(_Star);
    //}

    public void RefreshStar(StarData star)
    {
        if (star == null) return;
        Visible = true;
        All.Visible = true;
        Selected.Visible = false;
        if (_Star == star && Refreshed) return;

        _Star = star;
        _System = star.System;
        Refreshed = true;

        for (int idx = 0; idx < _Planets.Count; idx++)
        {
            _Planets[idx].Clear();
        }

        int groupIdx = 0;
        for (int planetIdx = 1; planetIdx < _Star.Planets.Count; planetIdx++)
        {
            if (_Star.Planets[planetIdx].Data.HasSub("Moon")) groupIdx--;
            _Planets[groupIdx].Insert(0, _Star.Planets[planetIdx]);
            groupIdx++;
        }

        // grow
        while (AllPlanets.Count < _Planets.Count)
        {
            UIDistrictsGroup newItem = AllPlanets[0].Duplicate(7) as UIDistrictsGroup;
            AllPlanets[0].GetParent().AddChild(newItem);
            AllPlanets.Add(newItem);
        }

        for (int idx = 0; idx < AllPlanets.Count; idx++)
        {
            if (_Planets[idx].Count > 0)
            {
                AllPlanets[idx].Visible = true;
                AllPlanets[idx].RefreshGroup(_Planets[idx]);
                AllPlanets[idx].SetAsOddRow(idx % 2 == 1);
            }
            else
            {
                AllPlanets[idx].Visible = false;
            }
        }

        HideButtons();
    }

    public void RefreshButtons<T>(List<T> possibleActions) where T : ActionBase
    {
        for (int groupIdx = 0; groupIdx < AllPlanets.Count; groupIdx++)
        {
            if (AllPlanets[groupIdx].Visible)
            {
                for (int planetIdx = 0; planetIdx < AllPlanets[groupIdx].Planets.Count; planetIdx++)
                {
                    AllPlanets[groupIdx].Planets[planetIdx].SetPossibleActions(possibleActions);
                }
            }
        }
    }

    public void HideButtons()
    {
        for (int groupIdx = 0; groupIdx < AllPlanets.Count; groupIdx++)
        {
            if (AllPlanets[groupIdx].Visible)
            {
                for (int planetIdx = 0; planetIdx < AllPlanets[groupIdx].Planets.Count; planetIdx++)
                {
                    AllPlanets[groupIdx].Planets[planetIdx].ClearPossibleActions();
                }
            }
        }
    }

    public void RefreshPlanet(PlanetData planet)
    {
        if (planet == null) return;
        Visible = true;
        All.Visible = false;
        Selected.Visible = true;
        if (_Planet == planet && Refreshed) return;

        _Planet = planet;
        _Colony = _Planet.Colony;
        Refreshed = true;

        SelectedPlanet.Refresh(_Planet);
    }
}