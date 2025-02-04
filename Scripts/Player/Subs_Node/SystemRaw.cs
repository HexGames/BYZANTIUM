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
        Data.AddData(actionTax, "Value", 2, def);
        DataBlock actionBuget = Data.AddData(system, "ActionBuget", def);
        Data.AddData(actionBuget, "Value", 2, def);
        DataBlock actionControl = Data.AddData(system, "ActionControl", def);
        Data.AddData(actionControl, "Value", 2, def);
        DataBlock actionWelfare = Data.AddData(system, "ActionWelfare", def);
        Data.AddData(actionWelfare, "Value", 2, def);

        DataBlock wealth = Data.AddData(system, "Wealth", 0, def);

        DataBlock actionBuildShip = Data.AddData(system, "ActionBuildShip", def);
        Data.AddData(actionBuildShip, "Design", "Babylon", def);
        Data.AddData(actionBuildShip, "LastDesign", "Babylon", def);
        Data.AddData(actionBuildShip, "Progress", 0, def);
        Data.AddData(actionBuildShip, "Overflow", 0, def);

        if (chosenPlanet == null)
        {
            chosenPlanet = StarRaw.GetBestHabitablePlanet(star);
        }
        Data.AddData(system, "Colony_List", def);
        DataBlock colony = ColonyRaw.CreateNewColony_Habitable(player, star, chosenPlanet, system, true, empireCapital, def);

        Data.AddData(system, "Trades", def);

        Data.AddData(system, "Link:Star", star.ValueS, def); // no StarData yet
        Data.AddData(star, "Link:Player:Sector:System", player.ValueS + ":" + system.ValueS, def); // no SystemData yet

        return system;
    }

    public static void GrowSystem(DataBlock player, DataBlock system, DataBlock star, int popsLevel, int economyLevel, DefLibrary def)
    {
        if (popsLevel >= 3)
        {
            Array<DataBlock> planets = star.GetSub("Planet_List").GetSubs();
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                DataBlock planet = planets[planetIdx];
                int planetMaxPops = PlanetRaw.GetBaseMaxPops(planet, Game.self.Def);
                if (planet.HasSub("Link:Player:System:Colony") == false && planetMaxPops > 0)
                {
                    DataBlock colony = ColonyRaw.CreateNewColony_Habitable(player, star, planet, system, false, false, def);
                }
            }
        }

        Array<DataBlock> colonies = system.GetSub("Colony_List").GetSubs();
        for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        {
            DataBlock colony = colonies[colonyIdx];
            GrowColony(player, system, colony, star, popsLevel, economyLevel, colony.HasSub("Capital"), def);
        }

        return;

        system.SetSubValueI("Wealth", "Current", 10 * economyLevel, def);
        system.SetSubValueI("Wealth", "Level", economyLevel, def);
    }

    public static void GrowColony(DataBlock player, DataBlock system, DataBlock colony, DataBlock star, int popsLevel, int economyLevel, bool empireCapital, DefLibrary def)
    {
        DataBlock ruralDistrict = ColonyRaw.GetFirstDistrictOfType(colony, "Rural_District", def);
        DataBlock urbanDistrict = ColonyRaw.GetFirstDistrictOfType(colony, "Urban_District", def);
        DataBlock industrialDistrict = ColonyRaw.GetFirstDistrictOfType(colony, "Industrial_District", def);

        int currentPops = ColonyRaw.GetPopsTotal(colony);
        int maxPops = colony.GetSubValueI("PopsMax");

        int growPops = Mathf.Clamp((maxPops - currentPops) * ( popsLevel - 1 ) / 4, 1, maxPops - currentPops);

        for (int i = 0; i < growPops / 3; i++)
        {
            int random = Game.RNG.RandiRange(0,2);
            if (random == 0) DistrictRaw.CreateNewPop(industrialDistrict, def);
            else if (random == 1) DistrictRaw.CreateNewPop(urbanDistrict, def);
            else DistrictRaw.CreateNewPop(ruralDistrict, def);
            growPops--;
        }

        while (growPops > 0)
        {
            if (growPops > 0)
            {
                DistrictRaw.CreateNewPop(ruralDistrict, def);
                growPops--;
            }
            if (growPops > 0)
            {
                DistrictRaw.CreateNewPop(urbanDistrict, def);
                growPops--;
            }
            if (growPops > 0)
            {
                DistrictRaw.CreateNewPop(industrialDistrict, def);
                growPops--;
            }
        }

        int levels = Mathf.RoundToInt(Mathf.Pow(economyLevel, 2.0));
        for (int i = 0; i < levels / 3; i++)
        {
            int random = Game.RNG.RandiRange(0,2);
            if (random == 0) DistrictRaw.LevelUp(industrialDistrict, def);
            else if (random == 1) DistrictRaw.LevelUp(urbanDistrict, def);
            else DistrictRaw.LevelUp(ruralDistrict, def);
            levels--;
        }

        while (growPops > 0)
        {
            if (levels > 0)
            {
                DistrictRaw.LevelUp(ruralDistrict, def);
                growPops--;
            }
            if (levels > 0)
            {
                DistrictRaw.LevelUp(urbanDistrict, def);
                levels--;
            }
            if (levels > 0)
            {
                DistrictRaw.LevelUp(industrialDistrict, def);
                levels--;
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
}