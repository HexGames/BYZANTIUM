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
    public void GenerateNewMapSave_Players_StartingColony_SystemResources(DataBlock system, int level, bool capital)
    {
        DataBlock resources = Data.AddData(system, "Resources", DefLibrary);

        Data.AddData(resources, "FactoryCost", 200, DefLibrary);

        Data.AddData(resources, "Districts*FocusBase", 10, DefLibrary);
        Data.AddData(resources, "Factories*FocusBase", 10, DefLibrary);
        Data.AddData(resources, "Research*FocusBase", 5, DefLibrary);
        Data.AddData(resources, "Shipbuilding*FocusBase", 0, DefLibrary);

        if (capital)
        {
            Data.AddData(resources, "Shipbuilding*FocusChosen", 25, DefLibrary);
        }
        else
        {
            switch(RNG.RandiRange(0,2))
            {
                case 0: Data.AddData(resources, "Districts*FocusChosen", 25, DefLibrary); break;
                case 1: Data.AddData(resources, "Factories*FocusChosen", 25, DefLibrary); break;
                case 2: Data.AddData(resources, "Research*FocusChosen", 25, DefLibrary); break;
            }
        }
    }
}
