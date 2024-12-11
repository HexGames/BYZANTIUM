
using Godot;
using System.Collections.Generic;

// Session || Per Turn
public partial class DistrictData
{
    public PlanetData _Planet = null;
    public ColonyData _Colony = null; // can be null for new colonies

    public PopData Pop = null;

    public DataBlock _Data = null; // slot for possible structures, null for new colonies

    public DefDistrictWrapper DistrictDef = null;

    public DistrictEconomyWrapper Economy_PerTurn = null;
    public List<DefDistrictWrapper> ActionsChangeDistrictsPossible_OnRefresh = new List<DefDistrictWrapper>();

    //public DistrictData(DefDistrictWrapper districtDef, PlanetData planet)
    //{
    //    _Planet = planet;
    //    DistrictDef = districtDef;
    //
    //    // DATACHECK
    //    if (DistrictDef == null)
    //    {
    //        GD.PrintErr("No Data for District Data");
    //    }
    //}

    //public DistrictData(DefDistrictWrapper districtDef, DataBlock districtSlot, PlanetData planet)
    //{
    //    _Planet = planet;
    //    DistrictDef = districtDef;
    //
    //    _Data = districtSlot;
    //
    //    // DATACHECK
    //    if (_Data == null || DistrictDef == null)
    //    {
    //        GD.PrintErr("No Data for District Data");
    //    }
    //}

    public DistrictData(DataBlock districtData, ColonyData colony)
    {
        _Colony = colony;
        _Planet = colony.Planet;

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

    //public bool IsFinishedDistrict()
    //{
    //    return DistrictDef != null && _Data.HasSub("InQueue") == false;
    //}

    public bool IsStateOwned()
    {
        return DistrictDef._Data.GetSubValueS("Control/Type") == "State_Owned";
    }

    public bool IsPrivate()
    {
        return DistrictDef._Data.GetSubValueS("Control/Type") == "Private";
    }

    public PopData GetPop()
    {
        return Pop;
    }

    public bool HasFullPop()
    {
        return Pop != null && Pop.GetProgress() == 1000;
    }
}
