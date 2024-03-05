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
    };

    protected DataBlock _Data = null;
    public List<Info> Resources = new List<Info>();

    public ResourcesWrapper(DataBlock resData)
    {
        _Data = resData;

        Refresh();
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
}
