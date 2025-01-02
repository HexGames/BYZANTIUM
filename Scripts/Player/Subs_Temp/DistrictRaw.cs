using Godot.Collections;

public static class DistrictRaw
{
    public static DataBlock CreateNewDistrictAndPop(DataBlock system, DataBlock colony, bool capital, DefLibrary def)
    {
        DataBlock districts = colony.GetSub("District_List");

        DataBlock district = Data.AddData(districts, "District", capital ? "Capital_I" : "Private_Business_I", def);
        //Data.AddData(district, "Change_Cooldown", 0, def);
        //Data.AddData(district, "Control_Cooldown", 0, def);
        Data.AddData(district, "InvestLevel", 0, def);
        Data.AddData(district, "Investment", 0, def);
        //Data.AddData(district, "Factory", 0, def);
        //Data.AddData(district, "Factory_Cooldown", 0, def);

        DataBlock pop = Data.AddData(district, "Pop", def);
        Data.AddData(pop, "GrowthProgress", 0, def);
        Data.AddData(pop, "Species", "Human", def);
        Data.AddData(pop, "Ethics", "Communist", def);
        Data.AddData(pop, "Wealth", 20, def);
        Data.AddData(pop, "Happiness", 50, def);
        Data.AddData(pop, "Mood", 0, def);

        system.SetSubValueS("ActionGrowth", "FocusColony", colony.ValueS, def);

        return district;
    }

    public static int GetGrowth(DataBlock colony, DataBlock district)
    {
        int planetDefaultGrowth = colony.GetSubValueI("DefaultPopGrowth");
        int happiness = 50;
        if (district.GetSubValueI("Pop", "GrowthProgress") == 1000)
        {
            happiness = district.GetSubValueI("Pop", "Happiness");
        }

        return (planetDefaultGrowth - (planetDefaultGrowth / 2)) + (planetDefaultGrowth / 2) * happiness / 50;
    }

    public static int GetPopGrowth(DataBlock district)
    {
        return district.GetSubValueI("Pop", "GrowthProgress");
    }

    public static void SetPopFullGrowth(DataBlock district)
    {
        district.SetSubValueI("Pop", "GrowthProgress", 1000, Game.self.Def);
    }

    public static void SetPopFullGrowth(DataBlock district, DefLibrary def)
    {
        district.SetSubValueI("Pop", "GrowthProgress", 1000, def);
    }

    public static void SetPopHalfGrowth(DataBlock district, DefLibrary def)
    {
        district.SetSubValueI("Pop", "GrowthProgress", 500, def);
    }
}
