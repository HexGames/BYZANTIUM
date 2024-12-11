using Godot;
using Godot.Collections;
using System.Collections.Generic;

public static class ColonyRaw
{
    public static DataBlock CreateNewColony_Habitable(DataBlock player, DataBlock star, DataBlock planet, DataBlock system, bool systemCapital, bool empireCapital, DefLibrary def)
    {
        if (planet.GetSubValueI("PopsMax") <= 0) return null;

        DataBlock empireData = def.GetEmpire(player.ValueS);

        DataBlock colonyList = system.GetSub("Colony_List");

        DataBlock colony = Data.AddData(colonyList, "Colony", planet.ValueS, def);

        Data.AddData(colony, "DefaultPopGrowth", PlanetRaw.GetPopGrowth(planet, def), def);

        if (empireCapital)
        {
            planet.SetSubValueI("PopsMax", planet.GetSubValueI("PopsMax") + planet.GetSubValueI("Size"), def);
        }

        Data.AddData(colony, "District_List", def);
        CreateNewDistrictAndPop(system, colony, systemCapital, def);

        Data.AddData(colony, "Link:Star:Planet", star.ValueS + ":" + planet.ValueS, def); // no PlanetData yet
        Data.AddData(planet, "Link:Player:System:Colony", player.ValueS + ":" + system.ValueS + ":" + colony.ValueS, def); // no ColonyData yet

        return colony;
    }

    public static DataBlock CreateNewDistrictAndPop(DataBlock system, DataBlock colony, bool capital, DefLibrary def)
    {
        DataBlock districts = colony.GetSub("District_List");

        DataBlock district = Data.AddData(districts, "District", capital ? "Capital" : "Private_Business", def);
        Data.AddData(district, "Change_Cooldown", 0, def);
        Data.AddData(district, "Factory", 0, def);
        Data.AddData(district, "Factory_Cooldown", 0, def);
        Data.AddData(district, "Investment", 0, def);
        Data.AddData(district, "Control_Cooldown", 0, def);

        DataBlock pop = Data.AddData(district, "Pop", def);
        Data.AddData(pop, "GrowthProgress", 0, def);
        Data.AddData(pop, "Species", "Human", def);
        Data.AddData(pop, "Ethics", "Communist", def);
        Data.AddData(pop, "Wealth", 20, def);
        Data.AddData(pop, "Happiness", 50, def);
        Data.AddData(pop, "Mood", 0, def);

        system.SetSubValueS("ActionGrowth/FocusColony", colony.ValueS, def);

        return district;
    }

    public static void SetPopsFullGrowth(DataBlock system, DataBlock colony, DataBlock planet)
    {
        SetPopsFullGrowth(system, colony, planet, Game.self.Def);
    }

    public static void SetPopsFullGrowth(DataBlock system, DataBlock colony, DataBlock planet, DefLibrary def)
    {
        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        for (int idx = 0; idx < districts.Count; idx++)
        {
            DistrictRaw.SetPopFullGrowth(districts[idx], def);
        }

        if (districts.Count < planet.GetSubValueI("PopsMax"))
        {
            CreateNewDistrictAndPop(system, colony, false, def);
        }
    }

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

        int popsMax = planet.GetSubValueI("PopsMax");
        int growth = 0;
        for (int idx = 0; idx < Mathf.Min(popsMax - GrowthValues.Count, GrowthValues.Count); idx++)
        {
            growth += GrowthValues[idx];
        }

        return growth;
    }
}
