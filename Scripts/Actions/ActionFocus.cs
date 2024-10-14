using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionFocus
{
    static public void SetFocusOn(SystemData system, string focusArea)
    {
        if (system.Resources.HasSub("Districts*FocusChosen"))
        {
            Data.DeleteDataSub(system.Resources, "Districts*FocusChosen");
        }
        if (system.Resources.HasSub("Factories*FocusChosen"))
        {
            Data.DeleteDataSub(system.Resources, "Factories*FocusChosen");
        }
        if (system.Resources.HasSub("Research*FocusChosen"))
        {
            Data.DeleteDataSub(system.Resources, "Research*FocusChosen");
        }
        if (system.Resources.HasSub("Shipbuilding*FocusChosen"))
        {
            Data.DeleteDataSub(system.Resources, "Shipbuilding*FocusChosen");
        }

        Data.AddData(system.Resources, focusArea + "*FocusChosen", 25, Game.self.Def);

        for (int idx = 0; idx < system.Resources_PerTurn.Incomes.Count; idx++)
        {
            if (system.Resources_PerTurn.Incomes[idx].Name == focusArea)
            {
                system.Resources_PerTurn.Incomes[idx].FocusChosen = 25;
            }
            else
            {
                system.Resources_PerTurn.Incomes[idx].FocusChosen = 0;
            }
        }

        if (Game.self.GalaxyUI.SystemInfo.ProductionInfo._System == system)
        {
            Game.self.GalaxyUI.SystemInfo.ProductionInfo.Refresh(system);
        }
    }
}