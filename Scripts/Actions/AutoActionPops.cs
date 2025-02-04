using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class AutoActionPops
{
    static public void EndTurn(SystemData system)
    {
        int growth = system.Pops_PerTurn.GrowthTotal;
        system.Pops_PerTurn.GrowthProgress += growth;
        if (system.Pops_PerTurn.GrowthProgress >= system.Pops_PerTurn.GrowthProgressMax)
        {
            for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
            {
                ColonyData colony = system.Colonies[colonyIdx];
                for (int idx = 0; idx < colony.Districts.Count; idx++)
                {
                    //if (colony.Districts[idx].GetPop().GetProgress() < 1000)
                    //{
                    //    DistrictRaw.SetPopFullGrowth(colony.Districts[idx]._Data, Game.self.Def);
                    //}
                }
            }

            PlanetData chosenNextPlanet = null;
            int newGrowth = 0;
            int remainingPops = 0;
            for (int planetIdx = 0; planetIdx < system.Star.Planets.Count; planetIdx++)
            {
                PlanetData planet = system.Star.Planets[planetIdx];
                //if (planet.Data.GetSubValueI("PopsMax") > 0)
                //{
                //    int planetGrowth = PlanetRaw.GetPopGrowth(planet.Data, Game.self.Def);
                //    if (planet.Colony == null && (chosenNextPlanet == null || planetGrowth > newGrowth))
                //    {
                //        chosenNextPlanet = planet;
                //        newGrowth = planetGrowth;
                //        remainingPops = planet.Data.GetSubValueI("PopsMax");
                //    }
                //}
            }

            ColonyData chosenNextColony = null;
            for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
            {
                ColonyData colony = system.Colonies[colonyIdx];

                //int popsMax = colony.Planet.Data.GetSubValueI("PopsMax");
                //if (popsMax > colony.Districts.Count)
                //{
                //    int colonyExtraGrowth = ColonyRaw.GetGrowth(colony.Planet.Data, colony.Data, true, Game.self.Def) - ColonyRaw.GetGrowth(colony.Planet.Data, colony.Data, false, Game.self.Def);
                //    int colonyRemainingPops = popsMax - colony.Districts.Count;
                //    if ((chosenNextColony == null && chosenNextPlanet == null) || colonyExtraGrowth > newGrowth || (colonyExtraGrowth == newGrowth && colonyRemainingPops > remainingPops))
                //    {
                //        chosenNextColony = colony;
                //        newGrowth = colonyExtraGrowth;
                //        remainingPops = colonyRemainingPops;
                //    }
                //}
            }

            //GD.Print("Growth for " + system.SystemName);
            //if (chosenNextColony != null)
            //{
            //    //GD.Print("123" + system.SystemName);
            //    DataBlock districtRaw = DistrictRaw.CreateNewDistrictAndPop(system.Data, chosenNextColony.Data, false, Game.self.Def);
            //    DistrictData district = new DistrictData(districtRaw, chosenNextColony);
            //    district.Economy_PerTurn = new DistrictEconomyWrapper(district);
            //    chosenNextColony.Districts.Add(district);
            //}
            //else if (chosenNextPlanet != null)
            //{
            //    //GD.Print("321" + system.SystemName);
            //    DataBlock colonyRaw = ColonyRaw.CreateNewColony_Habitable(system._Player.Data, system.Star.Data, chosenNextPlanet.Data, system.Data, false, false, Game.self.Def);
            //    ColonyData colony = Game.self.Map.Data.GenerateGameFromData_Player_System_Colony(colonyRaw, system);
            //    colony.Init_DistrictData();
            //    colony.Init_Resources();
            //}

            system.Pops_PerTurn.GrowthProgress -= system.Pops_PerTurn.GrowthProgressMax;
        }

        // save growth progress
        //if (growth > 0)
        {
            //bool foundDistrict = false;
            //ColonyData colony = system.GetGrowthColony();
            //for (int idx = 0; idx < colony.Districts.Count; idx++)
            //{
            //    if (colony.Districts[idx].Pop.Data.GetSubValueI("GrowthProgress") < 1000)
            //    {
            //        colony.Districts[idx].Pop.Data.SetSubValueI("GrowthProgress", system.Pops_PerTurn.GrowthProgress, Game.self.Def);
            //        //foundDistrict = true;
            //    }
            //}
            //if (foundDistrict == false)
            //{
            //    GD.Print("Problem" + system.SystemName);
            //}
        }
    }
}