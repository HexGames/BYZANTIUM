using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    // game


    // generator
    public void GenerateNewMapSave_Players_StartingColony_SystemResources(DataBlock system, int level)
    {
        DataBlock resources = Data.AddData(system, "Resources", DefLibrary);

        int popMax = 80;
        int pops = Mathf.Max(popMax * 5 / level, 1);

        Data.AddData(resources, "Pops*Pops", pops * 1000, DefLibrary);
        Data.AddData(resources, "Pops*PopsMax", popMax * 1000, DefLibrary);
        Data.AddData(resources, "Pops*Growth", 200, DefLibrary);
        Data.AddData(resources, "Pops*GrowthBonus", 0, DefLibrary);
        Data.AddData(resources, "Pops*GrowthPenalty", 0, DefLibrary);

        Data.AddData(resources, "Buildings*Factories", popMax * 5 / level, DefLibrary);
        Data.AddData(resources, "Buildings*FactoriesMax", popMax, DefLibrary);
        Data.AddData(resources, "Buildings*PrivateBusinesses", 0, DefLibrary);
        Data.AddData(resources, "Buildings*PrivateBusinessesMax", 0, DefLibrary);
        Data.AddData(resources, "Buildings*MilitaryBases", level, DefLibrary);
        Data.AddData(resources, "Buildings*MilitaryBasesMax", 5, DefLibrary);

        Data.AddData(resources, "Focus*Buildings", 10, DefLibrary);
        Data.AddData(resources, "Focus*Factories", 10, DefLibrary);
        Data.AddData(resources, "Focus*Research", 5, DefLibrary);
        Data.AddData(resources, "Focus*Influence", 5, DefLibrary);
        Data.AddData(resources, "Focus*Ships", 0, DefLibrary);

        Data.AddData(resources, "Energy*Income", 0, DefLibrary);
        Data.AddData(resources, "Minerals*Income", 0, DefLibrary);
        Data.AddData(resources, "Production*Income", 0, DefLibrary);
        Data.AddData(resources, "Shipbuilding*Income", 0, DefLibrary);

        Data.AddData(resources, "Research*Income", 0, DefLibrary);
        Data.AddData(resources, "Culture*Income", 0, DefLibrary);
        Data.AddData(resources, "BC*Income", 0, DefLibrary);
        Data.AddData(resources, "Authority*Used", 0, DefLibrary);
    }
}
