using Godot;
using Godot.Collections;
using System;
using System.ComponentModel;

//[Tool]
public partial class UIJobListItem : Control
{
    // is beeing duplicated
    private Panel BG = null;
    private Control Growing = null;
    private Control Declining = null;
    private RichTextLabel Pops = null;
    private string Pops_Original;
    private RichTextLabel Value = null;
    private string Value_Original;
    private Array<Control> SmallPips = new Array<Control>();
    private Array<Control> Pips = new Array<Control>();

    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");

        BG = GetNode<Panel>("BG");
        Growing = GetNode<Control>("BG/Left/Change/Growing");
        Declining = GetNode<Control>("BG/Left/Change/Declining");
        Pops = GetNode<RichTextLabel>("BG/Left/PopsValue");
        Pops_Original = Pops.Text;
        Value = GetNode<RichTextLabel>("BG/Left/JobValue");
        Value_Original = Value.Text;

        SmallPips.Clear();
        for (int id = 1; id <= 6; id++)
        {
            SmallPips.Add(GetNode<Control>("BG/Left/Target/SmallPip_" + id.ToString()));
        }

        Pips.Clear();
        for (int id = 1; id <= 4; id++)
        {
            Pips.Add(GetNode<Control>("BG/Left/Target/Pip_" + id.ToString()));
        }
    }

    private RandomNumberGenerator RNG = new RandomNumberGenerator();
    public void Refresh(JobsWrapper.Info jobInfo)
    {
        Pops.Text = Pops_Original.Replace("$000", Helper.ResValueToString(jobInfo.Pops, 1000));
        Value.Text = Value_Original.Replace("$000", Helper.ResValueToString(jobInfo.GetMainRes().GetBenefitValue()));

        int pips = 0;// jobInfo.Pips;
        for (int idx = 0; idx < Pips.Count; idx++)
        {
            if(idx < pips)
            {
                Pips[idx].Visible = true;
            }
            else
            {
                Pips[idx].Visible = false;
            }
        }

        Growing.Visible = false;
        Declining.Visible = false;
    }
}