using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIShipbuilding : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel TitleLabel;
    private string TitleLabel_Original;
    [Export]
    public Control Working = null;
    [Export]
    public RichTextLabel NameLabel;
    private string NameLabel_Original;
    [Export]
    public RichTextLabel LocationLabel;
    private string LocationLabel_Original;
    [Export]
    public RichTextLabel ProgressLabel;
    private string ProgressLabel_Original;
    [Export]
    public Control Extra = null;
    [Export]
    public TextureRect ExtraEnergy = null;
    [Export]
    public TextureRect ExtraProduction = null;
    [Export]
    public TextureRect ExtraBC = null;
    [Export]
    public TextureRect ExtraPrivateIndustry = null;
    [Export]
    public Control Idle = null;
    [Export]
    public RichTextLabel IdleLabel;
    private string IdleLabel_Original;
    [Export]
    public RichTextLabel DurationLabel;
    private string DurationLabel_Original;
    [Export]
    public ProgressBar Bar = null;
    [Export]
    public ColorRect PriorityLow = null;
    [Export]
    public ColorRect PriorityMed = null;
    [Export]
    public ColorRect PriorityHigh = null;

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel_Original = TitleLabel.Text;
        NameLabel_Original = NameLabel.Text;
        LocationLabel_Original = LocationLabel.Text;
        ProgressLabel_Original = ProgressLabel.Text;
        DurationLabel_Original = DurationLabel.Text; 
        IdleLabel_Original = IdleLabel.Text;
    }

    public void Refresh(string colonyName, DataBlock actionCon, int progressValue, int powerValue = 0)
    {
        string title = TitleLabel_Original.Replace("$name", colonyName);
        if (powerValue > 0) title.Replace("$value", powerValue.ToString());
        TitleLabel.Text = title;

        int count = 0; // actionCon.GetSub("Count").ValueI;
        if (count > 0)
        {
            DataBlock name = actionCon.GetSub("Name");
            NameLabel.Text = NameLabel_Original.Replace("$name", name.ValueS + (count > 1 ? " x" + count.ToString() : ""));

            DataBlock location = actionCon.GetSub("Link:System:Colony");
            LocationLabel.Text = LocationLabel_Original.Replace("$location", location.ValueS);

            ProgressLabel.Text = ProgressLabel_Original.Replace("$value", Helper.ResValueToString(progressValue));

            Working.Visible = true;
            Idle.Visible = false;

            DataBlock progressTotal = actionCon.GetSub("Progress:Total");
            DataBlock progressCurrent = actionCon.GetSub("Progress:Current");
            int remaining = Mathf.CeilToInt(1.0f * (progressTotal.ValueI - progressCurrent.ValueI) / progressValue);

            Bar.MaxValue = progressTotal.ValueI;
            Bar.Value = progressCurrent.ValueI; 
            
            DurationLabel.Text = DurationLabel_Original.Replace("$turns", remaining.ToString());

            //int priority = actionCon.GetSub("Priority").ValueI;
            //PriorityLow.Visible = priority >= 1;
            //PriorityMed.Visible = priority >= 2;
            //PriorityHigh.Visible = priority >= 3;

            ExtraEnergy.Visible = false;
            ExtraProduction.Visible = false;
            ExtraBC.Visible = false;
            ExtraPrivateIndustry.Visible = false;
            Extra.Visible = false;
        }
        else
        {
            //DataBlock progressTotal = actionCon.GetSub("Progress:Total");
            //DataBlock progressCurrent = actionCon.GetSub("Progress:Current");
            //int remaining = progressTotal.ValueI - progressCurrent.ValueI;
            //IdleLabel.Text = IdleLabel_Original.Replace("$turns", remaining.ToString());

            Working.Visible = false;
            Idle.Visible = true;

            //PriorityLow.Visible = false;
            //PriorityMed.Visible = false;
            //PriorityHigh.Visible = false;
        }
    }
}