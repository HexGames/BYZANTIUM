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
    public int Construction;
    public int ConstructionPenalty;

    public int WealthLevel;
    public int WealthPerTurn;
    public int WealthProgress;
    public int WealthMax;

    public int Inequality;

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
        Construction = 0;
        ConstructionPenalty = 0;

        WealthLevel = 0;
        WealthPerTurn = 0;
        WealthProgress = 0;
        WealthMax = 0;

        Inequality = 0;
    }

    public void Refresh_1()
    {
        Clear();

        if (_System != null)
        {
            Tax = _System.Data.GetSubValueI("ActionTax", "Tax");
            WealthLevel = _System.Data.GetSubValueI("Wealth", "Level");
            WealthProgress = _System.Data.GetSubValueI("Wealth", "Current");
            WealthMax = WealthLevel * (WealthLevel + 1) * 500;
        }

        Inequality = CalculateInequality();
    }

    public void Refresh_4()
    {
        for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
        {
            ColonyData colony = _System.Colonies[colonyIdx];
            for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
            {
                DistrictData district = colony.Districts[districtIdx];
                if (district.Economy_PerTurn.Resource == "BC")
                {
                    BC += district.Economy_PerTurn.Production_Final;
                }
                else if (district.Economy_PerTurn.Resource == "Influence")
                {
                    Influence += district.Economy_PerTurn.Production_Final;
                }
                else if (district.Economy_PerTurn.Resource == "Research")
                {
                    Research += district.Economy_PerTurn.Production_Final;
                }
                Construction += district.DistrictDef.Benefit.Construction;
            }
        }
    }

    private int CalculateInequality()
    {
        List<int> popsWealth = new List<int>(); 
        for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
        {
            for (int districtIdx = 0; districtIdx < _System.Colonies[colonyIdx].Districts.Count; districtIdx++)
            {
                if (_System.Colonies[colonyIdx].Districts[districtIdx].Pop.Data.GetSubValueI("Pop", "GrowthProgress") == 1000)
                {
                    popsWealth.Add(_System.Colonies[colonyIdx].Districts[districtIdx].Pop.Data.GetSubValueI("Wealth"));
                }
            }
        }

        if (popsWealth.Count <= 1)
            return 0;

        popsWealth.Sort((a, b) => (b - a));

        int wSum = 0;
        int sum = 0;
        for (int idx = 0; idx < popsWealth.Count; idx++)
        {
            wSum += (idx + 1) * popsWealth[idx];
            sum += popsWealth[idx];
        }

        int gini = 1000 - 2 * (1000 * popsWealth.Count - 1000 * wSum / sum) / (popsWealth.Count - 1);

        return (gini + 5) / 10;
    }

    public string ToString_BC() { return Helper.ResValueToString(BC); }
    public string ToString_Influence() { return Helper.ResValueToString(Influence); }
    public string ToString_Research() { return Helper.ResValueToString(Research); }
}
