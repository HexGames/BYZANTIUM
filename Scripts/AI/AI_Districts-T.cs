
using Godot;
using System.Collections.Generic;

public partial class AI_District
{
    /*private static RandomNumberGenerator RNG = new RandomNumberGenerator();
    public static DataBlock SuggestDistrictForPlanet(DataBlock planet, DefLibrary lib)
    {
        List<DataBlock> possibleDistricts = new List<DataBlock>();
        if (planet.HasSub("Habitable"))
        {
            for (int idx = 0; idx < lib.Districts.Count; idx++)
            {
                if (lib.Districts[idx].GetSub("Type").ValueS == "District")
                {
                    possibleDistricts.Add(lib.Districts[idx]);
                }
            }

            return possibleDistricts[RNG.RandiRange(0, possibleDistricts.Count - 1)];
        }
        else
        {
            string firstSuggestedFocus = "";
            DataBlock planetFeatures = planet.GetSub("Features");
            for (int idx = 0; idx < planetFeatures.Subs.Count; idx++)
            {
                if (lib.GetFeature(planetFeatures.Subs[idx].Name).GetSub("SuggestedFocus").ValueS != "none")
                {
                    firstSuggestedFocus = lib.GetFeature(planetFeatures.Subs[idx].Name).GetSub("SuggestedFocus").ValueS;
                    break;
                }
            }
            if (firstSuggestedFocus != "")
            {
                for (int idx = 0; idx < lib.Districts.Count; idx++)
                {
                    if (lib.Districts[idx].GetSub("Type").ValueS == planet.GetSub("SlotType").ValueS && lib.Districts[idx].GetSub("SuggestedFocus").ValueS == firstSuggestedFocus)
                    {
                        possibleDistricts.Add(lib.Districts[idx]);
                    }
                }
                if (possibleDistricts.Count > 0)
                    return possibleDistricts[RNG.RandiRange(0, possibleDistricts.Count - 1)];
            }

            for (int idx = 0; idx < lib.Districts.Count; idx++)
            {
                if (lib.Districts[idx].GetSub("Type").ValueS == planet.GetSub("SlotType").ValueS)
                {
                    possibleDistricts.Add(lib.Districts[idx]);
                }
            }

            return possibleDistricts[RNG.RandiRange(0, possibleDistricts.Count - 1)];
        }
    }

    public static DataBlock SuggestBuildingForPlanet(DataBlock planet, DefLibrary lib)
    {
        List<DataBlock> possibleDistricts = new List<DataBlock>();
        if (planet.HasSub("Habitable"))
        {
            for (int idx = 0; idx < lib.Districts.Count; idx++)
            {
                if (lib.Districts[idx].GetSub("Type").ValueS == "District")
                {
                    possibleDistricts.Add(lib.Districts[idx]);
                }
            }

            return possibleDistricts[RNG.RandiRange(0, possibleDistricts.Count - 1)];
        }
        else
        {
            string firstSuggestedFocus = "";
            DataBlock planetFeatures = planet.GetSub("Features");
            for (int idx = 0; idx < planetFeatures.Subs.Count; idx++)
            {
                if (lib.GetFeature(planetFeatures.Subs[idx].Name).GetSub("SuggestedFocus").ValueS != "none")
                {
                    firstSuggestedFocus = lib.GetFeature(planetFeatures.Subs[idx].Name).GetSub("SuggestedFocus").ValueS;
                    break;
                }
            }
            //if (firstSuggestedFocus != "")
            //{
            //    for (int idx = 0; idx < lib.Districts.Count; idx++)
            //    {
            //        if (lib.Districts[idx].GetSub("Type").ValueS == planet.GetSub("SlotType").ValueS && lib.Districts[idx].GetSub("SuggestedFocus").ValueS == firstSuggestedFocus)
            //        {
            //            possibleDistricts.Add(lib.Districts[idx]);
            //        }
            //    }
            //    if (possibleDistricts.Count > 0)
            //        return possibleDistricts[RNG.RandiRange(0, possibleDistricts.Count - 1)];
            //}

            for (int idx = 0; idx < lib.Districts.Count; idx++)
            {
                if (lib.Districts[idx].GetSub("Type").ValueS == planet.GetSub("SlotType").ValueS)
                {
                    possibleDistricts.Add(lib.Districts[idx]);
                }
            }

            return possibleDistricts[RNG.RandiRange(0, possibleDistricts.Count - 1)];
        }
    }*/
}
