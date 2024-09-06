using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIConstructionItem : Control
{
    [ExportCategory("Links")]
    private RichTextLabel TitleLabel;
    private static string TitleLabel_Original = "";
    private Panel ProgressBG = null;
    private ProgressBar ProgressCurrent = null;
    private ProgressBar ProgressNextTurn = null;
    private RichTextLabel Turn;
    private static string Turn_Original = "";

    [ExportCategory("Runtime")]
    public BuildingQueueWrapper.Info _BuildingInQueue = null;

    //private float MaxProgress = 128f;

    Game Game;

    public override void _Ready()
    {
        return;

        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel = GetNode<RichTextLabel>("MarginContainer/Panel/Name");
        if (TitleLabel_Original.Length == 0) TitleLabel_Original = TitleLabel.Text;
        ProgressBG = GetNode<Panel>("MarginContainer/Panel");
        if (HasNode("MarginContainer/Panel/Progress"))
        {
            //MaxProgress = ProgressBG.Size.X;
            ProgressCurrent = GetNode<ProgressBar>("MarginContainer/Panel/Progress");
            ProgressNextTurn = GetNode<ProgressBar>("MarginContainer/Panel/NextTurn");
        }
        Turn = GetNode<RichTextLabel>("MarginContainer/Panel/Name/Turns");
        if (Turn_Original.Length == 0) Turn_Original = Turn.Text;
    }

    public void Refresh(BuildingQueueWrapper.Info buildingInQueue)
    {
        _BuildingInQueue = buildingInQueue;

        TitleLabel.Text = TitleLabel_Original.Replace("$name", _BuildingInQueue.BuildingDef.ValueS).Replace("$location", _BuildingInQueue.Planet.PlanetName);

        if (ProgressCurrent != null)
        {
            ProgressCurrent.MaxValue = _BuildingInQueue.ProgressMax;
            ProgressCurrent.Value = _BuildingInQueue.Progress;
            ProgressNextTurn.MaxValue = _BuildingInQueue.ProgressMax;
            ProgressNextTurn.Value = _BuildingInQueue.ProgressNextTurn;
        }
        Turn.Text = Turn_Original.Replace("$value", _BuildingInQueue.Turns.ToString());
    }
}