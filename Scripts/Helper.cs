
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public static string ResValueToString(int value, int precision = 10, bool alwaysShowSign = false)
    {
        if (Mathf.Abs(value) >= 10 * precision)
        {
            return (alwaysShowSign && value > 0 ? "+" : "") + (value / precision).ToString();
        }
        else
        {
            return (alwaysShowSign && value > 0 ? "+" : "") + (value / precision).ToString() + ((value * 10 / precision) % 10 != 0 ? "." + (Mathf.Abs((value * 10 / precision) % 10)).ToString() : "");
        }
    }

    public static string GetIcon(string name, int size = 24)
    {
        return "[img=" + size.ToString() + "x" + size.ToString() + "]Assets/UI/Symbols/" + name + ".png[/img]";
    }

    public static string GetColorPrefix_Good()
    {
        return "[color=#88ff88]";
    }
    public static string GetColorPrefix_Bad()
    {
        return "[color=#ff8888]";
    }
    public static string GetColorPrefix_Waste()
    {
        return "[color=#ffff44]";
    }
    public static string GetColorPrefix_Neutral()
    {
        return "[color=#ffffff]";
    }

    public static string GetColorPrefix_Action()
    {
        return "[color=#ffff00]";
    }

    public static string GetColorPrefix_FleetMain()
    {
        return "[color=#ff0000]";
    }

    public static string GetColorPrefix_FleetDefence()
    {
        return "[color=#0078ff]";
    }

    public static string GetColorPrefix_FleetColony()
    {
        return "[color=#00a50c]";
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

    static Dictionary<string, int> RomanNumbersMap = new Dictionary<string, int>
    {
        {"M", 1000 },
        {"CM", 900},
        {"D", 500},
        {"CD", 400},
        {"C", 100},
        {"XC", 90},
        {"L", 50},
        {"XL", 40},
        {"X", 10},
        {"IX", 9},
        {"V", 5},
        {"IV", 4},
        {"I", 1}
    };

    public static string IntToRoman(int num)
    {
        var result = string.Empty;
        
        foreach (var pair in RomanNumbersMap)
        {
            result += string.Join(string.Empty, Enumerable.Repeat(pair.Key, num / pair.Value));
            num %= pair.Value;
        }
        return result;
    }

    public static int TurnsToComplete(int progressToGo, int production)
    {
        if (production <= 0) return 999;
        return (progressToGo + production - 1) / production;
    }
}
