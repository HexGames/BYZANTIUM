using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

// Generated
public partial class DataBlock : Resource
{
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

            case "City": return new Godot.Color("440000");
            case "Private_Business": return new Godot.Color("440000");
            case "Power_Plants": return new Godot.Color("444400");
            case "Mines": return new Godot.Color("444400");
            case "Goverment_Offices": return new Godot.Color("000022");
            case "Diplomatic_Offices": return new Godot.Color("000022");
            case "Research_Labs": return new Godot.Color("000044");
            case "Cultural_Center": return new Godot.Color("000044");
            case "Hydroponics_Farms": return new Godot.Color("004400");
            case "Nature_Biodomes": return new Godot.Color("004400");
            case "City_Biodomes": return new Godot.Color("004400");

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

            case "City": return 1;
            case "Private_Business": return 2;
            case "Power_Plants": return 3;
            case "Mines": return 4;
            case "Goverment_Offices": return 5;
            case "Diplomatic_Offices": return 6;
            case "Research_Labs": return 7;
            case "Cultural_Center": return 8;
            case "Hydroponics_Farms": return 9;
            case "Nature_Biodomes": return 10;
            case "City_Biodomes": return 11;
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

    public string ToToolTipString()
    {
        string text = "";
        if (Subs.Count > 0)
        {
            text += Name + ":" + "\n";

            for (int idx = 0; idx < Subs.Count; idx++)
            {
                text += "  " + Subs[idx].Name.PadRight(20) + Subs[idx].ValueToString().PadLeft(10) + "\n";
            }
        }
        else
        {
            text += (Name + ":").PadRight(20) + ValueToString().PadLeft(12) + "\n";
        }

        return text;
    }

    static public string ResToUIString(string name)
    {
        switch (name)
        {
            case "Credits": return "C";
            case "Metal": return "M";
            case "Energy": return "E";
            case "Authoriy": return "A";
            case "Influence": return "I";
            case "TechPoints": return "TP";
            case "CivicPoints": return "CP";
            case "Pops": return "Pops";
        }

        return "";
    }
}
