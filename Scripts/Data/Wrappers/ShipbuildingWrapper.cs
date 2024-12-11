using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class ShipbuildingWrapper
{
    public DesignData DesignCurrent;
    public DesignData DesignLast;

    public int DesignIdx = 0;
    public int DesignIdxMax = 0;
    public int Shipbuilding = 0;
    public int ProgressCurrent = 0;
    public int ProgressNextTurn = 0;
    public int ProgressMax = 0;
    public int Turns = 0;

    public SystemData _System;

    public ShipbuildingWrapper(SystemData system)
    {
        _System = system;
    }

    public void Clear()
    {
        DesignCurrent = null;
        DesignLast = null;

        DesignIdx = 0;
        DesignIdxMax = 0;
        Shipbuilding = 0;
        ProgressCurrent = 0;
        ProgressNextTurn = 0;
        ProgressMax = 0;
        Turns = 0;
    }

    public void Refresh()
    {
        Clear();

        string currentDesign = _System.Data.GetSubValueS("ActionBuildShip/Design");
        string lastDesign = _System.Data.GetSubValueS("ActionBuildShip/LastDesign");

        DesignCurrent = _System._Player.GetDesign(currentDesign);
        DesignLast = _System._Player.GetDesign(lastDesign);

        DesignIdx = _System._Player.GetDesignIdx(currentDesign);
        DesignIdxMax = _System._Player.Designs.Count;

        for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
        {
            ColonyData colony = _System.Colonies[colonyIdx];
            for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
            {
                DistrictData district = colony.Districts[districtIdx];
                if (district.Economy_PerTurn.Resource == "Shipbuilding")
                {
                    Shipbuilding += district.Economy_PerTurn.Production;
                }
            }
        }
        ProgressMax = DesignCurrent.Cost;
        ProgressCurrent = _System.Data.GetSubValueI("ActionBuildShip/Progress");
        ProgressNextTurn = Mathf.Min(ProgressCurrent + Shipbuilding, ProgressMax); 
        if (Shipbuilding > 0) Turns = ((ProgressMax - ProgressCurrent) + (Shipbuilding - 1)) / Shipbuilding;
        else Turns = 999;
    }
    public string ToString_Shipbuilding() { return Helper.ResValueToString(Shipbuilding); }
    public string ToString_Turns() { return Turns.ToString(); }
}
