using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIConstructionItem : Control
{
    [ExportCategory("Links")]
    private RichTextLabel TitleLabel;
    private string TitleLabel_Original;
    private Panel ProgressBG = null;
    private Panel ProgressCurrent = null;
    private Panel ProgressNextTurn = null;
    private RichTextLabel Turn;
    private string Turn_Original;

    [ExportCategory("Runtime")]
    public DataBlock _Data = null;

    private float MaxProgress = 128f;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel = GetNode<RichTextLabel>("MarginContainer/Panel/Name");
        TitleLabel_Original = TitleLabel.Text;
        ProgressBG = GetNode<Panel>("MarginContainer/Panel");
        if (HasNode("MarginContainer/Panel/Progres"))
        {
            MaxProgress = ProgressBG.Size.X;
            ProgressCurrent = GetNode<Panel>("MarginContainer/Panel/Progres");
            ProgressNextTurn = GetNode<Panel>("MarginContainer/Panel/NextTurn");
        }
        Turn = GetNode<RichTextLabel>("MarginContainer/Panel/Name/Turns");
        Turn_Original = Turn.Text;
    }

    public void Refresh(DataBlock building, int production, ref int overflow, ref int turns)
    {
        _Data = building;

        PlanetData planet = Data.GetLinkPlanetData(building, Game.Map.Data);
        TitleLabel.Text = TitleLabel_Original.Replace("$name", _Data.Name).Replace("$location", planet.PlanetName);

        if (ProgressCurrent != null)
        {
            DataBlock inConstruction = building.GetSub("InConstruction");
            DataBlock progressData = inConstruction.GetSub("Progress");
            int progressCurrent = progressData.ValueI;
            int progressMax = inConstruction.GetSub("Progress:Max").ValueI;
            int progressNextTurn = Mathf.Min(progressMax, progressCurrent + production);

            float progress = 1.0f * progressCurrent / progressMax;
            float nextTurn = 1.0f * progressNextTurn / progressMax;
            ProgressCurrent.Size = new Vector2(progress * MaxProgress, ProgressCurrent.Size.Y);
            ProgressNextTurn.Size = new Vector2(nextTurn * MaxProgress, ProgressCurrent.Size.Y);

            int remaining = progressMax - progressCurrent;
            turns += (remaining - overflow) / production;
            overflow = (remaining - overflow) % production;
            Turn.Text = Turn_Original.Replace("$value", turns.ToString());
        }
        else
        {
            int progressMax = building.GetSub("Progress:Max").ValueI;
            turns += (progressMax - overflow) / production;
            overflow = (progressMax - overflow) % production;
            Turn.Text = Turn_Original.Replace("$value", turns.ToString());
        }
    }
}