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
        DataBlock resources = Data.AddData(colony, "Resources", DefLibrary);

        Data.AddData(resources, "Credits:private", 3000, DefLibrary);
        Data.AddData(resources, "Minerals:private", 3000, DefLibrary);
        Data.AddData(resources, "Energy:private", 3000, DefLibrary);
        Data.AddData(resources, "Pops", 11000, DefLibrary);
    }
    private void GenerateNewMapSave_Players_StartingColony_Budget(DataBlock colony)
    {
        DataBlock budget = Data.AddData(colony, "Budget", DefLibrary);

        Data.AddData(budget, "Cooldown", 5, DefLibrary);

        DataBlock minerals = Data.AddData(budget, "Minerals", DefLibrary);
        {
            DataBlock project = Data.AddData(minerals, "Project", "Buildings", DefLibrary);
            Data.AddData(project, "Locked", 1, DefLibrary);
            Data.AddData(project, "Value", 6, DefLibrary);
        }
        {
            DataBlock project = Data.AddData(minerals, "Project", "Shipyard", DefLibrary);
            Data.AddData(project, "Locked", 1, DefLibrary);
            Data.AddData(project, "Value", 6, DefLibrary);
        }
        {
            DataBlock project = Data.AddData(minerals, "Project", "Project", DefLibrary);
            Data.AddData(project, "Locked", 1, DefLibrary);
            Data.AddData(project, "Value", 3, DefLibrary);
        }
        {
            DataBlock project = Data.AddData(minerals, "Project", "Galactic_Project", DefLibrary);
            Data.AddData(project, "Locked", 1, DefLibrary);
            Data.AddData(project, "Value", 0, DefLibrary);
        }
        {
            DataBlock project = Data.AddData(minerals, "Treasury", "Credits", DefLibrary);
            Data.AddData(project, "Locked", 0, DefLibrary);
            Data.AddData(project, "Value", 1, DefLibrary);
        }
        DataBlock energy = Data.AddData(budget, "Energy", DefLibrary);
        {
            DataBlock project = Data.AddData(energy, "Project", "Growth", DefLibrary);
            Data.AddData(project, "Locked", 1, DefLibrary);
            Data.AddData(project, "Value", 8, DefLibrary);
        }
        {
            DataBlock project = Data.AddData(energy, "Project", "Extra_Research", DefLibrary);
            Data.AddData(project, "Locked", 1, DefLibrary);
            Data.AddData(project, "Value", 8, DefLibrary);
        }
        {
            DataBlock project = Data.AddData(energy, "Project", "Terraforming", DefLibrary);
            Data.AddData(project, "Locked", 1, DefLibrary);
            Data.AddData(project, "Value", 0, DefLibrary);
        }
        {
            DataBlock project = Data.AddData(energy, "Treasury", "Credits", DefLibrary);
            Data.AddData(project, "Locked", 0, DefLibrary);
            Data.AddData(project, "Value", 1, DefLibrary);
        }
    }

    private void GenerateNewMapSave_Players_StartingColony_Buildings(DataBlock colony)
    {
        DataBlock buildings = Data.AddData(colony, "Buildings", DefLibrary);

        Data.AddData(buildings, "Private_Business", 250, DefLibrary);

        Data.AddData(buildings, "Power_Plants", 15, DefLibrary);
        Data.AddData(buildings, "Mines", 10, DefLibrary);
        Data.AddData(buildings, "Goverment_Offices", 5, DefLibrary);
        Data.AddData(buildings, "Diplomatic_Offices", 1, DefLibrary);
        Data.AddData(buildings, "Research_Labs", 5, DefLibrary);
        Data.AddData(buildings, "Cultural_Center", 5, DefLibrary);
    }

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
    private void GenerateNewMapSave_Players_StartingColony_Bonuses(DataBlock colony)
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
    }
}
