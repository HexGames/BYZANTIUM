using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDistricts : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control Selected;
    [Export]
    public UIDistrictsGroup SelectedPlanet;
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
    public PlanetData _SelectedPlanet = null;
    [Export]
    public ColonyData _SelectedColony = null;

    // planets in groups
    public List<List<PlanetData>> _Planets = new List<List<PlanetData>>();
    public List<bool> _GroupButtons = new List<bool>();

    public override void _Ready()
    {
        _Planets.Clear();
        _GroupButtons.Clear();
        for (int i = 0; i < 12; i++) // (3 habitable + gas + asteroids + nonhabitable) x 2 = 12 
        {
            _Planets.Add(new List<PlanetData>());
            _GroupButtons.Add(true);
        }
    }

    public void RefreshAll()
    {
        RefreshAll_v2(_Star);
    }

    public void RefreshAll(StarData star)
    {
        RefreshAll_v2(star);
    }

    public void RefreshAll_v1(StarData star)
    {
        All.Visible = true;
        Selected.Visible = false;

        _Star = star;
        _System = star.System;

        for (int idx = 0; idx < _Planets.Count; idx++)
        {
            _Planets[idx].Clear();
        }

        int habitableIdx = 0;
        for (int planetIdx = 1; planetIdx < _Star.Planets.Count; planetIdx++)
        {
            PlanetData planet = _Star.Planets[planetIdx];
            if (planet.IsHabitable())
            {
                _Planets[habitableIdx].Add(planet);
                habitableIdx++;
            }
        }
        for (int planetIdx = 1; planetIdx < _Star.Planets.Count; planetIdx++)
        {
            PlanetData planet = _Star.Planets[planetIdx];
            if (planet.Data.GetSubValueS("Type") == "Gas_Giant")
            {
                _Planets[habitableIdx].Add(planet);
            }
        }
        for (int planetIdx = 1; planetIdx < _Star.Planets.Count; planetIdx++)
        {
            PlanetData planet = _Star.Planets[planetIdx];
            if (planet.Data.GetSubValueS("Type") == "Asteroids")
            {
                _Planets[habitableIdx + 1].Add(planet);
            }
        }
        for (int planetIdx = 1; planetIdx < _Star.Planets.Count; planetIdx++)
        {
            PlanetData planet = _Star.Planets[planetIdx];
            if (planet.IsHabitable() == false && planet.Data.GetSubValueS("Type") != "Gas_Giant" && planet.Data.GetSubValueS("Type") != "Asteroids")
            {
                _Planets[habitableIdx + 2].Add(planet);
            }
        }

        // just for planets
        for (int idx = 0; idx < _Planets.Count; idx++)
        {
            if (_Planets[idx].Count > 4)
            {
                for (int restIdx = _Planets.Count - 1; restIdx > idx + 1; restIdx--)
                {
                    _Planets[restIdx].Clear();
                    _Planets[restIdx].AddRange(_Planets[restIdx - 1]);

                    _GroupButtons[restIdx] = _GroupButtons[restIdx - 1];
                }
                _Planets[idx + 1].Clear();
                int halfCount = (_Planets[idx].Count + 1) / 2;
                while (_Planets[idx].Count > halfCount)
                {
                    _Planets[idx + 1].Add(_Planets[idx][halfCount]);
                    _Planets[idx].RemoveAt(halfCount);
                }
                _GroupButtons[idx] = false;
            }
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
                AllPlanets[idx].RefreshGroup(_Planets[idx], _GroupButtons[idx]);
                AllPlanets[idx].SetAsOddRow(idx % 2 == 1);
            }
            else
            {
                AllPlanets[idx].Visible = false;
            }
        }
    }

    public void RefreshAll_v2(StarData star)
    {
        All.Visible = true;
        Selected.Visible = false;

        _Star = star;
        _System = star.System;

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
                AllPlanets[idx].RefreshGroup(_Planets[idx], false);
                AllPlanets[idx].SetAsOddRow(idx % 2 == 1);
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


        SelectedPlanet.RefreshPlanet(_SelectedPlanet);
    }
}