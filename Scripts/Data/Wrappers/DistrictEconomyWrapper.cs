using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

public class DistrictEconomyWrapper
{
    public string Resource;
    public int Production;
    public int TaxBC;
    public int Wealth;
    public int Reinvest;

    public int ExtraConstruction = 0;

    DistrictData _District;
    DefDistrictWrapper _DistrictDef;

    public DistrictEconomyWrapper(DistrictData district)
    {
        _District = district;
        _DistrictDef = district.DistrictDef;
    }
    public DistrictEconomyWrapper(DefDistrictWrapper districtDef)
    {
        _DistrictDef = districtDef;
    }

    public void Refresh()
    {
        int pop = 0;
        if (_District != null && _District._Data.HasSub("Pop")) pop = 1;

        Resource = _DistrictDef._Data.GetSub("Resource").ValueS;
        if (_DistrictDef._Data.HasSub("Benefit"))
        {
            DataBlock benefit = _DistrictDef._Data.GetSub("Benefit");
            if (benefit.HasSub("Extra"))
            {
                ExtraConstruction = benefit.GetSubValueI("Extra/Construction");
            }
        }

        int tax = 1;

        int popBonus = _DistrictDef._Data.GetSubValueI("Default/Pop*Bonus");
        int factoryBonus = _DistrictDef._Data.GetSubValueI("Default/Factory*Bonus");

        int factory = 0;
        int investment = 0;
        string controlType = _DistrictDef._Data.GetSubValueS("Control/Type");
        if (_District != null && _District._Data.HasSub("Pop")) // _District is not null
        {
            controlType = _District.DistrictDef._Data.GetSubValueS("Control/Type");
            factory = _District._Data.GetSubValueI("Factory");
            investment = _District._Data.GetSubValueI("Investment");
        }
        bool stateOwned =  controlType == "State_Owned";
        bool isPrivate = controlType == "Private";
        bool police = controlType == "Police";

        if (stateOwned)
        {
            DataBlock ecoData;
            if (factory > 0) ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("State_Factory_" + factory.ToString());
            else ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("State_Pop");

            DataBlock invData = null;
            if (investment > 0)
            {
                if (factory > 0) invData = Game.self.Def.EconomyData.GetSub("Invest").GetSub("State_Factory_" + factory.ToString(), "Level_" + investment.ToString());
                else invData = Game.self.Def.EconomyData.GetSub("Invest").GetSub("State_Pop", "Level_" + investment.ToString());
            }

            int bonus = popBonus + factoryBonus * factory;
            Production = ecoData.GetSub("Resource").ValueI + bonus;

            TaxBC = 0;
            Wealth = ecoData.GetSub("Wealth").ValueI;
            Reinvest = 0;

            if (invData != null)
            {
                Production += invData.GetSub("Resource").ValueI;
                TaxBC -= invData.GetSub("Cost*BC").ValueI;
                Wealth += invData.GetSubValueI("Wealth");
                Reinvest += invData.GetSubValueI("Reinvest");
            }
        }
        else if (isPrivate)
        {
            DataBlock ecoData;
            if (factory > 0) ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("Private_Factory_" + factory.ToString(), "Tax_" + tax.ToString());
            else ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("Private_Pop", "Tax_" + tax.ToString());

            DataBlock invData = null;
            if (investment > 0)
            {
                if (factory > 0) invData = Game.self.Def.EconomyData.GetSub("Invest").GetSub("Private_Factory_" + factory.ToString(), "Level_" + investment.ToString());
                else invData = Game.self.Def.EconomyData.GetSub("Invest").GetSub("Private_Pop", "Level_" + investment.ToString());
            }

            Production = ecoData.GetSub("Resource").ValueI;
            int privateBC = ecoData.GetSubValueI("PrivateBC");
            TaxBC = privateBC * Game.self.Def.EconomyData.GetSub("Tax").GetSub("Tax_" + tax.ToString()).GetSub("Percent").ValueI / 100;
            Reinvest = (privateBC - TaxBC) * Game.self.Def.EconomyData.GetSub("Reinvest").GetSub("Private_" + (factory > 0 ? "Factory_" + factory.ToString() : "Pop")).GetSub("Percent").ValueI / 100;
            Wealth = privateBC - TaxBC - Reinvest;

            if (invData != null)
            {
                Production += invData.GetSub("Resource").ValueI;
                TaxBC -= invData.GetSub("Cost*BC").ValueI;
                Wealth += invData.GetSubValueI("Wealth");
                Reinvest += invData.GetSubValueI("Reinvest");
            }
        }
        else if (police)
        {
            DataBlock ecoData;
            if (factory > 0) ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("Police_Factory_" + factory.ToString());
            else ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("Police_Pop");

            int bonus = popBonus + factoryBonus * factory;
            Production = ecoData.GetSub("Resource").ValueI + bonus;

            TaxBC = 0;
            Wealth = ecoData.GetSub("Wealth").ValueI;
            Reinvest = 0;
        }
        else
        {
            DataBlock ecoData;
            if (factory > 0) ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("State_Factory_" + factory.ToString());
            else ecoData = Game.self.Def.EconomyData.GetSub("District").GetSub("State_Factory_" + pop.ToString());

            Production = 0;
            TaxBC = 0;
            Wealth = ecoData.GetSub("Wealth").ValueI;
            Reinvest = 0;
        }
    }

    public string ToString_Short(int iconSize = 24)
    {
        string income = "";

        if (Resource == "BC")
        {
            int value = Production + TaxBC;
            if (value > 0) income += Helper.ResValueToString(value) + Helper.GetIcon("BC", iconSize);
            else if (TaxBC < 0) income += Helper.GetColorPrefix_Bad() + Helper.ResValueToString(value) + Helper.GetColorSufix() + Helper.GetIcon("BC", iconSize);
        }
        else
        {
            if (Production != 0) income += Helper.ResValueToString(Production) + Helper.GetIcon(Resource, iconSize);
            if (TaxBC > 0) income += (income.Length > 1 ? "  " : "") + Helper.ResValueToString(TaxBC) + Helper.GetIcon("BC", iconSize);
            else if (TaxBC < 0) income += (income.Length > 1 ? "  " : "") + Helper.GetColorPrefix_Bad() + Helper.ResValueToString(TaxBC) + Helper.GetColorSufix() + Helper.GetIcon("BC", iconSize);
        }
        //if (Wealth != 0) income += (income.Length > 1 ? "  " : "") + Helper.ResValueToString(Wealth) + Helper.GetIcon("Wealth", iconSize);
        //if (Reinvest != 0) income += (income.Length > 1 ? "  " : "") + Wealth.ToString() + Helper.GetIcon("Invest", iconSize);

        if (ExtraConstruction > 0) income += (income.Length > 1 ? "  " : "") + Helper.ResValueToString(ExtraConstruction) + Helper.GetIcon("Construction", iconSize);

        return income;
    }
}
