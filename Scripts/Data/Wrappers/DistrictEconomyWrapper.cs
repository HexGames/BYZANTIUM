using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

public class DistrictEconomyWrapper
{
    //public string Resource;

    //public bool Stoped;
    public class Resource
    {
        public int Production_Base = 0;
        public int Production_BaseLocalBonus = 0;
        public int Production_PerPop = 0;
        public int Production_PerPopLocalBonus = 0;
        public int Production_SystemPerPopMultiplier = 0;
        public int Production_FromSystemPerPop = 0;

        public int Production_SystemPerPop = 0;
        public int Production_SystemPerPopBonuis = 0;

        public int Production_TotalBase = 0;
        public int Production_TotalPerPop = 0;
        public int Production_TotalPrivate = 0;

        public int Production_TotalAll = 0;

        public void Clear()
        {
            Production_Base = 0;
            Production_BaseLocalBonus = 0;
            Production_PerPop = 0;
            Production_PerPopLocalBonus = 0;
            Production_SystemPerPopMultiplier = 0;
            Production_FromSystemPerPop = 0;

            Production_SystemPerPop = 0;
            Production_SystemPerPopBonuis = 0;

            Production_TotalBase = 0;
            Production_TotalPerPop = 0;
            Production_TotalPrivate = 0;
        }

        public void Refresh_FromDef(DefBenefitWrapper.Resource defRes)
        {
            Production_Base = defRes.Base;
            Production_BaseLocalBonus = defRes.LocalBonus;
            Production_PerPop = defRes.PerPop;
            Production_PerPopLocalBonus = defRes.LocalPerPopBonus;
            Production_SystemPerPopMultiplier = defRes.SystemPerPopMultiplier;

            Production_SystemPerPop = defRes.SystemPerPop;
            Production_SystemPerPopBonuis = defRes.SystemPerPopBonuis;
        }

        public void Refresh_AddToFromSystem(int bonusePerPop, int bonusPerPopExtra)
        {
            Production_FromSystemPerPop += bonusePerPop * (100 + bonusPerPopExtra) / 100;
        }

        public void Refresh_FromLevel(int level, bool isPrivate)
        {
            int total = level * Production_Base * (100 + Production_BaseLocalBonus) / 100;
            Production_TotalBase = total / (isPrivate ? 2 : 1);
            Production_TotalPrivate += total - Production_TotalBase;
        }

        public void Refresh_FromPops(int pops, bool isPrivate)
        {
            Production_TotalPerPop = pops * Production_PerPop * (100 + Production_SystemPerPop + Production_SystemPerPopBonuis) / 100;
            Production_TotalPrivate += 0;
        }

        public void Refresh_Total()
        {
            Production_TotalAll = Production_TotalBase + Production_TotalPerPop;
        }
    };

    public Resource BC = new Resource();
    public Resource Research = new Resource();
    public Resource Influence = new Resource();
    public Resource Shipbuilding = new Resource();
    public Resource Growth = new Resource();

    public int MaxPop_Extra = 0;

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
        BC.Clear();
        Research.Clear();
        Influence.Clear();
        Shipbuilding.Clear();
        Growth.Clear();

