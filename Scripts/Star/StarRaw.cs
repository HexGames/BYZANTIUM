using Godot;
using Godot.Collections;
using System.Collections.Generic;

public static class StarRaw
{
    public static DataBlock GetBestHabitablePlanet(DataBlock star)
    {
        Array<DataBlock> planetList = star.GetSub("Planet_List").GetSubs();
        DataBlock planet = null;
        int maxHabitability = 0;
        for (int idx = 0; idx < planetList.Count; idx++)
        {
            if (planetList[idx].GetSubValueI("PopsMax") > 0)
            {
                int factor = GetHabitalibityFactor(planetList[idx]);
                if (planet == null || maxHabitability < factor)
                {
                    planet = planetList[idx];
                    maxHabitability = factor;
                }
            }
        }
        return planet;
    }

    public static int GetHabitalibityFactor(DataBlock planet)
    {
        int factor = 0;

        if (planet.GetSubValueI("PopsMax") > 0)
        {
            int maxSize = planet.GetSubValueI("Size");
            if (planet.GetSub("Features").HasSub("Desert")) factor = 10 + maxSize;
            if (planet.GetSub("Features").HasSub("Arid")) factor = 20 + maxSize;
            if (planet.GetSub("Features").HasSub("Wet")) factor = 30 + maxSize;
            if (planet.GetSub("Features").HasSub("Fertile")) factor = 40 + maxSize;
            if (planet.GetSub("Features").HasSub("Gaia")) factor = 50 + maxSize;
        }

        return factor;
    }

    public static int GetHabitalibitySize(DataBlock star)
    {
        int size = 0;

        Array<DataBlock> planetList = star.GetSub("Planet_List").GetSubs(); 
        for (int idx = 0; idx < planetList.Count; idx++)
        {
            if (planetList[idx].GetSubValueI("PopsMax") > 0)
            {
                size += planetList[idx].GetSubValueI("Size");
            }
        }

        return size;
    }

    public static int GetStarPopsMax(DataBlock star, DefLibrary def)
    {
        int pops = 0;
        Array<DataBlock> planets = star.GetSub("Planet_List").GetSubs();
        for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
        {
            DataBlock planet = planets[planetIdx];
            pops += planet.GetSubValueI("PopsMax");
        }

        return pops;
    }
}
