
using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Per Session
public partial class DistrictData
{
    public DataBlock Data = null;

    public ColonyData _Colony = null;

    public DefDistrictWrapper DistrictDef = null;
    public List<PopData> Pops = new List<PopData>();

    public DistrictEconomyWrapper Economy_PerTurn = null;
    public List<DistrictNew> ActionsChangeDistricts_OnRefresh = new List<DistrictNew>();

    public DistrictData(DataBlock districtData, ColonyData colony)
    {
        _Colony = colony;

        Data = districtData;

        //if (districtData.HasSub("Pop"))
        //{
        //    Pop = new PopData(this);
        //}

        if (districtData.ValueS != "")
        {
            DistrictDef = Game.self.Def.GetDistrictInfo(districtData.ValueS);
        }
    }

    // --------------------------------------------------------------------------------------------
    public void Init_PopsData()
    {
        Pops.Clear();
        if (Data.HasSub("Pops_List"))
        {
            Array<DataBlock> pops = Data.GetSub("Pops_List").GetSubs("Pop");
            for (int popsIdx = 0; popsIdx < pops.Count; popsIdx++)
            {
                Pops.Add(new PopData(pops[popsIdx], this));
            }
        }
    }

    // --------------------------------------------------------------------------------------------
    public bool IsStateOwned()
    {
        return Data.GetSubValueS("Control") == "State_Owned";
    }

    public bool IsPrivate()
    {
        return Data.GetSubValueS("Control") == "Private";
    }

    //public PopData GetPop()
    //{
    //    return Pop;
    //}
    //
    //public bool HasFullPop()
    //{
    //    return Pop != null && Pop.GetProgress() == 1000;
    //}

    public int GetReinvestProgress()
    {
        return Data.GetSubValueI("Investment");
    }

    public int GetLevel()
    {
        return Data.GetSubValueI("Level");
    }
}
