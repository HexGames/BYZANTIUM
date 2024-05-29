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

        Data.AddData(resources, "Energy*Income", 0, df);
        Data.AddData(resources, "Minerals*Income", 0, df);
        Data.AddData(resources, "Production*Income", 0, df);
        Data.AddData(resources, "Shipbuilding*Income", 0, df);

        Data.AddData(resources, "Research*Income", 0, df);
        Data.AddData(resources, "Culture*Income", 0, df);
        Data.AddData(resources, "BC*Income", 0, df);
        Data.AddData(resources, "Authority*Used", 0, df);

        return resources;
    }

    public static DataBlock Create_Colony_Buildings(DataBlock colony, DefLibrary df)
    {
        DataBlock buildings = Data.AddData(colony, "Buildings", df);

        return buildings;
    }
}
