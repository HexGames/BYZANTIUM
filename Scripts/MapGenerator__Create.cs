using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    public static DataBlock Create_Colony_Resources(DataBlock colony, DefLibrary df)
    {
        DataBlock resources = Data.AddData(colony, "Resources", df);

        Data.AddData(resources, "Energy", 0, df);
        Data.AddData(resources, "Energy*Used", 0, df);
        Data.AddData(resources, "Minerals", 0, df);
        Data.AddData(resources, "Minerals*Used", 0, df);
        Data.AddData(resources, "Production*Income", 0, df);
        Data.AddData(resources, "Shipbuilding*Income", 0, df);

        Data.AddData(resources, "Trade", 0, df);
        Data.AddData(resources, "Trade*Used", 0, df);
        Data.AddData(resources, "TechPoints*Income", 0, df);
        Data.AddData(resources, "CulturePoints*Income", 0, df);
        Data.AddData(resources, "Authority", 0, df);
        Data.AddData(resources, "Authority*Used", 0, df);
        Data.AddData(resources, "Influence", 0, df);
        Data.AddData(resources, "Influence*Used", 0, df);
        Data.AddData(resources, "BC*Income", 0, df);

        return resources;
    }

    public static DataBlock Create_Colony_Buildings(DataBlock colony, DefLibrary df)
    {
        DataBlock buildings = Data.AddData(colony, "Buildings", df);

        return buildings;
    }
}
