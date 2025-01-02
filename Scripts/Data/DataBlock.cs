using Godot;
using Godot.Collections;
using System.Reflection.Metadata.Ecma335;

// Generated
[GlobalClass]
[Tool]
public partial class DataBlock : Resource
{
    [Export]
    public int Type = 0;
    [Export]
    public string Name = "";
    [Export]
    public int ValueI = 0;
    [Export]
    public string ValueS = "";
    [Export]
    public Array<DataBlock> Subs = new Array<DataBlock>();

    //[Export]
    //public DataBlock Parent = null;

    public string ValueToString()
    {
        Data.BaseType baseType = (Data.BaseType)(Type/10000);

        switch (baseType)
        {
            case Data.BaseType.INT: return ValueI.ToString();
            case Data.BaseType.STRING: return ValueS;
        }

        return "";
    }

    public Array<DataBlock> GetSubs()
    {
        return Subs;
    }

    public bool HasSub(string sub_0)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == sub_0)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasSub(string sub_0, string sub_1)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return false;
        
        return dataSub_0.HasSub(sub_1);
    }

    public bool HasSub(string sub_0, string sub_1, string sub_2)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return false;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return false;

        return dataSub_1.HasSub(sub_2);
    }

    public bool HasSub(string sub_0, string sub_1, string sub_2, string sub_3)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return false;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return false;
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return false;

        return dataSub_2.HasSub(sub_3);
    }

    //public bool HasSub(string path, int value)
    //{
    //    int splitIdx = path.Find("/");
    //    if (splitIdx > 0)
    //    {
    //        DataBlock sub = GetSub(path.Substring(0, splitIdx));
    //        if (sub != null)
    //        {
    //            sub.HasSub(path.Substring(splitIdx + 1), value);
    //        }
    //    }
    //    else
    //    {
    //        for (int idx = 0; idx < Subs.Count; idx++)
    //        {
    //            if (Subs[idx].Name == path && Subs[idx].ValueI == value)
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}

    //public bool HasSub(string type, string value)
    //{
    //    for (int idx = 0; idx < Subs.Count; idx++)
    //    {
    //        if (Subs[idx].Name == type && Subs[idx].ValueS == value)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}


    public DataBlock GetSub(int type)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Type == type)
            {
                return Subs[idx];
            }
        }
        return null;
    }

    public Array<DataBlock> GetSubs(int type)
    {
        Array<DataBlock> ret = new Array<DataBlock>();

        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Type == type)
            {
                ret.Add(Subs[idx]);
            }
        }

        return ret;
    }

    public DataBlock GetSub(string type, bool showWarning = true)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type)
            {
                return Subs[idx];
            }
        }
        //if (showWarning) 
        //    GD.Print("sub not found Data : Type - " + Name + " : " + type);

        return null;
    }

    public string GetSubValueS(string sub_0, bool showWarning = false)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return "";

        return dataSub_0.ValueS;
    }

    public string GetSubValueS(string sub_0, string sub_1, bool showWarning = false)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return "";
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return "";

        return dataSub_1.ValueS;
    }

    public string GetSubValueS(string sub_0, string sub_1, string sub_2, bool showWarning = false)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return "";
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return "";
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return "";

        return dataSub_2.ValueS;
    }

    public string GetSubValueS(string sub_0, string sub_1, string sub_2, string sub_3, bool showWarning = false)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return "";
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return "";
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return "";
        DataBlock dataSub_3 = dataSub_2.GetSub(sub_3);
        if (dataSub_3 == null) return "";

        return dataSub_3.ValueS;
    }

    public string GetSubValueS_Path(string path, bool showWarning = false)
    {
        int splitIdx = path.Find("/");
        if (splitIdx > 0)
        {
            DataBlock sub = GetSub(path.Substring(0, splitIdx));
            if (sub != null)
            {
                return sub.GetSubValueS_Path(path.Substring(splitIdx + 1), showWarning);
            }
        }
        else
        {
            for (int idx = 0; idx < Subs.Count; idx++)
            {
                if (Subs[idx].Name == path)
                {
                    return Subs[idx].ValueS;
                }
            }
        }

        //if (showWarning)
        //    GD.Print("sub not found Data : Type - " + Name + " : " + path);

        return "";
    }

    public int GetSubValueI(string sub_0, bool showWarning = false)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return 0;

        return dataSub_0.ValueI;
    }

    public int GetSubValueI(string sub_0, string sub_1, bool showWarning = false)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return 0;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return 0;

        return dataSub_1.ValueI;
    }

    public int GetSubValueI(string sub_0, string sub_1, string sub_2, bool showWarning = false)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return 0;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return 0;
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return 0;

        return dataSub_2.ValueI;
    }

    public int GetSubValueI(string sub_0, string sub_1, string sub_2, string sub_3, bool showWarning = false)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return 0;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return 0;
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return 0;
        DataBlock dataSub_3 = dataSub_2.GetSub(sub_3);
        if (dataSub_3 == null) return 0;

        return dataSub_3.ValueI;
    }

    public int GetSubValueI_Path(string path, bool showWarning = false)
    {
        int splitIdx = path.Find("/");
        if (splitIdx > 0)
        {
            DataBlock sub = GetSub(path.Substring(0, splitIdx));
            if (sub != null)
            {
                return sub.GetSubValueI_Path(path.Substring(splitIdx + 1), showWarning);
            }
        }
        else
        {
            for (int idx = 0; idx < Subs.Count; idx++)
            {
                if (Subs[idx].Name == path)
                {
                    return Subs[idx].ValueI;
                }
            }
        }

        //if (showWarning)
        //    GD.Print("sub not found Data : Type - " + Name + " : " + path);

        return 0;
    }

    public DataBlock GetSub(string type, string name)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type && Subs[idx].ValueS == name)
            {
                return Subs[idx];
            }
        }
        return null;
    }

    public DataBlock GetLink(string link)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name.StartsWith(link))
            {
                return Subs[idx];
            }
        }
        return null;
    }

    public Array<DataBlock> GetSubs(string type, bool startsWith = false)
    {
        Array<DataBlock> ret = new Array<DataBlock>();

        if (startsWith)
        {
            for (int idx = 0; idx < Subs.Count; idx++)
            {
                if (Subs[idx].Name.StartsWith(type))
                {
                    ret.Add(Subs[idx]);
                }
            }
        }
        else
        {
            for (int idx = 0; idx < Subs.Count; idx++)
            {
                if (Subs[idx].Name == type)
                {
                    ret.Add(Subs[idx]);
                }
            }
        }

        return ret;
    }
    public Array<DataBlock> GetLinks(string link)
    {
        return GetSubs(link, true);
    }

    public void SetValueI(int value, DefLibrary def)
    {
        Type = def.GetDBType("i_" + Name, Data.BaseType.INT);
        ValueI = value;
        ValueS = "";
    }

    public void SetSubValueI(string sub_0, int value, DefLibrary def)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return;

        dataSub_0.SetValueI(value, def);
    }

    public void SetSubValueI(string sub_0, string sub_1, int value, DefLibrary def)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return;

        dataSub_1.SetValueI(value, def);
    }

    public void SetSubValueI(string sub_0, string sub_1, string sub_2, int value, DefLibrary def)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return;
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return;

        dataSub_2.SetValueI(value, def);
    }

    public void SetSubValueI(string sub_0, string sub_1, string sub_2, string sub_3, int value, DefLibrary def)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return;
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return;
        DataBlock dataSub_3 = dataSub_2.GetSub(sub_3);
        if (dataSub_3 == null) return;

        dataSub_3.SetValueI(value, def);
    }

    public void SetSubValueI_Path(string path, int value, DefLibrary def)
    {
        int splitIdx = path.Find("/");
        if (splitIdx > 0)
        {
            DataBlock sub = GetSub(path.Substring(0, splitIdx));
            if (sub != null)
            {
                sub.SetSubValueI_Path(path.Substring(splitIdx + 1), value, def);
            }
        }
        else 
        {
            if (HasSub(path))
            {
                GetSub(path).SetValueI(value, def);
            }
        }
    }

    public void SetValueS(string value, DefLibrary def)
    {
        Type = def.GetDBType("s_" + Name, Data.BaseType.STRING);
        ValueI = 0;
        ValueS = value;
    }

    public void SetSubValueS(string sub_0, string value, DefLibrary def)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return;

        dataSub_0.SetValueS(value, def);
    }

    public void SetSubValueS(string sub_0, string sub_1, string value, DefLibrary def)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return;

        dataSub_1.SetValueS(value, def);
    }

    public void SetSubValueS(string sub_0, string sub_1, string sub_2, string value, DefLibrary def)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return;
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return;

        dataSub_2.SetValueS(value, def);
    }

    public void SetSubValueS(string sub_0, string sub_1, string sub_2, string sub_3, string value, DefLibrary def)
    {
        DataBlock dataSub_0 = GetSub(sub_0);
        if (dataSub_0 == null) return;
        DataBlock dataSub_1 = dataSub_0.GetSub(sub_1);
        if (dataSub_1 == null) return;
        DataBlock dataSub_2 = dataSub_1.GetSub(sub_2);
        if (dataSub_2 == null) return;
        DataBlock dataSub_3 = dataSub_2.GetSub(sub_3);
        if (dataSub_3 == null) return;

        dataSub_3.SetValueS(value, def);
    }

    public void SetSubValueS_Path(string path, string value, DefLibrary def)
    {
        int splitIdx = path.Find("/");
        if (splitIdx > 0)
        {
            DataBlock sub = GetSub(path.Substring(0, splitIdx));
            if (sub != null)
            {
                sub.SetSubValueS_Path(path.Substring(splitIdx + 1), value, def);
            }
        }
        else
        {
            if (HasSub(path))
            {
                GetSub(path).SetValueS(value, def);
            }
        }
    }
}
