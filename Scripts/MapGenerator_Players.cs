using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    private void GenerateNewMapSave_Players_Empire(DataBlock player, DataBlock empireInfo)
    {
        DataBlock empire = Data.AddData(player, "Empire", DefLibrary);

        Data.AddData(empire, "Flag", empireInfo.GetSub("Flag").ValueS, DefLibrary);
    }

    private void GenerateNewMapSave_Players_Resources(DataBlock player)
    {
        DataBlock resources = Data.AddData(player, "Resources", DefLibrary);

        Data.AddData(resources, "Wealth", 3000, DefLibrary);

        Data.AddData(resources, "BC", 0, DefLibrary);
        Data.AddData(resources, "BC*Income", 0, DefLibrary);
        Data.AddData(resources, "Trade", 0, DefLibrary);
        Data.AddData(resources, "Trade*Used", 0, DefLibrary);
        Data.AddData(resources, "TechPoints", 0, DefLibrary);
        Data.AddData(resources, "TechPoints*Income", 0, DefLibrary);
        Data.AddData(resources, "CulturePoints", 0, DefLibrary);
        Data.AddData(resources, "CulturePoints*Income", 0, DefLibrary);
        Data.AddData(resources, "Authority", 0, DefLibrary);
        Data.AddData(resources, "Authority*Used", 0, DefLibrary);
        Data.AddData(resources, "Influence", 0, DefLibrary);
        Data.AddData(resources, "Influence*Used", 0, DefLibrary);
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
