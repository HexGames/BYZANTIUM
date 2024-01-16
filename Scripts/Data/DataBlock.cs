using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Generated
[GlobalClass]
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

    // no allowed apparently
    //[Export]
    //public LocationData Location = null;
    //[Export]
    //public PawnData Pawn = null;

    public string ValueToString()
    {
        Data.BaseType baseType = (Data.BaseType)(Type/10000);

        switch(baseType)
        {
            case Data.BaseType.INT: return ValueI.ToString();
            case Data.BaseType.STRING: return ValueS;
        }

        return "";
    }
    public string ToUIString()
    {
        Data.BaseType baseType = (Data.BaseType)(Type/10000);

        switch (baseType)
        {
            case Data.BaseType.INT: return Name + ": " + ValueI.ToString();
            case Data.BaseType.STRING: return ValueS;
        }

        return "";
    }

    public Array<DataBlock> GetSubs()
    {
        return Subs;
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

    public DataBlock GetSub(string type)
    {
        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type)
            {
                return Subs[idx];
            }
        }
        return null;
    }

    public Array<DataBlock> GetSubs(string type)
    {
        Array<DataBlock> ret = new Array<DataBlock>();

        for (int idx = 0; idx < Subs.Count; idx++)
        {
            if (Subs[idx].Name == type)
            {
                ret.Add(Subs[idx]);
            }
        }

        return ret;
    }
}
