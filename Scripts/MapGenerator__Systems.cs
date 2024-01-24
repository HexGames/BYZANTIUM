using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    private DataBlock GenerateSolarSystem()
    {
        DataBlock system = new DataBlock();

        system.Type = DefLibrary.GetDBType("System", Data.BaseType.STRING);
        system.ValueS = "System_" + RNG.RandiRange(1, 99);

        DataBlock star = Data.AddData(system, "Planet", "Star", DefLibrary);
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
                DataBlock planet = Data.AddData(system, "Planet", "Outer_System", DefLibrary);
                Data.AddData(planet, "Type", "Outer_System", DefLibrary);
            }
            else
            {
                if (moons == 0 && (( gasSystem == 1 && n < gasPlanets) || (gasSystem == 2 && n > noOfPlanes - gasPlanets)))
                {
                    GenerateGasGiant(system, currentTemp, gasSystem == 2, out parentPlanetSize, out moons);

                }
                else if (n == noOfPlanes - gasPlanets && RNG.RandiRange(0, 99) < 80)
                {
                    GenerateAsteroidField(system, currentTemp);
                }
                else
                {
                    GeneratePlanet(system, currentTemp, ref parentPlanetSize, ref moons);
                }
            }
        }

        return system;
    }

    private void GenerateGasGiant(DataBlock system, int currentTemp, bool maxSize, out int parentPlanetSize, out int moons)
    {
        DataBlock planet = Data.AddData(system, "Planet", "Gas Giant", DefLibrary);
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

    private void GenerateAsteroidField(DataBlock system, int currentTemp)
    {
        DataBlock planet = Data.AddData(system, "Planet", "Asteroid_Belt", DefLibrary);
        Data.AddData(planet, "Size", RNG.RandiRange(1, 5), DefLibrary);
        Data.AddData(planet, "Type", "Asteroid_Belt", DefLibrary);
        Data.AddData(planet, "Temperature", currentTemp, DefLibrary);
    }

    private void GeneratePlanet(DataBlock system, int currentTemp, ref int parentPlanetSize, ref int moons)
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
        DataBlock planet = Data.AddData(system, "Planet", type, DefLibrary);
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
        GeneratePlanet_Atmomsphere(planet, type, size);
        GeneratePlanet_Core(planet, type, size, currentTemp);
        GeneratePlanet_WaterAndLife(planet, type, size, currentTemp);
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

    private void GeneratePlanet_Atmomsphere(DataBlock planet, string planetType, int planetSize)
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
                        GeneratePlanet_Atmomsphere_Options(planet, planetSize, "CO2", "N", "O2");
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
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "CO2", "N", "O2");
                    Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Desert":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Arid":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Temperate":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Ocean":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Swamp":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "CH4", 1, DefLibrary);
                    break;
                }
            case "Jungle":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Artic":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "O2");
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

    private void GeneratePlanet_Atmomsphere_Options(DataBlock planet, int planetSize, string firstOption, string secondOption = "", string thirdOption = "")
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

    private void GeneratePlanet_Core(DataBlock planet, string planetType, int planetSize, int temperature)
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

    private void GeneratePlanet_WaterAndLife(DataBlock planet, string planetType, int planetSize, int temperature)
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
    }

    private DataBlock GenerateSolarSystemCustom_Sol()
    {
        DataBlock system = new DataBlock();

        system.Type = DefLibrary.GetDBType("System", Data.BaseType.STRING);
        system.ValueS = "Sol";

        DataBlock star = Data.AddData(system, "Planet", "Star", DefLibrary);
        Data.AddData(star, "Star_Size", 4, DefLibrary);
        Data.AddData(star, "Star_Type", "Yellow_Dwarf", DefLibrary);

        {
            DataBlock planet = Data.AddData(system, "Planet", "Mercury", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Barren", DefLibrary);
            Data.AddData(planet, "Temperature", 5, DefLibrary);
            Data.AddData(planet, "Radiation", 2, DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Venus", DefLibrary);
            Data.AddData(planet, "Size", 4, DefLibrary);
            Data.AddData(planet, "Type", "Toxic", DefLibrary);
            Data.AddData(planet, "Temperature", 4, DefLibrary);
            Data.AddData(planet, "Atmosphere", "CO2", DefLibrary);
            Data.AddData(planet, "Toxic", DefLibrary);
            Data.AddData(planet, "Radiation", 1, DefLibrary);
            Data.AddData(planet, "Molten_Core", DefLibrary);
            Data.AddData(planet, "Magnetic_Field", DefLibrary);
            Data.AddData(planet, "Vulcanic", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Terra", DefLibrary);
            Data.AddData(planet, "Custom", DefLibrary);
            Data.AddData(planet, "Size", 4, DefLibrary);
            Data.AddData(planet, "Type", "Temperate", DefLibrary);
            Data.AddData(planet, "Temperature", 3, DefLibrary);
            Data.AddData(planet, "Atmosphere", "O2", DefLibrary);
            Data.AddData(planet, "Atmosphere", "N", DefLibrary);
            Data.AddData(planet, "Molten_Core", DefLibrary);
            Data.AddData(planet, "Magnetic_Field", DefLibrary);
            Data.AddData(planet, "Water", "Oceans", DefLibrary);
            Data.AddData(planet, "Ice", DefLibrary);
            Data.AddData(planet, "Life", "Complex_Life", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Moon", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Barren", DefLibrary);
            Data.AddData(planet, "Temperature", 3, DefLibrary);
            Data.AddData(planet, "Radiation", 1, DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Mars", DefLibrary);
            Data.AddData(planet, "Size", 3, DefLibrary);
            Data.AddData(planet, "Type", "Desert", DefLibrary);
            Data.AddData(planet, "Temperature", 2, DefLibrary);
            Data.AddData(planet, "Atmosphere", "CO2", DefLibrary);
            Data.AddData(planet, "Low_Atmosphere", DefLibrary);
            Data.AddData(planet, "Radiation", 1, DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Asteroid_Belt", DefLibrary);
            Data.AddData(planet, "Size", 3, DefLibrary);
            Data.AddData(planet, "Type", "Asteroid_Belt", DefLibrary);
            Data.AddData(planet, "Temperature", 2, DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Jupiter", DefLibrary);
            Data.AddData(planet, "Size", 9, DefLibrary);
            Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Ganymede", DefLibrary);
            Data.AddData(planet, "Size", 2, DefLibrary);
            Data.AddData(planet, "Type", "Frozen", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Forzen_Ocean", DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Saturn", DefLibrary);
            Data.AddData(planet, "Size", 8, DefLibrary);
            Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "CH4", 1, DefLibrary);
            Data.AddData(planet, "Rings", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Titan", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Frozen", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "CH4", 1, DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Outer_System", DefLibrary);
            Data.AddData(planet, "Type", "Outer_System", DefLibrary);
        }

        return system;
    }
}
