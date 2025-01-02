using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

public class DistrictEconomyWrapper
{
    public string Resource;

    public bool Stoped;

    public int Production_Base;
    public int Production_BonusToSystem;
    public int Production_BonusFromSystem;
    public int Production_LocalBonus;
    public int Production_BeforeTax;
    public int Production_Final;
    public int InvestBC_Final;
    //public int TaxBC_Final;
    public int Wealth_Final;
    public int ExtraBC;

    public bool ReinvestActive;
    public int ReinvestPerTurn;
    public int ReinvestProgress;
    public int ReinvestMax;
    public int ReinvestTurns;
    public int ReinvestToSystem;
    public int ReinvestFromSystem;

    DistrictData _District;
    DistrictNew _DistrictNew;

    public DistrictEconomyWrapper(DistrictData district)
    {
        _District = district;
    }
    public DistrictEconomyWrapper(DistrictNew districtNew)
    {
        _DistrictNew = districtNew;
    }

    public void Clear()
    {
        Resource = "";
        Stoped = false;
        Production_Base = 0;
        Production_BonusToSystem = 0;
        Production_BonusFromSystem = 0;
        Production_LocalBonus = 0;
        Production_BeforeTax = 0;
        Production_Final = 0;
        InvestBC_Final = 0;
        Wealth_Final = 0;
        ExtraBC = 0;
        ReinvestActive = false;
        ReinvestPerTurn = 0;
        ReinvestProgress = 0;
        ReinvestMax = 0;
        ReinvestTurns = 999;
        ReinvestToSystem = 0;
        ReinvestFromSystem = 0;
    }

    public void RefreshBase_1_1()
    {
        DefDistrictWrapper districtDef = null;
        
        if (_District != null) districtDef = _District.DistrictDef;
        else if (_DistrictNew != null) districtDef = _DistrictNew.DistrictDef;

        if (districtDef == null)
            return;

        Stoped = _District != null && _District._Data.GetSubValueI("ActionChange", "StopWorking") == 1;
        if (Stoped)
        {
            Resource = districtDef.Benefit.Resource;
            Wealth_Final = districtDef.Benefit.Wealth;
        }
        else
        {
            Resource = districtDef.Benefit.Resource;
            ExtraBC = districtDef.Benefit.ExtraBC;

            Production_Base = districtDef.Benefit.Income;
            Production_BonusToSystem = districtDef.Benefit.Bonus;
            Production_BonusFromSystem = 0;
            Production_LocalBonus = 0;

            Wealth_Final = districtDef.Benefit.Wealth;

            if (_District != null && districtDef.Control_Type == "Private")
            {
                ReinvestActive = true;
                ReinvestProgress = _District.GetReinvestProgress();
                ReinvestMax = 2 * districtDef.Cost_BC;
            }
        }
    }

    public void AddBonusFromSystem_1_2(int bonus)
    {
        if (Stoped)
            return;

        Production_BonusFromSystem += bonus;
    }
        
    public void RefreshFinal_1_3()
    {
        if (Stoped)
            return;

        DefDistrictWrapper districtDef = null;
        bool isPrivate = false;
        int taxPerc = 50;  
        int investment = 0;
        int privateDistricts = 0;

        if (_District != null)
        {
            districtDef = _District.DistrictDef;
            isPrivate = _District.IsPrivate();
            taxPerc = _District._Colony._System.GetTaxPerc();
            investment = _District._Data.GetSubValueI("InvestLevel");
            privateDistricts = _District._Colony._System.GetNumberOfPrivateDistricts();
        }
        else if (_DistrictNew != null)
        {
            districtDef = _DistrictNew.DistrictDef;
            isPrivate = _DistrictNew.IsPrivate();
            taxPerc = _DistrictNew._Colony._System.GetTaxPerc();
            investment = 0;
            privateDistricts = 0;
        }

        if (districtDef == null)
            return;

        Production_BeforeTax = (Production_Base + Production_BonusFromSystem) * (100 + Production_LocalBonus) / 100;

        if (isPrivate)
        {
            int investLocalBonus = 0;
            if (investment > 0)
            {
                int investmentIdx = Mathf.Clamp(investment - 1, 0, districtDef.Benefit.Invest_CostBC.Count);
                InvestBC_Final = districtDef.Benefit.Invest_CostBC[investment];
                investLocalBonus = districtDef.Benefit.Invest_ExtraLocalBonus[investment];
            }
            else
            {
                InvestBC_Final = 0;
            }

            int productionFromInvestment = Production_BeforeTax * investLocalBonus / 100;

            Production_Final = Production_BeforeTax * taxPerc / 100 + productionFromInvestment;

            if (privateDistricts > 0)
            {
                int remaining = Production_BeforeTax - Production_BeforeTax * taxPerc / 100;
                int toWealth = remaining - remaining * privateDistricts / (privateDistricts + 1);
                Wealth_Final += toWealth;
                int toSystem = (remaining - toWealth) * (privateDistricts - 1) / privateDistricts;
                ReinvestToSystem = toSystem;
                ReinvestPerTurn = remaining - toWealth - toSystem + InvestBC_Final;
            }
        }
        else
        {
            Production_Final = Production_BeforeTax;
        }

        RecalculateReinvestTurns();
    }

    public void RecalculateReinvestTurns()
    {
        if (ReinvestPerTurn > 0) ReinvestTurns = ((ReinvestMax - ReinvestProgress) + (ReinvestPerTurn - 1)) / ReinvestPerTurn;
    }

    public string ToString_Short(int iconSize = 24)
    {
        string income = "";

        if (Stoped)
        {
            income = "Retooling";
        }
        else if (Resource == "BC")
        {
            int value = Production_Final + ExtraBC - InvestBC_Final;
            income += Helper.ResValueToString(value, 10, false, true) + Helper.GetIcon(Resource, iconSize);
            //if (value > 0) income += Helper.ResValueToString(value) + Helper.GetIcon("BC", iconSize);
            //else if (value < 0) income += Helper.GetColorPrefix_Bad() + Helper.ResValueToString(value) + Helper.GetColorSufix() + Helper.GetIcon("BC", iconSize);
        }
        else
        {
            int bc = ExtraBC - InvestBC_Final;
            income += Helper.ResValueToString(Production_Final) + Helper.GetIcon(Resource, iconSize);
            //if (TaxBC > 0) income += (income.Length > 1 ? "  " : "") + Helper.ResValueToString(TaxBC) + Helper.GetIcon("BC", iconSize);
            //else if (TaxBC < 0) income += (income.Length > 1 ? "  " : "") + Helper.GetColorPrefix_Bad() + Helper.ResValueToString(TaxBC) + Helper.GetColorSufix() + Helper.GetIcon("BC", iconSize);
            if (bc != 0) income += (income.Length > 1 ? "  " : "") + Helper.ResValueToString(bc, 10, false, true) + Helper.GetIcon("BC", iconSize);
        }
        //if (Wealth != 0) income += (income.Length > 1 ? "  " : "") + Helper.ResValueToString(Wealth) + Helper.GetIcon("Wealth", iconSize);
        //if (Reinvest != 0) income += (income.Length > 1 ? "  " : "") + Wealth.ToString() + Helper.GetIcon("Invest", iconSize);


        return income;
    }
}
