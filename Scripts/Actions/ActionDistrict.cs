using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionDistrict
{
    static public void RefreshAvailableDistricts(DistrictData district)
    {
        district.ActionsChangeDistrictsPossible_OnRefresh.Clear();

        if (district._Planet.IsHabitable())
        {
            for (int idx = 0; idx < Game.self.Def.DistrictsInfo.Count; idx ++)
            {
                DefDistrictWrapper districtDef = Game.self.Def.DistrictsInfo[idx];
                bool defIsPrivate = districtDef._Data.GetSubValueS("Control/Type") == "Private";
                bool isPrivate = district.DistrictDef._Data.GetSubValueS("Control/Type") == "Private";
                bool inProgress = district.HasFullPop() == false;
                if (districtDef.Type == "District" && district.DistrictDef.Name != districtDef.Name)
                {
                    if (inProgress)
                    {
                        district.ActionsChangeDistrictsPossible_OnRefresh.Add(districtDef);
                    }
                    else if (isPrivate == defIsPrivate)
                    {
                        district.ActionsChangeDistrictsPossible_OnRefresh.Add(districtDef);
                    }
                }
            }
        }
    }

    static public void ChangeDistrict(DistrictData district, DefDistrictWrapper districtDef)
    {
        district.DistrictDef = districtDef;
        district._Data.SetValueS(districtDef.Name, Game.self.Def);

        if (district.HasFullPop())
        {
            district._Data.SetSubValueI("Change_Cooldown", 3, Game.self.Def);
        }
        else
        {
            district._Colony._System._Player.Stockpiles_PerTurn.Influence -= districtDef.Cost;
            district._Colony._System._Player.Stockpiles_PerTurn.Save();
        }

        district.Economy_PerTurn.Refresh();
        district._Colony._System.Pops_PerTurn.Refresh();
        district._Colony._System.Economy_PerTurn.Refresh();
        district._Colony._System.Shipbuilding_PerTurn.Refresh();
    }

    static public void Nationalize(DistrictData district, bool buy)
    {
        string name = district._Data.ValueS;
        name.Replace("Private_", "State_");
        district._Data.SetValueS(name, Game.self.Def);
        district.DistrictDef = Game.self.Def.GetDistrictInfo(district._Data.ValueS);

        if (buy)
        {
            district._Colony._System._Player.Stockpiles_PerTurn.BC -= 250;
            district._Colony._System._Player.Stockpiles_PerTurn.Save();
        }
        else
        {
            // TO DO happiness--
            district._Colony._System._Player.Stockpiles_PerTurn.Influence -= 150;
            district._Colony._System._Player.Stockpiles_PerTurn.Save();
        }

        district.Economy_PerTurn.Refresh();
        district._Colony._System.Pops_PerTurn.Refresh();
        district._Colony._System.Economy_PerTurn.Refresh();
        district._Colony._System.Shipbuilding_PerTurn.Refresh();
    }

    static public void Privatize(DistrictData district)
    {
        string name = district._Data.ValueS;
        name.Replace("State_", "Private_");
        district._Data.SetValueS(name, Game.self.Def);
        district.DistrictDef = Game.self.Def.GetDistrictInfo(district._Data.ValueS);

        district._Colony._System._Player.Stockpiles_PerTurn.BC += 150;
        district._Colony._System._Player.Stockpiles_PerTurn.Save();

        district.Economy_PerTurn.Refresh();
        district._Colony._System.Pops_PerTurn.Refresh();
        district._Colony._System.Economy_PerTurn.Refresh();
        district._Colony._System.Shipbuilding_PerTurn.Refresh();
    }

    static public void Improve(DistrictData district)
    {
        bool hasInfrastructurePoint = district._Colony._System.Infrastructure_PerTurn.Infrastructure > district._Colony._System.Infrastructure_PerTurn.InfrastructureUsed;
        int factoryLevel = district._Data.GetSubValueI("Factory");
        int factoryCooldown = district._Data.GetSubValueI("Factory_Cooldown");
        if (hasInfrastructurePoint && factoryCooldown > 0 && factoryLevel < 3)
        {
            district._Data.SetSubValueI("Factory", factoryLevel + 1, Game.self.Def);
            district._Data.SetSubValueI("Factory_Cooldown", 3, Game.self.Def);
        }

        district.Economy_PerTurn.Refresh();
        district._Colony._System.Pops_PerTurn.Refresh();
        district._Colony._System.Economy_PerTurn.Refresh();
        district._Colony._System.Shipbuilding_PerTurn.Refresh();
    }

    static public void Invest(DistrictData district)
    {
        int investLevel = district._Data.GetSubValueI("Investment");
        if (investLevel < 3)
        {
            district._Data.SetSubValueI("Investment", investLevel + 1, Game.self.Def);
        }

        district.Economy_PerTurn.Refresh();
        district._Colony._System.Pops_PerTurn.Refresh();
        district._Colony._System.Economy_PerTurn.Refresh();
        district._Colony._System.Shipbuilding_PerTurn.Refresh();
    }

    static public void Devest(DistrictData district)
    {
        int investLevel = district._Data.GetSubValueI("Investment");
        if (investLevel > 0)
        {
            district._Data.SetSubValueI("Investment", investLevel - 1, Game.self.Def);
        }

        district.Economy_PerTurn.Refresh();
        district._Colony._System.Pops_PerTurn.Refresh();
        district._Colony._System.Economy_PerTurn.Refresh();
        district._Colony._System.Shipbuilding_PerTurn.Refresh();
    }

    static public void EndTurn(SystemData system)
    {
        Array<DataBlock> colonies = system.Data.GetSub("Colony_List").GetSubs();
        for (int colonyIdx = 0; colonyIdx < colonies.Count; colonyIdx++)
        {
            DataBlock colony = colonies[colonyIdx];
            Array<DataBlock> districts = colony.GetSub("District_List").GetSubs();
            for (int idx = 0; idx < districts.Count; idx++)
            {
                // change
                {
                    int cooldown = districts[idx].GetSubValueI("Change_Cooldown");
                    if (cooldown > 0)
                    {
                        districts[idx].SetSubValueI("Change_Cooldown", cooldown - 1, Game.self.Def);
                    }
                }

                // factory
                {
                    int cooldown = districts[idx].GetSubValueI("Factory_Cooldown");
                    if (cooldown > 0)
                    {
                        districts[idx].SetSubValueI("Factory_Cooldown", cooldown - 1, Game.self.Def);
                    }
                }

                // control
                {
                    int cooldown = districts[idx].GetSubValueI("Control_Cooldown");
                    if (cooldown > 0)
                    {
                        districts[idx].SetSubValueI("Control_Cooldown", cooldown - 1, Game.self.Def);
                    }
                }
            }
        }
    }
}