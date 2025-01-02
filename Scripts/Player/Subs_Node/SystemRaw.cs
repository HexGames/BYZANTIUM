using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public static class SystemRaw
{
    public static DataBlock CreateNewSystem(DataBlock player, DataBlock star, DefLibrary def, bool empireCapital = false, DataBlock chosenPlanet = null)
    {
        DataBlock empireData = def.GetEmpire(player.ValueS);

        DataBlock systemList = player.GetSub("Systems_List");
        DataBlock system = Data.AddData(systemList, "System", star.ValueS, def);

        DataBlock actionTax = Data.AddData(system, "ActionTax", def);
        Data.AddData(actionTax, "Tax", 2, def);

        DataBlock actionBuildDistrict = Data.AddData(system, "ActionGrowth", def);
        Data.AddData(actionBuildDistrict, "FocusColony", "tempColony", def);

        //DataBlock actionInvestDistrict = Data.AddData(system, "ActionInvest", def);
        //Data.AddData(actionInvestDistrict, "FocusDistrict", -1, def);

        //DataBlock actionInfrastructure = Data.AddData(system, "ActionInfrastructure", def);
        //Data.AddData(actionInfrastructure, "Infrastructure", 0, def);

        DataBlock actionBuildShip = Data.AddData(system, "ActionBuildShip", def);
        Data.AddData(actionBuildShip, "Design", "Babylon", def);
        Data.AddData(actionBuildShip, "LastDesign", "Babylon", def);
        Data.AddData(actionBuildShip, "Progress", 0, def);
        Data.AddData(actionBuildShip, "Overflow", 0, def);

        DataBlock actionRebbelion = Data.AddData(system, "ActionRebellion", def);
        Data.AddData(actionRebbelion, "Current", 0, def);

        DataBlock wealth = Data.AddData(system, "Wealth", def);
        Data.AddData(wealth, "Current", 0, def);
        Data.AddData(wealth, "Level", 0, def);

        if (chosenPlanet == null)
        {
            chosenPlanet = StarRaw.GetBestHabitablePlanet(star);
        }
        Data.AddData(system, "Colony_List", def);
        DataBlock colony = ColonyRaw.CreateNewColony_Habitable(player, star, chosenPlanet, system, true, empireCapital, def);
        //if (empireCapital) ColonyRaw.SetPopsFullGrowth(system, colony, chosenPlanet, def);

        Data.AddData(system, "Trades", def);

        Data.AddData(system, "Link:Star", star.ValueS, def); // no StarData yet
        Data.AddData(star, "Link:Player:Sector:System", player.ValueS + ":" + system.ValueS, def); // no SystemData yet

        return system;
    }

    public static void GrowSystem(DataBlock player, DataBlock system, DataBlock star, int popsLevel, int economyLevel, bool empireCapital, DefLibrary def)
    {
        int maxPops = StarRaw.GetStarPopsMax(star, def);
        int currentPops = GetSystemPopsCurrent(system);
        int maxNewPops = maxPops - currentPops;
        int habSize = StarRaw.GetHabitalibitySize(star);

        int grows = 0;
        switch (popsLevel)
        {
            case 2: grows = Mathf.Max((1 + habSize) / 2 - 1, 1); break;
            case 3: grows = Mathf.Max(habSize - 1, 1); break;
            case 4: grows = Mathf.Max(2 * habSize - 1, 1); break;
            case 5: grows = maxNewPops; break;
        }
        for (int n = 0; n < grows; n++)
        {
            bool state = n > grows / 3;
            //ReassignGrowingDistrict(system, state, def);
            SetPopsFullGrowth(player, system, star, def);
        }
        SetPopsHalfGrowth(player, system, star, def);

        currentPops = GetSystemPopsCurrent(system); // refresh current pops
        int levels = 0;
        switch (economyLevel)
        {
            case 1: levels = 1 + habSize / 3; break;
            case 2: levels = Mathf.Max((currentPops + 1) / 2, 2); break;
            case 3: levels = Mathf.Max(currentPops, 3); break;
            case 4: levels = Mathf.Max(3 * currentPops / 2, 4); break;
            case 5: levels = Mathf.Max(2 * currentPops, 5); break;
        }

        system.SetSubValueI("Wealth", "Current", currentPops * economyLevel * 100, def);
        system.SetSubValueI("Wealth", "Level", economyLevel, def);

        //if (cp > currentPops * 3) cp = currentPops * 3;
        //system.SetSubValueI("ActionInfrastructure/Infrastructure", cp, def);

        ReassignAllDistricts(system, empireCapital, levels, def);
    }

    public static void ReassignAllDistricts(DataBlock system, bool empireCapital, int upgradeLevels, DefLibrary def)
    {
        int pops = GetSystemPopsCurrent(system);
        if (pops <= 0)
            return;

        int baseLevel = Mathf.Min(upgradeLevels / pops, 2);

        List<int> infrastructure = new List<int>();
        for (int d = 0; d < pops; d++) infrastructure.Add(baseLevel);
        upgradeLevels -= (baseLevel) * pops;

        if (upgradeLevels > 0 && infrastructure[0] < 2)
        {
            infrastructure[0]++;
            upgradeLevels--;
        }

        List<int> idxes = new List<int>();
        for (int i = 1; i < infrastructure.Count; i++) if (infrastructure[i] < 2) idxes.Add(i);

        while (upgradeLevels > 0 && idxes.Count > 0)
        {
            int rng_1 = Game.RNG.RandiRange(0, idxes.Count - 1);
            infrastructure[idxes[rng_1]]++;
            idxes.RemoveAt(rng_1);
            upgradeLevels--;
        }

        int unalocatedDistricts = pops - 1;
        int controlDistricts = unalocatedDistricts / 6;
        unalocatedDistricts -= controlDistricts;
        int bcDistricts = (unalocatedDistricts + 4) / 5;
        unalocatedDistricts -= bcDistricts;
        int constructionDistricts = (unalocatedDistricts + 3) / 4;
        unalocatedDistricts -= constructionDistricts;
        int researchDistricts = unalocatedDistricts / 3;
        unalocatedDistricts -= researchDistricts;
        int infuenceDistricts = unalocatedDistricts / 2;
        int shipbuildingDistricts = unalocatedDistricts - infuenceDistricts;
        unalocatedDistricts = 0;

        if (empireCapital && bcDistricts > 0)
        {
            bcDistricts -= 1;
            shipbuildingDistricts += 1;
        }

        int rng_2 = Game.RNG.RandiRange(0,2);
        bool stateConstruction = rng_2 == 0;
        bool stateShipbuilding = rng_2 == 1;
        bool stateInfluence = rng_2 == 2;

        int popIdx = 0;
        Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
        for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        {
            DataBlock colony = colonies[colonyIdx];
            Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
            for (int idx = 0; idx < districts.Count; idx++)
            {
                DataBlock district = districts[idx];
                if (district.GetSubValueI("Pop", "GrowthProgress") == 1000)
                {
                    string levelSuffix = Helper.IntToRoman(1 + infrastructure[popIdx]);

                    if (district.ValueS.StartsWith("Capital") == true)
                    {
                        district.ValueS = "Capital_" + levelSuffix;
                    }
                    else
                    {
                        if (controlDistricts > 0)
                        {
                            district.SetValueS("Police_" + levelSuffix, def);
                            controlDistricts--;
                        }
                        else if (researchDistricts > 0)
                        {
                            district.SetValueS("State_Research_" + levelSuffix, def);
                            researchDistricts--;
                        }
                        else if (shipbuildingDistricts > 0)
                        {
                            if (stateShipbuilding) district.SetValueS("Private_Shipyard_" + levelSuffix, def);
                            else district.SetValueS("State_Shipyard_" + levelSuffix, def);
                            shipbuildingDistricts--;
                        }
                        else if (infuenceDistricts > 0)
                        {
                            if (stateInfluence) district.SetValueS("State_Media_" + levelSuffix, def);
                            else district.SetValueS("Private_Media_" + levelSuffix, def);
                            infuenceDistricts--;
                        }
                    }

                    popIdx++;
                }
            }
        }
        //Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
        //for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        //{
        //    DataBlock colony = colonies[colonyIdx];
        //    Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        //    for (int idx = 0; idx < districts.Count; idx++)
        //    {
        //        DataBlock district = districts[idx];
        //        if (district.GetSubValueI("Pop/GrowthProgress") == 1000)
        //        {
        //            if (district.ValueS != "Capital")
        //            {
        //                if (popID == 0)
        //                {
        //                    if (pops > 7) district.SetValueS("State_Construction", def);
        //                    else district.SetValueS("Private_Construction", def);
        //                }
        //                else if (popID == 1)
        //                {
        //                    district.SetValueS("Research_Center", def);
        //                }
        //                else if (popID == 2)
        //                {
        //                    if (pops > 4) district.SetValueS("State_Shipyard", def);
        //                }
        //                else if (popID == 3)
        //                {
        //                    if (pops > 11) district.SetValueS("State_Media", def);
        //                    else district.SetValueS("Private_Media", def);
        //                }
        //                else if (popID == 4)
        //                {
        //                    district.SetValueS("Police", def);
        //                }
        //                else if (popID % 4 == 0)
        //                {
        //                    district.SetValueS("Police", def);
        //                }
        //                else
        //                {
        //                    if (Game.RNG.RandiRange(0, 4) == 0)
        //                    {
        //                        int rng = Game.RNG.RandiRange(0, 3);
        //                        if (rng == 0) district.SetValueS("State_Business", def);
        //                        else if (rng == 1) district.SetValueS("State_Media", def);
        //                        else if (rng == 2) district.SetValueS("State_Shipyard", def);
        //                        else if (rng == 3) district.SetValueS("Research_Center", def);
        //                    }
        //                    else
        //                    {
        //                        int rng = Game.RNG.RandiRange(0, 7);
        //                        if (rng == 0 || rng == 1) district.SetValueS("Private_Media", def);
        //                        else if (rng == 2 || rng == 3) district.SetValueS("Private_Shipyard", def);
        //                        else if (rng == 4) district.SetValueS("Private_Construction", def);
        //                    }
        //                }
        //            }
        //
        //            //district.SetSubValueI("Factory", infrastructure[popID], def);
        //            popID++;
        //        }
        //    }
        //}
    }

    //private static List<DataBlock> PossibleDistricts = new List<DataBlock>();
    //public static void ReassignGrowingDistrict(DataBlock system, bool state, DefLibrary def)
    //{
    //    int pops = GetSystemPopsCurrent(system);
    //    string colonyName = system.GetSubValueS("ActionGrowthFocus/Colony");
    //
    //    //int freeControl = GetSystemControlMax(system, def) - GetSystemControlCurrent(system, def);
    //    //int construction = GetSystemConstruction(system, def);
    //
    //    if (pops == 0)
    //    {
    //        return;
    //    }
    //    else if (pops == 1)
    //    {
    //        if (state) ReassignGrowingDistrict(system, "State_Construction", def);
    //        else ReassignGrowingDistrict(system, "Private_Construction", def);
    //        return;
    //    }
    //    else if (pops == 2)
    //    {
    //        if (state) ReassignGrowingDistrict(system, "State_Shipyard", def);
    //        else ReassignGrowingDistrict(system, "Private_Shipyard", def);
    //        return;
    //    }
    //    else if (pops == 3)
    //    {
    //        ReassignGrowingDistrict(system, "Research_Center", def);
    //        return;
    //    }
    //    else if (pops == 4)
    //    {
    //        if (state) ReassignGrowingDistrict(system, "State_Media", def);
    //        else ReassignGrowingDistrict(system, "Private_Media", def);
    //        return;
    //    }
    //
    //    PossibleDistricts.Clear();
    //    for (int idx = 0; idx < def.DistrictsList.Subs.Count; idx++)
    //    {
    //        if (def.DistrictsList.Subs[idx].HasSub("Type", "District"))
    //        {
    //            if ((state && def.DistrictsList.Subs[idx].ValueS.StartsWith("Private"))
    //                || (state == false && def.DistrictsList.Subs[idx].ValueS.StartsWith("State")))
    //            {
    //                PossibleDistricts.Add(def.DistrictsList.Subs[idx]);
    //            }
    //        }
    //    }
    //
    //    Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
    //    for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
    //    {
    //        DataBlock colony = colonies[colonyIdx];
    //        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
    //        for (int districtsIdx = 0; districtsIdx < districts.Count; districtsIdx++)
    //        {
    //            DataBlock district = districts[districtsIdx];
    //            if (district.GetSubValueI("Pop/GrowthProgress") < 1000)
    //            {
    //                district.SetValueS(PossibleDistricts[Game.RNG.RandiRange(0, PossibleDistricts.Count - 1)].ValueS, def);
    //            }
    //        }
    //    }
    //}

    public static void ReassignGrowingDistrict(DataBlock system, string districtName, DefLibrary def)
    {
        Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
        for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        {
            DataBlock colony = colonies[colonyIdx];
            Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
            for (int districtsIdx = 0; districtsIdx < districts.Count; districtsIdx++)
            {
                DataBlock district = districts[districtsIdx];
                if (district.GetSubValueI("Pop", "GrowthProgress") < 1000)
                {
                    district.SetValueS(districtName, def);
                }
            }
        }
    }

    public static int GetSystemPopsCurrent(DataBlock system)
    {
        int pops = 0;
        Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
        for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        {
            DataBlock colony = colonies[colonyIdx];
            Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
            for (int idx = 0; idx < districts.Count; idx++)
            {
                DataBlock district = districts[idx];
                if (district.GetSubValueI("Pop", "GrowthProgress") == 1000)
                {
                    pops++;
                }
            }
        }
        return pops;
    }

    public static void SetPopsFullGrowth(DataBlock player, DataBlock system, DataBlock star, DefLibrary def)
    {
        Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
        for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        {
            DataBlock colony = colonies[colonyIdx];
            Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
            for (int idx = 0; idx < districts.Count; idx++)
            {
                if (DistrictRaw.GetPopGrowth(districts[idx]) == 0)
                {
                    DistrictRaw.SetPopFullGrowth(districts[idx], def);
                }
            }
        }

        DataBlock chosenNextPlanet = null; 
        int newGrowth = 0;
        int remainingPops = 0;
        Array<DataBlock> planets = star.GetSub("Planet_List").GetSubs();
        for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
        {
            DataBlock planet = planets[planetIdx];
            if (planet.GetSubValueI("PopsMax") > 0)
            {
                int planetGrowth = PlanetRaw.GetPopGrowth(planet, def);
                if (planet.HasSub("Link:Player:System:Colony") == false && (chosenNextPlanet == null || planetGrowth > newGrowth))
                {
                    chosenNextPlanet = planet;
                    newGrowth = planetGrowth;
                    remainingPops = planet.GetSubValueI("PopsMax");
                }
            }
        }

        DataBlock chosenNextColony = null;
        for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        {
            DataBlock colony = colonies[colonyIdx];

            DataBlock planet = null;
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                if (planets[planetIdx].ValueS == colony.ValueS)
                {
                    planet = planets[planetIdx];
                    break;
                }
            }

            int popsMax = planet.GetSubValueI("PopsMax");
            Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
            if (popsMax > districts.Count)
            {
                int colonyExtraGrowth = ColonyRaw.GetGrowth(planet, colony, true, def) - ColonyRaw.GetGrowth(planet, colony, false, def);
                int colonyRemainingPops = popsMax - districts.Count;
                if ((chosenNextColony == null && chosenNextPlanet == null) || colonyExtraGrowth > newGrowth || (colonyExtraGrowth == newGrowth && colonyRemainingPops > remainingPops))
                {
                    chosenNextColony = colony;
                    newGrowth = colonyExtraGrowth;
                    remainingPops = colonyRemainingPops;
                }
            }
        }

        //if (system.ValueS.StartsWith("Rana"))
        //{
        //    string bau = "bau";
        //    if (chosenNextColony != null) bau += " " + chosenNextColony.ValueS + " " + newGrowth.ToString();
        //    else if (chosenNextPlanet != null) bau += " " + chosenNextPlanet.ValueS + " " + newGrowth.ToString();
        //    GD.Print(bau);
        //}

        if (chosenNextColony != null)
        {
            DistrictRaw.CreateNewDistrictAndPop(system, chosenNextColony, false, def);
        }
        else if (chosenNextPlanet != null)
        {
            ColonyRaw.CreateNewColony_Habitable(player, star, chosenNextPlanet, system, false, false, def);
        }
    }

    public static void SetPopsHalfGrowth(DataBlock player, DataBlock system, DataBlock star, DefLibrary def)
    {
        Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
        for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        {
            DataBlock colony = colonies[colonyIdx];
            Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
            for (int idx = 0; idx < districts.Count; idx++)
            {
                if (DistrictRaw.GetPopGrowth(districts[idx]) == 0)
                {
                    DistrictRaw.SetPopHalfGrowth(districts[idx], def);
                }
            }
        }
    }
}


