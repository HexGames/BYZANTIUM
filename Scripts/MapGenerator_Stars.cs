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
            Data.AddData(star, "Active_Star", DefLibrary);
            Data.AddData(star, "Building", "Star_Orbit", DefLibrary);

            GenerateNewMapSave_Stars_Planets_AddResourcesData(star);
        }
        
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Mercury");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Venus");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Terra");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Moon");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Mars");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Asteroids");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Jupiter");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Ganymede");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Saturn");
        GenerateNewMapSave_Stars_Planets_CustomPlanet(planetList, "Titan");
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

        GenerateNewMapSave_Stars_Planets_AddResourcesData(star);

        int minTemp = chosenStarData.GetSub("PlanetsHot", false) != null ? 2 : 1;
        int maxTemp = chosenStarData.GetSub("PlanetsCold", false) != null ? 4 : 5;
        bool gasFirst = chosenStarData.GetSub("GasGiantsFirst", false) != null;
        int noOfPlanes = RNG.RandiRange(chosenStarData.GetSub("Planets:Min").ValueI, chosenStarData.GetSub("Planets:Max").ValueI);
        int gasPlanets = 0;
        if (RNG.RandiRange(0, 99) < 80) gasPlanets++;
        if (RNG.RandiRange(0, 99) < 50) gasPlanets++;
        if (noOfPlanes >= 6 && RNG.RandiRange(0, 99) < 50) gasPlanets++;
        int maxPlanetSize = 3;
        int moons = 0;

        for (int n = 0; n < noOfPlanes; n++)
        {
            int currentTemp = RNG.RandiRange(1, 5);
            if (noOfPlanes > 1)
                Mathf.RoundToInt( 1.0f * maxTemp + 0.25f - 0.85f * (maxTemp - minTemp + 1) * n / ( noOfPlanes - 1 ));

            //if (n == noOfPlanes - 1)
            //{
            //    DataBlock planet = Data.AddData(planetList, "Planet", "Outer_System", DefLibrary);
            //    Data.AddData(planet, "Type", "Outer_System", DefLibrary);
            //}
            //else
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
                    GenerateNewMapSave_Stars_Planets_Random_Special(planetList, "Asteroids", currentTemp);
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

                GenerateNewMapSave_Stars_Planets_AddResourcesData(planet);
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

        //if (chosenPlanetData.GetSub("ExoticResourceFlag") != null) Data.AddData(planet, "ExoticResourceFlag", DefLibrary);

        if (currentTemp == 5 && currentTemp == 1)
        {
            Data.RemoveData(planet, "Barren", DefLibrary);
            Data.AddData(planet, "Extreme_Temps", DefLibrary);
        }
        if (currentTemp == 5 && RNG.RandiRange(0, 99) < 140 - size * 40) Data.AddData(planet, "High_Radiation", DefLibrary);
        if (currentTemp == 4 && RNG.RandiRange(0, 99) < 50 && RNG.RandiRange(0, 99) < 140 - size * 40) Data.AddData(planet, "High_Radiation", DefLibrary);
        if (size == 1 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Low_Gravity", DefLibrary);
        if (size == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "High_Gravity", DefLibrary);

        DataBlock planetFeatures = chosenPlanetData.GetSub("Features");
        if (planetFeatures != null)
        {
            GenerateNewMapSave_Stars_Planets_Random__AddFeatures(planet, planetFeatures);
        }

        if (moon) Data.AddData(planet, "Moon", DefLibrary);

        GenerateNewMapSave_Stars_Planets_AddResourcesData(planet);
    }

    private void GenerateNewMapSave_Stars_Planets_CustomPlanet(DataBlock planetList, string name)
    {
        DataBlock chosenPlanetData = DefLibrary.GetPlanetCustom(name);

        bool custom = chosenPlanetData.GetSub("Custom", false) != null;
        string type = chosenPlanetData.GetSub("Type").ValueS;
        bool hasSize = chosenPlanetData.GetSub("Size", false) != null;
        int size = 0;
        if (hasSize)
            size = chosenPlanetData.GetSub("Size").ValueI;
        bool hasTemperature = chosenPlanetData.GetSub("Temperature", false) != null;
        int temperature = 0;
        if (hasTemperature)
             temperature = chosenPlanetData.GetSub("Temperature").ValueI;
        bool moon = chosenPlanetData.GetSub("Moon", false) != null;

        DataBlock planet = Data.AddData(planetList, "Planet", name, DefLibrary);
        if (custom) Data.AddData(planet, "Custom", DefLibrary);
        Data.AddData(planet, "Type", type, DefLibrary);
        if (hasSize) Data.AddData(planet, "Size", size, DefLibrary);

        if (hasTemperature) Data.AddData(planet, "Temperature", temperature, DefLibrary);
        //if (chosenPlanetData.GetSub("ExoticResourceFlag") != null) Data.AddData(planet, "ExoticResourceFlag", DefLibrary);

        if (temperature == 5 && temperature == 1) Data.AddData(planet, "Extreme_Temps", DefLibrary);
        if (temperature == 5 && RNG.RandiRange(0, 99) < 140 - size * 40) Data.AddData(planet, "High_Radiation", DefLibrary);
        if (temperature == 4 && RNG.RandiRange(0, 99) < 50 && RNG.RandiRange(0, 99) < 140 - size * 40) Data.AddData(planet, "High_Radiation", DefLibrary);
        if (size == 1 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Low_Gravity", DefLibrary);
        if (size == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "High_Gravity", DefLibrary);

        DataBlock planetFeatures = chosenPlanetData.GetSub("Features");
        if (planetFeatures != null)
        {
            GenerateNewMapSave_Stars_Planets_Random__AddFeatures(planet, planetFeatures);
        }

        if (moon) Data.AddData(planet, "Moon", DefLibrary);

        GenerateNewMapSave_Stars_Planets_AddResourcesData(planet);
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
                    //if (feature.Name == "Rings")
                    //{
                    //    Data.AddData(planet, "Hidden_Rings", DefLibrary);
                    //}
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

    private void GenerateNewMapSave_Stars_Planets_AddResourcesData(DataBlock planet)
    {
        DataBlock resources = Data.AddData(planet, "Resources", DefLibrary);

        //Data.AddData(resources, "Energy*Income", 0, DefLibrary);
        //Data.AddData(resources, "Minerals*Income", 0, DefLibrary);
        //Data.AddData(resources, "Production*Income", 0, DefLibrary);
        //Data.AddData(resources, "Shipbuilding*Income", 0, DefLibrary);
        //
        //Data.AddData(resources, "Research*Income", 0, DefLibrary);
        //Data.AddData(resources, "Culture*Income", 0, DefLibrary);
        //Data.AddData(resources, "BC*Income", 0, DefLibrary);
        //Data.AddData(resources, "Authority*Used", 0, DefLibrary);
        //
        //Data.AddData(resources, "Pops*Growth", 0, DefLibrary);
    }
}
