using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{

    private void GenerateNewMapSave_Stars_Planets_Sol(DataBlock planetList)
    {
        {
            DataBlock star = Data.AddData(planetList, "Planet", "Star", DefLibrary);
            Data.AddData(star, "Star_Type", "Main_Sequence", DefLibrary);
            Data.AddData(star, "Building", "Star_Orbit", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Mercury", DefLibrary);
            Data.AddData(planet, "Type", "Barren", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Temperature", 5, DefLibrary);
            Data.AddData(planet, "Building", "Possible_Outpost", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Venus", DefLibrary);
            Data.AddData(planet, "Size", 2, DefLibrary);
            Data.AddData(planet, "Type", "Toxic", DefLibrary);
            Data.AddData(planet, "Temperature", 4, DefLibrary);
            Data.AddData(planet, "Building", "Possible_Outpost", DefLibrary);
            Data.AddData(planet, "MineralVeins", DefLibrary);
            Data.AddData(planet, "Uninhabitable", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Terra", DefLibrary);
            Data.AddData(planet, "Custom", DefLibrary);
            Data.AddData(planet, "Size", 2, DefLibrary);
            Data.AddData(planet, "Type", "Continental", DefLibrary);
            Data.AddData(planet, "Temperature", 3, DefLibrary);
            Data.AddData(planet, "Building", "Possible_Outpost", DefLibrary);
            Data.AddData(planet, "Complex_Life", DefLibrary);
            Data.AddData(planet, "Fertile", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Moon", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Barren", DefLibrary);
            Data.AddData(planet, "Temperature", 3, DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
            Data.AddData(planet, "Building", "Possible_Outpost", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Mars", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Desert", DefLibrary);
            Data.AddData(planet, "Temperature", 2, DefLibrary);
            Data.AddData(planet, "Building", "Possible_Outpost", DefLibrary);
            Data.AddData(planet, "Trapped_Gasses", DefLibrary);
            Data.AddData(planet, "Tiny_Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Asteroid_Field", DefLibrary);
            Data.AddData(planet, "Type", "Asteroid_Field", DefLibrary);
            Data.AddData(planet, "Gold_Asteroid", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Jupiter", DefLibrary);
            Data.AddData(planet, "Size", 6, DefLibrary);
            Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Building", "Stable_Orbit", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Ganymede", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Frozen", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
            Data.AddData(planet, "Building", "Possible_Outpost", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Saturn", DefLibrary);
            Data.AddData(planet, "Size", 5, DefLibrary);
            Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Building", "Possible_Outpost", DefLibrary);
            Data.AddData(planet, "Building", "Ring_Minerals", DefLibrary);
            Data.AddData(planet, "Hidden_Rings", DefLibrary);
            Data.AddData(planet, "Usefull_Gasses", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Titan", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Frozen", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
            Data.AddData(planet, "Building", "Possible_Outpost", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Outer_System", DefLibrary);
            Data.AddData(planet, "Type", "Outer_System", DefLibrary);
        }
    }

    private List<DataBlock> Planets_VeryHot = null;
    private List<DataBlock> Planets_Hot = null;
    private List<DataBlock> Planets_Temperate = null;
    private List<DataBlock> Planets_Cold = null;
    private List<DataBlock> Planets_Frozen = null;
    private List<DataBlock> Stars = null;
    private void GenerateNewMapSave_Stars_Planets_Random(DataBlock planetList)
    {
        GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists();

        DataBlock star = Data.AddData(planetList, "Planet", "Star", DefLibrary);

        DataBlock chosenStarData = Stars[RNG.RandiRange(0, Stars.Count - 1)];

        Data.AddData(star, "Star_Type", chosenStarData.ValueS, DefLibrary);
        DataBlock starFeatures = chosenStarData.GetSub("Features");
        if (starFeatures != null)
        {
            GenerateNewMapSave_Stars_Planets_Random__AddFeatures(star, starFeatures);
        }

        int minTemp = chosenStarData.GetSub("PlanetsHot") != null ? 2 : 1;
        int maxTemp = chosenStarData.GetSub("PlanetsCold") != null ? 4 : 5;
        bool gasFirst = chosenStarData.GetSub("GasGiantsFirst") != null;
        int noOfPlanes = RNG.RandiRange(chosenStarData.GetSub("Planets:Min").ValueI, chosenStarData.GetSub("Planets:Max").ValueI);
        int gasPlanets = 0;
        if (RNG.RandiRange(0, 99) < 80) gasPlanets++;
        if (RNG.RandiRange(0, 99) < 50) gasPlanets++;
        if (noOfPlanes >= 6 && RNG.RandiRange(0, 99) < 50) gasPlanets++;
        int maxPlanetSize = 3;
        int moons = 0;

        for (int n = 0; n < noOfPlanes; n++)
        {
            int currentTemp = Mathf.RoundToInt( 1.0f * maxTemp + 0.25f - 0.85f * (maxTemp - minTemp + 1) * n / ( noOfPlanes - 1 ));

            if (n == noOfPlanes - 1)
            {
                DataBlock planet = Data.AddData(planetList, "Planet", "Outer_System", DefLibrary);
                Data.AddData(planet, "Type", "Outer_System", DefLibrary);
            }
            else
            {
                if (moons == 0 && ((gasFirst == true && n < gasPlanets) || (gasFirst == false && n > noOfPlanes - 1 - gasPlanets)))
                {
                    GenerateNewMapSave_Stars_Planets_Random_Special(planetList, "Gas_Giant", currentTemp);

                    if (RNG.RandiRange(0, 99) < 15)
                    {
                        moons = 2;
                        maxPlanetSize = 2;
                    }
                    else if (RNG.RandiRange(0, 99) < 50)
                    {
                        moons = 2;
                        maxPlanetSize = 2;
                    }
                    else
                    {
                        maxPlanetSize = 3;
                    }
                }
                else if ((gasFirst == true && n == gasPlanets) || (gasFirst == false && n == noOfPlanes - 1 - gasPlanets) && RNG.RandiRange(0, 99) < 80)
                {
                    GenerateNewMapSave_Stars_Planets_Random_Special(planetList, "Asteroid_Field", currentTemp);
                    maxPlanetSize = 3;
                }
                else
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet(planetList, currentTemp, maxPlanetSize, moons > 0);
                    if (moons == 0)
                    {
                        if (RNG.RandiRange(0, 99) < 25)
                        {
                            moons = 1;
                            maxPlanetSize = 1;
                        }
                        else
                        {
                            maxPlanetSize = 3;
                        }
                    }
                    else
                    {
                        moons--;
                    }
                }
            }
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists()
    {
        if (Stars == null)
        {
            Stars = new List<DataBlock>();
            for (int idx = 0; idx < DefLibrary.Planets.Count; idx++)
            {
                if (DefLibrary.Planets[idx].Name == "Planet:Star" && DefLibrary.Planets[idx].GetSub("Weight") != null)
                {
                    for (int w = 0; w < DefLibrary.Planets[idx].GetSub("Weight").ValueI; w++)
                    {
                        Stars.Add(DefLibrary.Planets[idx]);
                    }
                }
            }
        }

        if (Planets_VeryHot == null)
        {
            Planets_VeryHot = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_VeryHot, 5);
        }

        if (Planets_Hot == null)
        {
            Planets_Hot = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_Hot, 4);
        }

        if (Planets_Temperate == null)
        {
            Planets_Temperate = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_Temperate, 3);
        }

        if (Planets_Cold == null)
        {
            Planets_Cold = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_Cold, 2);
        }

        if (Planets_Frozen == null)
        {
            Planets_Frozen = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_Frozen, 1);
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(List<DataBlock> planetsList, int temp)
    {
        for (int idx = 0; idx < DefLibrary.Planets.Count; idx++)
        {
            if (DefLibrary.Planets[idx].Name == "Planet"
                && DefLibrary.Planets[idx].GetSub("Weight") != null
                && DefLibrary.Planets[idx].GetSub("Temperature:Min") != null
                && DefLibrary.Planets[idx].GetSub("Temperature:Max") != null)
            {
                if (DefLibrary.Planets[idx].GetSub("Temperature:Min").ValueI <= temp && DefLibrary.Planets[idx].GetSub("Temperature:Max").ValueI >= temp)
                {
                    for (int w = 0; w < DefLibrary.Planets[idx].GetSub("Weight").ValueI; w++)
                    {
                        planetsList.Add(DefLibrary.Planets[idx]);
                    }
                }
            }
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random_Special(DataBlock planetList, string type, int currentTemp)
    {

        for (int idx = 0; idx < DefLibrary.Planets.Count; idx++)
        {
            if (DefLibrary.Planets[idx].Name == "Planet:" + type)
            {
                DataBlock planet = Data.AddData(planetList, "Planet", type, DefLibrary);
                Data.AddData(planet, "Type", type, DefLibrary);
                if (type == "Gas_Giant")
                {
                    int size = 5;
                    if (RNG.RandiRange(0, 99) < 50) size = 6;
                    Data.AddData(planet, "Size", size, DefLibrary);
                }

                DataBlock features = DefLibrary.Planets[idx].GetSub("Features");
                if (features != null)
                {
                    GenerateNewMapSave_Stars_Planets_Random__AddFeatures(planet, features);
                }
            }
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random_Planet(DataBlock planetList, int currentTemp, int maxSize, bool moon)
    {
        DataBlock chosenPlanetData = null;
        switch (currentTemp)
        {
            case 1: chosenPlanetData = Planets_Frozen[RNG.RandiRange(0, Planets_Frozen.Count - 1)]; break;
            case 2: chosenPlanetData = Planets_Cold[RNG.RandiRange(0, Planets_Cold.Count - 1)]; break;
            case 3: chosenPlanetData = Planets_Temperate[RNG.RandiRange(0, Planets_Temperate.Count - 1)]; break;
            case 4: chosenPlanetData = Planets_Hot[RNG.RandiRange(0, Planets_Hot.Count - 1)]; break;
            case 5: chosenPlanetData = Planets_VeryHot[RNG.RandiRange(0, Planets_VeryHot.Count - 1)]; break;
        }


        DataBlock planet = Data.AddData(planetList, "Planet", chosenPlanetData.ValueS, DefLibrary);
        Data.AddData(planet, "Type", chosenPlanetData.ValueS, DefLibrary);
        int size = 1;
        if (maxSize == 2 && RNG.RandiRange(0, 99) < 25) size++;
        else if (maxSize == 3)
        {
            if (RNG.RandiRange(0, 99) < 50) size = 2;
            else if (RNG.RandiRange(0, 99) < 50) size = 3;
        }
        if (chosenPlanetData.ValueS == "Barren") size = 1;
        Data.AddData(planet, "Size", size, DefLibrary);
        Data.AddData(planet, "Temperature", currentTemp, DefLibrary);

        if (moon) Data.AddData(planet, "Moon", DefLibrary);
        if (chosenPlanetData.GetSub("ExoticResourceFlag") != null) Data.AddData(planet, "ExoticResourceFlag", DefLibrary);

        if (currentTemp == 5) Data.AddData(planet, "High_Radiation", DefLibrary);
        if (currentTemp == 4 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "High_Radiation", DefLibrary);
        DataBlock planetFeatures = chosenPlanetData.GetSub("Features");
        if (planetFeatures != null)
        {
            GenerateNewMapSave_Stars_Planets_Random__AddFeatures(planet, planetFeatures);
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random__AddFeatures(DataBlock planet, DataBlock features)
    {
        Array<DataBlock> subs = features.GetSubs();
        for (int idx = 0; idx < subs.Count; idx++)
        {
            if (subs[idx].Name.EndsWith(":Perc") == false || RNG.RandiRange(0, 99) < subs[idx].ValueI)
            {
                DataBlock feature = subs[idx];
                while (feature.Name.StartsWith("OR"))
                {
                    feature = GenerateNewMapSave_Stars_Planets_Random__AddFeatures_ResolveOR(feature);
                }

                if (feature.Name.StartsWith("Building:"))
                {
                    Data.AddData(planet, Helper.Split_0(feature.Name), Helper.Split_1(feature.Name), DefLibrary);
                    
                    // add hidden features
                    if (feature.Name.Contains("Ring_"))
                    {
                        Data.AddData(planet, "Hidden_Rings", DefLibrary);
                    }
                }
                else
                {
                    Data.AddData(planet, Helper.Split_0(feature.Name), DefLibrary);
                }
            }
        }
    }

    private DataBlock GenerateNewMapSave_Stars_Planets_Random__AddFeatures_ResolveOR(DataBlock or)
    {
        List<DataBlock> weightList = new List<DataBlock>();
        Array<DataBlock> subs = or.GetSubs();
        for (int idx = 0; idx < subs.Count; idx++)
        {
            for (int w = 0; w < subs[idx].ValueI; w++)
            {
                weightList.Add(subs[idx]);
            }
        }
        return weightList[RNG.RandiRange(0, weightList.Count - 1)];
    }
}
