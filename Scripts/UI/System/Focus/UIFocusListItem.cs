using Godot;
using Godot.Collections;
using System;
using System.ComponentModel;
using System.Threading.Tasks.Dataflow;

//[Tool]
public partial class UIFocusListItem : Control
{
    // is beeing duplicated
    private Panel BG = null;
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
    public bool Refresh(JobsWrapper.Info jobInfo)
    {
        if (jobInfo.FocusValue > 0)
        {
            Refresh(jobInfo.FocusValue, jobInfo.FocusChange);
            Visible = true;
            return true;
        }
        else
        {
            Visible = false;
            return false;
        }
    }

    public void Refresh(int value, int change)
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
    }
}