using Godot;
using Godot.Collections;
using System.Collections.Generic;

public class ResourcesWrapperTemp : ResourcesWrapper
{
    public ResourcesWrapperTemp(DataBlock resData) : base(resData)
    {
    }

    public void Clear()
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
    }

    public void Add(string name, int value)
    {
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            if (Resources[idx].Name == name)
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
                            Resources[idx].Value_2 = 0;
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
                            Resources[idx].Value_1 = 0;
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
                        _Data.GetSub(Resources[idx].Name + ":Income").ValueI = Resources[idx].Value_2;
                        break;
                    }
                case Info.Type.INCOME:
                    {
                        _Data.GetSub(Resources[idx].Name + ":Income").ValueI = Resources[idx].Value_2;
                        break;
                    }
                case Info.Type.TOTAL_USED:
                    {
                        _Data.GetSub(Resources[idx].Name + "").ValueI = Resources[idx].Value_1;
                        _Data.GetSub(Resources[idx].Name + ":Used").ValueI = Resources[idx].Value_2;
                        break;
                    }
            }
        }
    }
}
