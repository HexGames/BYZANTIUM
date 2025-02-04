using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class ControlWrapper
{
    public int ControlTotal = 0;
    public int ControlDistricts = 0;
    public int ControlStateUsed = 0;

    public int RebellionLoyal = 0;
    public int RebellionUnsure = 20;
    public int RebellionWavering = 40;
    public int RebellionTurmoil = 60;
    public int RebellionRebelious = 80;
    public int RebellionMax = 100;

    public int RebellionChange = 0;
    public int RebellionCurrent = 0;

    public PlayerData LoyalTo = null;

    public SystemData _System;

    public ControlWrapper(SystemData system)
    {
        _System = system;
    }

    public void Clear()
    {
        ControlTotal = 0;
        ControlDistricts = 0;
        ControlStateUsed = 0;

        RebellionLoyal = 0;
        RebellionUnsure = 20;
        RebellionWavering = 40;
        RebellionTurmoil = 60;
        RebellionRebelious = 80;
        RebellionMax = 100;

        RebellionChange = 0;
        RebellionCurrent = 0;

        LoyalTo = _System._Player;
    }

    public void Refresh()
    {
        //Clear();
        //
        //int pops = _System.GetPopsCurrent();
        //if (_System != null)
        //{
        //    for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
        //    {
        //        ColonyData colony = _System.Colonies[colonyIdx];
        //        for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
        //        {
        //            DistrictData district = colony.Districts[districtIdx];
        //            string controlType = "";
        //            if (district._Data.HasSub("Pop"))
        //            {
        //                controlType = district.DistrictDef.Control;
        //            }
        //            bool stateOwned =  controlType == "State_Owned";
        //
        //            if (stateOwned) ControlStateUsed -= 10;
        //
        //            if (district.Economy_PerTurn.Resource == "Control")
        //            {
        //                ControlDistricts += district.Economy_PerTurn.Production_Final;
        //            }
        //        }
        //    }
        //}
        //
        //ControlTotal = ControlDistricts + ControlStateUsed;
        //
        //if (ControlTotal > 0)
        //{
        //    int skewRatio = 1000 * pops / (10 * pops + ControlTotal);
        //
        //    RebellionUnsure = Mathf.Clamp(100 - (80 * skewRatio / 100), 5, 80);
        //    RebellionWavering = Mathf.Clamp(100 - (60 * skewRatio / 100), 10, 85);
        //    RebellionTurmoil = Mathf.Clamp(100 - (40 * skewRatio / 100), 15, 90);
        //    RebellionRebelious = Mathf.Clamp(100 - (20 * skewRatio / 100), 20, 95);
        //}
        //else if (ControlTotal < 0)
        //{
        //    int skewRatio = 1000 * pops / (10 * pops - ControlTotal);
        //
        //    RebellionUnsure = Mathf.Clamp(20 * skewRatio / 100, 5, 80);
        //    RebellionWavering = Mathf.Clamp(40 * skewRatio / 100, 10, 85);
        //    RebellionTurmoil = Mathf.Clamp(60 * skewRatio / 100, 15, 90);
        //    RebellionRebelious = Mathf.Clamp(80 * skewRatio / 100, 20, 95);
        //}

        RebellionChange = 2 * _System.Pops_PerTurn.PopsUnhappy - _System.Pops_PerTurn.PopsHappy - (_System.Pops_PerTurn.PopsNeutral / 2);
        RebellionCurrent = _System.Data.GetSubValueI("ActionRebellion", "Current");
    }

    public string ToString_ControlTotal() { return Helper.ResValueToString(ControlTotal); }
    public string ToString_LoyaltyChange() { return Helper.ResValueToString(RebellionChange, 1); }
}
