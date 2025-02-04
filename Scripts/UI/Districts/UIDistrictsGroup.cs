using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDistrictsGroup : Control
{
    public UIActionButton Build;
    public Array<UIDistrictsGroupDistrict> Districts = new Array<UIDistrictsGroupDistrict>();
    public Control Deselect;
    public Control DeselectOdd;
    public Control EvenGap;

    public List<PlanetData> _Planets = new List<PlanetData>();
    public List<ColonyData> _Colonies = new List<ColonyData>();
    public override void _Ready()
    {
        Node districtList = GetNode("DistrictList");
        Districts.Clear();
        for (int idx = 0; idx < districtList.GetChildCount(); idx++)
        {
            Node node = districtList.GetChild(idx);
            if (node is UIDistrictsGroupDistrict)
            {
                Districts.Add(node as UIDistrictsGroupDistrict);
            }
        }

        Build = GetNode<UIActionButton>("BuildBg/Build");
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

    public void RefreshGroup(List<PlanetData> planets, bool showButtons)
    { 
        _Planets.Clear();
        _Planets.AddRange(planets);

        for (int idx = 0; idx < Districts.Count; idx++)
        {
            Districts[idx].Visible = false;
        }

        while (Districts.Count < 7)
        {
            UIDistrictsGroupDistrict newItem = Districts[0].Duplicate(7) as UIDistrictsGroupDistrict;
            Districts[0].GetParent().AddChild(newItem);
            Districts.Add(newItem);
        }

        int districtIdx = 0;
        for (int planetIdx = 0; planetIdx < _Planets.Count; planetIdx++)
        {
            ColonyData colony = _Planets[planetIdx].Colony;
            if (colony != null)
            {
                for (int idx = colony.Districts.Count - 1; idx >= 0; idx--)
                {
                    Districts[districtIdx].Visible = true;
                    Districts[districtIdx].Refresh(colony.Districts[idx], false);
                    districtIdx++;
                }
            }
            else if (_Planets[planetIdx].IsHabitable())
            {
                Districts[districtIdx].Visible = true;
                Districts[districtIdx].Refresh(_Planets[planetIdx]);
                districtIdx++;
            }
            else
            {
                Districts[districtIdx].Visible = true;
                Districts[districtIdx].Refresh(_Planets[planetIdx]);
                districtIdx++;
            }
        }

        while (districtIdx < Districts.Count)
        {
            Districts[districtIdx].Visible = false;
            districtIdx++;
        }

        if (showButtons)
        {
            Build.Visible = true;
        }
        else
        {
            Build.Visible = false;
        }
    }

    public void RefreshPlanet(PlanetData planet)
    {
        _Planets.Clear();
        _Planets.Add(planet);

        for (int idx = 0; idx < Districts.Count; idx++)
        {
            Districts[idx].Visible = false;
        }

        while (Districts.Count < 7)
        {
            UIDistrictsGroupDistrict newItem = Districts[0].Duplicate(7) as UIDistrictsGroupDistrict;
            Districts[0].GetParent().AddChild(newItem);
            Districts.Add(newItem);
        }

        int districtIdx = 0;
        for (int planetIdx = 0; planetIdx < _Planets.Count; planetIdx++)
        {
            ColonyData colony = _Planets[planetIdx].Colony;
            if (colony != null)
            {
                for (int idx = 0; idx < colony.Districts.Count; idx++)
                {
                    Districts[districtIdx].Visible = true;
                    Districts[districtIdx].Refresh(colony.Districts[idx], true);
                    Districts[districtIdx].RefreshPops(districtIdx % 2 == 1);
                    districtIdx++;
                }
            }
            else if (_Planets[planetIdx].IsHabitable())
            {
                Districts[districtIdx].Visible = true;
                Districts[districtIdx].Refresh(_Planets[planetIdx]);
                Districts[districtIdx].RefreshPops(districtIdx % 2 == 1);
                districtIdx++;
            }
            else
            {
                Districts[districtIdx].Visible = true;
                Districts[districtIdx].Refresh(_Planets[planetIdx]);
                Districts[districtIdx].RefreshPops(districtIdx % 2 == 1);
                districtIdx++;
            }
        }

        while (districtIdx < Districts.Count)
        {
            Districts[districtIdx].Visible = false;
            districtIdx++;
        }

        if (Deselect != null)
        {
            Deselect.Visible = true;
            DeselectOdd.Visible = districtIdx % 2 == 1;
        }

        Build.Visible = false;
    }

    public void SetAsOddRow(bool odd)
    {
        if (EvenGap != null) EvenGap.Visible = odd;
    }
}