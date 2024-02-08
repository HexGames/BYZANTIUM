using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    private void GenerateNewMapSave_Players_Resources(DataBlock player)
    {
        DataBlock resources = Data.AddData(player, "Resources", DefLibrary);

        Data.AddData(resources, "Energy:Income", 0, DefLibrary);
        Data.AddData(resources, "Metal:Income", 0, DefLibrary);
        Data.AddData(resources, "Credits", 0, DefLibrary);
        Data.AddData(resources, "Credits:Income", 0, DefLibrary);
        Data.AddData(resources, "Authoriy", 0, DefLibrary);
        Data.AddData(resources, "Authoriy:Used", 0, DefLibrary);
        Data.AddData(resources, "Influence", 0, DefLibrary);
        Data.AddData(resources, "Influence:Used", 0, DefLibrary);
        Data.AddData(resources, "TechPoints:Income", 0, DefLibrary);
        Data.AddData(resources, "CivicPoints:Income", 0, DefLibrary);

        Data.AddData(resources, "Pops", 0, DefLibrary);
    }

    private void GenerateNewMapSave_Players_Status(DataBlock player)
    {
        DataBlock status = Data.AddData(player, "Status", DefLibrary);

        Data.AddData(status, "TechPoints_max", 0, DefLibrary);
        Data.AddData(status, "TechPoints_tech", "None", DefLibrary);
    }

    private void GenerateNewMapSave_Players_Civics(DataBlock player)
    {
        DataBlock civics = Data.AddData(player, "Civics", DefLibrary);
    }

    private void GenerateNewMapSave_Players_Bonuses(DataBlock player)
    {
        DataBlock bonuses = Data.AddData(player, "Bonuses", DefLibrary);
    }
}
