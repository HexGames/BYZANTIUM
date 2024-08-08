using Godot;

//[Tool]
public partial class UIPlanetInfoItem: Control
{
    // is beeing duplicated
    private Panel BG = null;
    private RichTextLabel Property = null;
    private static string Property_Original = "";
    private RichTextLabel Value = null;
    private static string Value_Original = "";
    private UITooltipTrigger Tooltip = null;

    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");

        BG = GetNode<Panel>("BG");
        Property = GetNode<RichTextLabel>("BG/Name");
        if (Property_Original.Length == 0) Property_Original = Property.Text;
        Value = GetNode<RichTextLabel>("BG/Value");
        if (Value_Original.Length == 0) Value_Original = Value.Text;
        Tooltip = GetNode<UITooltipTrigger>("BG/Tooltip");
    }

    public void Refresh(DataBlock data)
    {
        Property.Text = Property_Original.Replace("$name", data.ToUIName());
        Value.Text = Value_Original.Replace("$value", data.ToUIValue());

        DataBlock featureData = Game.Def.GetFeature(data.Name);

        Tooltip.Row_1 = "";

        if (featureData != null)
        {
            Tooltip.Title = featureData.Name;
            DataBlock benefit = featureData.GetSub("Benefit", false);
            if (benefit != null)
            {
                string row = "";
                for (int idx = 0; idx < benefit.Subs.Count; idx++)
                {
                    if (row.Length > 0)
                    {
                        Tooltip.Row_1 += "\n";
                        Tooltip.Row_1_Right += "\n";
                    }
                    row = benefit.Subs[idx].ToUIDescription();
                    Tooltip.Row_1 += row;
                }
            }
        }
        else
        {
            Tooltip.Title = data.Name;
            Tooltip.Row_1 = data.ToUIDescription();
        }
    }

    public void Refresh(string text, string tooltip, bool right)
    {
        if (right)
        {
            Property.Text = "";
            Value.Text = Value_Original.Replace("$value", text);
        }
        else
        {
            Property.Text = Property_Original.Replace("$name", text);
            Value.Text = "";
        }

        Tooltip.Title = "";
        Tooltip.Row_1 = tooltip;
        Tooltip.Row_2 = "";
    }
}