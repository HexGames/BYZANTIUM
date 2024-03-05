using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISectorCampaign : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel NameLabel;
    private string NameLabel_Original;
    [Export]
    public RichTextLabel LocationLabel;
    private string LocationLabel_Original;
    [Export]
    public RichTextLabel DurationLabel;
    private string DurationLabel_Original;
    [Export]
    public RichTextLabel ProgressLabel;
    private string ProgressLabel_Original;
    [Export]
    public ProgressBar Bar = null;

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        NameLabel_Original = NameLabel.Text;
        LocationLabel_Original = LocationLabel.Text;
        DurationLabel_Original = DurationLabel.Text;
        ProgressLabel_Original = ProgressLabel.Text;
    }

    public void RefreshCampaignProgress(DataBlock campaignProgress, int progressValue)
    {
        DataBlock name = campaignProgress.GetSub("Name");
        NameLabel.Text = NameLabel_Original.Replace("$name", name.ValueS);

        DataBlock location = campaignProgress.GetSub("Link:System:Colony");
        LocationLabel.Text = LocationLabel_Original.Replace("$location", location.ValueS);

        ProgressLabel.Text = ProgressLabel_Original.Replace("$value", progressValue.ToString());

        DataBlock fixedTurnsTotal = Game.Def.GetCampaign(name.ValueS).GetSub("FixedTurns");
        DataBlock fixedTurnsCurrent = campaignProgress.GetSub("FixedTurns:Current");
        if (fixedTurnsCurrent != null)
        {
            int remaining = fixedTurnsTotal.ValueI - fixedTurnsCurrent.ValueI;
            DurationLabel.Text = DurationLabel_Original.Replace("$turns", remaining.ToString());

            Bar.MaxValue = fixedTurnsTotal.ValueI;
            Bar.Value = fixedTurnsCurrent.ValueI;
        }
    }
}