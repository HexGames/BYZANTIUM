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

        Array<DataBlock> resDataSubs = resData.GetSubs();
        for (int idxData = 0; idxData < resDataSubs.Count; idxData++)
        {
            bool found = false;
            string name = Helper.Split_0(resDataSubs[idxData].Name);
            string type = Helper.Split_1(resDataSubs[idxData].Name);
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
}
