using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

// Editor
public partial class MapGenerator : Node
{
    private void GenerateNewMapSave_Players_StartingColony_SectorConTreasury(DataBlock sector)
    {
        DataBlock campaign = Data.AddData(sector, "ActionConTreasury", DefLibrary);

        Data.AddData(campaign, "Priority", 1, DefLibrary);

        Data.AddData(campaign, "Progress:Current", 5, DefLibrary);
        Data.AddData(campaign, "Progress:Total", 10, DefLibrary);
    }

    private void GenerateNewMapSave_Players_StartingColony_SectorResources(DataBlock sector)
    {
        DataBlock resources = Data.AddData(sector, "Resources", DefLibrary);

        Data.AddData(resources, "Energy", 0, DefLibrary);
        Data.AddData(resources, "Energy*Used", 0, DefLibrary);
        Data.AddData(resources, "Minerals", 0, DefLibrary);
        Data.AddData(resources, "Minerals*Used", 0, DefLibrary);
        Data.AddData(resources, "Production*Income", 0, DefLibrary);
        Data.AddData(resources, "Shipbuilding*Income", 0, DefLibrary);
        //Data.AddData(resources, "PrivateIndustry", 0, DefLibrary);

        Data.AddData(resources, "Trade", 0, DefLibrary);
        Data.AddData(resources, "Trade*Used", 0, DefLibrary);
        Data.AddData(resources, "TechPoints*Income", 0, DefLibrary);
        Data.AddData(resources, "CulturePoints*Income", 0, DefLibrary);
        Data.AddData(resources, "Authority", 0, DefLibrary);
        Data.AddData(resources, "Authority*Used", 0, DefLibrary);
        Data.AddData(resources, "Influence", 0, DefLibrary);
        Data.AddData(resources, "Influence*Used", 0, DefLibrary);
        Data.AddData(resources, "BC*Income", 0, DefLibrary);
    }

    //private void GenerateNewMapSave_Players_StartingColony_SectorBudget(DataBlock sector)
    //{
    //    DataBlock budget = Data.AddData(sector, "Budget", DefLibrary);
    //
    //    Data.AddData(budget, "Cooldown", 5, DefLibrary);
    //
    //    DataBlock production = Data.AddData(budget, "Production", DefLibrary);
    //    {
    //        DataBlock project = Data.AddData(production, "Project", "Construction_1", DefLibrary);
    //        Data.AddData(project, "Locked", 1, DefLibrary);
    //        Data.AddData(project, "Value", 4, DefLibrary);
    //    }
    //    {
    //        DataBlock project = Data.AddData(production, "Project", "Construction_2", DefLibrary);
    //        Data.AddData(project, "Locked", 1, DefLibrary);
    //        Data.AddData(project, "Value", 3, DefLibrary);
    //    }
    //    {
    //        DataBlock project = Data.AddData(production, "Project", "Shipyard", DefLibrary);
    //        Data.AddData(project, "Locked", 1, DefLibrary);
    //        Data.AddData(project, "Value", 4, DefLibrary);
    //    }
    //    {
    //        DataBlock project = Data.AddData(production, "Treasury", "Treasury", DefLibrary);
    //        Data.AddData(project, "Locked", 0, DefLibrary);
    //        Data.AddData(project, "Value", 1, DefLibrary);
    //    }
    //}

    //private void GenerateNewMapSave_Players_StartingColony_SectorCampaign(DataBlock sector, DataBlock system, DataBlock colony)
    //{
    //    DataBlock campaign = Data.AddData(sector, "Campaign", DefLibrary);
    //
    //    Data.AddData(campaign, "Name", "Growth_Incentives", DefLibrary);
    //    Data.AddData(campaign, "Link:System:Colony", system.ValueS + ":" + colony.ValueS, DefLibrary);
    //    Data.AddData(campaign, "FixedTurns:Current", 3, DefLibrary);
    //
    //    DataBlock overflow = Data.AddData(campaign, "Overflow", DefLibrary);
    //    Data.AddData(overflow, "Energy", 0, DefLibrary);
    //}

    //private void GenerateNewMapSave_Players_StartingColony_SectorConstruction(DataBlock sector, DataBlock system, DataBlock colony)
    //{
    //    DataBlock campaign = Data.AddData(sector, "Construction_1", DefLibrary);
    //
    //    Data.AddData(campaign, "Name", "Growth_Incentives", DefLibrary);
    //    Data.AddData(campaign, "Link:System:Colony", system.ValueS + ":" + colony.ValueS, DefLibrary);
    //    Data.AddData(campaign, "Progress:Current", 9000, DefLibrary);
    //    Data.AddData(campaign, "Progress:Total", 10000, DefLibrary);
    //
    //    DataBlock overflow = Data.AddData(campaign, "Overflow", DefLibrary);
    //    Data.AddData(overflow, "Production", 0, DefLibrary);
    //}

    private void GenerateNewMapSave_Players_StartingColony_SectorActionBuild(DataBlock sector)
    {
        DataBlock actionBuild = Data.AddData(sector, "ActionBuildQueue", DefLibrary);
        Data.AddData(actionBuild, "Overflow", 0, DefLibrary);
    }
}
