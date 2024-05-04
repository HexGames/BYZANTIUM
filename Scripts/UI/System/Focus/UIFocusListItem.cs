using Godot;
using Godot.Collections;

//[Tool]
public partial class UIFocusListItem : Control
{
    // is beeing duplicated
    private Panel BG = null;
    private UITooltipTrigger ValueTooltip = null;
    private string ValueTooltip_Row_1_Original = null;
    private string ValueTooltip_Row_1_Right_Original = null;
    private Control Growing = null;
    private Control Declining = null;
    private Array<Control> Pips = new Array<Control>();
    private ProgressBar Bar = null;
    private Array<Control> Empty = new Array<Control>();

    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");

        BG = GetNode<Panel>("BG");

        if (HasNode("BG/Row/Job/UITooltipTrigger"))
        {
            ValueTooltip = GetNode<UITooltipTrigger>("BG/Row/Job/UITooltipTrigger");
            ValueTooltip_Row_1_Original = ValueTooltip.Row_1;
            ValueTooltip_Row_1_Right_Original = ValueTooltip.Row_1_Right;
        }
        Growing = GetNode<Control>("BG/Row/Change/Growing");
        Declining = GetNode<Control>("BG/Row/Change/Declining");

        Pips.Clear();
        for (int id = 1; id <= 4; id++)
        {
            Pips.Add(GetNode<Control>("BG/Row/Target/Pip_" + id.ToString()));
        }

        Bar = GetNode<ProgressBar>("BG/Row/Target/Bar");

        Empty.Clear();
        for (int id = 1; id <= 4; id++)
        {
            Empty.Add(GetNode<Control>("BG/Row/Target/Empty_" + id.ToString()));
        }
    }

    private RandomNumberGenerator RNG = new RandomNumberGenerator();
    public void Refresh(JobsWrapper.Info jobInfo)
    {
        if (jobInfo != null && jobInfo.Focused)
        {
            Refresh(jobInfo.FocusValue, jobInfo.FocusChange, jobInfo.Pops, jobInfo.GetMainRes());
            Visible = true;
        }
        else
        {
            Visible = false;
        }
    }

    public void Refresh(int value, int change, int pops = 0, int res = 0)
    {
        int pips = value / 100;
        for (int idx = 0; idx < Pips.Count; idx++)
        {
            if (idx < pips)
            {
                Pips[idx].Visible = true;
            }
            else
            {
                Pips[idx].Visible = false;
            }
        }

        int progress = value % 100;
        if (progress > 0)
        {
            Bar.Value = progress;
            Bar.Visible = true;
        }
        else
        {
            Bar.Visible = false;
        }

        int empty = Empty.Count - pips - (progress > 0 ? 1 : 0);
        for (int idx = 0; idx < Empty.Count; idx++)
        {
            if (idx < empty)
            {
                Empty[idx].Visible = true;
            }
            else
            {
                Empty[idx].Visible = false;
            }
        }

        Growing.Visible = change > 0;
        Declining.Visible = change < 0;

        if (ValueTooltip != null)
        {
            ValueTooltip.Row_1 = ValueTooltip_Row_1_Original.Replace("$value", Helper.ResValueToString(pops, 1000));
            ValueTooltip.Row_1_Right = ValueTooltip_Row_1_Right_Original.Replace("$value", Helper.ResValueToString(res));
        }
    }
}