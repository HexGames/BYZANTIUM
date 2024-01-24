using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    private DataBlock GeneratePlayerResources()
    {
        DataBlock resources = new DataBlock();

        resources.Type = DefLibrary.GetDBType("Resources", Data.BaseType.NONE);

        Data.AddData(resources, "Energy_income", 0, DefLibrary);
        Data.AddData(resources, "Metal_income", 0, DefLibrary);
        Data.AddData(resources, "Credits", 0, DefLibrary);
        Data.AddData(resources, "Credits_income", 0, DefLibrary);
        Data.AddData(resources, "Authoriy", 0, DefLibrary);
        Data.AddData(resources, "Authoriy_used", 0, DefLibrary);
        Data.AddData(resources, "Influence", 0, DefLibrary);
        Data.AddData(resources, "Influence_used", 0, DefLibrary);
        Data.AddData(resources, "TechPoints_income", 0, DefLibrary);
        Data.AddData(resources, "CivicPoints_income", 0, DefLibrary);

        Data.AddData(resources, "Pops", 0, DefLibrary);

        return resources;
    }

    private DataBlock GeneratePlayerStatus()
    {
        DataBlock resources = new DataBlock();

        resources.Type = DefLibrary.GetDBType("Status", Data.BaseType.NONE);

        Data.AddData(resources, "TechPoints_max", 0, DefLibrary);
        Data.AddData(resources, "TechPoints_tech", "None", DefLibrary);

        return resources;
    }
    private DataBlock GeneratePlayerCivics()
    {
        DataBlock civics = new DataBlock();

        civics.Type = DefLibrary.GetDBType("Civics", Data.BaseType.NONE);

        return civics;
    }

    private DataBlock GeneratePlayerBonuses()
    {
        DataBlock bonuses = new DataBlock();

        bonuses.Type = DefLibrary.GetDBType("Bonuses", Data.BaseType.NONE);

        return bonuses;
    }

    private DataBlock GeneratePlayerColony(LocationData system, DataBlock planet)
    {
        DataBlock colony = new DataBlock();

        colony.Type = DefLibrary.GetDBType("Colony", Data.BaseType.STRING);
        colony.ValueS = planet.ValueS;

        Data.AddData(colony, "Action", "None", DefLibrary);

        DataBlock resources = Data.AddData(colony, "Resources", DefLibrary);

        Data.AddData(resources, "Credits_treasury", 3000, DefLibrary);
        Data.AddData(resources, "Pops", 11000, DefLibrary);

        DataBlock buildings = Data.AddData(colony, "Buildings", DefLibrary);

        Data.AddData(buildings, "Private_Business", 250, DefLibrary);

        Data.AddData(buildings, "Power_Plants", 15, DefLibrary);
        Data.AddData(buildings, "Mines", 10, DefLibrary);
        Data.AddData(buildings, "Goverment_Offices", 5, DefLibrary);
        Data.AddData(buildings, "Diplomatic_Offices", 1, DefLibrary);
        Data.AddData(buildings, "Research_Labs", 5, DefLibrary);
        Data.AddData(buildings, "Cultural_Center", 5, DefLibrary);

        GeneratePlayerColonytSuport(colony);

        return colony;
    }

    private void GeneratePlayerColonytSuport(DataBlock colony)
    {
        DataBlock support = Data.AddData(colony, "Support", DefLibrary);

        // 50
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
}
