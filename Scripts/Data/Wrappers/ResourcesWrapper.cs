using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class ResourcesWrapper
{
    public enum ParentType
    {
        Feature,
        Planet,
        Building,
        Colony,
        System,
        Sector,
        Player
    };

    public class IncomeInfo
    {
        public ResourcesWrapper _Parent = null;
        public string Name = "Res";

        public bool HasStockpile = false;

        public int Income = 0;
        public int Level = 0;
        public int PerPop = 0;
        public int PerCPop = 0;
        public int PerLevel = 0;
        public int PerLevel_System = 0;
        public int PerLevel_FromSystem = 0;
        public int Bonus = 0;
        public int Stockpile = 0;

        public IncomeInfo Copy()
        {
            IncomeInfo info = new IncomeInfo();

            info._Parent = _Parent;
            info.Name = Name;
            
            info.HasStockpile = HasStockpile;
            
            info.Income = Income;
            info.Level = Level;
            info.PerPop = PerPop;
            info.PerCPop = PerCPop;
            info.PerLevel = PerLevel;
            info.PerLevel_System = PerLevel_System;
            info.PerLevel_FromSystem = PerLevel_FromSystem;
            info.Bonus = Bonus;
            info.Stockpile = Stockpile;

            return info;
        }

        public int GetIncomeTotal()
        {
            int popIncome = 0;
            if (_Parent.Pops != null)
                popIncome = _Parent.Pops.Pops * PerPop / 1000 + _Parent.Pops.GetCPops() * PerCPop / 1000;
            return (Income + popIncome + (PerLevel + PerLevel_FromSystem) * Level) * (100 + Bonus) / 100;
        }

        public int GetIncomeTotalSpecial(bool idleConstruction)
        {
            if (Name == "BC")
                return GetIncomeTotal() + _Parent.GetIncome("Production").GetIncomeTotal() / 2;

            return GetIncomeTotal();
        }

        public string ToString_Income(bool total = true) { return Helper.ResValueToString(total ? GetIncomeTotal() : Income); }
        public string ToString_IncomeTotalSpecial() { return Helper.ResValueToString(GetIncomeTotalSpecial(true)); }
        public string ToString_Level() { return Level.ToString(); }
        public string ToString_PerPop() { return Helper.ResValueToString(PerPop); }
        public string ToString_PerCPop() { return Helper.ResValueToString(PerCPop); }
        public string ToString_PerLevel(bool total = true) { return Helper.ResValueToString(total ? PerLevel + PerLevel_FromSystem : PerLevel); }
        public string ToString_PerLevelSystem() { return Helper.ResValueToString(PerLevel_System); }
        public string ToString_Bonus() { return (Bonus >= 0 ? "+" : "") + Bonus + "%"; }
        public string ToString_Stockpile() { return Helper.ResValueToString(Stockpile); }

        public void Set(string type, int value)
        {
            switch(type)
            {
                case "Income": Income = value; break;
                case "Level": Level = value; break;
                case "PerPop": PerPop = value; break;
                case "PerControlledPop": PerCPop = value; break;
                case "PerLevel": PerLevel = value; break;
                case "PerLevelSystem": PerLevel_System = value; break;
                case "Bonus": Bonus = value; break;
                case "Stockpile": Stockpile = value; HasStockpile = true; break;
            }
        }

        public void SaveStockpile()
        {
            _Parent._Data.GetSub(Name + "*Stockpile").ValueI = Stockpile;
        }
    }

    public class LimitInfo
    {
        public ResourcesWrapper _Parent = null;
        public string Name = "Res";

        public int Max = 0;
        public int MaxBonus = 0;
        public int Used = 0;
        public int UsedBonus = 0;

        public LimitInfo Copy()
        {
            LimitInfo info = new LimitInfo();

            info._Parent = _Parent;
            info.Name = Name;

            info.Max = Max;
            info.MaxBonus = MaxBonus;
            info.Used = Used;
            info.UsedBonus = UsedBonus;

            return info;
        }

        public int GetMaxTotal()
        {
            return Max * (100 + MaxBonus) / 100;
        }
        public int GetUsedTotal()
        {
            return Used * (100 + UsedBonus) / 100;
        }

        public string ToString_Max(bool total = true) { return Helper.ResValueToString(total ? GetMaxTotal() : Max); }
        public string ToString_MaxBonus() { return (MaxBonus >= 0 ? "+" : "") + MaxBonus + "%"; }
        public string ToString_Used(bool total = true) { return Helper.ResValueToString(total ? GetUsedTotal() : Used); }
        public string ToString_UsedBonus() { return (UsedBonus >= 0 ? "+" : "") + UsedBonus + "%"; }

        public void Set(string type, int value)
        {
            switch (type)
            {
                case "Max": Max = value; break;
                case "MaxBonus": MaxBonus = value; break;
                case "Used": Used = value; break;
                case "UsedBonus": UsedBonus = value; break;
            }
        }
    }

    public class PopsInfo
    {
        public ResourcesWrapper _Parent = null;
        public string Name = "Res";

        public int CPops = 0;
        public int Pops = 0;
        public int PopsMax = 0;
        public int PopsMaxBonus = 0;
        public int Control = 0;
        public int Growth = 0;
        public int GrowthBonus = 0;
        public int GrowthPenalty = 0;

        public PopsInfo Copy()
        {
            PopsInfo info = new PopsInfo();

            info._Parent = _Parent;
            info.Name = Name;

            info.CPops = CPops;
            info.Pops = Pops;
            info.PopsMax = PopsMax;
            info.PopsMaxBonus = PopsMaxBonus;
            info.Control = Control;
            info.Growth = Growth;
            info.GrowthBonus = GrowthBonus;
            info.GrowthPenalty = GrowthPenalty;

            return info;
        }

        public int GetPopsMaxTotal() { return PopsMax * (100 + PopsMaxBonus) / 100; }
        public int GetCPops() { return CPops > 0 ? CPops : Pops * Control / 100; }
        public int GetGrowthTotal() { return Growth * ((100 + GrowthBonus) / 100) * ((100 - GrowthPenalty) / 100); }
        public int GetTrueGrowth() { return Pops * GetGrowthTotal() / 10000; }

        public string ToString_Pops() { return Helper.ResValueToString(Pops, 1000); }
        public string ToString_CPops() { return Helper.ResValueToString(GetCPops(), 1000); }
        public string ToString_IPops() { return Helper.ResValueToString(Pops - GetCPops(), 1000); }
        public string ToString_PopsMax(bool total = true) { return Helper.ResValueToString(total ? GetPopsMaxTotal() : PopsMax); }
        public string ToString_PopsMaxBonus() { return (PopsMaxBonus >= 0 ? "+" : "") + PopsMaxBonus + "%"; }
        public string ToString_Control() { return Control.ToString() + "%"; }
        public string ToString_Growth(bool total = true) { return Helper.ResValueToString(total ? GetGrowthTotal() : Growth, 100) + "%"; }
        public string ToString_GrowthBonus() { return (GrowthBonus >= 0 ? "+" : "") + GrowthBonus + "%"; }
        public string ToString_GrowthPenalty() { return (GrowthPenalty >= 0 ? "-" : "") + GrowthPenalty + "%"; }
        public string ToString_TrueGrowth() { return Helper.ResValueToString(GetTrueGrowth(), 100); }

        public void Set(string type, int value)
        {
            switch (type)
            {
                case "CPops": Pops = value; break;
                case "Pops": Pops = value; break;
                case "PopsMax": PopsMax = value; break;
                case "PopsMaxBonus": PopsMaxBonus = value; break;
                case "Control": Control = value; break;
                case "Growth": Growth = value; break;
                case "GrowthBonus": GrowthBonus = value; break;
                case "GrowthPenalty": GrowthPenalty = value; break;
            }
        }

        public void SavePops()
        {
            _Parent._Data.GetSub("Pops*Pops").ValueI = Pops;
        }
    }

    public DataBlock _Data = null;
    public List<IncomeInfo> Incomes = new List<IncomeInfo>();
    public List<LimitInfo> Limits = new List<LimitInfo>();
    public PopsInfo Pops = null;
    public ParentType Type = ParentType.Player;

    public ResourcesWrapper(DataBlock resData, ParentType type)
    {
        _Data = resData;
        Type = type;

        //Refresh();
    }
    public void Clear()
    {
        Incomes.Clear();
        Limits.Clear();
        Pops = null;
    }

    public void Refresh()
    {
        Clear();

        Array<DataBlock> resDataSubs = _Data.GetSubs();
        for (int idxData = 0; idxData < resDataSubs.Count; idxData++)
        {
            bool found = false;
            string name = Helper.Split_0(resDataSubs[idxData].Name, '*');
            string type = Helper.Split_1(resDataSubs[idxData].Name, '*');

            if (name == "Pops")
            {
                if (Pops != null)
                {
                    Pops.Set(type, resDataSubs[idxData].ValueI);
                }
                else
                {
                    Pops = new PopsInfo();
                    Pops._Parent = this;
                    Pops.Name = name;
                    Pops.Set(type, resDataSubs[idxData].ValueI);
                }
            }
            else if (type == "Income" || type == "Level" || type == "PerPop" || type == "PerControlledPop" || type == "PerLevel" || type == "PerLevelSystem" || type == "Bonus" || type == "Stockpile")
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
                    newIncome._Parent = this;
                    newIncome.Name = name;
                    newIncome.Set(type, resDataSubs[idxData].ValueI);
                    Incomes.Add(newIncome);
                }
            }
            else if (type == "Max" || type == "MaxBonus" || type == "Used" || type == "UsedBonus")
            {
                for (int idxRes = 0; idxRes < Limits.Count; idxRes++)
                {
                    if (Limits[idxRes].Name == name)
                    {
                        Limits[idxRes].Set(type, resDataSubs[idxData].ValueI);
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    LimitInfo newLimit = new LimitInfo();
                    newLimit._Parent = this;
                    newLimit.Name = name;
                    newLimit.Set(type, resDataSubs[idxData].ValueI);
                    Limits.Add(newLimit);
                }
            }
        }
    }

    public void PropagateSystemBonuses(ResourcesWrapper systemRes)
    {
        for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
        {
            for (int otherIdx = 0; otherIdx < systemRes.Incomes.Count; otherIdx++)
            {
                if (Incomes[resIdx].Name == systemRes.Incomes[otherIdx].Name)
                {
                    Incomes[resIdx].PerLevel_FromSystem += systemRes.Incomes[otherIdx].PerLevel_System;
                }
            }
        }
    }

    public void Add(ResourcesWrapper otherRes, bool totals = false, bool addMissing = false)
    {
        for (int otherIdx = 0; otherIdx < otherRes.Incomes.Count; otherIdx++)
        {
            bool found = false;
            for (int resIdx = 0; resIdx < Incomes.Count; resIdx++)
            {
                if (Incomes[resIdx].Name == otherRes.Incomes[otherIdx].Name)
                {
                    found = true;
                    if (totals)
                    {
                        Incomes[resIdx].Income += otherRes.Incomes[otherIdx].GetIncomeTotal();
                    }
                    else
                    {
                        Incomes[resIdx].Income += otherRes.Incomes[otherIdx].Income;
                        Incomes[resIdx].Level += otherRes.Incomes[otherIdx].Level;
                        Incomes[resIdx].PerPop += otherRes.Incomes[otherIdx].PerPop;
                        Incomes[resIdx].PerCPop += otherRes.Incomes[otherIdx].PerCPop;
                        Incomes[resIdx].PerLevel += otherRes.Incomes[otherIdx].PerLevel;
                        Incomes[resIdx].PerLevel_System += otherRes.Incomes[otherIdx].PerLevel_System;
                        Incomes[resIdx].Bonus += otherRes.Incomes[otherIdx].Bonus;
                        // Incomes[resIdx].Stockpile += otherRes.Incomes[otherIdx].Stockpile;
                    }
                }
            }
            
            if (addMissing && found == false)
            {
                IncomeInfo otherInfo = otherRes.Incomes[otherIdx];
                IncomeInfo info = otherInfo.Copy();
                Incomes.Add(info);
            }
        }
        for (int otherIdx = 0; otherIdx < otherRes.Limits.Count; otherIdx++)
        {
            bool found = false;
            for (int resIdx = 0; resIdx < Limits.Count; resIdx++)
            {
                if (Limits[resIdx].Name == otherRes.Limits[otherIdx].Name)
                {
                    found = true;
                    if (totals)
                    {
                        Limits[resIdx].Max += otherRes.Limits[otherIdx].GetMaxTotal();
                        Limits[resIdx].Used += otherRes.Limits[otherIdx].GetUsedTotal();
                    }
                    else
                    {
                        Limits[resIdx].Max += otherRes.Limits[otherIdx].Max;
                        Limits[resIdx].MaxBonus += otherRes.Limits[otherIdx].MaxBonus;
                        Limits[resIdx].Used += otherRes.Limits[otherIdx].Used;
                        Limits[resIdx].UsedBonus += otherRes.Limits[otherIdx].UsedBonus;
                    }
                }
            }

            if (addMissing && found == false)
            {
                LimitInfo otherInfo = otherRes.Limits[otherIdx];
                LimitInfo info = otherInfo.Copy();
                Limits.Add(info);
            }
        }

        if (otherRes.Pops != null)
        {
            if (Pops != null)
            {
                if (totals)
                {
                    Pops.CPops += otherRes.Pops.GetCPops();
                    Pops.Pops += otherRes.Pops.Pops;
                    Pops.PopsMax += otherRes.Pops.GetPopsMaxTotal();
                    Pops.Growth += otherRes.Pops.GetGrowthTotal();
                }
                else
                {
                    Pops.Pops += otherRes.Pops.Pops;
                    Pops.PopsMax += otherRes.Pops.PopsMax;
                    Pops.PopsMaxBonus += otherRes.Pops.PopsMaxBonus;
                    Pops.Control += otherRes.Pops.Control;
                    Pops.Growth += otherRes.Pops.Growth;
                    Pops.GrowthBonus += otherRes.Pops.GrowthBonus;
                }
            }
            else if (addMissing)
            {
                PopsInfo otherInfo = otherRes.Pops;
                Pops = otherInfo.Copy();
            }
        }
    }

    public void ProcessGrowth()
    {
        if (Pops != null)
        {
            Pops.Pops = Mathf.Min(Pops.Pops + Pops.GetTrueGrowth(), Pops.GetPopsMaxTotal());
            Pops.SavePops();
        }
    }

    public void ProcessIncome()
    {
        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            if (Incomes[idx].HasStockpile)
            {
                Incomes[idx].Stockpile += Incomes[idx].GetIncomeTotal();
                Incomes[idx].SaveStockpile();
            }
        }
    }

    public IncomeInfo GetIncome(string name)
    {
        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            if (Incomes[idx].Name == name)
            {
                return Incomes[idx];
            }
        }
        return null;
    }

    public LimitInfo GetLimit(string name)
    {
        for (int idx = 0; idx < Limits.Count; idx++)
        {
            if (Limits[idx].Name == name)
            {
                return Limits[idx];
            }
        }
        return null;
    }

    public PopsInfo GetPops()
    {
        return Pops;
    }

    public string GetStockpileString(string name)
    {
        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            if (Incomes[idx].Name == name)
            {
                int value = Incomes[idx].GetIncomeTotal();
                return Helper.ResValueToString(Incomes[idx].Stockpile) + "(" + (value >= 0 ? "+" + Helper.ResValueToString(value) : "[color=#ff8888]" + Helper.ResValueToString(value) + "[/color]") + ")";
                //int value = Incomes[idx].Value_1 - Incomes[idx].Value_2;
                //return Helper.ResValueToString(Resources[idx].Value_3) + "(" + (value >= 0 ? "+" + Helper.ResValueToString(value) : "[color=#ff8888]" + Helper.ResValueToString(value) + "[/color]") + ")";
                //return Helper.ResValueToString(Resources[idx].Value_3) + "(+" + Helper.ResValueToString(Resources[idx].Value_1) + (Resources[idx].Value_2 > 0 ? "[color=#ff8888]-" + Helper.ResValueToString(Resources[idx].Value_2) + "[/color])" : ")");
            }
        }
        return "";
    }

    public string GetStockpileTooltip(string name)
    {
        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            if (Incomes[idx].Name == name)
            {
                string tooltip = "temp";
                //tooltip += Helper.ResValueToString(Resources[idx].Value_1) + "[img=24x24]Assets/UI/Symbols/" + Resources[idx].Name + ".png[/img] Income";
                //if (Resources[idx].ResType == Info.Type.INCOME_UPKEEP_PERPOP)
                //{
                //    tooltip = Helper.ResValueToString(Resources[idx].Value_2) + "[img=24x24]Assets/UI/Symbols/" + Resources[idx].Name + ".png[/img] Income";
                //}


                //int value = Resources[idx].Value_1 - Resources[idx].Value_2;
                //return Helper.ResValueToString(Resources[idx].Value_3) + "(" + (value >= 0 ? "+" + Helper.ResValueToString(value) : "[color=#ff8888]" + Helper.ResValueToString(value) + "[/color]") + ")";
                //return Helper.ResValueToString(Resources[idx].Value_3) + "(+" + Helper.ResValueToString(Resources[idx].Value_1) + (Resources[idx].Value_2 > 0 ? "[color=#ff8888]-" + Helper.ResValueToString(Resources[idx].Value_2) + "[/color])" : ")");
            }
        }
        return "";
    }

    public string GetIncomeString(string name)
    {
        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            if (Incomes[idx].Name == name)
            {
                int value = Incomes[idx].GetIncomeTotal();
                return (value >= 0 ? Helper.ResValueToString(value) : "[color=#ff8888]" + Helper.ResValueToString(value) + "[/color]");
            }
        }
        return "";
    }

    public string GetIncomeString(string name, int value)
    {
        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            if (Incomes[idx].Name == name)
            {
                return (value >= 0 ? Helper.ResValueToString(value) : "[color=#ff8888]" + Helper.ResValueToString(value) + "[/color]");
            }
        }
        return "";
    }

    public string GetLimitString(string name)
    {
        for (int idx = 0; idx < Limits.Count; idx++)
        {
            if (Limits[idx].Name == name)
            {
                string colorStart = "";
                string colorEnd = "";
                if (Type == ParentType.Player && Limits[idx].GetUsedTotal() > Limits[idx].GetMaxTotal())
                {
                    colorStart = "[color=#ff8888]";
                    colorEnd = "[/color]";
                }
                return colorStart + Helper.ResValueToString(Limits[idx].GetUsedTotal()) + "/" + Helper.ResValueToString(Limits[idx].GetMaxTotal()) + colorEnd;
            }
        }
        return "";
    }

    public string ToStringCondensed()
    {
        string totalIncome = "";

        for (int idx = 0; idx < Incomes.Count; idx++)
        {
            if (Incomes[idx].GetIncomeTotal() != 0)
            {
                totalIncome += " " + Incomes[idx].ToString_Income() + Helper.GetIcon(Incomes[idx].Name);
            }
        }

        if (Pops != null)
        {
            if (Pops.GrowthBonus != 0)
            {
                totalIncome += Pops.ToString_GrowthBonus() + Helper.GetIcon("Growth");
            }
            if (Pops.GrowthPenalty != 0)
            {
                totalIncome += Pops.ToString_GrowthPenalty() + Helper.GetIcon("Growth");
            }
        }

        return totalIncome;
    }
}
