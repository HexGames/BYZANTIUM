using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    private void GenerateNewMapSave_Players_StartingColony_Resources(DataBlock colony)
    {
        DataBlock resources = Create_Colony_Resources(colony, DefLibrary);

        //Data.AddData(resources, "Wealth*Stockpile", 3000, DefLibrary);

        Data.AddData(resources, "Pops*Pops", 12000, DefLibrary);
        Data.AddData(resources, "Pops*PopsMax", 16000, DefLibrary);
        Data.AddData(resources, "Pops*Growth", 200, DefLibrary);
    }

    private void GenerateNewMapSave_Players_StartingColony_SpacePort(DataBlock colony)
    {
        DataBlock buildings = Create_Colony_Buildings(colony, DefLibrary);

        Data.AddData(buildings, "Building", "Starport", DefLibrary);
    }

    private void GenerateNewMapSave_Players_StartingColony_Buildings(DataBlock colony)
    {
        DataBlock buildings = Create_Colony_Buildings(colony, DefLibrary);

        //Data.AddData(buildings, "Private_Business", 250, DefLibrary);

        Data.AddData(buildings, "Building", "Capital", DefLibrary);
        Data.AddData(buildings, "Building", "Shipyard_I", DefLibrary);
        Data.AddData(buildings, "Building", "Cities", DefLibrary);
        Data.AddData(buildings, "Building", "Colonial_Oversight", DefLibrary);
        Data.AddData(buildings, "Building", "District", DefLibrary);
        //Data.AddData(buildings, "Research_Labs", 2, DefLibrary);
        //Data.AddData(buildings, "Culture_Centers", 2, DefLibrary);
    }

    /*private void GenerateNewMapSave_Players_StartingColony_Construction(DataBlock colony, DataBlock system)
    {
        DataBlock campaign = Data.AddData(colony, "ActionConstruction", DefLibrary);

        Data.AddData(campaign, "Priority", 0, DefLibrary);

        Data.AddData(campaign, "Count", 0, DefLibrary);
        Data.AddData(campaign, "Name", "none", DefLibrary);
        Data.AddData(campaign, "Link:System:Colony", system.ValueS + ":" + colony.ValueS, DefLibrary);
        Data.AddData(campaign, "Progress:Current", 1, DefLibrary);
        Data.AddData(campaign, "Progress:Total", 3, DefLibrary);

        Data.AddData(campaign, "Overflow", 0, DefLibrary);
    }

    private void GenerateNewMapSave_Players_StartingColony_Shipbuilding(DataBlock colony, DataBlock system)
    {
        DataBlock campaign = Data.AddData(colony, "ActionShipbuilding", DefLibrary);

        Data.AddData(campaign, "Priority", 0, DefLibrary);

        Data.AddData(campaign, "Count", 0, DefLibrary);
        Data.AddData(campaign, "Name", "none", DefLibrary);
        Data.AddData(campaign, "Link:System:Colony", system.ValueS + ":" + colony.ValueS, DefLibrary);
        Data.AddData(campaign, "Progress:Current", 0, DefLibrary);
        Data.AddData(campaign, "Progress:Total", 3, DefLibrary);

        Data.AddData(campaign, "Overflow", 0, DefLibrary);
    }*/

    private void GenerateNewMapSave_Players_StartingColony_Support(DataBlock colony)
    {
        DataBlock support = Data.AddData(colony, "Support", DefLibrary);// 50

        DataBlock faction = null;
        faction = Data.AddData(support, "Democratic", DefLibrary);
        Data.AddData(faction, "Facton_Size", 30, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Industrialist", DefLibrary);
        Data.AddData(faction, "Facton_Size", 20, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);

        // 20
        faction = Data.AddData(support, "Ecologist", DefLibrary);
        Data.AddData(faction, "Facton_Size", 5, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Religious", DefLibrary);
        Data.AddData(faction, "Facton_Size", 5, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Liberty", DefLibrary);
        Data.AddData(faction, "Facton_Size", 5, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Unity", DefLibrary);
        Data.AddData(faction, "Facton_Size", 5, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);

        // 30
        faction = Data.AddData(support, "Pacifist", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Millitarist", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Scientist", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Bio-engineering", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Mechanist", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Autocratic", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Xenofile", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Xenofob", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Federation", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
        faction = Data.AddData(support, "Empire", DefLibrary);
        Data.AddData(faction, "Facton_Size", 3, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
    }

    /*private void GenerateNewMapSave_Players_StartingColony_Bonuses(DataBlock colony)
    {
        DataBlock bonuses = Data.AddData(colony, "Bonuses", DefLibrary);
    }

    private void GenerateNewMapSave_Players_StartingStation_Resources(DataBlock colony)
    {
        DataBlock resources = Data.AddData(colony, "Resources", DefLibrary);

        Data.AddData(resources, "Credits:private", 100, DefLibrary);
        Data.AddData(resources, "Pops", 2, DefLibrary);
    }

    private void GenerateNewMapSave_Players_StartingStation_Buildings(DataBlock colony)
    {
        DataBlock buildings = Data.AddData(colony, "Buildings", DefLibrary);

        Data.AddData(buildings, "Living_Quarters", 2, DefLibrary);
        Data.AddData(buildings, "Shipyard", 1, DefLibrary);
    }

    private void GenerateNewMapSave_Players_StartingStation_Support(DataBlock colony)
    {
        DataBlock support = Data.AddData(colony, "Support", DefLibrary);// 50

        DataBlock faction = null;
        faction = Data.AddData(support, "Industrialist", DefLibrary);
        Data.AddData(faction, "Facton_Size", 100, DefLibrary);
        Data.AddData(faction, "Happiness", 7, DefLibrary);
    }
    private void GenerateNewMapSave_Players_StartingStation_Bonuses(DataBlock colony)
    {
        DataBlock bonuses = Data.AddData(colony, "Bonuses", DefLibrary);
    }*/
}
