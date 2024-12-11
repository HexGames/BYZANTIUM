using Godot.Collections;

public static class DistrictRaw
{
    public static int GetGrowth(DataBlock colony, DataBlock district)
    {
        int planetDefaultGrowth = colony.GetSubValueI("DefaultPopGrowth");
        int happiness = 50;
        if (district.HasSub("Pop/GrowthProgress", 1000))
        {
            happiness = district.GetSubValueI("Pop/Happiness");
        }

        return (planetDefaultGrowth - (planetDefaultGrowth / 2)) + (planetDefaultGrowth / 2) * happiness / 50;
    }

    public static int GetPopGrowth(DataBlock district)
    {
        return district.GetSubValueI("Pop/GrowthProgress");
    }

    public static void SetPopFullGrowth(DataBlock district)
    {
        district.SetSubValueI("Pop/GrowthProgress", 1000, Game.self.Def);
    }

    public static void SetPopFullGrowth(DataBlock district, DefLibrary def)
    {
        district.SetSubValueI("Pop/GrowthProgress", 1000, def);
    }

    public static void SetPopHalfGrowth(DataBlock district, DefLibrary def)
    {
        district.SetSubValueI("Pop/GrowthProgress", 500, def);
    }
}
