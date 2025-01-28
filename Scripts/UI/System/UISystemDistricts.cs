using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISystemDistricts : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control Selected;
    [Export]
    public UISystemDistrictsPlanet SelectedPlanet;
    [Export]
    public Control All;
    [Export]
    public Array<UISystemDistrictsPlanet> AllPlanets = new Array<UISystemDistrictsPlanet>();

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;
    [Export]
    public PlanetData _SelectedPlanet = null;
    [Export]
    public ColonyData _SelectedColony = null;

    public void RefreshAll()
    {
        RefreshAll(_Star);
    }

    public void RefreshAll(StarData star)
    {
        All.Visible = true;
        Selected.Visible = false;

        _Star = star;
        _System = star.System;

        // grow
        while (AllPlanets.Count < _Star.Planets.Count - 1)
        {
            UISystemDistrictsPlanet newItem = AllPlanets[0].Duplicate(7) as UISystemDistrictsPlanet;
            AllPlanets[0].GetParent().AddChild(newItem);
            AllPlanets.Add(newItem);
        }

        for (int idx = 0; idx < AllPlanets.Count; idx++)
        {
            if (idx + 1 < _Star.Planets.Count)
            {
                AllPlanets[idx].Visible = true;
                AllPlanets[idx].Refresh(_Star.Planets[idx + 1]);
            }
            else
            {
                AllPlanets[idx].Visible = false;
            }
        }
    }

    public void RefreshSelected(PlanetData selectedPlanet)
    {
        All.Visible = false;
        Selected.Visible = true;

        _SelectedPlanet = selectedPlanet;
        _SelectedColony = selectedPlanet.Colony;
    }
}