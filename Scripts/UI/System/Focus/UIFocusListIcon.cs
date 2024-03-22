using Godot;
using Godot.Collections;

//[Tool]
public partial class UIFocusListIcon : Control
{
    // is beeing duplicated
    private UITooltipTrigger ValueTooltip = null;
    private string ValueTooltip_Row_1_Original = null;
    private string ValueTooltip_Row_1_Right_Original = null;

    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");

        ValueTooltip = GetNode<UITooltipTrigger>("UITooltipTrigger");
        ValueTooltip_Row_1_Original = ValueTooltip.Row_1;
        ValueTooltip_Row_1_Right_Original = ValueTooltip.Row_1_Right;
    }

    private RandomNumberGenerator RNG = new RandomNumberGenerator();
    public void Refresh(JobsWrapper.Info jobInfo)
    {
        if (jobInfo.Focused == false)
        {
            Refresh(jobInfo.Pops, jobInfo.GetMainRes());
            Visible = true;
        }
        else
        {
            Visible = false;
        }
    }

    public void Refresh(int pops = 0, int res = 0)
    {
        ValueTooltip.Row_1 = ValueTooltip_Row_1_Original.Replace("$value", Helper.ResValueToString(pops, 1000));
        ValueTooltip.Row_1_Right = ValueTooltip_Row_1_Right_Original.Replace("$value", Helper.ResValueToString(res));
    }
}