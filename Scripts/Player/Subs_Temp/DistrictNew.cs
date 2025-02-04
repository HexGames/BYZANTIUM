
using Godot;
using System.Collections.Generic;

// Per selection
public partial class DistrictNew
{
    public ColonyData _Colony = null;

    public DefDistrictWrapper DistrictDef = null;
    //public PopData Pop = null;

    public DistrictEconomyWrapper Economy = null;

    public int Cost_BC = 0;
    public int Cost_Inf = 0;
    public int Cost_Happiness = 0;
    public int Cost_Time = 0;
    public bool StopWorking = false; 

    public DistrictNew(DefDistrictWrapper districtDef/*, PopData pop*/, ColonyData colony)
    {
        DistrictDef = districtDef;
        //Pop = pop;
        _Colony = colony;

        Economy = new DistrictEconomyWrapper(this);

        Economy.RefreshBase_1_1();
        //for (int otherColonyIdx = 0; otherColonyIdx < _Colony._System.Colonies.Count; otherColonyIdx++)
        //{
        //    ColonyData otherColony = _Colony._System.Colonies[otherColonyIdx];
        //
        //    for (int otherDistrictIdx = 0; otherDistrictIdx < otherColony.Districts.Count; otherDistrictIdx++)
        //    {
        //        DistrictData otherDistrict = otherColony.Districts[otherDistrictIdx];
        //        if (otherDistrict.Economy_PerTurn.Resource == Economy.Resource)
        //        {
        //            Economy.AddBonusFromSystem_1_2(otherDistrict.Economy_PerTurn.Production_BonusToSystem);
        //        }
        //    }
        //}
        Economy.RefreshFinal_1_3();
    }

    public void SetAsUpgrade()
    {
        Cost_BC = DistrictDef.Cost_BC;
        Cost_Inf = 0;
        Cost_Happiness = 0;
        Cost_Time = 1;
        StopWorking = false;
    }

    public void SetAsPrivatize()
    {
        Cost_BC = -3 * DistrictDef.Cost_BC / 5;
        Cost_Inf = 0;
        Cost_Happiness = 0;
        Cost_Time = 2;
        StopWorking = true;
    }

    public void SetAsNationalizeBC()
    {
        Cost_BC = 3 * DistrictDef.Cost_BC / 2;
        Cost_Inf = 0;
        Cost_Happiness = 0;
        Cost_Time = 2;
        StopWorking = true;
    }

    public void SetAsNationalizeInf()
    {
        Cost_BC = 0;
        Cost_Inf = DistrictDef.Cost_BC;
        Cost_Happiness = 0;
        Cost_Time = 2;
        StopWorking = true;
    }

    public void SetAsNewDistrict(DefDistrictWrapper oldDistrict)
    {
        Cost_BC = 0;
        Cost_Inf = 0;
        Cost_Happiness = 0;
        Cost_Time = 0;
        StopWorking = true;
        //if (oldDistrict.Control_Type == "State_Owned")
        //{
        //    Cost_Inf = -3 * DistrictDef.Cost_BC / 5;
        //}
        //if (DistrictDef.Control_Type == "State_Owned")
        //{
        //    Cost_Inf = 3 * DistrictDef.Cost_BC / 5;
        //}
    }

    public void SetAsChangeDistrict()
    {
        Cost_BC = DistrictDef.Cost_BC;
        Cost_Inf = 0;
        Cost_Happiness = 0;
        Cost_Time = Mathf.Max(1, 1);
        StopWorking = true;
    }

    //public bool IsStateOwned()
    //{
    //    return DistrictDef.Control_Type == "State_Owned";
    //}
    //
    //public bool IsPrivate()
    //{
    //    return DistrictDef.Control_Type == "Private";
    //}

    //public PopData GetPop()
    //{
    //    return Pop;
    //}
    //
    //public bool HasFullPop()
    //{
    //    return Pop != null && Pop.GetProgress() == 1000;
    //}
}
