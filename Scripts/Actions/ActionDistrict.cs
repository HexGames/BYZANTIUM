using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionDistrict
{
    static public void RefreshAllBuildActions(DistrictData district)
    {
        district.ActionsChangeDistricts_OnRefresh.Clear();

        // upgrade
        DistrictNew upgrade = new DistrictNew(district.DistrictDef, district._Colony);
        upgrade.SetAsUpgrade();
        district.ActionsChangeDistricts_OnRefresh.Add(upgrade);

        // change
        for (int changeIdx = 0; changeIdx < district.DistrictDef.ChangeTo.Count; changeIdx++)
        {
            DefDistrictWrapper otherDef = Game.self.Def.GetDistrictInfo(district.DistrictDef.ChangeTo[changeIdx]);
            DistrictNew newDistrict = new DistrictNew(otherDef, district._Colony);
            newDistrict.SetAsChangeDistrict();
            district.ActionsChangeDistricts_OnRefresh.Add(newDistrict);
        }

        // constrol
        if (district.IsStateOwned())
        {
            DistrictNew newDistrict = new DistrictNew(district.DistrictDef, district._Colony);
            newDistrict.SetAsPrivatize();
            district.ActionsChangeDistricts_OnRefresh.Add(newDistrict);
        }
        else // if (district.IsPrivate())
        {
            DistrictNew newDistrict = new DistrictNew(district.DistrictDef, district._Colony);
            newDistrict.SetAsNationalizeBC();
            district.ActionsChangeDistricts_OnRefresh.Add(newDistrict);
        }
    }

    static public void ChangeDistrict(DistrictData district, DistrictNew toDistrict)
    {
        SystemData system = district._Colony._System;

        system._Player.Stockpiles_PerTurn.BC -= toDistrict.Cost_BC;
        system._Player.Stockpiles_PerTurn.Influence -= toDistrict.Cost_Inf;
        system._Player.Stockpiles_PerTurn.Save();

        if (toDistrict.StopWorking)
        {
            // change
            string oldDistrictName = district.DistrictDef.Name;
            district.DistrictDef = toDistrict.DistrictDef;
            district.Data.SetValueS(district.DistrictDef.Name, Game.self.Def);

            DataBlock changeData = Data.AddData(district.Data, "ActionChange", Game.self.Def);
            Data.AddData(changeData, "FromDistrict", oldDistrictName, Game.self.Def);
            Data.AddData(changeData, "Progress", 0, Game.self.Def);
            Data.AddData(changeData, "ProgressMax", toDistrict.Cost_Time, Game.self.Def);
            Data.AddData(changeData, "StopWorking", toDistrict.StopWorking ? 1 : 0, Game.self.Def);
            DataBlock costData = Data.AddData(changeData, "Cost", Game.self.Def);
            Data.AddData(costData, "BC", toDistrict.Cost_BC, Game.self.Def);
            Data.AddData(costData, "Influence", toDistrict.Cost_Inf, Game.self.Def);

            system.Economy_PerTurn.Refresh_1();
            system.RefreshSystemDistricts_1();
            system.RefreshInvest_2();
            system.Pops_PerTurn.RefreshHappiness_4();
            system.Control_PerTurn.Refresh();
            system.Economy_PerTurn.Refresh_4();
            system.Shipbuilding_PerTurn.Refresh();

            //Game.self.GalaxyUI.SystemInfo.Refresh(system.Star);
            //Game.self.GalaxyUI.PlanetInfo.Refresh(district._Colony.Planet);
        }
        else
        {
            // upgrade 
            int time = Mathf.Max(toDistrict.Cost_Time - system.Economy_PerTurn.Construction + system.Economy_PerTurn.ConstructionPenalty, 1);

            DataBlock changeData = Data.AddData(district.Data, "ActionChange", Game.self.Def);
            Data.AddData(changeData, "ToDistrict", toDistrict.DistrictDef.Name, Game.self.Def);
            Data.AddData(changeData, "Progress", 0, Game.self.Def);
            Data.AddData(changeData, "ProgressMax", time, Game.self.Def);
            Data.AddData(changeData, "StopWorking", toDistrict.StopWorking ? 1 : 0, Game.self.Def);
            DataBlock costData = Data.AddData(changeData, "Cost", Game.self.Def);
            Data.AddData(costData, "BC", toDistrict.Cost_BC, Game.self.Def);
            Data.AddData(costData, "Influence", toDistrict.Cost_Inf, Game.self.Def);

            system.Economy_PerTurn.ConstructionPenalty += 1;
        }
    }

    static public void CancelChange(DistrictData district)
    {
        if (district.Data.HasSub("ActionChange") == false)
            return;

        int bc = district.Data.GetSubValueI("ActionChange", "Cost", "BC");
        int inf = district.Data.GetSubValueI("ActionChange", "Cost", "Influence");

        SystemData system = district._Colony._System;
        system._Player.Stockpiles_PerTurn.BC += bc;
        system._Player.Stockpiles_PerTurn.Influence += inf;
        system._Player.Stockpiles_PerTurn.Save();

        if (district.Data.HasSub("ActionChange", "FromDistrict"))
        {
            string oldDistrictName = district.Data.GetSubValueS("ActionChange", "FromDistrict");
            district.DistrictDef = Game.self.Def.GetDistrictInfo(oldDistrictName);
            district.Data.SetValueS(district.DistrictDef.Name, Game.self.Def);
        }
        else
        {
            system.Economy_PerTurn.ConstructionPenalty -= 1;
        }

        Data.DeleteDataSub(district.Data, "ActionChange");
    }

    static public void Invest(DistrictData district)
    {
        int investLevel = district.Data.GetSubValueI("InvestLevel");
        if (investLevel < 3)
        {
            district.Data.SetSubValueI("InvestLevel", investLevel + 1, Game.self.Def);
        }

        SystemData system = district._Colony._System;

        system.RefreshSystemDistricts_1();
        system.Pops_PerTurn.RefreshHappiness_4();
        system.Economy_PerTurn.Refresh_4();
        system.Shipbuilding_PerTurn.Refresh();
    }

    static public void Devest(DistrictData district)
    {
        int investLevel = district.Data.GetSubValueI("InvestLevel");
        if (investLevel > 0)
        {
            district.Data.SetSubValueI("InvestLevel", investLevel - 1, Game.self.Def);
        }

        SystemData system = district._Colony._System;

        system.RefreshSystemDistricts_1();
        system.Pops_PerTurn.RefreshHappiness_4();
        system.Economy_PerTurn.Refresh_4();
        system.Shipbuilding_PerTurn.Refresh();
    }

    static public void EndTurn(SystemData system)
    {
        for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
        {
            ColonyData colony = system.Colonies[colonyIdx];
            for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
            {
                DistrictData district = colony.Districts[districtIdx];
                if (district.Data.HasSub("ActionChange"))
                {
                    int progressMax = district.Data.GetSubValueI("ActionChange", "ProgressMax");
                    int progress = district.Data.GetSubValueI("ActionChange", "Progress");
                    int progressRemainig = progressMax - progress;
                    if (progress + 1 < progressMax)
                    {
                        // contrinue progress
                        district.Data.SetSubValueI("ActionChange", "Progress", progress + 1, Game.self.Def);
                        int costBC = district.Data.GetSubValueI("ActionChange", "Cost", "BC");
                        int costInf = district.Data.GetSubValueI("ActionChange", "Cost", "Influence");
                        costBC *= (progressRemainig - 1) / progressRemainig;
                        costInf *= (progressRemainig - 1) / progressRemainig;
                        district.Data.SetSubValueI("ActionChange", "Cost", "BC", costBC, Game.self.Def);
                        district.Data.SetSubValueI("ActionChange", "Cost", "Influence", costInf, Game.self.Def);
                    }
                    else
                    {
                        if (district.Data.HasSub("ActionChange", "ToDistrict"))
                        {
                            // upgrade
                            string newDistrictName = district.Data.GetSubValueS("ActionChange", "ToDistrict");
                            DefDistrictWrapper newDistrictDef = Game.self.Def.GetDistrictInfo(newDistrictName);

                            if (newDistrictDef != null)
                            {
                                district.DistrictDef = newDistrictDef;
                                district.Data.SetValueS(newDistrictDef.Name, Game.self.Def);

                                // economy gets refreshed next turn (this is End Turn);
                            }
                            else
                            {
                                GD.PrintErr("Upraget district not found in def.");
                            }
                        }

                        Data.DeleteDataSub(district.Data, "ActionChange");
                    }
                }
            }
        }
    }
}