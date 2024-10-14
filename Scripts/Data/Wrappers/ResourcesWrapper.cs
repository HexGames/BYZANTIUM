using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class ResourcesWrapper
{
    public enum ParentType
    {
        FEATURE,
        DISTRICT,
        COLONY,
        SYSTEM
    };

    public class IncomeInfo
    {
        public class FromOther
        {
            public ColonyData Colony;
            public DistrictData District;
            public int Value;

            public FromOther(ColonyData colony, int value)
            {
                Colony = colony;
                District = null;
                Value = value;
            }

            public FromOther(DistrictData district, int value)
            {
                Colony = null;
                District = district;
                Value = value;
            }

            public string ToString_AsIncome() { return Helper.ResValueToString(Value); }
            public string ToString_AsFocus() {  return Value.ToString(); }
            public string ToString_AsBonus() { return Value.ToString(); }
        };

        //public ResourcesWrapper _Parent = null;
        public string Name = "Res";

        public int IncomeFixed = 0;
        //public int IncomeFromPops = 0;
        public int IncomePerPop = 0;
        //public int IncomeSystemFocus = 0;
        public int SystemIncome = 0;
        public int LocalIncome = 0;
        public int LocalBonus = 0;
        public int Bonus = 0;
        public int Focus = 0;
        public int FocusBase = 0;
        public int FocusChosen = 0;

        public List<FromOther> IncomeFixedOther = new List<FromOther>();
        public List<FromOther> IncomePerPopOther = new List<FromOther>();
        public List<FromOther> FocusOther = new List<FromOther>();
        public List<FromOther> BonusOther = new List<FromOther>();

        public IncomeInfo Copy()
        {
            IncomeInfo info = new IncomeInfo();
        
            //info._Parent = _Parent;
            info.Name = Name;
            
            info.IncomeFixed = IncomeFixed;
            info.IncomePerPop = IncomePerPop;
            //info.IncomeSystemFocus = IncomeSystemFocus;
            info.SystemIncome = SystemIncome;
            info.LocalIncome = LocalIncome;
            info.LocalBonus = LocalBonus;
            info.Bonus = Bonus;
            info.Focus = Focus;
            info.FocusBase = FocusBase;
            info.FocusChosen = FocusChosen;
        
            return info;
        }

        //public int GetIncomeTotal(int pops)
        //{
        //    int popIncome = pops * PerPop / 1000;
        //    return (Income + popIncome) * (100 + Bonus) / 100;
        //}

        public int IncomeFixedTotal()
        {
            int income = IncomeFixed;
            for (int idx = 0; idx < IncomeFixedOther.Count; idx++)
            {
                income += (IncomeFixedOther[idx].Value + LocalIncome) * (100 + LocalBonus) / 100;
            }
            return income;
        }
        public int IncomePerPopTotal()
        {
            int income = IncomeFixed;
            for (int idx = 0; idx < IncomePerPopOther.Count; idx++)
            {
                income += (IncomePerPopOther[idx].Value + LocalIncome) * (100 + LocalBonus) / 100;
            }
            return income;
        }

        public int IncomeFromSystem(SystemData system)
        {
            return system.Resources_PerTurn.GetIncomeSystemFocus(Name) * GetSystemProduction(system) / 10000;
        }

        public int IncomeAllTotal(SystemData system)
        {
            //int bonus = (100 + BonusTotal());
            //int incomeFixed = IncomeFixedTotal();
            //int incomeFromSystem = IncomeFromSystem(system);
            //int incomeFromPops = IncomePerPop * system.Pops_PerTurn.GetPops() / 1000;
            //int total = incomeFixed + incomeFromSystem + incomeFromPops;
            //return bonus * total / 100;
            return (100 + BonusTotal()) * (IncomeFixedTotal() + IncomeFromSystem(system) + IncomePerPop * system.Pops_PerTurn.GetPops() / 1000) / 100;
        }

        public int FocusTotal()
        {
            int focusFromOther = 0;
            for (int idx = 0; idx < FocusOther.Count; idx++)
                focusFromOther += FocusOther[idx].Value;
            return FocusBase + Focus + focusFromOther + FocusChosen;
        }

        public int BonusTotal()
        {
            int bonusFromOther = 0;
            for (int idx = 0; idx < BonusOther.Count; idx++)
                bonusFromOther += BonusOther[idx].Value;
            return Bonus + bonusFromOther;
        }

        //public string ToString_Income(bool total = true) { return Helper.ResValueToString(total ? GetIncomeTotal() : Income); }
        public string ToString_Income() { return Helper.ResValueToString(IncomeFixed); }
        public string ToString_IncomeFromSystem(SystemData system) { return Helper.ResValueToString(IncomeFromSystem(system)); }
        public string ToString_IncomeTotal(SystemData system) { return Helper.ResValueToString(IncomeAllTotal(system)); }
        //public string ToString_IncomeTotal() { return Helper.ResValueToString(IncomeTotal()); }
        //public string ToString_PerPop() { return Helper.ResValueToString(PerPop); }
        //public string ToString_SystemIncome() { return Helper.ResValueToString(SystemIncome); }
        //public string ToString_LocalBonus() { return (LocalBonus >= 0 ? "+" : "") + LocalBonus.ToString() + "%"; }
        //public string ToString_Bonus() { return (Bonus >= 0 ? "+" : "") + Bonus.ToString() + "%"; }
        public string ToString_Focus() { return Focus.ToString(); }
        public string ToString_FocusBase() { return FocusBase.ToString(); }
        public string ToString_FocusChosen() { return FocusChosen.ToString(); }
        public string ToString_FocusTotal() { return FocusTotal().ToString(); }
        public string ToString_BonusTotal() { int bt = BonusTotal(); return (bt > 0 ? "+" : "") + bt.ToString(); }

        public void Set(string type, int value)
        {
            switch(type)
            {
                case "Income": IncomeFixed = value; break;
                case "PerPop": IncomePerPop = value; break;
                // IncomeSystemFocus
                case "SystemIncome": SystemIncome = value; break;
                case "LocalBonus": LocalBonus = value; break;
                case "Bonus": Bonus = value; break;
                case "Focus": Focus = value; break;
                case "FocusBase": FocusBase = value; break;
                case "FocusChosen": FocusChosen = value; break;
            }
        }

        //public void SaveStockpile()
        //{
        //    //_Parent._Data.GetSub(Name + "*Stockpile").ValueI = Stockpile;
        //}
    }
    public class SpecialInfo
    {
        //public ResourcesWrapper _Parent = null

        public int Growth = 0;
        public int GrowthLocalBonus = 0;
        public int PopMax = 0;
        public int PopMaxPenalty = 0;
        public int FactoryCost = 0;
        public int FactoryBonus = 0;

        public void Reset()
        { 
            Growth = 0;
            GrowthLocalBonus = 0;
            PopMax = 0;
            PopMaxPenalty = 0;
            FactoryCost = 0;
            FactoryBonus = 0;
        }

        public SpecialInfo Copy()
        {
            SpecialInfo info = new SpecialInfo();

            //info._Parent = _Parent;

            info.Growth = Growth;
            info.GrowthLocalBonus = GrowthLocalBonus;
            info.PopMax = PopMax;
            info.PopMaxPenalty = PopMaxPenalty;
            info.FactoryCost = FactoryCost;
            info.FactoryBonus = FactoryBonus;

            return info;
        }

        //public int GetIncomeTotal(int pops)
        //{
        //    int popIncome = pops * PerPop / 1000;
        //    return (Income + popIncome) * (100 + Bonus) / 100;
        //}

        //public string ToString_Income(bool total = true) { return Helper.ResValueToString(total ? GetIncomeTotal() : Income); }
        //public string ToString_Income() { return Helper.ResValueToString(Income); }
        //public string ToString_PerPop() { return Helper.ResValueToString(PerPop); }
        //public string ToString_SystemIncome() { return Helper.ResValueToString(SystemIncome); }
        //public string ToString_LocalBonus() { return (LocalBonus >= 0 ? "+" : "") + LocalBonus.ToString() + "%"; }
        //public string ToString_Bonus() { return (Bonus >= 0 ? "+" : "") + Bonus.ToString() + "%"; }
        //public string ToString_Focus() { return Focus.ToString(); 

        public void Set(string type, int value)
        {
            switch (type)
            {
                case "Growth": Growth = value; break;
                case "GrowthLocalBonus": GrowthLocalBonus = value; break;
                case "PopMax": PopMax = value; break;
                case "PopMaxPenalty": PopMaxPenalty = value; break;
                case "FactoryCost": FactoryCost = value; break;
                case "FactoryBonus": FactoryBonus = value; break;
            }
        }

        //public void SaveStockpile()
        //{
        //    //_Parent._Data.GetSub(Name + "*Stockpile").ValueI = Stockpile;
        //}
    }

    public DataBlock _Data = null;
    public List<IncomeInfo> Incomes = new List<IncomeInfo>();
    public SpecialInfo SpecialIncome = new SpecialInfo();
    public ParentType Type = ParentType.SYSTEM;

    static IncomeInfo Default = new IncomeInfo();

    public ResourcesWrapper(DataBlock resData, ParentType type)
    {
        _Data = resData;
        Type = type;

        //Refresh();
    }
    public void Clear()
    {
        Incomes.Clear();
        SpecialIncome.Reset();
    }

    public void Refresh()
    {
        Clear();

        if (_Data == null)
            return;

        Array<DataBlock> resDataSubs = _Data.GetSubs();
        for (int idxData = 0; idxData < resDataSubs.Count; idxData++)
        {
            bool found = false;
            string name = Helper.Split_0(resDataSubs[idxData].Name, '*');
            string type = Helper.Split_1(resDataSubs[idxData].Name, '*');

            if (type == "Income" || type == "PerPop" || type == "SystemIncome" || type == "LocalBonus" || type == "Bonus" || type == "Focus" || type == "FocusBase" || type == "FocusChosen")
            {
                for (int idxRes = 0; idxRes < Incomes.Count; idxRes++)
                {
                    if (Incomes[idxRes].Name == name)
                    {
                        Incomes[idxRes].Set(type, resDataSubs[idxData].ValueI);
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    IncomeInfo newIncome = new IncomeInfo();
                    //newIncome._Parent = this;
                    newIncome.Name = name;
                    newIncome.Set(type, resDataSubs[idxData].ValueI);
                    Incomes.Add(newIncome);
                }
            }
            else
            {
                SpecialIncome.Set(name, resDataSubs[idxData].ValueI);
            }
        }
    }
    //public void AddResourcesFromFeatures(ResourcesWrapper otherRes)
    //{
    //    for (int otherIdx = 0; otherIdx < otherRes.Incomes.Count; otherIdx++)
    //    {
    //        bool found = false;
    //        for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
    //        {
    //            if (Incomes[resIdx].Name == otherRes.Incomes[otherIdx].Name)
    //            {
    //                found = true;
    //                //Incomes[resIdx].IncomeTotal += otherRes.Incomes[otherIdx].IncomeTotal;
    //                Incomes[resIdx].IncomePerPop += otherRes.Incomes[otherIdx].IncomePerPop;
    //                //Incomes[resIdx].IncomeSystemFocus += otherRes.Incomes[otherIdx].IncomeSystemFocus;
    //                Incomes[resIdx].SystemIncome += otherRes.Incomes[otherIdx].SystemIncome;
    //                Incomes[resIdx].LocalIncome += otherRes.Incomes[otherIdx].LocalIncome;
    //                Incomes[resIdx].LocalBonus += otherRes.Incomes[otherIdx].LocalBonus;
    //                Incomes[resIdx].Bonus += otherRes.Incomes[otherIdx].Bonus;
    //                Incomes[resIdx].Focus += otherRes.Incomes[otherIdx].Focus;
    //                Incomes[resIdx].FocusBase += otherRes.Incomes[otherIdx].FocusBase;
    //                Incomes[resIdx].FocusChosen += otherRes.Incomes[otherIdx].FocusChosen;
    //            }
    //        }
    //
    //        if (addMissing && found == false)
    //        {
    //            IncomeInfo otherInfo = otherRes.Incomes[otherIdx];
    //            IncomeInfo info = otherInfo.Copy();
    //            Incomes.Add(info);
    //        }
    //    }
    //
    //    SpecialIncome.Growth += otherRes.SpecialIncome.Growth;
    //    SpecialIncome.GrowthLocalBonus += otherRes.SpecialIncome.GrowthLocalBonus;
    //    SpecialIncome.PopMax += otherRes.SpecialIncome.PopMax;
    //    SpecialIncome.PopMaxPenalty += otherRes.SpecialIncome.PopMaxPenalty;
    //    SpecialIncome.FactoryCost += otherRes.SpecialIncome.FactoryCost;
    //    SpecialIncome.FactoryBonus += otherRes.SpecialIncome.FactoryBonus;
    //}

    public void AddResources_Features(ResourcesWrapper otherRes)
    {
        for (int otherIdx = 0; otherIdx < otherRes.Incomes.Count; otherIdx++)
        {
            bool found = false;
            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == otherRes.Incomes[otherIdx].Name)
                {
                    found = true;
                }
            }

            if (found == false)
            {
                IncomeInfo info = new IncomeInfo();
                info.Name = otherRes.Incomes[otherIdx].Name;
                Incomes.Add(info);
            }

            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == otherRes.Incomes[otherIdx].Name)
                {
                    Incomes[resIdx].IncomePerPop += otherRes.Incomes[otherIdx].IncomePerPop;
                    //Incomes[resIdx].IncomeSystemFocus += otherRes.Incomes[otherIdx].IncomeSystemFocus;
                    Incomes[resIdx].SystemIncome += otherRes.Incomes[otherIdx].SystemIncome;
                    //Incomes[resIdx].LocalIncome += otherRes.Incomes[otherIdx].LocalIncome;
                    Incomes[resIdx].LocalBonus += otherRes.Incomes[otherIdx].LocalBonus;
                    Incomes[resIdx].Bonus += otherRes.Incomes[otherIdx].Bonus;
                    Incomes[resIdx].Focus += otherRes.Incomes[otherIdx].Focus;
                    //Incomes[resIdx].FocusBase += otherRes.Incomes[otherIdx].FocusBase;
                    //Incomes[resIdx].FocusChosen += otherRes.Incomes[otherIdx].FocusChosen;
                }
            }
        }
    
        SpecialIncome.Growth += otherRes.SpecialIncome.Growth;
        SpecialIncome.GrowthLocalBonus += otherRes.SpecialIncome.GrowthLocalBonus;
        SpecialIncome.PopMax += otherRes.SpecialIncome.PopMax;
        SpecialIncome.PopMaxPenalty += otherRes.SpecialIncome.PopMaxPenalty;
        SpecialIncome.FactoryCost += otherRes.SpecialIncome.FactoryCost;
        SpecialIncome.FactoryBonus += otherRes.SpecialIncome.FactoryBonus;
    }
    public void AddResources_Districts(DistrictData district, ResourcesWrapper otherRes)
    {
        for (int otherIdx = 0; otherIdx < otherRes.Incomes.Count; otherIdx++)
        {
            bool found = false;
            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == otherRes.Incomes[otherIdx].Name)
                {
                    found = true;
                }
            }

            if (found == false)
            {
                IncomeInfo info = new IncomeInfo();
                info.Name = otherRes.Incomes[otherIdx].Name;
                Incomes.Add(info);
            }

            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == otherRes.Incomes[otherIdx].Name)
                {
                    //Incomes[resIdx].IncomePerPop += otherRes.Incomes[otherIdx].IncomePerPop;
                    //Incomes[resIdx].IncomeSystemFocus += otherRes.Incomes[otherIdx].IncomeSystemFocus;
                    Incomes[resIdx].SystemIncome += otherRes.Incomes[otherIdx].SystemIncome;
                    //Incomes[resIdx].LocalIncome += otherRes.Incomes[otherIdx].LocalIncome;
                    Incomes[resIdx].LocalBonus += otherRes.Incomes[otherIdx].LocalBonus;
                    Incomes[resIdx].Bonus += otherRes.Incomes[otherIdx].Bonus;
                    Incomes[resIdx].Focus += otherRes.Incomes[otherIdx].Focus;
                    //Incomes[resIdx].FocusBase += otherRes.Incomes[otherIdx].FocusBase;
                    //Incomes[resIdx].FocusChosen += otherRes.Incomes[otherIdx].FocusChosen;

                    Incomes[resIdx].IncomeFixedOther.Add(new IncomeInfo.FromOther(district, otherRes.Incomes[otherIdx].IncomeFixed));
                    Incomes[resIdx].IncomePerPopOther.Add(new IncomeInfo.FromOther(district, otherRes.Incomes[otherIdx].IncomePerPop));
                }
            }
        }

        SpecialIncome.Growth += otherRes.SpecialIncome.Growth;
        SpecialIncome.GrowthLocalBonus += otherRes.SpecialIncome.GrowthLocalBonus;
        SpecialIncome.PopMax += otherRes.SpecialIncome.PopMax;
        SpecialIncome.PopMaxPenalty += otherRes.SpecialIncome.PopMaxPenalty;
        SpecialIncome.FactoryCost += otherRes.SpecialIncome.FactoryCost;
        SpecialIncome.FactoryBonus += otherRes.SpecialIncome.FactoryBonus;
    }

    public void AddSystemIncome(ResourcesWrapper colonyRes, bool addMissing = false)
    {
        for(int otherIdx = 0; otherIdx < colonyRes.Incomes.Count; otherIdx++)
        {
            bool found = false;
            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == colonyRes.Incomes[otherIdx].Name)
                {
                    found = true;
                    Incomes[resIdx].SystemIncome += colonyRes.Incomes[otherIdx].SystemIncome;
                }
            }

            if (addMissing && found == false)
            {
                IncomeInfo info = new IncomeInfo();
                info.SystemIncome += colonyRes.Incomes[otherIdx].SystemIncome;
                Incomes.Add(info);
            }
        }
    }

    public void AddSystemIncomeToColonies(ResourcesWrapper systemRes)
    {
        for (int otherIdx = 0; otherIdx < systemRes.Incomes.Count; otherIdx++)
        {
            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == systemRes.Incomes[otherIdx].Name)
                {
                    Incomes[resIdx].LocalIncome += systemRes.Incomes[otherIdx].SystemIncome;
                }
            }
        }
    }

    public void AddColonyResources(ColonyData colony)
    {
        if (Type != ParentType.SYSTEM)
            return;

        ResourcesWrapper colonyRes = colony.Resources_PerTurn;
        for (int otherIdx = 0; otherIdx < colonyRes.Incomes.Count; otherIdx++)
        {
            bool found = false;
            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == colonyRes.Incomes[otherIdx].Name)
                {
                    found = true;
                }
            }

            if (found == false)
            {
                IncomeInfo info = new IncomeInfo();
                info.Name = colonyRes.Incomes[otherIdx].Name;
                Incomes.Add(info);
            }

            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == colonyRes.Incomes[otherIdx].Name)
                {
                    Incomes[resIdx].IncomePerPop += colonyRes.Incomes[otherIdx].IncomePerPop;

                    int incomeFixedTotal = colonyRes.Incomes[otherIdx].IncomeFixedTotal();
                    if (incomeFixedTotal != 0) Incomes[resIdx].IncomeFixedOther.Add(new IncomeInfo.FromOther(colony, incomeFixedTotal));
                    int incomePerPopTotal = colonyRes.Incomes[otherIdx].IncomeFixedTotal();
                    if (incomePerPopTotal != 0) Incomes[resIdx].IncomePerPopOther.Add(new IncomeInfo.FromOther(colony, incomePerPopTotal));
                    if (colonyRes.Incomes[otherIdx].Focus != 0) Incomes[resIdx].FocusOther.Add(new IncomeInfo.FromOther(colony, colonyRes.Incomes[otherIdx].Focus));
                    if (colonyRes.Incomes[otherIdx].Bonus != 0) Incomes[resIdx].BonusOther.Add(new IncomeInfo.FromOther(colony, colonyRes.Incomes[otherIdx].Bonus));
                }
            }
        }

        SpecialIncome.Growth += colonyRes.SpecialIncome.Growth;
        //SpecialIncome.GrowthLocalBonus += otherRes.SpecialIncome.GrowthLocalBonus; // already added
        SpecialIncome.PopMax += colonyRes.SpecialIncome.PopMax;
        SpecialIncome.PopMaxPenalty += colonyRes.SpecialIncome.PopMaxPenalty;
        SpecialIncome.FactoryCost += colonyRes.SpecialIncome.FactoryCost;
        SpecialIncome.FactoryBonus += colonyRes.SpecialIncome.FactoryBonus;
    }

    public IncomeInfo GetIncome(string name, bool returnDefault = true)
    {
        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            if (Incomes[idx].Name == name)
            {
                return Incomes[idx];
            }
        }
        return returnDefault ? Default : null;
    }

    public int GetIncomeSystemFocus(string name) 
    {
        int focus = 0;
        int focusTotal = 0;
        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            focusTotal += Incomes[idx].FocusTotal();

            if (Incomes[idx].Name == name)
            {
                focus = Incomes[idx].FocusTotal();
            }
        }
        if (focusTotal == 0) return 0;
        else return 10000 * focus / focusTotal;
    }

    public static int GetSystemProduction(SystemData system)
    {
        int pops = system.Pops_PerTurn.GetPops() / 1000;
        int workingFactories = Mathf.Min(system.Pops_PerTurn.GetPops(), system.Buildings_PerTurn.Factories) / 1000;
        int emptyFactories = system.Buildings_PerTurn.Factories / 1000 - workingFactories;

        return (pops + workingFactories * 4) * 10 + emptyFactories * 5;
    }
    public static int GetSystemBC(SystemData system)
    {
        int pops = system.Pops_PerTurn.GetPops() / 1000;
        int workingFactories = Mathf.Min(system.Pops_PerTurn.GetPops(), system.Buildings_PerTurn.Factories) / 1000;
        int emptyFactories = system.Buildings_PerTurn.Factories / 1000 - workingFactories;

        return (pops + workingFactories) * 10;
    }

    //public void PropagateSystemBonuses(ResourcesWrapper systemRes)
    //{
    //    for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
    //    {
    //        for (int otherIdx = 0; otherIdx < systemRes.Incomes.Count; otherIdx++)
    //        {
    //            if (Incomes[resIdx].Name == systemRes.Incomes[otherIdx].Name)
    //            {
    //                Incomes[resIdx].PerLevel_FromSystem += systemRes.Incomes[otherIdx].PerLevel_System;
    //            }
    //        }
    //    }
    //}

    //public void Add(ResourcesWrapper otherRes, bool totals = false, bool addMissing = false)
    //{
    //    for (int otherIdx = 0; otherIdx < otherRes.Incomes.Count; otherIdx++)
    //    {
    //        bool found = false;
    //        for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
    //        {
    //            if (Incomes[resIdx].Name == otherRes.Incomes[otherIdx].Name)
    //            {
    //                found = true;
    //                if (totals)
    //                {
    //                    Incomes[resIdx].Income += otherRes.Incomes[otherIdx].GetIncomeTotal();
    //                }
    //                else
    //                {
    //                    Incomes[resIdx].Income += otherRes.Incomes[otherIdx].Income;
    //                    Incomes[resIdx].Level += otherRes.Incomes[otherIdx].Level;
    //                    Incomes[resIdx].PerPop += otherRes.Incomes[otherIdx].PerPop;
    //                    Incomes[resIdx].PerCPop += otherRes.Incomes[otherIdx].PerCPop;
    //                    Incomes[resIdx].PerLevel += otherRes.Incomes[otherIdx].PerLevel;
    //                    Incomes[resIdx].PerLevel_System += otherRes.Incomes[otherIdx].PerLevel_System;
    //                    Incomes[resIdx].Bonus += otherRes.Incomes[otherIdx].Bonus;
    //                    // Incomes[resIdx].Stockpile += otherRes.Incomes[otherIdx].Stockpile;
    //                }
    //            }
    //        }
    //
    //        if (addMissing && found == false)
    //        {
    //            IncomeInfo otherInfo = otherRes.Incomes[otherIdx];
    //            IncomeInfo info = otherInfo.Copy();
    //            Incomes.Add(info);
    //        }
    //    }
    //}

    //public void ProcessGrowth()
    //{
    //    if (Pops != null)
    //    {
    //        Pops.Pops = Mathf.Min(Pops.Pops + Pops.GetTrueGrowth(), Pops.PopsMax);
    //        Pops.SavePops();
    //    }
    //}

    //public void ProcessIncome()
    //{
    //    for (int idx = 0; idx < Incomes.Count; idx++)
    //    {
    //        if (Incomes[idx].HasStockpile)
    //        {
    //            Incomes[idx].Stockpile += Incomes[idx].GetIncomeTotal();
    //            Incomes[idx].SaveStockpile();
    //        }
    //    }
    //}

    //public IncomeInfo GetIncome(string name)
    //{
    //    for (int idx = 0; idx < Incomes.Count; idx++)
    //    {
    //        if (Incomes[idx].Name == name)
    //        {
    //            return Incomes[idx];
    //        }
    //    }
    //    return null;
    //}

    //public LimitInfo GetLimit(string name)
    //{
    //    for (int idx = 0; idx < Limits.Count; idx++)
    //    {
    //        if (Limits[idx].Name == name)
    //        {
    //            return Limits[idx];
    //        }
    //    }
    //    return null;
    //

    //public string GetIncomeString(string name)
    //{
    //    for (int idx = 0; idx < Incomes.Count; idx++)
    //    {
    //        if (Incomes[idx].Name == name)
    //        {
    //            int value = Incomes[idx].GetIncomeTotal();
    //            return (value >= 0 ? Helper.ResValueToString(value) : "[color=#ff8888]" + Helper.ResValueToString(value) + "[/color]");
    //        }
    //    }
    //    return "";
    //}

    //public string GetIncomeString(string name, int value)
    //{
    //    for (int idx = 0; idx < Incomes.Count; idx++)
    //    {
    //        if (Incomes[idx].Name == name)
    //        {
    //            return (value >= 0 ? Helper.ResValueToString(value) : "[color=#ff8888]" + Helper.ResValueToString(value) + "[/color]");
    //        }
    //    }
    //    return "";
    //}

    //public string ToStringCondensed()
    //{
    //    string totalIncome = "";
    //
    //    for (int idx = 0; idx < Incomes.Count; idx++)
    //    {
    //        if (Incomes[idx].GetIncomeTotal() != 0)
    //        {
    //            totalIncome += " " + Incomes[idx].ToString_Income() + Helper.GetIcon(Incomes[idx].Name);
    //        }
    //    }
    //
    //    if (Pops != null)
    //    {
    //        if (Pops.GrowthBonus != 0)
    //        {
    //            totalIncome += Pops.ToString_GrowthBonus() + Helper.GetIcon("Growth");
    //        }
    //        if (Pops.GrowthPenalty != 0)
    //        {
    //            totalIncome += Pops.ToString_GrowthPenalty() + Helper.GetIcon("Growth");
    //        }
    //    }
    //
    //    return totalIncome;
    //}
}
