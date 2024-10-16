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

    public bool HasSub(string type)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasSub(string type, int value)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type && Subs[idx].ValueI == value)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasSub(string type, string value)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type && Subs[idx].ValueS == value)
            {
                return true;
            }
        }
        return false;
    }


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
        if (showWarning) 
            GD.Print("sub not found Data : Type - " + Name + " : " + type);

        return null;
    }

    public string GetSubValueS(string type, bool showWarning = false)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type)
            {
                return Subs[idx].ValueS;
            }
        }
        if (showWarning)
            GD.Print("sub not found Data : Type - " + Name + " : " + type);

        return "";
    }

    public int GetSubValueI(string type, bool showWarning = false)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type)
            {
                return Subs[idx].ValueI;
            }
        }
        if (showWarning)
            GD.Print("sub not found Data : Type - " + Name + " : " + type);

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

    public void SetValueS(string value, DefLibrary def)
    {
        Type = def.GetDBType("s_" + Name, Data.BaseType.STRING);
        ValueI = 0;
        ValueS = value;
    }
}
