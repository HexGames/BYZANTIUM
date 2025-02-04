using Godot;
using Godot.Collections;
using System.Collections.Generic;

public static class ColonyRaw
{
    public static DataBlock CreateNewColony_Habitable(DataBlock player, DataBlock star, DataBlock planet, DataBlock system, bool systemCapital, bool empireCapital, DefLibrary def)
    {
        if (PlanetRaw.GetBaseMaxPops(planet, def) <= 0) return null;

        DataBlock empireData = def.GetEmpire(player.ValueS);

        DataBlock colonyList = system.GetSub("Colony_List");

        DataBlock colony = Data.AddData(colonyList, "Colony", planet.ValueS, def);

        //Data.AddData(colony, "DefaultPopGrowth", PlanetRaw.GetPopGrowth(planet, def), def);

        Data.AddData(colony, "PopsMax", PlanetRaw.GetBaseMaxPops(planet, def) + (empireCapital ? 5 : 0), def);
        if (empireCapital) Data.AddData(colony, "Capital", def);

        Data.AddData(colony, "District_List", def);
        DistrictRaw.CreateNewDistrict(system, colony, def.GetDistrictInfo("Agriculture_District"), 1, def);
        DistrictRaw.CreateNewDistrict(system, colony, def.GetDistrictInfo("Urban_District"), 0, def);
        DistrictRaw.CreateNewDistrict(system, colony, def.GetDistrictInfo("Industrial_District"), 0, def);

        Data.AddData(colony, "Link:Star:Planet", star.ValueS + ":" + planet.ValueS, def); // no PlanetData yet
        Data.AddData(planet, "Link:Player:System:Colony", player.ValueS + ":" + system.ValueS + ":" + colony.ValueS, def); // no ColonyData yet

        return colony;
    }

    public static DataBlock GetFirstDistrictOfType(DataBlock colony, string type, DefLibrary def)
    {
        // without district info
        //for (int defIdx = 0; defIdx < def.Districts.Count; defIdx++)
        //{
        //    if (def.Districts[defIdx].GetSubValueS("Type") == type)
        //    {
        //        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        //        for (int districtIdx = 0; districtIdx < districts.Count; districtIdx++)
        //        {
        //            if (districts[districtIdx].ValueS == def.Districts[defIdx].ValueS)
        //            {
        //                return districts[districtIdx];
        //            }
        //        }
        //    }
        //}

        // with district info
        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        for (int districtIdx = 0; districtIdx < districts.Count; districtIdx++)
        {
            if (def.GetDistrictInfo(districts[districtIdx].ValueS).Type == type)
            {
                return districts[districtIdx];
            }
        }

        return null;
    }

    public static int GetPopsTotal(DataBlock colony)
    {
        int pops = 0;
        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        for (int districtIdx = 0; districtIdx < districts.Count; districtIdx++)
        {
            pops += districts[districtIdx].GetSub("Pops_List").GetSubs("Pop").Count;
        }

        return pops;
    }

    //public static void SetPopsFullGrowth(DataBlock system, DataBlock colony, DataBlock planet)
    //{
    //    SetPopsFullGrowth(system, colony, planet, Game.self.Def);
    //}
    //
    //public static void SetPopsFullGrowth(DataBlock system, DataBlock colony, DataBlock planet, DefLibrary def)
    //{
    //    Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
    //    for (int idx = 0; idx < districts.Count; idx++)
    //    {
    //        DistrictRaw.SetPopFullGrowth(districts[idx], def);
    //    }
    //
    //    if (districts.Count < planet.GetSubValueI("PopsMax"))
    //    {
    //        DistrictRaw.CreateNewDistrictAndPop(system, colony, false, def);
    //    }
    //}

    private static List<int> GrowthValues = new List<int>(12);
    public static int GetGrowth(DataBlock planet, DataBlock colony, bool includeNextPop, DefLibrary def)
    {
        GrowthValues.Clear();
        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        for (int idx = 0; idx < districts.Count; idx++)
        {
            DataBlock district = districts[idx];
            GrowthValues.Add(DistrictRaw.GetGrowth(colony, district));
        }
        if (includeNextPop)
        {
            GrowthValues.Add(colony.GetSubValueI("DefaultPopGrowth"));
        }

        GrowthValues.Sort((a, b) => (b - a));

        //int popsMax = planet.GetSubValueI("PopsMax");
        //int growth = 0;
        //for (int idx = 0; idx < Mathf.Min(popsMax - GrowthValues.Count, GrowthValues.Count); idx++)
        //{
        //    growth += GrowthValues[idx];
        //}

        return 0;
    }
}
