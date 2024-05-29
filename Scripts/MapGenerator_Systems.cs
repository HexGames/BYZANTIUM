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
