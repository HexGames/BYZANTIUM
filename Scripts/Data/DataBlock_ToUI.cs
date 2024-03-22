using Godot;

// Generated
public partial class DataBlock : Resource
{
    /*public Godot.Color ToUIColor()
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
    }*/

    /*public int ToUIRow()
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
    }*/

    public bool ToUIShow()
    {
        if (Name.StartsWith("Link")) return false;
        return true;
    }

    public string ToUIName()
    {
        //switch (Name)
        //{
        //    case "Size": return "Size";
        //    case "Temperature": return "Temperature";
        //}

        return Name.Replace('_', ' ');
    }
    public string ToUIValue()
    {
        switch (Name)
        {
            case "Size":
                {
                    switch (ValueI)
                    {
                        case 1: return "Small";
                        case 2: return "Average";
                        case 3: return "Large";
                        case 4: return "Small Giant";
                        case 5: return "Giant";
                        case 6: return "Supergiant";
                    }
                    break;
                }
            case "Temperature":
                {
                    switch (ValueI)
                    {
                        case 1: return "Frozen";
                        case 2: return "Cold";
                        case 3: return "Normal";
                        case 4: return "Hot";
                        case 5: return "Molten";
                    }
                    break;
                }
        }

        return ValueS.Replace('_', ' ');
    }

    public string ToToolTipString()
    {
        string text = "";
        if (Subs.Count > 0)
        {
            text += ToUIName() + ":" + "\n";

            for (int idx = 0; idx < Subs.Count; idx++)
            {
                text += "  " + Subs[idx].ToUIName().PadRight(20) + Subs[idx].ToUIValue().PadLeft(10) + "\n";
            }
        }
        else
        {
            text += (ToUIName() + ":").PadRight(20) + ToUIValue().PadLeft(12) + "\n";
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
            case "CulturePoints": return "CP";
            case "Pops": return "Pops";
        }

        return "";
    }
}
