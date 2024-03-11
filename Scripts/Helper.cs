
public class Helper
{
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
}
