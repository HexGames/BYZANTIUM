
using System;

public class Helper
{
    // https://www.desmos.com/calculator/j69axyulw7
    // https://www.desmos.com/calculator/mkuwt9fpaf
    public static float SoftLimit(float x)
    {
        x = MathF.Max(x, 0);
        return x / (x + 1);
    }

    public static string Tabs(int tabs)
    {
        string text = "";
        for (int n = 0; n < tabs; n++)
        {
            text += "\t";
        }
        return text;
    }

    public static void Split(string text, char separator, out string firstPart, out string secondPart)
    {
        char[] delimiter = { separator };
        string[] split = text.Split(delimiter);

        firstPart = split[0];
        secondPart = split[1];
    }

    public static string Split_0(string text, char separator = ':')
    {
        return text.Split(separator)[0];
    }
    
    public static string Split_1(string text, char separator = ':')
    {
        string[] split = text.Split(separator);
        return split.Length > 1 ? split[1] : "";
    }

    //public static string Split_minus1(string text, char separator = ':')
    //{
    //    string ret = "";
    //    string[] split = text.Split(separator);
    //
    //    for (int i = 0; i < split.Length - 1; i++)
    //    {
    //        if (i > 0) ret += ":";
    //        ret += split[i];
    //    }
    //
    //    return text.Split(separator)[0];
    //}

    public static string ResValueToString(int value, int precision = 10)
    {
        if (value >= 10 * precision)
        {
            return (value / precision).ToString();
        }
        else
        {
            return (value / precision).ToString() + ((value * 10 / precision) % 10 != 0 ? "." + ((value * 10 / precision) % 10).ToString() : "");
        }
    }

    public static string GetColorPrefix_Good()
    {
        return "[color=#ff8888]";
    }
    public static string GetColorPrefix_Bad()
    {
        return "[color=#88ff88]";
    }
    public static string GetColorPrefix_Waste()
    {
        return "[color=#ffff44]";
    }
    public static string GetColorPrefix_Neutral()
    {
        return "[color=#ffffff]";
    }

    public static string GetColorSufix()
    {
        return "[/color]";
    }
    public static string GetOrdinal(int num)
    {
        if (num <= 0) return num.ToString();

        switch (num % 100)
        {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }

        switch (num % 10)
        {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }
    }
}
