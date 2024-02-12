using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    private void GenerateNewMapSave_Players_Ship_Designs(DataBlock player)
    {
        DataBlock shipDesigns = Data.AddData(player, "ShipDesigns", DefLibrary);

        Data.AddData(shipDesigns, "Slot_1", "-", DefLibrary);
        DataBlock design_2 = Data.AddData(shipDesigns, "Slot_2", "Babylon", DefLibrary);
        {
            Data.AddData(design_2, "ShipType", "Medium", DefLibrary);
            DataBlock modules = Data.AddData(design_2, "Modules", DefLibrary);
            {
                Data.AddData(modules, "Computer", "No_Computer", DefLibrary);
                Data.AddData(modules, "Armor", "Alloy", DefLibrary);
                Data.AddData(modules, "ECM", "No_ECM", DefLibrary);
                Data.AddData(modules, "Shield", "No_Shield", DefLibrary);
                Data.AddData(modules, "Warp_Drive", "Warp_Drive", DefLibrary);
                Data.AddData(modules, "Thrusters", "No_Thrusters", DefLibrary);
                DataBlock weapon_1 = Data.AddData(modules, "Weapon:1", "Railgun", DefLibrary);
                Data.AddData(weapon_1, "Count", "2", DefLibrary);
                DataBlock weapon_2 = Data.AddData(modules, "Weapon:2", "Heavy_Laser", DefLibrary);
                Data.AddData(weapon_1, "Count", "1", DefLibrary);
                DataBlock weapon_3 = Data.AddData(modules, "Weapon:3", "Nuclear_Mssiles", DefLibrary);
                Data.AddData(weapon_1, "Count", "2", DefLibrary);
                Data.AddData(modules, "Special:1", "Empty_Special_Slot", DefLibrary);
                Data.AddData(modules, "Special:2", "Empty_Special_Slot", DefLibrary);
            }
        }
        Data.AddData(shipDesigns, "Slot_3", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_4", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_5", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_6", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_7", "-", DefLibrary);

        Data.AddData(shipDesigns, "Slot_1_Obsolete", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_2_Obsolete", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_3_Obsolete", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_4_Obsolete", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_5_Obsolete", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_6_Obsolete", "-", DefLibrary);
        Data.AddData(shipDesigns, "Slot_7_Obsolete", "-", DefLibrary);
    }

    private void GenerateNewMapSave_Players_StartingShip(DataBlock player, DataBlock system)
    {
        DataBlock fleets = Data.AddData(player, "Fleets", DefLibrary);

        DataBlock fleet = Data.AddData(fleets, "Fleet", "Babylon's_Eye", DefLibrary);

        DataBlock ship = Data.AddData(fleet, "Ship", "Babylon_I", DefLibrary);
        Data.AddData(ship, "Design", "Babylon", DefLibrary);
        Data.AddData(ship, "HP", 0, DefLibrary);
        Data.AddData(ship, "XP", 0, DefLibrary);
        Data.AddData(ship, "Marines", 100, DefLibrary);
        Data.AddData(ship, "Fighters", 0, DefLibrary);
        Data.AddData(ship, "Supply", 300, DefLibrary);
    }
}
