using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public static class FleetRaw
{
    static public void RemoveFleet(DataBlock player, DataBlock fleet, DefLibrary def)
    {
        Data.RemoveData(player.GetSub("Fleets"), fleet.Name, fleet.ValueS, def);
    }
    static public void RemoveShip(DataBlock player, DataBlock fleet, DataBlock ship, DefLibrary def)
    {
        Data.RemoveData(fleet, ship.Name, ship.ValueS, def);

        if (fleet.HasSub("ship") == false)
        {
            RemoveFleet(player, fleet, def);
        }
    }
}