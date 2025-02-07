using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDistrictsGroup : Control
{
    //public UIActionButton Build;
    public Array<UIDistrictsGroupPlanet> Planets = new Array<UIDistrictsGroupPlanet>();
    public Control Deselect;
    public Control DeselectOdd;
    public Control EvenGap;

    public List<PlanetData> _Planets = new List<PlanetData>();
    public List<ColonyData> _Colonies = new List<ColonyData>();
    public override void _Ready()
    {
        Node planetList = GetNode("PlanetList");
        Planets.Clear();
        for (int idx = 0; idx < planetList.GetChildCount(); idx++)
        {
            Node node = planetList.GetChild(idx);
            if (node is UIDistrictsGroupPlanet)
            {
                Planets.Add(node as UIDistrictsGroupPlanet);
            }
        }

        //Build = GetNode<UIActionButton>("BuildBg/Build");
        if (HasNode("DeselectBg"))
        {
            Deselect = GetNode<Control>("DeselectBg/Deselect");
            DeselectOdd = GetNode<Control>("DeselectBg/Gap_Odd");
        }
        else
        {
            Deselect = null;
            DeselectOdd = null;
        }

        if (HasNode("Gap_Even"))
        {
            EvenGap = GetNode<Control>("Gap_Even");
        }
        else
        {
            EvenGap = null;
        }
    }

    public void RefreshGroup(List<PlanetData> planets)
    { 
        _Planets.Clear();
        _Planets.AddRange(planets);

        for (int idx = 0; idx < Planets.Count; idx++)
        {
            Planets[idx].Visible = false;
        }

        while (Planets.Count < 2)
        {
            UIDistrictsGroupPlanet newItem = Planets[0].Duplicate(7) as UIDistrictsGroupPlanet;
            Planets[0].GetParent().AddChild(newItem);
            Planets.Add(newItem);
        }

        int districtIdx = 0;
        for (int planetIdx = 0; planetIdx < _Planets.Count; planetIdx++)
        {
            ColonyData colony = _Planets[planetIdx].Colony;
            Planets[districtIdx].Visible = true;
            Planets[districtIdx].Refresh(_Planets[planetIdx]);
            districtIdx++;
        }

        while (districtIdx < Planets.Count)
        {
            Planets[districtIdx].Visible = false;
            districtIdx++;
        }
    }

    /*public void RefreshPlanet(PlanetData planet)
    {
        _Planets.Clear();
        _Planets.Add(planet);

        for (int idx = 0; idx < Planets.Count; idx++)
        {
            Planets[idx].Visible = false;
        }

        while (Planets.Count < 7)
        {
            UIDistrictsGroupPlanet newItem = Planets[0].Duplicate(7) as UIDistrictsGroupPlanet;
            Planets[0].GetParent().AddChild(newItem);
            Planets.Add(newItem);
        }

        int districtIdx = 0;
        for (int planetIdx = 0; planetIdx < _Planets.Count; planetIdx++)
        {
            ColonyData colony = _Planets[planetIdx].Colony;
            if (colony != null)
            {
                Planets[districtIdx].Visible = true;
                Planets[districtIdx].Refresh(colony.Districts[7], colony.Districts[6], colony.Districts[2]);
                districtIdx++;
                Planets[districtIdx].Visible = true;
                Planets[districtIdx].Refresh(colony.Districts[5], colony.Districts[4], colony.Districts[1]);
                districtIdx++;
                Planets[districtIdx].Visible = true;
                Planets[districtIdx].Refresh(colony.Districts[3], null, colony.Districts[0]);
                districtIdx++;
            }
            else if (_Planets[planetIdx].IsHabitable())
            {
                Planets[districtIdx].Visible = true;
                Planets[districtIdx].Refresh(_Planets[planetIdx]);
                Planets[districtIdx].RefreshPops(districtIdx % 2 == 1);
                districtIdx++;
            }
            else
            {
                Planets[districtIdx].Visible = true;
                Planets[districtIdx].Refresh(_Planets[planetIdx]);
                Planets[districtIdx].RefreshPops(districtIdx % 2 == 1);
                districtIdx++;
            }
        }

        while (districtIdx < Planets.Count)
        {
            Planets[districtIdx].Visible = false;
            districtIdx++;
        }

        if (Deselect != null)
        {
            Deselect.Visible = true;
            DeselectOdd.Visible = districtIdx % 2 == 1;
        }

        //Build.Visible = false;
    }*/

    public void SetAsOddRow(bool odd)
    {
        if (EvenGap != null) EvenGap.Visible = odd;
    }
}