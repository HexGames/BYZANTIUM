using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

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

    public Godot.Color ToUIColor()
    {
        switch (Name)
        {
            case "Star_Size": return new Godot.Color("3c1800");
            case "Size": return new Godot.Color("3c1800");
            case "Temperature": return new Godot.Color("3c1800");
            case "Alpine": return new Godot.Color("3c1800");
            case "Vulcanic": return new Godot.Color("3c1800");

            case "Atmosphere": return new Godot.Color("000636");
            case "Low_Atmosphere": return new Godot.Color("000636");
            case "Toxic": return new Godot.Color("000636");
            case "CH4": return new Godot.Color("000636");
            case "He3": return new Godot.Color("000636");
            case "Molten_Core": return new Godot.Color("560600");
            case "Magnetic_Field": return new Godot.Color("560600");
            case "Radiation": return new Godot.Color("560600");

            case "Water": return new Godot.Color("0b008a");
            case "Ice": return new Godot.Color("0b008a");
            case "Life": return new Godot.Color("00360c");
            case "Forzen_Ocean": return new Godot.Color("00360c");
            case "Hostile_Life": return new Godot.Color("00360c");
        }

        return new Godot.Color("232323");
    }

    public int ToUIRow()
    {
        if (Name.StartsWith("Link")) return -1;

        switch (Name)
        {
            case "Custom": return -1;
            case "Star_Type": return -1;
            case "Type": return -1;

            case "Star_Size": return 1;
            case "Size": return 1;
            case "Temperature": return 1;
            case "Alpine": return 1;
            case "Vulcanic": return 1;

            case "Atmosphere": return 2;
            case "Low_Atmosphere": return 2;
            case "Toxic": return 2;
            case "CH4": return 2;
            case "He3": return 2;
            case "Molten_Core": return 2;
            case "Magnetic_Field": return 2;
            case "Radiation": return 2;

            case "Life": return 3;
            case "Water": return 3;
            case "Ice": return 3;
            case "Forzen_Ocean": return 3;
            case "Hostile_Life": return 3;
        }
        return 0;
    }

    public string ToUIString()
    {
        switch (Name)
        {
            case "Size":
                {
                    switch (ValueI)
                    {
                        case 1: return "Size: Very Small";
                        case 2: return "Size: Small";
                        case 3: return "Size: Below Average";
                        case 4: return "Size: Average Size";
                        case 5: return "Size: Above Average";
                        case 6: return "Size: Large";
                        case 7: return "Size: Very Large";
                        case 8: return "Size: Huge";
                        case 9: return "Size: Giant";
                    }
                    break;
                }
            case "Temperature":
                {
                    switch (ValueI)
                    {
                        case 1: return "Temp: Frozen";
                        case 2: return "Temp: Cold";
                        case 3: return "Temp: Normal";
                        case 4: return "Temp: Hot";
                        case 5: return "Temp: Molten";
                    }
                    break;
                }
            case "Radiation":
                {
                    switch (ValueI)
                    {
                        case 1: return "Low Radiation";
                        case 2: return "High Radiation";
                    }
                    break;
                }
        }

        Data.BaseType baseType = (Data.BaseType)(Type/10000);
        switch (baseType)
        {
            case Data.BaseType.NONE: return Name.Replace('_', ' ');
            case Data.BaseType.INT: return Name.Replace('_',' ') + ": " + ValueI.ToString();
            case Data.BaseType.STRING: return Name.Replace('_', ' ') + ": " + ValueS.Replace('_', ' ');
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
