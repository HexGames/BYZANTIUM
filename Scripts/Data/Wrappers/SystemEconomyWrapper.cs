using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

public class SystemEconomyWrapper
{
    public int Tax;
    public int BC;
    public int Influence;
    public int Research;

    SystemData _System;

    public SystemEconomyWrapper(SystemData system)
    {
        _System = system;
    }
    public void Clear()
    {
        Tax = 0;
        BC = 0;
        Influence = 0;
        Research = 0;
    }

    public void Refresh()
    {
        Clear();

        Tax = _System.Data.GetSubValueI("ActionTax/Tax");
        if (_System != null)
        {
            for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
            {
                ColonyData colony = _System.Colonies[colonyIdx];
                for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
                {
                    DistrictData district = colony.Districts[districtIdx];
                    if (district.Economy_PerTurn.Resource == "BC")
                    {
                        BC += district.Economy_PerTurn.Production;
                    }
                    else if (district.Economy_PerTurn.Resource == "Influence")
                    {
                        Influence += district.Economy_PerTurn.Production;
                    }
                    else if (district.Economy_PerTurn.Resource == "Research")
                    {
                        Research += district.Economy_PerTurn.Production;
                    }
                    BC += district.Economy_PerTurn.TaxBC;
                }
            }
        }
    }

    public string ToString_BC() { return Helper.ResValueToString(BC); }
    public string ToString_Influence() { return Helper.ResValueToString(Influence); }
    public string ToString_Research() { return Helper.ResValueToString(Research); }
}
