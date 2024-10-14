using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class BuildingsWrapper
{
    public SystemData _System = null;
    public ColonyData _Colony = null;

    public int Factories = 0;
    public int FactoriesMax = 0;
    public int Bases = 0;
    public int BasesMax = 0;

    public BuildingsWrapper(SystemData system)
    {
        _System = system;
        _Colony = null;

        //Refresh();
    }

    public BuildingsWrapper(ColonyData colony)
    {
        _System = null;
        _Colony = colony;

        //Refresh();
    }

    public void Clear()
    {
        Factories = 0;
        Bases = 0;
    }

    public void Refresh()
    {
        Clear();

        if (_Colony != null)
        {
            if (_Colony.Buildings != null)
            {
                Factories = _Colony.Buildings.GetSub("Factories").ValueI;
                FactoriesMax = _Colony.Planet.Data.GetSub("Size").ValueI * 30 * 1000;
                Bases = _Colony.Buildings.GetSub("Bases").ValueI;
            }
            if (_Colony.Planet.Data.HasSub("Size"))
            {
                BasesMax = _Colony.Planet.Data.GetSub("Size").ValueI * 1000;
            }
        }
        else if (_System != null) 
        {
            for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
            {
                if (_System.Colonies[colonyIdx].Buildings != null)
                {
                    Factories += _System.Colonies[colonyIdx].Buildings.GetSub("Factories").ValueI;
                    FactoriesMax += _System.Colonies[colonyIdx].Planet.Data.GetSub("Size").ValueI * 30 * 1000;
                    Bases += _System.Colonies[colonyIdx].Buildings.GetSub("Bases").ValueI;
                }
                if (_System.Colonies[colonyIdx].Planet.Data.HasSub("Size"))
                {
                    BasesMax = _System.Colonies[colonyIdx].Planet.Data.GetSub("Size").ValueI * 1000;
                }
            }
        }
    }

    public string ToString_Factories() { return Helper.ResValueToString(Factories, 1000); }
    public string ToString_FactoriesMax() { return Helper.ResValueToString(FactoriesMax, 1000); }
    public string ToString_Bases() { return Helper.ResValueToString(Bases, 1000); }
    public string ToString_BasesMax() { return Helper.ResValueToString(BasesMax, 1000); }
}