/*public static int GetSystemControlCurrent(DataBlock system, DefLibrary def)
{
    int control = 0;
    Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
    for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
    {
        DataBlock colony = colonies[colonyIdx];
        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        for (int districtsIdx = 0; districtsIdx < districts.Count; districtsIdx++)
        {
            DataBlock district = districts[districtsIdx];
            if (district.GetSubValueI("Pop/GrowthProgress") == 1000)
            {
                DataBlock districtDef = def.GetDistrict(district.ValueS);
                control += districtDef.GetSubValueI("Control/Cost");
            }
        }
    }
    return control;
}

public static int GetSystemControlMax(DataBlock system, DefLibrary def)
{
    int controlMax = 0;
    Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
    for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
    {
        DataBlock colony = colonies[colonyIdx];
        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        for (int districtsIdx = 0; districtsIdx < districts.Count; districtsIdx++)
        {
            DataBlock district = districts[districtsIdx];
            if (district.GetSubValueI("Pop/GrowthProgress") == 1000)
            {
                DataBlock districtDef = def.GetDistrict(district.ValueS);
                if (districtDef.HasSub("Resource", "Control"))
                {
                    int popBonus = districtDef.GetSubValueI("Default/Pop*Bonus");
                    int factoryBonus = districtDef.GetSubValueI("Default/Factory*Bonus");
                    int factory = district.GetSubValueI("Factory");
                    bool police = district.HasSub("Control", "Police");
                    bool stateOwned = district.HasSub("Control", "State_Owned");
                    DataBlock ecoData = null;
                    if (stateOwned)
                    {
                        if (factory > 0) ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("State_Factory_" + factory.ToString());
                        else ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("State_Pop");
                    }
                    else if (police)
                    {
                        if (factory > 0) ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("Police_Factory_" + factory.ToString());
                        else ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("Police_Pop");

                    }
                    int bonus = popBonus + factoryBonus * factory;
                    int control = (ecoData != null ? ecoData.GetSub("Resource").ValueI : 0) + bonus;

                    controlMax += control;
                }
            }
        }
    }
    return controlMax;
}

public static int GetSystemConstruction(DataBlock system, DefLibrary def)
{
    int constructionTotal = 0;
    Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
    for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
    {
        DataBlock colony = colonies[colonyIdx];
        Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
        for (int districtsIdx = 0; districtsIdx < districts.Count; districtsIdx++)
        {
            DataBlock district = districts[districtsIdx];
            if (district.GetSubValueI("Pop/GrowthProgress") == 1000)
            {
                DataBlock districtDef = def.GetDistrict(district.ValueS);
                if (districtDef.HasSub("Resource", "Construction"))
                {
                    int popBonus = districtDef.GetSubValueI("Default/Pop*Bonus");
                    int factoryBonus = districtDef.GetSubValueI("Default/Factory*Bonus");
                    int factory = district.GetSubValueI("Factory");
                    bool stateOwned = district.HasSub("Control", "State_Owned");
                    bool isPrivate = district.HasSub("Control", "Private");
                    DataBlock ecoData = null;
                    if (stateOwned)
                    {
                        if (factory > 0) ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("State_Factory_" + factory.ToString());
                        else ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("State_Pop");
                    }
                    else if (isPrivate)
                    {
                        if (factory > 0) ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("Private_Factory_" + factory.ToString());
                        else ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("Private_Pop", "Tax_1");

                    }
                    int bonus = popBonus + factoryBonus * factory;
                    int construction = (ecoData != null ? ecoData.GetSub("Resource").ValueI : 0) + bonus;

                    constructionTotal += construction;
                }
            }
        }
    }
    return constructionTotal;
}*/