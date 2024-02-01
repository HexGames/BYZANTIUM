using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionColonyBuild : Action
{
    private ColonyData Colony = null;

    static public void RefreshColonyActions(ColonyData colony, DefLibrary df)
    {
        colony.ActionsBuildPossible.Clear();

        for (int idx = 0; idx < df.Buildings.Count; idx++)
        {
            ActionColonyBuild action = new ActionColonyBuild();

            action.Init(df);

            action.Selected = colony.DataBlock;
            action.TargetChosen = df.Buildings[idx];

            action.Colony = colony;

            colony.ActionsBuildPossible.Add(action);
        }
    }

    public override void Start()
    {
        int turns = TargetChosen.GetSub("Turns").ValueI;

        Colony.ActionBuild = Data.AddData(Selected, "ActionColonyBuild", DefLib);
    
        Data.AddData(Colony.ActionBuild, "Progress", 0, DefLib);
        Data.AddData(Colony.ActionBuild, "Progress_Max", turns, DefLib);
        Data.AddData(Colony.ActionBuild, "Building", TargetChosen.ValueS, DefLib);
    }

    static public void Update(ColonyData colony, DefLibrary df)
    {
        int progress = colony.ActionBuild.GetSub("Progress").ValueI;
        int progress_max = colony.ActionBuild.GetSub("Progress_Max").ValueI;

        progress++;

        colony.ActionBuild.GetSub("Progress").ValueI = progress;

        if (progress >= progress_max) End(colony, df);
    }

    static public void End(ColonyData colony, DefLibrary df)
    {
        DataBlock buildingsData = colony.Buildings;//.GetSub("Buildings");
        DataBlock chosenBuilding = colony.ActionBuild.GetSub("Building");

        DataBlock existingBuilding = buildingsData.GetSub(chosenBuilding.ValueS);
        if (existingBuilding != null)
        {
            existingBuilding.ValueI++;
        }
        else
        {
            Data.AddData(buildingsData, chosenBuilding.ValueS, 1, df);
        }

        colony.DataBlock.Subs.Remove(colony.ActionBuild);
        colony.ActionBuild = null;
    }

        //public override void Update()
        //{
        //    int turns = ActionData.GetSub("Turns").ValueI;
        //
        //    turns--;
        //
        //    ActionData.GetSub("Turns").ValueI = turns;
        //
        //    if (turns == 0) End();
        //}
        //
        //public override void End()
        //{
        //    DataBlock buildingsData = Selected.GetSub("Buildings");
        //
        //    DataBlock existingBuilding = buildingsData.GetSub(TargetChosen.ValueS);
        //    if (existingBuilding != null)
        //    {
        //        existingBuilding.ValueI++;
        //    }
        //    else
        //    {
        //        Data.AddData(buildingsData, TargetChosen.ValueS, 1, DefLib);
        //    }
        //}
    }