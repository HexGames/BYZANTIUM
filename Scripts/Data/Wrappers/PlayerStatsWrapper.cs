using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

public class PlayerStatsWrapper
{
    public int Pops;
    public int Systems;
    public int DistrictLevels;
    public int FleetPower;

    PlayerData _Player;

    public PlayerStatsWrapper(PlayerData player)
    {
        _Player = player;
    }

    public void Refresh_6()
    {
        Pops = 0;
        Systems = _Player.Systems.Count;
        DistrictLevels = 0;
        FleetPower = 0;
        for (int systemIdx = 0; systemIdx < _Player.Systems.Count; systemIdx++)
        {
            SystemData system = _Player.Systems[systemIdx];
            Pops += system.Pops_PerTurn.Pops;
            for (int coloniyIdx = 0; coloniyIdx < system.Colonies.Count; coloniyIdx++)
            {
                ColonyData colony = system.Colonies[coloniyIdx];
                for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
                {
                    string name = colony.Districts[districtIdx].DistrictDef.Name;
                    if (name.EndsWith("_III"))
                    {
                        DistrictLevels += 3;
                    }
                    else if (name.EndsWith("_II"))
                    {
                        DistrictLevels += 2;
                    }
                    else
                    {
                        DistrictLevels += 1;
                    }
                }
            }
        }
        for (int fleetIdx = 0; fleetIdx < _Player.Fleets.Count; fleetIdx++)
        {
            FleetData fleet = _Player.Fleets[fleetIdx];
            for (int shipIdx = 0; shipIdx < fleet.Ships.Count; shipIdx++)
            {
                FleetPower += fleet.Ships[shipIdx].ShipPower;
            }
        }
    }
}
