using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Transactions;
using System.Xml;

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

            //GenerateNewMapSave_Stars_Planets_AddResourcesData(star);
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
    private List<DataBlock> NonHabitable_Frozen = null;
    private List<DataBlock> NonHabitable_VeryHot = null;
    private List<DataBlock> NonHabitable_Hot = null;
    private List<DataBlock> NonHabitable_Temperate = null;
    private List<DataBlock> NonHabitable_Cold = null;
    private List<DataBlock> Stars = null;
    private enum PlanetType
    {
        NONE,
        UNINHABITABLE,
        HABITABLE,
        ASTEROIDS,
        GAS_GIANT
    };
    private class OrbitType
    {
        //public int typ
        public int Points = 0;
    };
    private class Orbit
    {
        public PlanetType Planet = PlanetType.NONE;
        public int PlanetSize = 0;
        public bool PlanetBonus = false;
        public PlanetType Moon_1 = PlanetType.NONE;
        public int Moon_1_Size = 0;
        public bool Moon_1_Bonus = false;
        public PlanetType Moon_2 = PlanetType.NONE;
        public int Moon_2_Size = 0;
        public bool Moon_2_Bonus = false;

        public int Points = 0;
    };
    private void GenerateNewMapSave_Stars_Planets_Level(string starName, DataBlock planetList, int level, bool capital, string capitalType, int temperature)
    {
        int levelPoints = 0;

        GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists();

        // Add star
        DataBlock star = Data.AddData(planetList, "Planet", "Star", DefLibrary);
        DataBlock chosenStarData = Stars[RNG.RandiRange(0, Stars.Count - 1)];
        Data.AddData(star, "Star_Type", chosenStarData.ValueS, DefLibrary);
        DataBlock starFeatures = chosenStarData.GetSub("Features");
        if (starFeatures != null)
        {
            GenerateNewMapSave_Stars_Planets__AddFeatures(star, starFeatures);
        }

        //List<Orbit> orbits = Generate_Orbits_v1(level);
        List<Orbit> orbits = Generate_Orbits_v2(level);

        // Add the actual planets
        // search for best habitable
        int mainIdx = 3;
        int mainSize = 0;
        if (capitalType.Length > 1)
        {
            for (int idx = 0; idx < orbits.Count; idx++)
            {
                if (orbits[idx].Planet == PlanetType.HABITABLE)
                {
                    if ((orbits[idx].PlanetSize > mainSize)
                        || (orbits[idx].PlanetSize == mainSize && temperature <= 2))
                    {
                        mainIdx = idx;
                    }
                }
                if (orbits[idx].Moon_2 == PlanetType.HABITABLE)
                {
                    if ((orbits[idx].Moon_2_Size > mainSize)
                        || (orbits[idx].Moon_2_Size == mainSize && temperature <= 2))
                    {
                        mainIdx = idx;
                    }
                }
                if (orbits[idx].Moon_1 == PlanetType.HABITABLE)
                {
                    if ((orbits[idx].Moon_1_Size > mainSize)
                        || (orbits[idx].Moon_1_Size == mainSize && temperature <= 2))
                    {
                        mainIdx = idx;
                    }
                }
            }
        }

        int currentTemp = 5;
        for (int idx = 0; idx < orbits.Count; idx++)
        {
            if (idx == mainIdx) currentTemp = temperature;

            // planet
            if (orbits[idx].Planet == PlanetType.UNINHABITABLE)
            {
                DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_Uninhabitable(orbits[idx].PlanetSize, currentTemp, orbits[idx].PlanetBonus);
                planet.SetValueS(starName + "_" + idx.ToString(), DefLibrary);
                planetList.Subs.Add(planet);
            }
            else if (orbits[idx].Planet == PlanetType.HABITABLE)
            {
                if (idx == mainIdx && capitalType.Length > 1)
                {
                    DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_HabitableType(orbits[idx].PlanetSize, capitalType, currentTemp, orbits[idx].PlanetBonus);
                    planet.SetValueS(starName + "_" + idx.ToString(), DefLibrary);
                    planetList.Subs.Add(planet);
                }
                else
                {
                    DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_Habitable(orbits[idx].PlanetSize, currentTemp, orbits[idx].PlanetBonus);
                    planet.SetValueS(starName + "_" + idx.ToString(), DefLibrary);
                    planetList.Subs.Add(planet);
                }
            }
            else if (orbits[idx].Planet == PlanetType.ASTEROIDS)
            {
                DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_Asteroids(orbits[idx].PlanetBonus);
                planet.SetValueS(starName + "_" + idx.ToString(), DefLibrary);
                planetList.Subs.Add(planet);
            }
            else if (orbits[idx].Planet == PlanetType.GAS_GIANT)
            {
                DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_GasGiant(orbits[idx].PlanetSize, orbits[idx].PlanetBonus);
                planet.SetValueS(starName + "_" + idx.ToString(), DefLibrary);
                planetList.Subs.Add(planet);
            }

            // moon 1
            if (orbits[idx].Moon_1 == PlanetType.UNINHABITABLE)
            {
                DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_Uninhabitable(orbits[idx].Moon_1_Size, currentTemp, orbits[idx].Moon_1_Bonus);
                Data.AddData(planet, "Moon", DefLibrary);
                planet.SetValueS(starName + "_" + idx.ToString() + "_Moon", DefLibrary);
                planetList.Subs.Add(planet);
            }
            else if (orbits[idx].Moon_1 == PlanetType.HABITABLE)
            {
                if (idx == mainIdx && capitalType.Length > 1)
                {
                    DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_HabitableType(orbits[idx].Moon_1_Size, capitalType, currentTemp, orbits[idx].Moon_1_Bonus);
                    Data.AddData(planet, "Moon", DefLibrary);
                    planet.SetValueS(starName + "_" + idx.ToString() + "_Moon", DefLibrary);
                    planetList.Subs.Add(planet);
                }
                else
                {
                    DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_Habitable(orbits[idx].Moon_1_Size, currentTemp, orbits[idx].Moon_1_Bonus);
                    Data.AddData(planet, "Moon", DefLibrary);
                    planet.SetValueS(starName + "_" + idx.ToString() + "_Moon", DefLibrary);
                    planetList.Subs.Add(planet);
                }
            }

            // moon 2
            if (orbits[idx].Moon_2 == PlanetType.UNINHABITABLE)
            {
                DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_Uninhabitable(orbits[idx].Moon_2_Size, currentTemp, orbits[idx].Moon_2_Bonus);
                Data.AddData(planet, "Moon", DefLibrary);
                planet.SetValueS(starName + "_" + idx.ToString() + "_Sec_Moon", DefLibrary);
                planetList.Subs.Add(planet);
            }
            else if (orbits[idx].Moon_2 == PlanetType.HABITABLE)
            {
                if (idx == mainIdx && capitalType.Length > 1)
                {
                    DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_HabitableType(orbits[idx].Moon_2_Size, capitalType, currentTemp, orbits[idx].Moon_2_Bonus);
                    Data.AddData(planet, "Moon", DefLibrary);
                    planet.SetValueS(starName + "_" + idx.ToString() + "_Sec_Moon", DefLibrary);
                    planetList.Subs.Add(planet);
                }
                else
                {
                    DataBlock planet = GenerateNewMapSave_Stars_Planets_Level_Habitable(orbits[idx].Moon_2_Size, currentTemp, orbits[idx].Moon_2_Bonus);
                    Data.AddData(planet, "Moon", DefLibrary);
                    planet.SetValueS(starName + "_" + idx.ToString() + "_Sec_Moon", DefLibrary);
                    planetList.Subs.Add(planet);
                }
            }

            if (currentTemp == 5) currentTemp--;
            else if (currentTemp > 1 && RNG.RandiRange(0, 99) < 50) currentTemp--;
        }
    }

    private List<Orbit> Generate_Orbits_v1(int level)
    {
        // Calculate planets and orbits
        List<Orbit> orbits = new List<Orbit>();
        bool gasFirst = RNG.RandfRange(0, 99) < 25;
        int targetPoints = level * 30;

        // where s is size  or  size + 3 if bonus
        // types            0 u         1 uu        2 h         3 hu        4 hh        5 a         6 g         7 gu        8 guu       9 gh        10 ghh
        // min points       0 10        1 20        2 15        3 40        4 45        5 10        6 10        7 20        8 30        9 25        10 40
        // max points       0 20        1 40        2 60        3 80        4 100       5 20        6 20        7 40        8 60        9 60        10 100

        int gasGiants = 0;
        for (int o = 0; o < 7; o++) // the max 7 orbits
        {
            if (targetPoints < 0)
                break;

            int type = -1;
            if (targetPoints >= 100)
            {
                if (RNG.RandiRange(0, 99) < 5 - 2 * gasGiants) type = 10; // 5
                else if (RNG.RandiRange(0, 99) < 10) type = 4; // 9.5
                else if (RNG.RandiRange(0, 99) < 10 - 4 * gasGiants) type = 9; // 8.5
                else if (RNG.RandiRange(0, 99) < 20) type = 2; // 15
                else type = 3; // 62
            }
            else
            {
                if (o == 0 || targetPoints >= 80)
                {
                    if (RNG.RandiRange(0, 99) < 10 - 4 * gasGiants) type = 9;
                    else if (RNG.RandiRange(0, 99) < 20) type = 3;
                    else type = 2;
                }
                else
                {
                    if (RNG.RandiRange(0, 99) < 5 && targetPoints >= 60 - 25 * gasGiants) type = 8; // 5
                    else if (RNG.RandiRange(0, 99) < 10) type = 5; // 9.5
                    else if (RNG.RandiRange(0, 99) < 10) type = 1; // 8.5
                    else if (RNG.RandiRange(0, 99) < 30 - 12 * gasGiants) type = 7; // 24
                    else if (RNG.RandiRange(0, 99) < 30 + 30 * gasGiants) type = 0; // 14
                    else type = 6; // 33
                }
            }

            if (type > 5) gasGiants++;

            Orbit orbit = new Orbit();
            switch (type)
            {
                case 0:
                    {
                        orbit.Planet = PlanetType.UNINHABITABLE;

                        orbit.PlanetSize = 1;
                        if (RNG.RandiRange(0, 99) > 50) orbit.PlanetSize = 2;
                        else if (RNG.RandiRange(0, 99) > 10) orbit.PlanetSize = 3;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20;

                        orbit.Points = (orbit.PlanetBonus ? 20 : 10);

                        if (gasFirst) orbits.Add(orbit);
                        else orbits.Insert(0, orbit);
                        break;
                    }
                case 1:
                    {
                        orbit.Planet = PlanetType.UNINHABITABLE;
                        orbit.Moon_1 = PlanetType.UNINHABITABLE;

                        orbit.PlanetSize = 2;
                        if (RNG.RandiRange(0, 99) > 40) orbit.PlanetSize = 3;
                        orbit.Moon_1_Size = 1;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 30;
                        orbit.Moon_1_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20 + (orbit.PlanetBonus ? 20 : 10);

                        orbit.Points = (orbit.PlanetBonus ? 20 : 10) + (orbit.Moon_1_Bonus ? 20 : 10);

                        if (gasFirst) orbits.Add(orbit);
                        else orbits.Insert(0, orbit);
                        break;
                    }
                case 2:
                    {
                        orbit.Planet = PlanetType.HABITABLE;

                        orbit.PlanetSize = 1;
                        if (RNG.RandiRange(0, 99) > 50 && targetPoints >= 30) orbit.PlanetSize = 2;
                        else if (RNG.RandiRange(0, 99) > 10 && targetPoints >= 45) orbit.PlanetSize = 3;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20 * orbit.PlanetSize;

                        orbit.Points = orbit.PlanetSize * (orbit.PlanetBonus ? 20 : 15);

                        if (gasFirst) orbits.Add(orbit);
                        else orbits.Insert(0, orbit);
                        break;
                    }
                case 3:
                    {
                        orbit.Planet = PlanetType.HABITABLE;
                        orbit.Moon_1 = PlanetType.UNINHABITABLE;

                        orbit.PlanetSize = 2;
                        if (RNG.RandiRange(0, 99) > 40 && targetPoints >= 55) orbit.PlanetSize = 3;
                        orbit.Moon_1_Size = 1;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20 * orbit.PlanetSize + 10;
                        orbit.Moon_1_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= (orbit.PlanetBonus ? 20 : 15) * orbit.PlanetSize + 20;

                        orbit.Points = orbit.PlanetSize * (orbit.PlanetBonus ? 20 : 15) + (orbit.Moon_1_Bonus ? 20 : 10);

                        if (gasFirst) orbits.Add(orbit);
                        else orbits.Insert(0, orbit);
                        break;
                    }
                case 4:
                    {
                        orbit.Planet = PlanetType.HABITABLE;
                        orbit.Moon_1 = PlanetType.HABITABLE;

                        orbit.PlanetSize = 2;
                        if (RNG.RandiRange(0, 99) > 40 && targetPoints >= 60) orbit.PlanetSize = 3;
                        orbit.Moon_1_Size = 1;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20 * orbit.PlanetSize + 15;
                        orbit.Moon_1_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= (orbit.PlanetBonus ? 20 : 15) * orbit.PlanetSize + 20;

                        orbit.Points = orbit.PlanetSize * (orbit.PlanetBonus ? 20 : 15) + orbit.Moon_1_Size * (orbit.Moon_1_Bonus ? 20 : 15);

                        if (gasFirst) orbits.Add(orbit);
                        else orbits.Insert(0, orbit);
                        break;
                    }
                case 5:
                    {
                        orbit.Planet = PlanetType.ASTEROIDS;
                        orbit.PlanetSize = 1;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20;

                        orbit.Points = (orbit.PlanetBonus ? 20 : 10);

                        if (gasFirst)
                        {
                            int idx = 0;
                            while (idx < orbits.Count && orbit.Planet == PlanetType.GAS_GIANT) idx++;
                            orbits.Insert(idx, orbit);
                        }
                        else
                        {
                            int idx = 0;
                            while (idx < orbits.Count && orbit.Planet == PlanetType.GAS_GIANT) idx++;
                            orbits.Insert(idx, orbit);
                        }
                        break;
                    }
                case 6:
                    {
                        orbit.Planet = PlanetType.GAS_GIANT;

                        orbit.PlanetSize = 4;
                        if (RNG.RandiRange(0, 99) > 80) orbit.PlanetSize = 5;
                        else if (RNG.RandiRange(0, 99) > 10) orbit.PlanetSize = 6;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20;

                        orbit.Points = (orbit.PlanetBonus ? 20 : 10);

                        if (gasFirst) orbits.Insert(0, orbit);
                        else orbits.Add(orbit);
                        break;
                    }
                case 7:
                    {
                        orbit.Planet = PlanetType.GAS_GIANT;
                        orbit.Moon_1 = PlanetType.UNINHABITABLE;

                        orbit.PlanetSize = 5;
                        if (RNG.RandiRange(0, 99) > 20) orbit.PlanetSize = 6;

                        orbit.Moon_1_Size = 1;
                        if (RNG.RandiRange(0, 99) > 30) orbit.Moon_1_Size = 2;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 30;
                        orbit.Moon_1_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20 + (orbit.PlanetBonus ? 20 : 10);

                        orbit.Points = (orbit.PlanetBonus ? 20 : 10) + (orbit.Moon_1_Bonus ? 20 : 10);

                        if (gasFirst) orbits.Insert(0, orbit);
                        else orbits.Add(orbit);
                        break;
                    }
                case 8:
                    {
                        orbit.Planet = PlanetType.GAS_GIANT;
                        orbit.Moon_1 = PlanetType.UNINHABITABLE;
                        orbit.Moon_2 = PlanetType.UNINHABITABLE;

                        orbit.PlanetSize = 5;
                        if (RNG.RandiRange(0, 99) > 50) orbit.PlanetSize = 6;

                        orbit.Moon_1_Size = 1;
                        if (RNG.RandiRange(0, 99) > 30) orbit.Moon_1_Size = 2;
                        orbit.Moon_2_Size = 1;
                        if (RNG.RandiRange(0, 99) > 30) orbit.Moon_2_Size = 2;

                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 40;
                        orbit.Moon_1_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 30 + (orbit.PlanetBonus ? 20 : 10);
                        orbit.Moon_2_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20 + (orbit.PlanetBonus ? 20 : 10) + (orbit.Moon_1_Bonus ? 20 : 10);

                        orbit.Points = (orbit.PlanetBonus ? 20 : 10) + (orbit.Moon_1_Bonus ? 20 : 10) + (orbit.Moon_2_Bonus ? 20 : 10);

                        if (gasFirst) orbits.Insert(0, orbit);
                        else orbits.Add(orbit);
                        break;
                    }
                case 9:
                    {
                        orbit.Planet = PlanetType.GAS_GIANT;
                        orbit.Moon_1 = PlanetType.HABITABLE;

                        orbit.PlanetSize = 5;
                        if (RNG.RandiRange(0, 99) > 20) orbit.PlanetSize = 6;

                        orbit.Moon_1_Size = 1;
                        if (RNG.RandiRange(0, 99) > 30) orbit.Moon_1_Size = 2;

                        orbit.Moon_1_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 10 + 20 * orbit.Moon_1_Size;
                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20 + (orbit.Moon_1_Bonus ? 20 : 15) * orbit.Moon_1_Size;

                        orbit.Points = (orbit.PlanetBonus ? 20 : 10) + orbit.Moon_1_Size * (orbit.Moon_1_Bonus ? 20 : 15);

                        if (gasFirst) orbits.Insert(0, orbit);
                        else orbits.Add(orbit);
                        break;
                    }
                case 10:
                    {
                        orbit.Planet = PlanetType.GAS_GIANT;
                        orbit.Moon_1 = PlanetType.HABITABLE;
                        orbit.Moon_2 = PlanetType.HABITABLE;

                        orbit.PlanetSize = 5;
                        if (RNG.RandiRange(0, 99) > 50) orbit.PlanetSize = 6;

                        orbit.Moon_1_Size = 1;
                        if (RNG.RandiRange(0, 99) > 30) orbit.Moon_1_Size = 2;
                        orbit.Moon_2_Size = 1;
                        if (RNG.RandiRange(0, 99) > 30) orbit.Moon_2_Size = 2;

                        orbit.Moon_1_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 10 + orbit.Moon_1_Size * 20 + orbit.Moon_2_Size * 15;
                        orbit.Moon_2_Bonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 10 + orbit.Moon_1_Size * (orbit.Moon_1_Bonus ? 20 : 15) + orbit.Moon_2_Size * 20;
                        orbit.PlanetBonus = RNG.RandiRange(0, 99) > 40 && targetPoints >= 20 + orbit.Moon_1_Size * (orbit.Moon_1_Bonus ? 20 : 15) + orbit.Moon_2_Size * (orbit.Moon_2_Bonus ? 20 : 15);

                        orbit.Points = (orbit.PlanetBonus ? 20 : 10) + orbit.Moon_1_Size * (orbit.Moon_1_Bonus ? 20 : 15) + orbit.Moon_2_Size * (orbit.Moon_2_Bonus ? 20 : 15);

                        if (gasFirst) orbits.Insert(0, orbit);
                        else orbits.Add(orbit);
                        break;
                    }
            }

            targetPoints -= orbit.Points;
        }

        return orbits;
    }


    private List<Orbit> Generate_Orbits_v2(int level)
    {
        List<Orbit> orbits = new List<Orbit>();

        int x = RNG.RandiRange(0, 2);
        int habitableSize = level + x; // 1-7
        int habitablePlanets = RNG.RandiRange(1, level);
        if (habitableSize >= 4 && habitablePlanets < 2) habitablePlanets = 2;
        if (habitableSize >= 7 && habitablePlanets < 3) habitablePlanets = 3;
        if (habitablePlanets > 3) habitablePlanets = 3;
        if (habitablePlanets > habitableSize) habitablePlanets = habitableSize; // 1-3
        int otherPlanets = 3 + level + 2 - x; // 4 - 10
        int gasGiants = RNG.RandiRange(1 + otherPlanets / 5, otherPlanets / 2); // 1-5
        int asteroids = RNG.RandiRange(1, 1 + level / 3); // 1-2
        int uninhabitable = otherPlanets - gasGiants - asteroids; // 2 - 8 ?
        if (uninhabitable < 0)
        {
            gasGiants += uninhabitable;
            uninhabitable = 0;
        }
        int totalPlanets = gasGiants + habitablePlanets + asteroids + uninhabitable;

        int g = gasGiants;
        while (g > 0)
        {
            Generate_Orbits_v2_AddNewOrbit(orbits, PlanetType.GAS_GIANT);
            g--;
        }

        List<int> habitablePlanetsSize = new List<int>();
        for (int n = 0; n < habitablePlanets; n++) habitablePlanetsSize.Add(1);
        int h = habitableSize - habitablePlanets;
        while (h > 0)
        {
            int r = RNG.RandiRange(0, habitablePlanetsSize.Count - 1);
            if (habitablePlanetsSize[r] < 3) habitablePlanetsSize[r]++;
            else if (habitablePlanetsSize[(r + 1) % habitablePlanetsSize.Count] < 3) habitablePlanetsSize[(r + 1) % habitablePlanetsSize.Count]++;
            else if (habitablePlanetsSize[(r + 2) % habitablePlanetsSize.Count] < 3) habitablePlanetsSize[(r + 2) % habitablePlanetsSize.Count]++;
            h--;
        }

        bool gasFirst = RNG.RandiRange(0, 3) == 0;
        if (habitablePlanetsSize.Count > 1)
        {
            if (habitablePlanetsSize[1] <= 2 && orbits.Count > 0)
            {
                Generate_Orbits_v2_AddMoon(orbits[orbits.Count - 1], PlanetType.HABITABLE, habitablePlanetsSize[1]);
                if (asteroids > 0)
                {
                    Generate_Orbits_v2_AddNewOrbit(orbits, PlanetType.ASTEROIDS, !gasFirst, habitablePlanetsSize[1]);
                }
            }
            else
            {
                Generate_Orbits_v2_AddNewOrbit(orbits, PlanetType.HABITABLE, !gasFirst, habitablePlanetsSize[1]);
                if (asteroids > 0)
                {
                    Generate_Orbits_v2_AddNewOrbit(orbits, PlanetType.ASTEROIDS, !gasFirst, habitablePlanetsSize[1]);
                }
            }
        }
        if (habitablePlanetsSize.Count > 0) Generate_Orbits_v2_AddNewOrbit(orbits, PlanetType.HABITABLE, !gasFirst, habitablePlanetsSize[0]);
        if (habitablePlanetsSize.Count > 2) Generate_Orbits_v2_AddNewOrbit(orbits, PlanetType.HABITABLE, !gasFirst, habitablePlanetsSize[2]);

        int trys = 100;
        int count = uninhabitable;
        while (count > 0 && trys > 0)
        {
            bool success = false;
            if (RNG.RandiRange(0,1) == 0 && orbits.Count > 0) success = Generate_Orbits_v2_AddMoon(orbits[RNG.RandiRange(0, orbits.Count - 1)], PlanetType.UNINHABITABLE); 
            else success = Generate_Orbits_v2_AddNewOrbit(orbits, PlanetType.UNINHABITABLE, !gasFirst);

            if (success) count--;
            trys--;
        }

        if (trys <= 0)
        {
            GD.Print("planets not added - system maybe full?");
        }

        if (asteroids > 1)
        {
            Generate_Orbits_v2_AddNewOrbit(orbits, PlanetType.ASTEROIDS, !gasFirst);
        }

        if (level == 5)
        {
            GD.Print("HabSize: " + habitableSize.ToString());
            string orbitStr = "S";
            for (int i = 0; i < orbits.Count; i++)
            {
                orbitStr += " " + orbits[i].PlanetSize + orbits[i].Planet.ToString().Substring(0, 1);
                if (orbits[i].Moon_1 != PlanetType.NONE) orbitStr += " " + orbits[i].Moon_1_Size + orbits[i].Moon_1.ToString().Substring(0, 1);
                if (orbits[i].Moon_2 != PlanetType.NONE) orbitStr += " " + orbits[i].Moon_2_Size + orbits[i].Moon_2.ToString().Substring(0, 1);
            }
            GD.Print("::: " + orbitStr);
        }

        return orbits;
    }

    private bool Generate_Orbits_v2_AddNewOrbit(List<Orbit> orbits, PlanetType type, bool insert = true, int size = 0)
    {
        if (orbits.Count >= 7)
            return false;

        Orbit orbit = new Orbit();
        orbit.Planet = type;
        if (size > 0) orbit.PlanetSize = size;
        else orbit.PlanetSize = (type == PlanetType.GAS_GIANT ? 3 : 0) + RNG.RandiRange(1, 3);
        orbit.PlanetBonus = true;

        if (insert) orbits.Insert(0,orbit);
        else orbits.Add(orbit);

        return true;
    }

    private bool Generate_Orbits_v2_AddMoon(Orbit orbit, PlanetType type, int size = 0)
    {
        if (orbit.Moon_1 == PlanetType.NONE)
        {
            orbit.Moon_1 = type;
            if (size > 0) orbit.Moon_1_Size = size;
            else orbit.Moon_1_Size = RNG.RandiRange(1, 2);
            orbit.Moon_1_Bonus = true;
            return true;
        }
        else if (orbit.Moon_2 == PlanetType.NONE && orbit.Planet == PlanetType.GAS_GIANT)
        {
            orbit.Moon_2 = type;
            if (size > 0) orbit.Moon_2_Size = size;
            else orbit.Moon_2_Size = RNG.RandiRange(1, orbit.PlanetSize >= 3 ? 2 : 1);
            orbit.Moon_2_Bonus = true;
            return true;
        }

        return false;
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
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_VeryHot, 5, true);
        }

        if (Planets_Hot == null)
        {
            Planets_Hot = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_Hot, 4, true);
        }

        if (Planets_Temperate == null)
        {
            Planets_Temperate = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_Temperate, 3, true);
        }

        if (Planets_Cold == null)
        {
            Planets_Cold = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_Cold, 2, true);
        }

        if (Planets_Frozen == null)
        {
            Planets_Frozen = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(Planets_Frozen, 1, true);
        }

        if (NonHabitable_VeryHot == null)
        {
            NonHabitable_VeryHot = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(NonHabitable_VeryHot, 5, false);
        }

        if (NonHabitable_Hot == null)
        {
            NonHabitable_Hot = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(NonHabitable_Hot, 4, false);
        }

        if (NonHabitable_Temperate == null)
        {
            NonHabitable_Temperate = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(NonHabitable_Temperate, 3, false);
        }

        if (NonHabitable_Cold == null)
        {
            NonHabitable_Cold = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(NonHabitable_Cold, 2, false);
        }

        if (NonHabitable_Frozen == null)
        {
            NonHabitable_Frozen = new List<DataBlock>();
            GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(NonHabitable_Frozen, 1, false);
        }
    }

    private void GenerateNewMapSave_Stars_Planets_Random_GenerateWeightedLists_Planets(List<DataBlock> planetsList, int temp, bool habitable = true)
    {
        for (int idx = 0; idx < DefLibrary.Planets.Count; idx++)
        {
            if (DefLibrary.Planets[idx].Name == "Planet"
                && DefLibrary.Planets[idx].GetSub("Weight", false) != null
                && DefLibrary.Planets[idx].GetSub("Temperature:Min", false) != null
                && DefLibrary.Planets[idx].GetSub("Temperature:Max", false) != null)
            {
                if (DefLibrary.Planets[idx].GetSub("Temperature:Min").ValueI <= temp && DefLibrary.Planets[idx].GetSub("Temperature:Max").ValueI >= temp
                    && ((habitable == true && DefLibrary.Planets[idx].HasSub("Habitable"))||(habitable == false && DefLibrary.Planets[idx].HasSub("Uninhabitable"))))
                {
                    for (int w = 0; w < DefLibrary.Planets[idx].GetSub("Weight").ValueI; w++)
                    {
                        planetsList.Add(DefLibrary.Planets[idx]);
                    }
                }
            }
        }
    }

    private DataBlock GenerateNewMapSave_Stars_Planets_Level_HabitableType(int size, string planetType, int temperature, bool bonus)
    {
        DataBlock chosenPlanetData = DefLibrary.GetPlanet(planetType);

        DataBlock planet = Data.CreateData("Planet", "TempName", DefLibrary);
        Data.AddData(planet, "Type", planetType, DefLibrary);
        Data.AddData(planet, "Size", size, DefLibrary);
        Data.AddData(planet, "Temperature", temperature, DefLibrary);
        Data.AddData(planet, "Habitable", DefLibrary);
        Data.AddData(planet, "SlotType", chosenPlanetData.GetSub("SlotType").ValueS, DefLibrary);

        DataBlock planetFeatures = chosenPlanetData.GetSub("Features");
        if (planetFeatures != null)
        {
            GenerateNewMapSave_Stars_Planets__AddFeatures(planet, planetFeatures, bonus);
        }

        return planet;
    }

    private DataBlock GenerateNewMapSave_Stars_Planets_Level_Habitable(int size, int temperature, bool bonus)
    {
        DataBlock chosenPlanetData = null;
        temperature = Mathf.Clamp(temperature, 2, 4);
        switch (temperature)
        {
            //case 1: chosenPlanetData = Planets_Frozen[RNG.RandiRange(0, Planets_Frozen.Count - 1)]; break;
            case 2: chosenPlanetData = Planets_Cold[RNG.RandiRange(0, Planets_Cold.Count - 1)]; break;
            case 3: chosenPlanetData = Planets_Temperate[RNG.RandiRange(0, Planets_Temperate.Count - 1)]; break;
            case 4: chosenPlanetData = Planets_Hot[RNG.RandiRange(0, Planets_Hot.Count - 1)]; break;
            //case 5: chosenPlanetData = Planets_VeryHot[RNG.RandiRange(0, Planets_VeryHot.Count - 1)]; break;
        }

        DataBlock planet = Data.CreateData("Planet", "TempName", DefLibrary);
        Data.AddData(planet, "Type", chosenPlanetData.ValueS, DefLibrary);
        Data.AddData(planet, "Size", size, DefLibrary);
        Data.AddData(planet, "Temperature", temperature, DefLibrary);
        Data.AddData(planet, "Habitable", DefLibrary);
        Data.AddData(planet, "SlotType", chosenPlanetData.GetSub("SlotType").ValueS, DefLibrary);

        DataBlock planetFeatures = chosenPlanetData.GetSub("Features");
        if (planetFeatures != null)
        {
            GenerateNewMapSave_Stars_Planets__AddFeatures(planet, planetFeatures, bonus);
        }

        return planet;
    }

    private DataBlock GenerateNewMapSave_Stars_Planets_Level_Uninhabitable(int size, int temperature, bool bonus)
    {
        DataBlock chosenPlanetData = null;
        switch (temperature)
        {
            case 1: chosenPlanetData = NonHabitable_Frozen[RNG.RandiRange(0, NonHabitable_Frozen.Count - 1)]; break;
            case 2: chosenPlanetData = NonHabitable_Cold[RNG.RandiRange(0, NonHabitable_Cold.Count - 1)]; break;
            case 3: chosenPlanetData = NonHabitable_Temperate[RNG.RandiRange(0, NonHabitable_Temperate.Count - 1)]; break;
            case 4: chosenPlanetData = NonHabitable_Hot[RNG.RandiRange(0, NonHabitable_Hot.Count - 1)]; break;
            case 5: chosenPlanetData = NonHabitable_VeryHot[RNG.RandiRange(0, NonHabitable_VeryHot.Count - 1)]; break;
        }

        DataBlock planet = Data.CreateData("Planet", "TempName", DefLibrary);
        Data.AddData(planet, "Type", chosenPlanetData.ValueS, DefLibrary);
        Data.AddData(planet, "Size", size, DefLibrary);
        Data.AddData(planet, "Temperature", temperature, DefLibrary);
        Data.AddData(planet, "Uninhabitable", DefLibrary);
        Data.AddData(planet, "SlotType", chosenPlanetData.GetSub("SlotType").ValueS, DefLibrary);

        DataBlock planetFeatures = chosenPlanetData.GetSub("Features");
        if (planetFeatures != null)
        {
            GenerateNewMapSave_Stars_Planets__AddFeatures(planet, planetFeatures, bonus);
        }

        return planet;
    }

    private DataBlock GenerateNewMapSave_Stars_Planets_Level_GasGiant(int size, bool bonus)
    {
        DataBlock planetDef = DefLibrary.GetPlanet("Gas_Giant");

        DataBlock planet = Data.CreateData("Planet", "TempName", DefLibrary);
        Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
        Data.AddData(planet, "Size", size, DefLibrary);
        Data.AddData(planet, "SlotType", planetDef.GetSub("SlotType").ValueS, DefLibrary);

        DataBlock features = planetDef.GetSub("Features");
        if (features != null)
        {
            GenerateNewMapSave_Stars_Planets__AddFeatures(planet, features, bonus);
        }

        return planet;
    }

    private DataBlock GenerateNewMapSave_Stars_Planets_Level_Asteroids(bool bonus)
    {
        DataBlock planetDef = DefLibrary.GetPlanet("Asteroids");

        DataBlock planet = Data.CreateData("Planet", "TempName", DefLibrary);
        Data.AddData(planet, "Type", "Asteroids", DefLibrary);
        Data.AddData(planet, "SlotType", planetDef.GetSub("SlotType").ValueS, DefLibrary);

        DataBlock features = planetDef.GetSub("Features");
        if (features != null)
        {
            GenerateNewMapSave_Stars_Planets__AddFeatures(planet, features, bonus);
        }

        return planet;
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
            GenerateNewMapSave_Stars_Planets__AddFeatures(planet, planetFeatures, true);
        }

        if (moon) Data.AddData(planet, "Moon", DefLibrary);

        //GenerateNewMapSave_Stars_Planets_AddResourcesData(planet);
    }

    private void GenerateNewMapSave_Stars_Planets__AddFeatures(DataBlock planet, DataBlock features, bool bonus = false)
    {
        DataBlock featuresData = Data.AddData(planet, "Features", DefLibrary);
        List<string> possibleBonuses = new List<string>();
        Array<DataBlock> subs = features.GetSubs();
        for (int idx = 0; idx < subs.Count; idx++)
        {
            if (subs[idx].Name == "PopMaxPerSize")
            {
                Data.AddData(featuresData, "PopMaxPerSize", subs[idx].ValueI, DefLibrary);
            }
            else if (subs[idx].Name == "Bonus")
            {
                possibleBonuses.Add(subs[idx].ValueS);
            }
            else if (subs[idx].Name.EndsWith(":Perc") == false || RNG.RandiRange(0, 99) < subs[idx].ValueI)
            {
                DataBlock feature = subs[idx];
                while (feature.Name.StartsWith("OR"))
                {
                    feature = GenerateNewMapSave_Stars_Planets__AddFeatures_ResolveOR(feature);
                }

                if (feature.Name.StartsWith("Building:"))
                {
                    Data.AddData(featuresData, Helper.Split_0(feature.Name), Helper.Split_1(feature.Name), DefLibrary);

                    // add hidden features
                    //if (feature.Name == "Rings")
                    //{
                    //    Data.AddData(planet, "Hidden_Rings", DefLibrary);
                    //}
                }
                else
                {
                    Data.AddData(featuresData, Helper.Split_0(feature.Name), DefLibrary);
                }
            }
        }

        if (bonus && possibleBonuses.Count > 0)
        {
            Data.AddData(featuresData, possibleBonuses[RNG.RandiRange(0, possibleBonuses.Count - 1)], DefLibrary);
        }
    }

    private DataBlock GenerateNewMapSave_Stars_Planets__AddFeatures_ResolveOR(DataBlock or)
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
