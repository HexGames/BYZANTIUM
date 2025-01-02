using Godot.Collections;

public static class PlanetRaw
{
    public static int GetPopGrowth(DataBlock planet, DefLibrary def)
    {
        int popGrowth = 0;
        Array<DataBlock> features = planet.GetSub("Features").GetSubs();
        for (int idx = 0; idx < features.Count; idx++)
        {
            DataBlock feature = features[idx];
            int value = def.GetFeature(feature.Name).GetSubValueI("Benefit", "Extra", "PopGrowth");
            if (value > 0)
            {
                popGrowth = value;
            }
        }
        return popGrowth;
    }
}
