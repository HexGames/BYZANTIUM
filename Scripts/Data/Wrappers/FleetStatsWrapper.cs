using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class FleetStatsWrapper
{
    public int Supply;
    public int SupplyMax;

    public FleetData _Fleet = null;

    public FleetStatsWrapper(FleetData fleet)
    {
        _Fleet = fleet;

        //Refresh();
    }
    public void Clear()
    {
        Supply = 0;
        SupplyMax = 0;
    }

    public void Refresh()
    {
        Clear();

        for (int idx = 0; idx < _Fleet.Ships.Count; idx++) 
        {
            Supply += _Fleet.Ships[idx].Data.GetSub("Supply").ValueI;
            SupplyMax += _Fleet.Ships[idx].Data.GetSub("Supply*Max").ValueI;
        }
    }
}
