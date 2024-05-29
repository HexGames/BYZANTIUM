using Godot;

//[Tool]
public partial class UIPlanetInfoItem: Control
{
    // is beeing duplicated
    private Panel BG = null;
    private Label Property = null;
    private Label Value = null;
    private UITooltipTrigger Tooltip = null;

    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");

        BG = GetNode<Panel>("BG");
        Property = GetNode<Label>("BG/Name");
        Value = GetNode<Label>("BG/Value");
        Tooltip = GetNode<UITooltipTrigger>("BG/Tooltip");
    }

    public void Refresh(DataBlock data)
    {
        Property.Text = data.ToUIName();
        Value.Text = data.ToUIValue();

        DataBlock featureData = Game.Def.GetFeature(data.Name);

        Tooltip.Row_2 = "";

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
                        Tooltip.Row_2 += "\n";
                        Tooltip.Row_2_Right += "\n";
                    }
                    row = benefit.Subs[idx].ToUIDescription();
                    Tooltip.Row_2 += row;
                }
            }
        }
        else
        {
            Tooltip.Title = data.Name;
            Tooltip.Row_2 = data.ToUIDescription();
        }
    }
}