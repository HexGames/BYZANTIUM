
public class Helper
{
    public static string Tabs(int tabs)
    {
        string text = "";
        for ( int n = 0; n < tabs; n++)
        {
            text += "\t";
        }
        return text;
    }
}
