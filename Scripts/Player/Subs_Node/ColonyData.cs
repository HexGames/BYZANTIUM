using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class ColonyData : Node
{
    [ExportCategory("ColonyData-Node")]
    [Export]
    public SystemData _System = null;

    [ExportCategory("ColonyRawData")]
    [Export]
    public DataBlock Data = null;

    [ExportCategory("ColonyData")]
    [Export]
    public string ColonyName = "";

    //[Export]
    //public DataBlock Type = null;
    //[Export]
    //public DataBlock Pops = null;
    //[Export]
    //public DataBlock Districts = null;
    //[Export]
    //public DataBlock Resources = null;
    //[Export]
    //public DataBlock Trades = null;

    [ExportCategory("ColonyData-Links")]
    [Export]
    public PlanetData Planet = null;

    // [--- ("ColonyData-Session") ---]
    // districts
    public List<DistrictData> Districts = new List<DistrictData>();

    // --------------------------------------------------------------------------------------------
    public void Init_DistrictData()
    {
        Districts.Clear();
        if (Data.HasSub("District_List"))
        {
            Array<DataBlock> districts = Data.GetSub("District_List").GetSubs("District");
            for (int districtIdx = 0; districtIdx < districts.Count; districtIdx++)
            {
                Districts.Add(new DistrictData(districts[districtIdx], this));
            }
        }
    }

    public void Init_Resources()
    {
        for (int districtIdx = 0; districtIdx < Districts.Count; districtIdx++)
        {
            DistrictData district = Districts[districtIdx];
            district.Economy_PerTurn = new DistrictEconomyWrapper(district);
        }
    }

    // --------------------------------------------------------------------------------------------
    private static List<int> GrowthValues = new List<int>(12);
    public int GetGrowth(out int waste/*, bool includeNextPop = false*/)
    {
        GrowthValues.Clear();
        for (int idx = 0; idx < Districts.Count; idx++)
        { 
            GrowthValues.Add(DistrictRaw.GetGrowth(Data, Districts[idx]._Data));
        }
        //if (includeNextPop)
        //{
        //    GrowthValues.Add(Data.GetSubValueI("DefaultPopGrowth"));
        //}

        GrowthValues.Sort((a, b) => (b - a));

        int popsMax = Planet.Data.GetSubValueI("PopsMax");
        int growth = 0;
        waste = 0;
        for (int idx = 0; idx < GrowthValues.Count; idx++)
        {
            growth += GrowthValues[idx];
            if (idx >= popsMax - GrowthValues.Count)
            {
                waste -= GrowthValues[idx];
            }
        }

        return growth;
    }

    public int GetGrowthProgress()
    {
        for (int idx = 0; idx < Districts.Count; idx++)
        {
            int progress = Districts[idx].Pop.Data.GetSubValueI("GrowthProgress");
            if (progress < 1000)
            {
                return progress;
            }
        }
        return 0;
    }
}
