using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class InfrastructureWrapper
{
    public int InfrastructureUsed = 0;
    public int Infrastructure = 0;
    public int Construction = 0;
    public int ConstructionProgress = 0;
    public int ConstructionProgressNextTurn = 0;
    public int ConstructionProgressMax = 0;
    public int ConstructionTurns = 0;

    public SystemData _System;

    public InfrastructureWrapper(SystemData system)
    {
        _System = system;
    }

    public void Clear()
    {
        InfrastructureUsed = 0;
        Infrastructure = 0;
        Construction = 0;
        ConstructionProgress = 0;
        ConstructionProgressNextTurn = 0;
        ConstructionProgressMax = 0;
        ConstructionTurns = 0;
    }

    public void Refresh()
    {
        Clear();

        if (_System != null) 
        {
            for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
            {
                ColonyData colony = _System.Colonies[colonyIdx];
                for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
                {
                    DistrictData district = colony.Districts[districtIdx];
                    if (district.Pop != null)
                    {
                        InfrastructureUsed += district._Data.GetSubValueI("Factory");
                    }

                    if (district.Economy_PerTurn.Resource == "Construction")
                    {
                        Construction += district.Economy_PerTurn.Production;
                    }
                    if (district.Economy_PerTurn.ExtraConstruction > 0)
                    {
                        Construction += district.Economy_PerTurn.ExtraConstruction;
                    }
                }
            }
        }

        Infrastructure = _System.Data.GetSubValueI("ActionInfrastructure/Infrastructure", true);
        ConstructionProgress = _System.Data.GetSubValueI("ProgressConstruction", true);
        ConstructionProgressMax = 50 + 50 * Infrastructure;
        ConstructionProgressNextTurn = Mathf.Min(ConstructionProgress + Construction, ConstructionProgressMax);
        if (Construction > 0) ConstructionTurns = ((ConstructionProgressMax - ConstructionProgress) + (Construction - 1)) / Construction;
        else ConstructionTurns = 999;
    }

    public string ToString_InfrastructureUsed() { return InfrastructureUsed.ToString(); }
    public string ToString_Infrastructure() { return Infrastructure.ToString(); }
    public string ToString_Construction() { return Helper.ResValueToString(Construction); }
    public string ToString_ConstructionTurns() { return ConstructionTurns < 900 ? ConstructionTurns.ToString() : "oo"; }
}
