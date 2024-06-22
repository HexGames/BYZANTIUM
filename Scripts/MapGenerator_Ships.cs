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
        DataBlock Designs = Data.AddData(player, "Designs", DefLibrary);

        DataBlock design_2 = Data.AddData(Designs, "Design", "Babylon", DefLibrary);
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
    }

    private void GenerateNewMapSave_Players_StartingShip(DataBlock player, DataBlock star)
    {
        DataBlock fleets = Data.AddData(player, "Fleets", DefLibrary);

        DataBlock fleet = Data.AddData(fleets, "Fleet", "Babylon's_Eye", DefLibrary);

        Data.AddData(fleet, "Link:Star", star.ValueS, DefLibrary); // no StarData yet
        Data.AddData(star, "Link:Player:Fleet", player.ValueS + ":" + fleet.ValueS, DefLibrary); // no StarData yet

        DataBlock ship = Data.AddData(fleet, "Ship", "Babylon_I", DefLibrary);
        Data.AddData(ship, "Design", "Babylon", DefLibrary);
        Data.AddData(ship, "HP", 0, DefLibrary);
        Data.AddData(ship, "XP", 0, DefLibrary);
        Data.AddData(ship, "Marines", 100, DefLibrary);
        Data.AddData(ship, "Fighters", 0, DefLibrary);
        Data.AddData(ship, "Supply", 300, DefLibrary);

        // second fleet
        fleet = Data.AddData(fleets, "Fleet", "Byblos Hammer", DefLibrary);

        Data.AddData(fleet, "Link:Star", star.ValueS, DefLibrary); // no StarData yet
        Data.AddData(star, "Link:Player:Fleet", player.ValueS + ":" + fleet.ValueS, DefLibrary); // no StarData yet

        ship = Data.AddData(fleet, "Ship", "Babylon_II", DefLibrary);
        Data.AddData(ship, "Design", "Babylon", DefLibrary);
        Data.AddData(ship, "HP", 0, DefLibrary);
        Data.AddData(ship, "XP", 0, DefLibrary);
        Data.AddData(ship, "Marines", 100, DefLibrary);
        Data.AddData(ship, "Fighters", 0, DefLibrary);
        Data.AddData(ship, "Supply", 300, DefLibrary);
    }
}