        MaxPop_Extra = 0;
    }

    public void RefreshBase_1_1()
    {
        if (_District != null && _District.DistrictDef != null)
        {
            BC.Refresh_FromDef(_District.DistrictDef.Benefit.BC);
            Research.Refresh_FromDef(_District.DistrictDef.Benefit.Research);
            Influence.Refresh_FromDef(_District.DistrictDef.Benefit.Influence);
            Shipbuilding.Refresh_FromDef(_District.DistrictDef.Benefit.Shipbuilding);
            Growth.Refresh_FromDef(_District.DistrictDef.Benefit.Growth);

            MaxPop_Extra += _District.DistrictDef.Benefit.MaxPop;
        }
    }

    public void AddBonusFromSystem_1_2(DistrictEconomyWrapper fromDistrict)
    {
        if (_District != null && _District.DistrictDef != null)
        {
            BC.Refresh_AddToFromSystem(fromDistrict.BC.Production_SystemPerPop, fromDistrict.BC.Production_SystemPerPopBonuis);
            Research.Refresh_AddToFromSystem(fromDistrict.Research.Production_SystemPerPop, fromDistrict.Research.Production_SystemPerPopBonuis);
            Influence.Refresh_AddToFromSystem(fromDistrict.Influence.Production_SystemPerPop, fromDistrict.Influence.Production_SystemPerPopBonuis);
            Shipbuilding.Refresh_AddToFromSystem(fromDistrict.Shipbuilding.Production_SystemPerPop, fromDistrict.Shipbuilding.Production_SystemPerPopBonuis);
            Growth.Refresh_AddToFromSystem(fromDistrict.Growth.Production_SystemPerPop, fromDistrict.Growth.Production_SystemPerPopBonuis);
        }
    }

    public void RefreshFinal_1_3()
    {
        if (_District != null && _District.DistrictDef != null)
        {
            int level = _District.GetLevel();
            BC.Refresh_FromLevel(level, _District.IsPrivate());
            Research.Refresh_FromLevel(level, _District.IsPrivate());
            Influence.Refresh_FromLevel(level, _District.IsPrivate());
            Shipbuilding.Refresh_FromLevel(level, _District.IsPrivate());
            Growth.Refresh_FromLevel(level, _District.IsPrivate());

            int pops = _District.Pops.Count;
            BC.Refresh_FromPops(pops, _District.IsPrivate());
            Research.Refresh_FromPops(pops, _District.IsPrivate());
            Influence.Refresh_FromPops(pops, _District.IsPrivate());
            Shipbuilding.Refresh_FromPops(pops, _District.IsPrivate());
            Growth.Refresh_FromPops(pops, _District.IsPrivate());

            BC.Refresh_Total();
            Research.Refresh_Total();
            Influence.Refresh_Total();
            Shipbuilding.Refresh_Total();
            Growth.Refresh_Total();
        }
    }

        //if (_District != null) districtDef = _District.DistrictDef;
        //else if (_DistrictNew != null) districtDef = _DistrictNew.DistrictDef;
        //
        //if (districtDef == null)
        //    return;
        //
        //Stoped = _District != null && _District._Data.GetSubValueI("ActionChange", "StopWorking") == 1;
        //if (Stoped)
        //{
        //    Resource = districtDef.Benefit.Resource;
        //    Wealth_Final = districtDef.Benefit.Wealth;
        //}
        //else
        //{
        //    Resource = districtDef.Benefit.Resource;
        //    ExtraBC = districtDef.Benefit.ExtraBC;
        //
        //    Production_Base = districtDef.Benefit.Income;
        //    Production_BonusToSystem = districtDef.Benefit.Bonus;
        //    Production_BonusFromSystem = 0;
        //    Production_LocalBonus = 0;
        //
        //    Wealth_Final = districtDef.Benefit.Wealth;
        //
        //    if (_District != null && districtDef.Control_Type == "Private")
        //    {
        //        ReinvestActive = true;
        //        ReinvestProgress = _District.GetReinvestProgress();
        //        ReinvestMax = 2 * districtDef.Cost_BC;
        //    }
        //}

    //public void AddBonusFromSystem_1_2(int bonus)
    //{
    //    if (Stoped)
    //        return;
    //
    //    Production_BonusFromSystem += bonus;
    //}
    //    
    //public void RefreshFinal_1_3()
    //{
    //    if (Stoped)
    //        return;
    //
    //    DefDistrictWrapper districtDef = null;
    //    bool isPrivate = false;
    //    int taxPerc = 50;  
    //    int investment = 0;
    //    int privateDistricts = 0;
    //
    //    if (_District != null)
    //    {
    //        districtDef = _District.DistrictDef;
    //        isPrivate = _District.IsPrivate();
    //        taxPerc = _District._Colony._System.GetTaxPerc();
    //        investment = _District._Data.GetSubValueI("InvestLevel");
    //        privateDistricts = _District._Colony._System.GetNumberOfPrivateDistricts();
    //    }
    //    else if (_DistrictNew != null)
    //    {
    //        districtDef = _DistrictNew.DistrictDef;
    //        isPrivate = _DistrictNew.IsPrivate();
    //        taxPerc = _DistrictNew._Colony._System.GetTaxPerc();
    //        investment = 0;
    //        privateDistricts = 0;
    //    }
    //
    //    if (districtDef == null)
    //        return;
    //
    //    Production_BeforeTax = (Production_Base + Production_BonusFromSystem) * (100 + Production_LocalBonus) / 100;
    //
    //    if (isPrivate)
    //    {
    //        int investLocalBonus = 0;
    //        if (investment > 0)
    //        {
    //            int investmentIdx = Mathf.Clamp(investment - 1, 0, districtDef.Benefit.Invest_CostBC.Count);
    //            InvestBC_Final = districtDef.Benefit.Invest_CostBC[investment];
    //            investLocalBonus = districtDef.Benefit.Invest_ExtraLocalBonus[investment];
    //        }
    //        else
    //        {
    //            InvestBC_Final = 0;
    //        }
    //
    //        int productionFromInvestment = Production_BeforeTax * investLocalBonus / 100;
    //
    //        Production_Final = Production_BeforeTax * taxPerc / 100 + productionFromInvestment;
    //
    //        if (privateDistricts > 0)
    //        {
    //            int remaining = Production_BeforeTax - Production_BeforeTax * taxPerc / 100;
    //            int toWealth = remaining - remaining * privateDistricts / (privateDistricts + 1);
    //            Wealth_Final += toWealth;
    //            int toSystem = (remaining - toWealth) * (privateDistricts - 1) / privateDistricts;
    //            ReinvestToSystem = toSystem;
    //            ReinvestPerTurn = remaining - toWealth - toSystem + InvestBC_Final;
    //        }
    //    }
    //    else
    //    {
    //        Production_Final = Production_BeforeTax;
    //    }
    //
    //    RecalculateReinvestTurns();
    //}

    //public void RecalculateReinvestTurns()
    //{
    //    if (ReinvestPerTurn > 0) ReinvestTurns = ((ReinvestMax - ReinvestProgress) + (ReinvestPerTurn - 1)) / ReinvestPerTurn;
    //}

    public string ToString_Short(int iconSize = 24)
    {
        string income = "";

        if (BC.Production_TotalAll > 0)
        {
            income += Helper.ResValueToString(BC.Production_TotalAll, 10, false, true) + Helper.GetIcon("BC", iconSize);
        }
        if (Influence.Production_TotalAll > 0)
        {
            income += Helper.ResValueToString(Influence.Production_TotalAll, 10, false, true) + Helper.GetIcon("Influence", iconSize);
        }
        if (Shipbuilding.Production_TotalAll > 0)
        {
            income += Helper.ResValueToString(Shipbuilding.Production_TotalAll, 10, false, true) + Helper.GetIcon("Shipbuilding", iconSize);
        }
        if (Research.Production_TotalAll > 0)
        {
            income += Helper.ResValueToString(Research.Production_TotalAll, 10, false, true) + Helper.GetIcon("Research", iconSize);
        }
        if (Growth.Production_TotalAll > 0)
        {
            income += Helper.ResValueToString(Growth.Production_TotalAll, 10, false, true) + Helper.GetIcon("Growth", iconSize);
        }
        if (MaxPop_Extra > 0)
        {
            income += "+" + MaxPop_Extra.ToString() + Helper.GetIcon("Pops", iconSize) + "Max";
        }

        return income;
    }
}
