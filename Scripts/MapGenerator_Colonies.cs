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
    public DataBlock GenerateNewMapSave_Players_Colony(DataBlock colonyList, DataBlock star, DataBlock planet, DataBlock player, DataBlock system, int level)
    {
        DataBlock empireData = DefLibrary.GetEmpire(player.ValueS);

        DataBlock colony = Data.AddData(colonyList, "Colony", planet.ValueS, DefLibrary);

        if (planet.HasSub("Habitable"))
        {
            int popsMax = planet.GetSub("Size").ValueI * planet.GetSub("PopMaxPerSize").ValueI * 200 * level;

            Data.AddData(colony, "Type", "Urban_World", DefLibrary);

            DataBlock popsList = Data.AddData(colony, "Pops_List", DefLibrary);
            DataBlock pops = Data.AddData(popsList, "Pops", "Pops_1", DefLibrary);
            Data.AddData(pops, "Pops", Mathf.Max(1000, popsMax), DefLibrary);
            Data.AddData(pops, "Specie", empireData.GetSub("Specie").ValueS, DefLibrary);
            Data.AddData(pops, "Ethic", empireData.GetSub("Ethic").ValueS, DefLibrary);
            Data.AddData(pops, "LoyalTo", player.Name, DefLibrary);

            DataBlock structures = Data.AddData(colony, "Buildings", DefLibrary);
            Data.AddData(structures, "Factories", popsMax, DefLibrary);
            Data.AddData(structures, "Bases", 1000 * level, DefLibrary);

            //GenerateNewMapSave_Players_StartingColony_Resources(colony);
        }
        else if (planet.HasSub("Uninhabitable"))
        {
            Data.AddData(colony, "Type", "Mining_Outposts", DefLibrary);
        }
        else if (planet.HasSub("Asteroids"))
        {
            Data.AddData(colony, "Type", "Asteroid_Mines", DefLibrary);
        }
        else if (planet.HasSub("Gas_Giant"))
        {
            Data.AddData(colony, "Type", "Reserch_Stations", DefLibrary);
        }

        Data.AddData(colony, "Link:Star:Planet", star.ValueS + ":" + planet.ValueS, DefLibrary); // no PlanetData yet
        Data.AddData(planet, "Link:Player:System:Colony", player.ValueS + ":" + system.ValueS + ":" + colony.ValueS, DefLibrary); // no ColonyData yet

        return colony;
    }
}
