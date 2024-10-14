using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class PopsWrapper
{
    public class PopsInfo
    {
        public List<DataBlock> DataBlocks = new List<DataBlock>();

        public int Pops = 0;
        public string Specie = "";
        public string Ethic = "";
        public string LoyalTo = "";
    }

    public SystemData _System = null;
    public ColonyData _Colony = null;

    public List<PopsInfo> Pops = new List<PopsInfo>();

    public int PopsMax = 0;
    public int Growth = 0;
    public int GrowthBonus = 0;
    public int GrowthPenalty = 0;

    public PopsWrapper(SystemData system)
    {
        _System = system;
        _Colony = null;

        //Refresh();
    }

    public PopsWrapper(ColonyData colony)
    {
        _System = null;
        _Colony = colony;

        //Refresh();
    }

    public void Clear()
    {
        Pops.Clear();

        PopsMax = 0;
        Growth = 0;
        GrowthBonus = 0;
        GrowthPenalty = 0;
    }

    public void Refresh()
    {
        Clear();

        if (_Colony != null)
        {
            if (_Colony.Pops != null)
            {
                Array<DataBlock> popsDataSubs = _Colony.Pops.GetSubs();
                for (int idx = 0; idx < popsDataSubs.Count; idx++)
                {
                    PopsInfo newPops = new PopsInfo();
                    newPops.DataBlocks.Add(popsDataSubs[idx]);
                    newPops.Pops = popsDataSubs[idx].GetSub("Pops").ValueI;
                    newPops.Specie = popsDataSubs[idx].GetSub("Specie").ValueS;
                    newPops.Ethic = popsDataSubs[idx].GetSub("Ethic").ValueS;
                    newPops.LoyalTo = popsDataSubs[idx].GetSub("LoyalTo").ValueS;
                    Pops.Add(newPops);
                }

                PopsMax = _Colony.Planet.Data.GetSub("Size").ValueI * 30 * 1000;
            }
        }
        else if (_System != null) 
        {
            for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
            {
                if (_System.Colonies[colonyIdx].Pops != null)
                {
                    Array<DataBlock> popsDataSubs = _System.Colonies[colonyIdx].Pops.GetSubs();
                    for (int subsIdx = 0; subsIdx < popsDataSubs.Count; subsIdx++)
                    {
                        int pops = popsDataSubs[subsIdx].GetSub("Pops").ValueI;
                        string specie = popsDataSubs[subsIdx].GetSub("Specie").ValueS;
                        string ethic = popsDataSubs[subsIdx].GetSub("Specie").ValueS;
                        string loyalTo = popsDataSubs[subsIdx].GetSub("LoyalTo").ValueS;

                        bool found = false;
                        for (int popsIdx = 0; popsIdx < Pops.Count; popsIdx++)
                        {
                            if (Pops[popsIdx].Specie == specie && Pops[popsIdx].Ethic == ethic && Pops[popsIdx].LoyalTo == loyalTo)
                            {
                                Pops[popsIdx].DataBlocks.Add(popsDataSubs[subsIdx]);
                                Pops[popsIdx].Pops += pops;
                                found = true;
                                break;
                            }
                        }

                        if (found == false)
                        {
                            PopsInfo newPops = new PopsInfo();
                            newPops.DataBlocks.Add(popsDataSubs[subsIdx]);
                            newPops.Pops = pops;
                            newPops.Specie = specie;
                            newPops.Ethic = ethic;
                            newPops.LoyalTo = loyalTo;
                            Pops.Add(newPops);
                        }
                    }

                    PopsMax += _System.Colonies[colonyIdx].Planet.Data.GetSub("Size").ValueI * 30 * 1000;
                }
            }
        }
    }

    public int GetPops()
    {
        int pops = 0;

        for (int idx = 0; idx < Pops.Count; idx++)
        {
            pops += Pops[idx].Pops;
        }
        
        return pops;
    }

    public string ToString_Pops() { return Helper.ResValueToString(GetPops(), 1000); }
    public string ToString_PopsMax() { return Helper.ResValueToString(PopsMax, 1000); }
}
