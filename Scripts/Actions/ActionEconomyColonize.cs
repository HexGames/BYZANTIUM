using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionEconomyColonize : ActionPlanet
{
    // ----------------------------------------------------------------------------------------- Object
    public DefDistrictWrapper DistrictDef = null;
    public int Cost_BC = 0;
    public int Time = 0;

    public ActionEconomyColonize(SystemData system, PlanetData planet, DefDistrictWrapper district)
    {
        ActionID = ID.ECONOMY_COLONIZE;

        System = system;
        Planet = planet;
        DistrictDef = district;

        if (district != null)
        {
            Cost_BC = district.Cost_BC;
        }
        else
        {
            Cost_BC = 20000;
        }

        Time = 10;
    }

    public override void ExecuteOrder()
    {
        DataBlock actionData = Data.AddData(System.Data, "ActionBuild", Game.self.Def);
        Data.AddData(actionData, "Colonize", Game.self.Def);
        Data.AddData(actionData, "Planet", Planet.PlanetName, Game.self.Def);
        if (DistrictDef != null) Data.AddData(actionData, "DistrictDef", DistrictDef.Name, Game.self.Def);
        int overflow = System.Data.GetSubValueI("ActionBuildOverflow");
        Data.DeleteDataSub(System.Data, "ActionBuildOverflow");
        Data.AddData(actionData, "Progress", overflow, Game.self.Def);
        Data.AddData(actionData, "ProgressMax", Cost_BC, Game.self.Def);
    }

    public override string ToUI_Title(int ID = 0)
    {
        return Planet.PlanetName;
    }


    // ----------------------------------------------------------------------------------------- Static
    public static void RefreshActions(SystemData system)
    {
        system.ActionEconomyColonize_PerTurn.Clear();
        for (int planetIdx = 0; planetIdx < system.Star.Planets.Count; planetIdx++)
        {
            PlanetData planet = system.Star.Planets[planetIdx];
            if (planet.Colony == null)
            {
                if (planet.IsHabitable())
                {
                    var action = new ActionEconomyColonize(system, planet, null);
                    system.ActionEconomyColonize_PerTurn.Add(action);

                }
                else
                {
                    for (int defIdx = 0; defIdx < Game.self.Def.DistrictsInfo.Count; defIdx++)
                    {
                        DefDistrictWrapper districtDef = Game.self.Def.DistrictsInfo[defIdx];
                        if (districtDef.Type == planet.Data.GetSubValueS("SlotType") && districtDef.Control == "State")
                        {
                            var action = new ActionEconomyColonize(system, planet, districtDef);
                            system.ActionEconomyColonize_PerTurn.Add(action);
                        }
                    }
                }
            }
        }
    }

    public static void CancelOrder(SystemData system)
    {
        if (system.Data.HasSub("ActionBuild", "Colonize"))
        {
            system._Player.Stockpiles_PerTurn.BC += system.Data.GetSubValueI("ActionBuild", "Progress") * 80 / 100;
            system._Player.Stockpiles_PerTurn.Save();
            Data.DeleteDataSub(system.Data, "ActionBuild");
        }
    }

    public static void EndTurn(SystemData system)
    {
        if (system.Data.HasSub("ActionBuild", "Colonize"))
        {
            int progress = system.Data.GetSubValueI("ActionBuild", "Progress");
            int progressMax = system.Data.GetSubValueI("ActionBuild", "ProgressMax");

            progress += 50; // system.Economy_PerTurn.Budget

            if (progress < progressMax)
            {
                system.Data.SetSubValueI("ActionBuild", "Progress", progress, Game.self.Def);
            }
            else // progress >= progressMax
            {
                string planetName = system.Data.GetSubValueS("ActionBuild", "Planet");
                PlanetData planet = null;
                for (int planetIdx = 0; planetIdx < system.Star.Planets.Count; planetIdx++)
                {
                    PlanetData systemPlanet = system.Star.Planets[planetIdx];
                    if (systemPlanet.Colony == null && systemPlanet.Name == planetName)
                    {
                        planet = systemPlanet;
                        break;
                    }
                }

                if (planet != null)
                {
                    if (planet.IsHabitable())
                    {
                        DataBlock colonyData = ColonyRaw.CreateNewColony_Habitable(system.Star.Data, planet.Data, system._Player.Data, system.Data, false, false, Game.self.Def);
                        ColonyData colony = Game.self.Map.Data.GenerateGameFromData_Player_System_Colony(colonyData, system);

                        colony.Init_DistrictData();
                        colony.Init_Resources();
                    }
                    else
                    {
                        string districtName = system.Data.GetSubValueS("ActionBuild", "DistrictDef");
                        if (districtName != "")
                        {
                            DataBlock colonyData = ColonyRaw.CreateNewColony_Unhabitable(system.Star.Data, planet.Data, system._Player.Data, system.Data, districtName, Game.self.Def);
                            ColonyData colony = Game.self.Map.Data.GenerateGameFromData_Player_System_Colony(colonyData, system);

                            colony.Init_DistrictData();
                            colony.Init_Resources();
                        }
                    }
                }

                Data.DeleteDataSub(system.Data, "ActionBuild");
                progress -= progressMax;
                Data.AddData(system.Data, "ActionBuildOverflow", progress, Game.self.Def);
            }
        }
    }
}