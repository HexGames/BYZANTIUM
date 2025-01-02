using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionDistrict
{
    static public void RefreshUpgradeDistricts(DistrictData district)
    {
        district.ActionsChangeDistricts_OnRefresh.Clear(); // TODO make singe list instead of 3

        for (int idx = 0; idx < Game.self.Def.DistrictsInfo.Count; idx++)
        {
            bool inProgress = district.HasFullPop() == false;
            DefDistrictWrapper otherDef = Game.self.Def.DistrictsInfo[idx];
            if (district.DistrictDef.Type == otherDef.Type
                && district.DistrictDef.Name == otherDef.UpgradeOf)
            {
                DistrictNew newDistrict = new DistrictNew(otherDef, district.Pop, district._Colony);
                newDistrict.SetAsUpgrade();
                district.ActionsChangeDistricts_OnRefresh.Add(newDistrict);
            }
        }
    }

    static public void RefreshChangeControlDistricts(DistrictData district)
    {
        district.ActionsChangeDistricts_OnRefresh.Clear();

        for (int idx = 0; idx < Game.self.Def.DistrictsInfo.Count; idx++)
        {
            bool inProgress = district.HasFullPop() == false;
            DefDistrictWrapper otherDef = Game.self.Def.DistrictsInfo[idx];
            if (district.DistrictDef.Privatize_to == otherDef.Name)
            {
                DistrictNew newDistrict = new DistrictNew(otherDef, district.Pop, district._Colony);
                newDistrict.SetAsPrivatize();
                district.ActionsChangeDistricts_OnRefresh.Add(newDistrict);
            }
            else if (district.DistrictDef.Nationalize_to == otherDef.Name)
            {
                DistrictNew newDistrict_BC = new DistrictNew(otherDef, district.Pop, district._Colony);
                newDistrict_BC.SetAsNationalizeBC();
                district.ActionsChangeDistricts_OnRefresh.Add(newDistrict_BC);

                DistrictNew newDistrict_Inf = new DistrictNew(otherDef, district.Pop, district._Colony);
                newDistrict_Inf.SetAsNationalizeInf();
                district.ActionsChangeDistricts_OnRefresh.Add(newDistrict_Inf);
            }
        }
    }

    static public void RefreshChangeTypeDistricts(DistrictData district)
    {
        district.ActionsChangeDistricts_OnRefresh.Clear();

        for (int idx = 0; idx < Game.self.Def.DistrictsInfo.Count; idx++)
        {
            bool inProgress = district.HasFullPop() == false;
            DefDistrictWrapper otherDef = Game.self.Def.DistrictsInfo[idx];
            if (district.DistrictDef.Type == otherDef.Type
                && (district.DistrictDef.Control_Type == otherDef.Control_Type || inProgress)
                && district.DistrictDef.Level == otherDef.Level
                && district.DistrictDef.Name != otherDef.Name)
            {
                DistrictNew newDistrict = new DistrictNew(otherDef, district.Pop, district._Colony);
                if (inProgress) newDistrict.SetAsNewDistrict(district.DistrictDef);
                else newDistrict.SetAsChangeDistrict();
                district.ActionsChangeDistricts_OnRefresh.Add(newDistrict);
            }
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
            district._Data.SetValueS(district.DistrictDef.Name, Game.self.Def);

            DataBlock changeData = Data.AddData(district._Data, "ActionChange", Game.self.Def);
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

            DataBlock changeData = Data.AddData(district._Data, "ActionChange", Game.self.Def);
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
        if (district._Data.HasSub("ActionChange") == false)
            return;

        int bc = district._Data.GetSubValueI("ActionChange", "Cost", "BC");
        int inf = district._Data.GetSubValueI("ActionChange", "Cost", "Influence");

        SystemData system = district._Colony._System;
        system._Player.Stockpiles_PerTurn.BC += bc;
        system._Player.Stockpiles_PerTurn.Influence += inf;
        system._Player.Stockpiles_PerTurn.Save();

        if (district._Data.HasSub("ActionChange", "FromDistrict"))
        {
            string oldDistrictName = district._Data.GetSubValueS("ActionChange", "FromDistrict");
            district.DistrictDef = Game.self.Def.GetDistrictInfo(oldDistrictName);
            district._Data.SetValueS(district.DistrictDef.Name, Game.self.Def);
        }
        else
        {
            system.Economy_PerTurn.ConstructionPenalty -= 1;
        }

        Data.DeleteDataSub(district._Data, "ActionChange");
    }

    //bool costInfluence = district.HasFullPop() == false && district.IsPrivate() && toDistrict.IsStateOwned();
    //bool costBC = district.HasFullPop();
    //int bcPerc = 100;

    //if (toDistrict.IsPrivate() && toDistrict.DistrictDef.Level == district.DistrictDef.Level) bcPerc = 25;

    //district.DistrictDef = toDistrict.DistrictDef;
    //district._Data.SetValueS(toDistrict.DistrictDef.Name, Game.self.Def);

    //int constructionStarts = 0;
    //for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
    //{
    //    ColonyData colony = system.Colonies[colonyIdx];
    //    for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
    //    {
    //        DistrictData otherDistrict = colony.Districts[districtIdx];
    //        if (otherDistrict != district)
    //        {
    //            int turns = district._Data.GetSubValueI("Change_Cooldown");
    //            if (turns > 0)
    //            {
    //                district._Data.SetSubValueI("Change_Cooldown", turns + 1, Game.self.Def);
    //                inConstruction++;
    //            }
    //        }
    //    }
    //}

    //Mathf.Max(1, 2 * toDistrict.DistrictDef.Level + 2) + constructionStarts - system.Economy_PerTurn.Construction
    //district._Data.SetSubValueI("Change_Cooldown", toDistrict.Cost_Time, Game.self.Def);

    //static public bool CanNationalize(DistrictData district)
    //{
    //    return district.DistrictDef.Nationalize_to != null;
    //}
    //
    //static public void Nationalize(DistrictData district, bool buy)
    //{
    //    district._Data.SetValueS(district.DistrictDef.Nationalize_to, Game.self.Def);
    //    district.DistrictDef = Game.self.Def.GetDistrictInfo(district._Data.ValueS);
    //
    //    SystemData system = district._Colony._System;
    //
    //    district._Data.SetSubValueI("Control_Cooldown", 2, Game.self.Def);
    //    if (buy)
    //    {
    //        system._Player.Stockpiles_PerTurn.BC -= district.DistrictDef.Nationalize_BC;
    //        system._Player.Stockpiles_PerTurn.Save();
    //    }
    //    else
    //    {
    //        // TO DO happiness--
    //        system._Player.Stockpiles_PerTurn.Influence -= district.DistrictDef.Nationalize_Influence;
    //        system._Player.Stockpiles_PerTurn.Save();
    //    }
    //
    //    district.Economy_PerTurn.Clear();
    //    system.RefreshSystemDistricts();
    //    system.Pops_PerTurn.Refresh();
    //    system.Economy_PerTurn.Refresh();
    //    system.Shipbuilding_PerTurn.Refresh();
    //}
    //
    //static public bool CanPrivatize(DistrictData district)
    //{
    //    return district.DistrictDef.Privatize_to != null;
    //}
    //
    //static public void Privatize(DistrictData district)
    //{
    //    district._Data.SetValueS(district.DistrictDef.Privatize_to, Game.self.Def);
    //    district.DistrictDef = Game.self.Def.GetDistrictInfo(district._Data.ValueS);
    //
    //    SystemData system = district._Colony._System;
    //
    //    district._Data.SetSubValueI("Control_Cooldown", 1, Game.self.Def);
    //    system._Player.Stockpiles_PerTurn.BC += district.DistrictDef.Privatize_BC;
    //    system._Player.Stockpiles_PerTurn.Save();
    //
    //    district.Economy_PerTurn.Clear();
    //    system.RefreshSystemDistricts();
    //    system.Pops_PerTurn.Refresh();
    //    system.Economy_PerTurn.Refresh();
    //    system.Shipbuilding_PerTurn.Refresh();
    //}

    //static public void Improve(DistrictData district)
    //{
    //    bool hasInfrastructurePoint = true; // district._Colony._System.Infrastructure_PerTurn.Infrastructure > district._Colony._System.Infrastructure_PerTurn.InfrastructureUsed;
    //    int factoryLevel = district._Data.GetSubValueI("Factory");
    //    int factoryCooldown = district._Data.GetSubValueI("Factory_Cooldown");
    //    if (hasInfrastructurePoint && factoryCooldown > 0 && factoryLevel < 3)
    //    {
    //        district._Data.SetSubValueI("Factory", factoryLevel + 1, Game.self.Def);
    //        district._Data.SetSubValueI("Factory_Cooldown", 3, Game.self.Def);
    //    }
    //
    //    district.Economy_PerTurn.RefreshBase();
    //    district.Economy_PerTurn.RefreshFinal();
    //    district._Colony._System.Pops_PerTurn.Refresh();
    //    district._Colony._System.Economy_PerTurn.Refresh();
    //    district._Colony._System.Shipbuilding_PerTurn.Refresh();
    //}

    static public void Invest(DistrictData district)
    {
        int investLevel = district._Data.GetSubValueI("InvestLevel");
        if (investLevel < 3)
        {
            district._Data.SetSubValueI("InvestLevel", investLevel + 1, Game.self.Def);
        }

        SystemData system = district._Colony._System;

        system.RefreshSystemDistricts_1();
        system.Pops_PerTurn.RefreshHappiness_4();
        system.Economy_PerTurn.Refresh_4();
        system.Shipbuilding_PerTurn.Refresh();
    }

    static public void Devest(DistrictData district)
    {
        int investLevel = district._Data.GetSubValueI("InvestLevel");
        if (investLevel > 0)
        {
            district._Data.SetSubValueI("InvestLevel", investLevel - 1, Game.self.Def);
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
                if (district._Data.HasSub("ActionChange"))
                {
                    int progressMax = district._Data.GetSubValueI("ActionChange", "ProgressMax");
                    int progress = district._Data.GetSubValueI("ActionChange", "Progress");
                    int progressRemainig = progressMax - progress;
                    if (progress + 1 < progressMax)
                    {
                        // contrinue progress
                        district._Data.SetSubValueI("ActionChange", "Progress", progress + 1, Game.self.Def);
                        int costBC = district._Data.GetSubValueI("ActionChange", "Cost", "BC");
                        int costInf = district._Data.GetSubValueI("ActionChange", "Cost", "Influence");
                        costBC *= (progressRemainig - 1) / progressRemainig;
                        costInf *= (progressRemainig - 1) / progressRemainig;
                        district._Data.SetSubValueI("ActionChange", "Cost", "BC", costBC, Game.self.Def);
                        district._Data.SetSubValueI("ActionChange", "Cost", "Influence", costInf, Game.self.Def);
                    }
                    else
                    {
                        if (district._Data.HasSub("ActionChange", "ToDistrict"))
                        {
                            // upgrade
                            string newDistrictName = district._Data.GetSubValueS("ActionChange", "ToDistrict");
                            DefDistrictWrapper newDistrictDef = Game.self.Def.GetDistrictInfo(newDistrictName);

                            if (newDistrictDef != null)
                            {
                                district.DistrictDef = newDistrictDef;
                                district._Data.SetValueS(newDistrictDef.Name, Game.self.Def);

                                // economy gets refreshed next turn (this is End Turn);
                            }
                            else
                            {
                                GD.PrintErr("Upraget district not found in def.");
                            }
                        }

                        Data.DeleteDataSub(district._Data, "ActionChange");
                    }
                }
            }
        }
    }
}