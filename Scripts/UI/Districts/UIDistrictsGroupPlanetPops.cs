using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDistrictsGroupPlanetPops : Control
{
    // is beeing duplicated
    public Container PopsParent;
    public Array<UIDistrictsGroupPlanetPopsItem> Pops = new Array<UIDistrictsGroupPlanetPopsItem>();

    // runtime
    public DistrictData _District = null;

    public override void _Ready()
    {
        Pops.Clear();
        for (int idx = 0; idx < GetChildCount(); idx++)
        {
            Node node = GetChild(idx);
            if (node is UIDistrictsGroupPlanetPopsItem)
            {
                Pops.Add(node as UIDistrictsGroupPlanetPopsItem);
            }
        }
    }

    public void Refresh(DistrictData district)
    {
        _District = district;

        while (Pops.Count < 1)
        {
            UIDistrictsGroupPlanetPopsItem newItem = Pops[0].Duplicate(7) as UIDistrictsGroupPlanetPopsItem;
            Pops[0].GetParent().AddChild(newItem);
            Pops.Add(newItem);
        }

        for (int idx = 0; idx < Pops.Count; idx++)
        {
            if (idx < 1)
            {
                Pops[idx].Visible = true;
                //Pops[idx].Refresh(_District.Pop);
            }
            else
            {
                Pops[idx].Visible = false;
            }
        }
    }
}