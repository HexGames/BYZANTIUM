using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    // generator
    public DataBlock GenerateNewMapSave_Players_Colony(DataBlock star, DataBlock planet, DataBlock player, DataBlock system, int level, bool capital = false)
    {
        DataBlock colony = MapGenerator.CreateNewColony(star, planet, player, system, level, DefLibrary, capital);
        return colony;
    }

    public static DataBlock CreateNewColony(DataBlock star, DataBlock planet, DataBlock player, DataBlock system, int level, DefLibrary def, bool capital)
    {
        DataBlock empireData = def.GetEmpire(player.ValueS);

        DataBlock colonyList = system.GetSub("Colony_List");
        DataBlock colony = Data.AddData(colonyList, "Colony", planet.ValueS, def);

        int popsLevel = 0;
        if (planet.HasSub("Habitable"))
        {
            popsLevel = planet.GetSub("Size").ValueI * 30 * 200 * level;
            Data.AddData(colony, "Type", "Urban_World", def);

            DataBlock popsList = Data.AddData(colony, "Pops_List", def);
            DataBlock pops = Data.AddData(popsList, "Pops", "Pops_1", def);
            Data.AddData(pops, "Pops", Mathf.Max(1000, popsLevel), def);
            Data.AddData(pops, "Specie", empireData.GetSub("Specie").ValueS, def);
            Data.AddData(pops, "Ethic", empireData.GetSub("Ethic").ValueS, def);
            Data.AddData(pops, "LoyalTo", player.Name, def);

            DataBlock structures = Data.AddData(colony, "Buildings", def);
            Data.AddData(structures, "Factories", popsLevel, def);
            Data.AddData(structures, "Bases", 1000 * level, def);

            //GenerateNewMapSave_Players_StartingColony_Resources(colony);
        }
        //else if (planet.HasSub("Uninhabitable"))
        //{
        //    Data.AddData(colony, "Type", "Mining_Outposts", DefLibrary);
        //}
        //else if (planet.HasSub("Asteroids"))
        //{
        //    Data.AddData(colony, "Type", "Asteroid_Mines", DefLibrary);
        //}
        //else if (planet.HasSub("Gas_Giant"))
        //{
        //    Data.AddData(colony, "Type", "Reserch_Stations", DefLibrary);
        //}

        DataBlock districts = Data.AddData(colony, "Districts", def);
        if (planet.HasSub("Habitable"))
        {
            for (int n = 0; n < planet.GetSub("Size").ValueI; n++)
            {
                DataBlock slot = Data.AddData(districts, "District", def);
                if (n == 0 && capital)
                {
                    slot.SetValueS("Capital", def);
                }
                else if (popsLevel >= 30000 * n && level >= 0)
                {
                    slot.SetValueS(AI_District.SuggestDistrictForPlanet(planet, def).ValueS, def);
                }
                else
                {
                    Data.AddData(slot, "RequiredPops", 30 * n, def);
                }
            }
        }
        else if (level >= 0)
        {
            Data.AddData(districts, "District", AI_District.SuggestDistrictForPlanet(planet, def).ValueS, def);
        }
        else
        {
            Data.AddData(districts, "District", def);
        }

        //DataBlock resources = Data.AddData(colony, "Resources", def);
        //Data.AddData(resources, "Shipbuilding*Income", 0, def);
        //Data.AddData(resources, "Research*Income", 0, def);
        //Data.AddData(resources, "Influence*Income", 0, def);
        //Data.AddData(resources, "BC*Income", 0, def);


        Data.AddData(colony, "Link:Star:Planet", star.ValueS + ":" + planet.ValueS, def); // no PlanetData yet
        Data.AddData(planet, "Link:Player:System:Colony", player.ValueS + ":" + system.ValueS + ":" + colony.ValueS, def); // no ColonyData yet

        return colony;
    }
}
