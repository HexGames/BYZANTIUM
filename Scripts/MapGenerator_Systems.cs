using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    private void GenerateNewMapSave_Players_StartingColony_SystemResources(DataBlock system)
    {
        DataBlock resources = Data.AddData(system, "Resources", DefLibrary);

        //Data.AddData(resources, "Pops", 25000, DefLibrary);
        //Data.AddData(resources, "Pops*Used", 16000, DefLibrary);
        //Data.AddData(resources, "Growth", 200, DefLibrary);

        Data.AddData(resources, "Energy", 0, DefLibrary);
        Data.AddData(resources, "Energy*Used", 0, DefLibrary);
        Data.AddData(resources, "Minerals", 0, DefLibrary);
        Data.AddData(resources, "Minerals*Used", 0, DefLibrary);
        Data.AddData(resources, "Production*Income", 0, DefLibrary);
        Data.AddData(resources, "Shipbuilding*Income", 0, DefLibrary);
        //Data.AddData(resources, "PrivateIndustry", 0, DefLibrary);

        Data.AddData(resources, "Trade", 0, DefLibrary);
        Data.AddData(resources, "Trade*Used", 0, DefLibrary);
        Data.AddData(resources, "TechPoints*Income", 0, DefLibrary);
        Data.AddData(resources, "CulturePoints*Income", 0, DefLibrary);
        Data.AddData(resources, "Authority", 0, DefLibrary);
        Data.AddData(resources, "Authority*Used", 0, DefLibrary);
        Data.AddData(resources, "Influence", 0, DefLibrary);
        Data.AddData(resources, "Influence*Used", 0, DefLibrary);
        Data.AddData(resources, "BC*Income", 0, DefLibrary);
    }
}
