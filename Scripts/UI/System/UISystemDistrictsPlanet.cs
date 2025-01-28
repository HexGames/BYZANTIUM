using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISystemDistrictsPlanet : Control
{
    public UISystemDistrictsPlanetName PlanetName;
    public Array<UISystemDistrictsPlanetDistrict> Districts = new Array<UISystemDistrictsPlanetDistrict>();

    [ExportCategory("Runtime")]
    [Export]
    public PlanetData _Planet = null;
    [Export]
    public ColonyData _Colony = null;
    public override void _Ready()
    {
        PlanetName = GetNode<UISystemDistrictsPlanetName>("HPlanet");
        Districts.Clear();
        for (int idx = 0; idx < GetChildCount(); idx++)
        {
            Node node = GetChild(idx);
            if (node is UISystemDistrictsPlanetDistrict)
            {
                Districts.Add(node as UISystemDistrictsPlanetDistrict);
            }
        }
    }

    public void Refresh(PlanetData planet)
    {
        _Planet = planet;
        _Colony = _Planet.Colony;

        PlanetName.Refresh(_Planet);

        if (_Colony != null)
        {
            // grow
            while (Districts.Count < _Colony.Districts.Count)
            {
                UISystemDistrictsPlanetDistrict newItem = Districts[0].Duplicate(7) as UISystemDistrictsPlanetDistrict;
                Districts[0].GetParent().AddChild(newItem);
                Districts.Add(newItem);
            }

            for (int idx = 0; idx < Districts.Count; idx++)
            {
                if (idx < _Colony.Districts.Count)
                {
                    Districts[idx].Visible = true;
                    Districts[idx].Refresh(_Colony.Districts[idx]);
                }
                else
                {
                    Districts[idx].Visible = false;
                }
            }
        }
        else
        {
            for (int idx = 0; idx < Districts.Count; idx++)
            {
                Districts[idx].Visible = false;
            }
        }
    }
}