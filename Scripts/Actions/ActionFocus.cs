using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionFocus
{
    static public bool CanSetControlAt(SystemData system, string controlArea, int level)
    {
        DataBlock controlData = system.Data.GetSub("Control");
        if (controlData.HasSub(controlArea + "_Level"))
        {
            if (controlData.HasSub(controlArea + "_Lock_Timer"))
            {
                DataBlock controlLockTimer = controlData.GetSub(controlArea + "_Lock_Timer");
                if (controlLockTimer.ValueI > 0)
                {
                    return false;
                }
            }

            if (controlArea == "Migration")
            {
                return true;
            }

            DataBlock control = controlData.GetSub(controlArea + "_Level");
            if (control.ValueI != level - 1 && control.ValueI != level + 1)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    static public void SetControlAt(SystemData system, string controlArea, int level)
    {
        DataBlock controlData = system.Data.GetSub("Control");
        if (controlData.HasSub(controlArea + "_Level"))
        {
            if (controlData.HasSub(controlArea + "_Lock_Timer"))
            {
                DataBlock controlLockTimer = controlData.GetSub(controlArea + "_Lock_Timer");
                if (controlLockTimer.ValueI > 0) return;
            }

            DataBlock control = controlData.GetSub(controlArea + "_Level");
            int oldLevel = control.ValueI;
            DefEffectWrapper oldEffect = Game.self.Def.GetEffectInfo(controlArea + "_" + oldLevel.ToString());

            system.Resources_PerTurn.RemoveResources_Effect(oldEffect.Res_PerSession);

            control.SetValueI(level, Game.self.Def);
            DefEffectWrapper newEffect = Game.self.Def.GetEffectInfo(controlArea + "_" + level.ToString());

            system.Resources_PerTurn.AddResources_Effect(newEffect.Res_PerSession);

            system.Control_PerTurn.Refresh();
        }
    }

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

        system.Resources_PerTurn.Refresh();
        system.DistrictsQueue_PerTurn.Refresh();
        system.Shipbuilding_PerTurn.Refresh();

        if (Game.self.GalaxyUI.SystemInfo.ProductionInfo._System == system)
        {
            Game.self.GalaxyUI.SystemInfo.ProductionInfo.Refresh(system);
        }
    }
}