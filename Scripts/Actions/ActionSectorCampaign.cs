using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionSectorCampaign : Action
{
    private SectorData Sector = null;

    static public void RefreshColonyActions(SectorData sector, DefLibrary df)
    {
        sector.ActionsCampaignPossible.Clear();

        for (int idx = 0; idx < df.BuildingsInfo.Count; idx++)
        {
            ActionSectorCampaign action = new ActionSectorCampaign();

            action.Init(df);

            action.Selected = sector.Data;
            action.TargetInfo = df.BuildingsInfo[idx];

            action.Sector = sector;

            sector.ActionsCampaignPossible.Add(action);
        }
    }

    public override void Start()
    {
        //Sector.ActionCampaign = Data.AddData(Selected, "ActionSectorBuild", DefLib);
        //
        //Data.AddData(Sector.ActionCampaign, "Progress", 0, DefLib);
        //Data.AddData(Sector.ActionCampaign, "Progress_Max", TargetInfo.Turns, DefLib);
        //Data.AddData(Sector.ActionCampaign, "Building", TargetInfo.Name, DefLib);
    }

    static public void Update(SectorData sector, DefLibrary df)
    {
        //int progress = sector.ActionCampaign.GetSub("Progress").ValueI;
        //int progress_max = sector.ActionCampaign.GetSub("Progress_Max").ValueI;
        //
        //progress++;
        //
        //sector.ActionCampaign.GetSub("Progress").ValueI = progress;
        //
        //if (progress >= progress_max) End(sector, df);
    }

    static public void End(SectorData sector, DefLibrary df)
    {
        //DataBlock buildingsData = sector.Buildings;//.GetSub("Buildings");
        //DataBlock chosenBuilding = sector.ActionCampaign.GetSub("Building");
        //
        //DataBlock existingBuilding = buildingsData.GetSub(chosenBuilding.ValueS);
        //if (existingBuilding != null)
        //{
        //    existingBuilding.ValueI++;
        //}
        //else
        //{
        //    Data.AddData(buildingsData, chosenBuilding.ValueS, 1, df);
        //}
        //
        //sector.Data.Subs.Remove(sector.ActionCampaign);
        //sector.ActionCampaign = null;
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