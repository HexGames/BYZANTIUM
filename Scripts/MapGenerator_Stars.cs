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
            Data.AddData(star, "Active", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Mercury", DefLibrary);
            Data.AddData(planet, "Type", "Barren", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Temperature", 5, DefLibrary);
            Data.AddData(planet, "High_Radiation", DefLibrary);
            Data.AddData(planet, "Low_Atmosphere", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Venus", DefLibrary);
            Data.AddData(planet, "Size", 2, DefLibrary);
            Data.AddData(planet, "Type", "Toxic", DefLibrary);
            Data.AddData(planet, "Temperature", 4, DefLibrary);
            Data.AddData(planet, "Big_Mineral_Veins", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Terra", DefLibrary);
            Data.AddData(planet, "Custom", DefLibrary);
            Data.AddData(planet, "Size", 2, DefLibrary);
            Data.AddData(planet, "Type", "Continental", DefLibrary);
            Data.AddData(planet, "Temperature", 3, DefLibrary);
            Data.AddData(planet, "Big_Oceans", DefLibrary);
            Data.AddData(planet, "Fertile", DefLibrary);
            Data.AddData(planet, "Complex_Life", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Moon", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Barren", DefLibrary);
            Data.AddData(planet, "Temperature", 3, DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Mars", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Desert", DefLibrary);
            Data.AddData(planet, "Temperature", 2, DefLibrary);
            Data.AddData(planet, "Low_Atmosphere", DefLibrary);
            Data.AddData(planet, "Rich_Mineral_Veins", DefLibrary);
            Data.AddData(planet, "Tiny_Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Asteroid_Field", DefLibrary);
            Data.AddData(planet, "Type", "Asteroid_Field", DefLibrary);
            Data.AddData(planet, "RichMinerals", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Jupiter", DefLibrary);
            Data.AddData(planet, "Size", 6, DefLibrary);
            Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Ganymede", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Frozen", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Saturn", DefLibrary);
            Data.AddData(planet, "Size", 5, DefLibrary);
            Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Deuterium", DefLibrary);
            Data.AddData(planet, "Rings", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(planetList, "Planet", "Titan", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Frozen", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
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

                Data.AddData(planet, Helper.Split_0(feature.Name), DefLibrary);
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

    /*private void GenerateNewMapSave_Stars_Planets_Random(DataBlock planetList)
    {
        DataBlock star = Data.AddData(planetList, "Planet", "Star", DefLibrary);
        int starSize = RNG.RandiRange(1, 7);
        Data.AddData(star, "Star_Size", starSize, DefLibrary);
        Data.AddData(star, "Star_Type", "Yellow_Dwarf", DefLibrary);


        int noOfPlanes = RNG.RandiRange(3 * (3 - Math.Abs(4 - starSize)), 6 + 2 * (3 - Math.Abs(4 - starSize)));
        int gasSystem = RNG.RandiRange(1, 2);
        int gasPlanets = 0;
        if (RNG.RandiRange(0, 99) < 20) gasPlanets = 0;
        else if (RNG.RandiRange(0, 99) < 80) gasPlanets = 1;
        else if (RNG.RandiRange(0, 99) < 80) gasPlanets = 2;
        else if (RNG.RandiRange(0, 99) < 80) gasPlanets = 3;
        else if (RNG.RandiRange(0, 99) < 80) gasPlanets = 4;
        int currentTemp = 5;
        int parentPlanetSize = 8;
        int moons = 0;

        for (int n = 0; n < noOfPlanes; n++)
        {
            if (starSize < 3)
            {
                while (RNG.RandiRange(0, 99) < 66) currentTemp--;
            }
            else if (starSize < 6)
            {
                if (n > 0 && RNG.RandiRange(0, 99) < 66) currentTemp--;
            }
            else
            {
                if (n > 0 && RNG.RandiRange(0, 99) < 50) currentTemp--;
            }

            if (currentTemp < 1) currentTemp = 1;

            if (n == noOfPlanes - 1)
            {
                DataBlock planet = Data.AddData(planetList, "Planet", "Outer_System", DefLibrary);
                Data.AddData(planet, "Type", "Outer_System", DefLibrary);
            }
            else
            {
                if (moons == 0 && ((gasSystem == 1 && n < gasPlanets) || (gasSystem == 2 && n > noOfPlanes - gasPlanets)))
                {
                    GenerateNewMapSave_Stars_Planets_Random_GasGiant(planetList, currentTemp, gasSystem == 2, out parentPlanetSize, out moons);

                }
                else if (n == noOfPlanes - gasPlanets && RNG.RandiRange(0, 99) < 80)
                {
                    GenerateNewMapSave_Stars_Planets_Random_AsteroidField(planetList, currentTemp);
                }
                else
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet(planetList, currentTemp, ref parentPlanetSize, ref moons);
                }
            }
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random_GasGiant(DataBlock planetList, int currentTemp, bool maxSize, out int parentPlanetSize, out int moons)
    {
        DataBlock planet = Data.AddData(planetList, "Planet", "Gas Giant", DefLibrary);
        parentPlanetSize = RNG.RandiRange(7, 8 + (maxSize ? 1 : 0));
        Data.AddData(planet, "Size", parentPlanetSize, DefLibrary);
        Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
        Data.AddData(planet, "Temperature", currentTemp, DefLibrary);
        if (RNG.RandiRange(0, 99) < 40) Data.AddData(planet, "CH4", 1, DefLibrary);
        if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "He3", 1, DefLibrary);
        if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Rings", DefLibrary);
        if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Magnetic_Field", DefLibrary);

        if (RNG.RandiRange(0, 99) < 5) moons = 3;
        else if (RNG.RandiRange(0, 99) < 15) moons = 2;
        else if (RNG.RandiRange(0, 99) < 50) moons = 1;
        else moons = 0;
    }

    private void GenerateNewMapSave_Stars_Planets_Random_AsteroidField(DataBlock planetList, int currentTemp)
    {
        DataBlock planet = Data.AddData(planetList, "Planet", "Asteroid_Belt", DefLibrary);
        Data.AddData(planet, "Size", RNG.RandiRange(1, 5), DefLibrary);
        Data.AddData(planet, "Type", "Asteroid_Belt", DefLibrary);
        Data.AddData(planet, "Temperature", currentTemp, DefLibrary);
    }

    private void GenerateNewMapSave_Stars_Planets_Random_Planet(DataBlock planetList, int currentTemp, ref int parentPlanetSize, ref int moons)
    {
        List<string> possibleTypes = new List<string>();

        switch (currentTemp)
        {
            case 1:
                {
                    possibleTypes.Add("Barren");
                    possibleTypes.Add("Frozen");
                    break;
                }
            case 2:
                {
                    possibleTypes.Add("Barren");
                    possibleTypes.Add("Toxic");
                    possibleTypes.Add("Desert");
                    possibleTypes.Add("Arid");
                    possibleTypes.Add("Temperate");
                    possibleTypes.Add("Ocean");
                    possibleTypes.Add("Swamp");
                    possibleTypes.Add("Artic");
                    possibleTypes.Add("Frozen");
                    break;
                }
            case 3:
                {
                    possibleTypes.Add("Vulcanic");
                    possibleTypes.Add("Barren");
                    possibleTypes.Add("Toxic");
                    possibleTypes.Add("Desert");
                    possibleTypes.Add("Arid");
                    possibleTypes.Add("Temperate");
                    possibleTypes.Add("Ocean");
                    possibleTypes.Add("Swamp");
                    possibleTypes.Add("Jungle");
                    possibleTypes.Add("Artic");
                    break;
                }
            case 4:
                {
                    possibleTypes.Add("Lava");
                    possibleTypes.Add("Vulcanic");
                    possibleTypes.Add("Barren");
                    possibleTypes.Add("Toxic");
                    possibleTypes.Add("Desert");
                    possibleTypes.Add("Arid");
                    possibleTypes.Add("Temperate");
                    possibleTypes.Add("Ocean");
                    possibleTypes.Add("Swamp");
                    possibleTypes.Add("Jungle");
                    break;
                }
            case 5:
                {
                    possibleTypes.Add("Lava");
                    possibleTypes.Add("Vulcanic");
                    possibleTypes.Add("Barren");
                    break;
                }
        }

        string type = possibleTypes[RNG.RandiRange(0, possibleTypes.Count - 1)];
        DataBlock planet = Data.AddData(planetList, "Planet", type, DefLibrary);
        int size = RNG.RandiRange(1, 5);
        if (RNG.RandiRange(0, 99) < 20 ) size++;
        if (RNG.RandiRange(0, 99) < 20 ) size++;
        if (moons > 0)
        {
            size = RNG.RandiRange(1, parentPlanetSize / 2);
        }
        else
        {
            parentPlanetSize = size;
        }
        Data.AddData(planet, "Size", size, DefLibrary);

        Data.AddData(planet, "Type", type, DefLibrary);
        Data.AddData(planet, "Temperature", currentTemp, DefLibrary);
        GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere(planet, type, size);
        GenerateNewMapSave_Stars_Planets_Random_Planet_Core(planet, type, size, currentTemp);
        GenerateNewMapSave_Stars_Planets_Random_Planet_WaterAndLife(planet, type, size, currentTemp);
        if (RNG.RandiRange(0, 99) < 5) Data.AddData(planet, "Rings", DefLibrary);

        if (moons > 0)
        {
            Data.AddData(planet, "Moon", DefLibrary);
            moons--;
        }
        else
        {
            if (RNG.RandiRange(0, 99) < 10) moons++;
            if (RNG.RandiRange(0, 99) < 10) moons++;
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere(DataBlock planet, string planetType, int planetSize)
    {
        switch (planetType)
        {
            case "Lava":
                {
                    if (RNG.RandiRange(0, 99) < 10 * (planetSize - 1))
                    {
                        Data.AddData(planet, "Atmosphere", "CO2", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 100 - 40 * (planetSize - 2)) Data.AddData(planet, "Low_Atmosphere", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 40) Data.AddData(planet, "Toxic", DefLibrary);
                    }
                    break;
                }
            case "Vulcanic":
                {
                    if (RNG.RandiRange(0, 99) < 20 * (3 - planetSize))
                    {
                        break;
                    }
                    else
                    {
                        GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "CO2", "N", "O2");
                        if (RNG.RandiRange(0, 99) < 20 ) Data.AddData(planet, "Toxic", DefLibrary);
                    }
                    break;
                }
            case "Barren":
                {
                    if (RNG.RandiRange(0, 99) < 10 * (planetSize - 1))
                    {
                        Data.AddData(planet, "Atmosphere", "N", DefLibrary);
                        Data.AddData(planet, "Low_Atmosphere", DefLibrary);
                    }
                    break;
                }
            case "Toxic":
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "CO2", "N", "O2");
                    Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Desert":
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Arid":
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Temperate":
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Ocean":
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Swamp":
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "CH4", 1, DefLibrary);
                    break;
                }
            case "Jungle":
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Artic":
                {
                    GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "CH4", 1, DefLibrary);
                    break;
                }
            case "Frozen":
                {
                    if (RNG.RandiRange(0, 99) < 10 * (planetSize - 1))
                    {
                        Data.AddData(planet, "Atmosphere", "N", DefLibrary);
                        Data.AddData(planet, "Low_Atmosphere", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "CH4", 1, DefLibrary);
                    }
                    break;
                }
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random_Planet_Atmomsphere_Options(DataBlock planet, int planetSize, string firstOption, string secondOption = "", string thirdOption = "")
    {
        if (RNG.RandiRange(0, 99) < 70 || secondOption == "")
        {
            Data.AddData(planet, "Atmosphere", firstOption, DefLibrary);
        }
        else if (RNG.RandiRange(0, 99) < 70 || thirdOption == "")
        {
            Data.AddData(planet, "Atmosphere", secondOption, DefLibrary);
        }
        else
        {
            Data.AddData(planet, "Atmosphere", thirdOption, DefLibrary);
        }
        if (RNG.RandiRange(0, 99) < 100 - 40 * (planetSize - 2)) Data.AddData(planet, "Low_Atmosphere", DefLibrary);
    }

    private void GenerateNewMapSave_Stars_Planets_Random_Planet_Core(DataBlock planet, string planetType, int planetSize, int temperature)
    {
        int iradiated = 2;
        if (temperature <= 3) iradiated = 1;

        bool molternCore = false;
        if (RNG.RandiRange(0, 99) < 10 * (planetSize - 1)) molternCore = true;
        if (planetType == "Lava" || planetType == "Vulcanic") molternCore = true;

        bool magneticField = false;
        if (molternCore && RNG.RandiRange(0, 99) < 50) magneticField = true;

        if (planetType == "Lava" || planetType == "Vulcanic" || molternCore && RNG.RandiRange(0, 99) < 20)
        {
            Data.AddData(planet, "Vulcanic", DefLibrary);
        }
        else if (molternCore)
        {
            Data.AddData(planet, "Molten_Core", DefLibrary);
            if (magneticField) Data.AddData(planet, "Magnetic_Field", DefLibrary);
            if (RNG.RandiRange(0, 99) < 20)
            {
                if (planetType == "Ocean") Data.AddData(planet, "Islands", DefLibrary);
                else Data.AddData(planet, "Alpine", DefLibrary);
            }
            iradiated--;
        }

        if (iradiated == 2) Data.AddData(planet, "Radiation", 2, DefLibrary);
        if (iradiated == 1) Data.AddData(planet, "Radiation", 1, DefLibrary);
    }

    private void GenerateNewMapSave_Stars_Planets_Random_Planet_WaterAndLife(DataBlock planet, string planetType, int planetSize, int temperature)
    {

        switch (planetType)
        {
            case "Lava":
                {
                    if (planet.GetSub("Atmosphere") != null)
                    {
                        if (RNG.RandiRange(0, 99) < 5) Data.AddData(planet, "Water", "Vapor", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 5) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    }
                    break;
                }
            case "Vulcanic":
                {
                    if (planet.GetSub("Atmosphere") != null)
                    {
                        if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Water", "Vapor", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    }
                    break;
                }
            case "Barren":
                {
                    break;
                }
            case "Toxic":
                {
                    if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Water", "Vapor", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Water", "Lakes", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Simple_life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Desert":
                {
                    Data.AddData(planet, "Water", "Vapor", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else if(RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Arid":
                {
                    Data.AddData(planet, "Water", "Lakes", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 40) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Temperate":
                {
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Water", "Lakes", DefLibrary);
                    else Data.AddData(planet, "Water", "Oceans", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Ocean":
                {
                    Data.AddData(planet, "Water", "Water_Covered", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Swamp":
                {
                    Data.AddData(planet, "Water", "Lakes", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Jungle":
                {
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Water", "Lakes", DefLibrary);
                    else Data.AddData(planet, "Water", "Oceans", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 80) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Simple_life", DefLibrary);

                    break;
                }
            case "Artic":
                {
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Water", "Lakes", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 80) Data.AddData(planet, "Water", "Oceans", DefLibrary);
                    else Data.AddData(planet, "Water", "Water-Covered", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Frozen":
                {
                    if (temperature == 1 && RNG.RandiRange(0, 99) < 10)
                    {
                        Data.AddData(planet, "Ice", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    }
                    else if (temperature == 2 && RNG.RandiRange(0, 99) < 50)
                    {
                        Data.AddData(planet, "Ice", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    }
                    break;
                }
        }

        DataBlock lifeData = planet.GetSub("Life");
        if (lifeData != null && lifeData.ValueS == "Complex_life")
        {
            if (RNG.RandiRange(0, 99) < 50)  Data.AddData(planet, "Hostile_Life", DefLibrary);
        }
    }*/
}
