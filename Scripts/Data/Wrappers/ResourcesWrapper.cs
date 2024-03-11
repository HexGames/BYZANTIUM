using Godot;
using Godot.Collections;
using System.Collections.Generic;

public class ResourcesWrapper
{
    public class Info
    {
        public enum Type
        {
            VALUE,
            VALUE_INCOME,
            INCOME,
            TOTAL_USED
        }

        public Type ResType = Type.VALUE;

        public int Value_1 = 0;
        public int Value_2 = 0;

        public string Name = "Res";

        public int GetBenefitValue()
        {
            switch(ResType)
            {
                case Type.VALUE: return Value_1;
                case Type.VALUE_INCOME: return Value_2;
                case Type.INCOME: return Value_2;
                case Type.TOTAL_USED: return Value_1;
            }
            return 0;
        }
    };

    protected DataBlock _Data = null;
    public List<Info> Resources = new List<Info>();

    public ResourcesWrapper(DataBlock resData)
    {
        _Data = resData;

        //Refresh();
    }

    public void Refresh()
    {
        Resources.Clear();

        Array<DataBlock> resDataSubs = _Data.GetSubs();
        for (int idxData = 0; idxData < resDataSubs.Count; idxData++)
        {
            bool found = false;
            string name = Helper.Split_0(resDataSubs[idxData].Name, '*');
            string type = Helper.Split_1(resDataSubs[idxData].Name, '*');
            for (int idxRes = 0; idxRes < Resources.Count; idxRes++)
            {
                if (Resources[idxRes].Name == name)
                {
                    if (type == "Income")
                    {
                        Resources[idxRes].ResType = Info.Type.VALUE_INCOME;
                        Resources[idxRes].Value_2 = resDataSubs[idxData].ValueI;
                    }
                    else if (type == "Used")
                    {
                        Resources[idxRes].ResType = Info.Type.TOTAL_USED;
                        Resources[idxRes].Value_2 = resDataSubs[idxData].ValueI;
                    }
                    else
                    {
                        Resources[idxRes].Value_1 = resDataSubs[idxData].ValueI;
                    }
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                Info newRes = new Info();
                newRes.Name = name;
                if (type == "Income")
                {
                    newRes.ResType = Info.Type.INCOME;
                    newRes.Value_2 = resDataSubs[idxData].ValueI;
                }
                else if (type == "Used")
                {
                    newRes.ResType = Info.Type.TOTAL_USED;
                    newRes.Value_2 = resDataSubs[idxData].ValueI;
                }
                else
                {
                    newRes.ResType = Info.Type.VALUE;
                    newRes.Value_1 = resDataSubs[idxData].ValueI;
                }
                Resources.Add(newRes);
            }
        }
    }


    public void Add(string name, int value)
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            if (Resources[idx].Name == Helper.Split_0(name, '*'))
            {
                switch (Resources[idx].ResType)
                {
                    case Info.Type.VALUE:
                        {
                            Resources[idx].Value_1 += value;
                            break;
                        }
                    case Info.Type.VALUE_INCOME:
                        {
                            Resources[idx].Value_2 += value;
                            break;
                        }
                    case Info.Type.INCOME:
                        {
                            Resources[idx].Value_2 += value;
                            break;
                        }
                    case Info.Type.TOTAL_USED:
                        {
                            Resources[idx].Value_1 += value;
                            break;
                        }
                }
                break;
            }
        }
    }

    public void Add(ResourcesWrapper otherRes, int factor = 1)
    {
        for (int resIdx = 0; resIdx < Resources.Count; resIdx++)
        {
            for (int otherIdx = 0; otherIdx < otherRes.Resources.Count; otherIdx++)
            {
                if (Resources[resIdx].Name == otherRes.Resources[otherIdx].Name)
                {
                    Resources[resIdx].Value_1 += factor * otherRes.Resources[otherIdx].Value_1;
                    Resources[resIdx].Value_2 += factor * otherRes.Resources[otherIdx].Value_2;
                }
            }
        }
    }

    public void Add(JobsWrapper jobs)
    {
        for (int jobIdx = 0; jobIdx < jobs.Jobs.Count; jobIdx++)
        {
            Add(jobs.Jobs[jobIdx].Benefit);
        }
    }

    public void Use(string name, int value)
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            if (Resources[idx].Name == name)
            {
                switch (Resources[idx].ResType)
                {
                    case Info.Type.VALUE:
                        {
                            Resources[idx].Value_1 -= value;
                            break;
                        }
                    case Info.Type.VALUE_INCOME:
                        {
                            Resources[idx].Value_2 -= value;
                            break;
                        }
                    case Info.Type.INCOME:
                        {
                            Resources[idx].Value_2 -= value;
                            break;
                        }
                    case Info.Type.TOTAL_USED:
                        {
                            Resources[idx].Value_2 += value;
                            break;
                        }
                }
                break;
            }
        }
    }

    public void Use(ResourcesWrapper otherRes)
    {
        for (int resIdx = 0; resIdx < Resources.Count; resIdx++)
        {
            for (int otherIdx = 0; otherIdx < otherRes.Resources.Count; otherIdx++)
            {
                if (Resources[resIdx].Name == otherRes.Resources[otherIdx].Name)
                {
                    Resources[resIdx].Value_1 -= otherRes.Resources[otherIdx].Value_1;
                    Resources[resIdx].Value_2 -= otherRes.Resources[otherIdx].Value_2;
                }
            }
        }
    }
    public void AddIncome()
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            if (Resources[idx].ResType == Info.Type.VALUE_INCOME)
            {
                Resources[idx].Value_1 += Resources[idx].Value_2;
            }
        }
    }

    public void MultiplyAll(float value)
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            Resources[idx].Value_1 = Mathf.FloorToInt(value * Resources[idx].Value_1);
            Resources[idx].Value_2 = Mathf.FloorToInt(value * Resources[idx].Value_2);
        }
    }


    public void Save()
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            switch (Resources[idx].ResType)
            {
                case Info.Type.VALUE:
                    {
                        _Data.GetSub(Resources[idx].Name).ValueI = Resources[idx].Value_1;
                        break;
                    }
                case Info.Type.VALUE_INCOME:
                    {
                        _Data.GetSub(Resources[idx].Name).ValueI = Resources[idx].Value_1;
                        _Data.GetSub(Resources[idx].Name + "*Income").ValueI = Resources[idx].Value_2;
                        break;
                    }
                case Info.Type.INCOME:
                    {
                        _Data.GetSub(Resources[idx].Name + "*Income").ValueI = Resources[idx].Value_2;
                        break;
                    }
                case Info.Type.TOTAL_USED:
                    {
                        _Data.GetSub(Resources[idx].Name + "").ValueI = Resources[idx].Value_1;
                        _Data.GetSub(Resources[idx].Name + "*Used").ValueI = Resources[idx].Value_2;
                        break;
                    }
            }
        }
    }

    public Info Get(string name)
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            if (Resources[idx].Name == name)
            {
                return Resources[idx];
            }
        }
        return null;
    }

    //public string GetPopsString()
    //{
    //    for (int idx = 0; idx < Resources.Count; idx++)
    //    {
    //        if (Resources[idx].Name == "Pops")
    //        {
    //            if (Resources[idx].Value_2 >= 10)
    //            {
    //                return (Resources[idx].Value_2 / 1000).ToString();
    //            }
    //            else
    //            {
    //                return (Resources[idx].Value_2 / 1000).ToString() + ((Resources[idx].Value_2 / 100) % 10 != 0 ? "." + ((Resources[idx].Value_2 / 100) % 10).ToString() : "");
    //            }
    //        }
    //    }
    //    return "";
    //}

    public string GetIncomeString(string name)
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            if (Resources[idx].Name == name)
            {
                return Helper.ResValueToString(Resources[idx].Value_2);
            }
        }
        return "";
    }

    public string GetUsedPerTotalString(string name, int precision = 10)
    {
        int modFactor = precision / 10;
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            if (Resources[idx].Name == name)
            {
                string str = "";

                if (Resources[idx].Value_2 >= 10 * precision)
                {
                    str += (Resources[idx].Value_2 / precision).ToString();
                }
                else
                {
                    str += (Resources[idx].Value_2 / precision).ToString() + ((Resources[idx].Value_2 / modFactor) % 10 != 0 || (Resources[idx].Value_2 / modFactor) % 10 != 0 ? "." + ((Resources[idx].Value_2 / modFactor) % 10).ToString() : "");
                }

                str += "/";

                if (Resources[idx].Value_1 >= 10 * precision)
                {
                    str += (Resources[idx].Value_1 / precision).ToString();
                }
                else
                {
                    str += (Resources[idx].Value_1 / precision).ToString() + ((Resources[idx].Value_1 / modFactor) % 10 != 0 || (Resources[idx].Value_1 / modFactor) % 10 != 0 ? "." + ((Resources[idx].Value_1 / modFactor) % 10).ToString() : "");
                }


                return str;
            }
        }
        return "";
    }

    public string GetPercentString(string name)
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            if (Resources[idx].Name == name)
            {
                return (Resources[idx].Value_1 / 100).ToString() + ((Resources[idx].Value_1 / 10) % 10 != 0 ? "." + ((Resources[idx].Value_1 / 10) % 10).ToString() : "" + "%");
            }
        }
        return "";
    }

    /*public void Clear()
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            switch (Resources[idx].ResType)
            {
                case Info.Type.VALUE:
                    {
                        Resources[idx].Value_1 = 0;
                        break;
                    }
                case Info.Type.VALUE_INCOME:
                    {
                        Resources[idx].Value_2 = 0;
                        break;
                    }
                case Info.Type.INCOME:
                    {
                        Resources[idx].Value_2 = 0;
                        break;
                    }
                case Info.Type.TOTAL_USED:
                    {
                        Resources[idx].Value_1 = 0;
                        Resources[idx].Value_2 = 0;
                        break;
                    }
            }
        }
    }*/
}
