
using Godot;
using System.Collections.Generic;

// Per Session
public partial class DistrictData
{
    public DataBlock _Data = null;

    public ColonyData _Colony = null;

    public DefDistrictWrapper DistrictDef = null;
    public PopData Pop = null;

    public DistrictEconomyWrapper Economy_PerTurn = null;
    public List<DistrictNew> ActionsChangeDistricts_OnRefresh = new List<DistrictNew>();
    //public List<DistrictNew> ActionsChangeControlDistricts_OnRefresh = new List<DistrictNew>();
    //public List<DistrictNew> ActionsUpgradeDistricts_OnRefresh = new List<DistrictNew>();

    public DistrictData(DataBlock districtData, ColonyData colony)
    {
        _Colony = colony;

        _Data = districtData;

        if (districtData.HasSub("Pop"))
        {
            Pop = new PopData(this);
        }

        if (districtData.ValueS != "")
        {
            DistrictDef = Game.self.Def.GetDistrictInfo(districtData.ValueS);
        }
    }

    public bool IsStateOwned()
    {
        return DistrictDef.Control_Type == "State_Owned";
    }

    public bool IsPrivate()
    {
        return DistrictDef.Control_Type == "Private";
    }

    public PopData GetPop()
    {
        return Pop;
    }

    public bool HasFullPop()
    {
        return Pop != null && Pop.GetProgress() == 1000;
    }

    public int GetReinvestProgress()
    {
        return _Data.GetSubValueI("Investment");
    }
}
