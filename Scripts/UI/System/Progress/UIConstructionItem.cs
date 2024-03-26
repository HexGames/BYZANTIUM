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
    private ProgressBar ProgressCurrent = null;
    private ProgressBar ProgressNextTurn = null;
    private RichTextLabel Turn;
    private string Turn_Original;

    [ExportCategory("Runtime")]
    public DataBlock _Data = null;

    //private float MaxProgress = 128f;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel = GetNode<RichTextLabel>("MarginContainer/Panel/Name");
        TitleLabel_Original = TitleLabel.Text;
        ProgressBG = GetNode<Panel>("MarginContainer/Panel");
        if (HasNode("MarginContainer/Panel/Progress"))
        {
            //MaxProgress = ProgressBG.Size.X;
            ProgressCurrent = GetNode<ProgressBar>("MarginContainer/Panel/Progress");
            ProgressNextTurn = GetNode<ProgressBar>("MarginContainer/Panel/NextTurn");
        }
        Turn = GetNode<RichTextLabel>("MarginContainer/Panel/Name/Turns");
        Turn_Original = Turn.Text;
    }

    public void Refresh(DataBlock building, int production, ref int overflow, ref int turns)
    {
        _Data = building;

        PlanetData planet = Data.GetLinkPlanetData(building, Game.Map.Data);
        TitleLabel.Text = TitleLabel_Original.Replace("$name", _Data.Name).Replace("$location", planet.PlanetName);

        DataBlock realBuilding = null;
        if (planet.Colony != null)
        {
            realBuilding = planet.Colony.Buildings.GetSub(building.ValueS);
        }

        if (realBuilding != null)
        {
            DataBlock inConstruction = realBuilding.GetSub("InConstruction");
            int progressCurrent = inConstruction.GetSub("Progress").ValueI;
            int progressMax = inConstruction.GetSub("Progress:Max").ValueI;
            int progressNextTurn = Mathf.Min(progressMax, progressCurrent + production);

            if (ProgressCurrent != null)
            {
                ProgressCurrent.MaxValue = progressMax;
                ProgressCurrent.Value = progressCurrent;
                ProgressNextTurn.MaxValue = progressMax;
                ProgressNextTurn.Value = progressNextTurn;
            }

            int remaining = progressMax - progressCurrent;
            turns += Mathf.CeilToInt(1.0f * (remaining - overflow) / production);
            overflow = turns * production - (remaining - overflow);
            Turn.Text = Turn_Original.Replace("$value", turns.ToString());
        }
        else
        {
            int progressMax = building.GetSub("Progress:Max").ValueI;
            int progressNextTurn = Mathf.Min(progressMax, production);

            if (ProgressCurrent != null)
            {
                ProgressCurrent.MaxValue = progressMax;
                ProgressCurrent.Value = 0;
                ProgressNextTurn.MaxValue = progressMax;
                ProgressNextTurn.Value = progressNextTurn;
            }

            int remaining = progressMax;
            turns += Mathf.CeilToInt(1.0f * (remaining - overflow) / production);
            overflow = turns * production - (remaining - overflow);
            Turn.Text = Turn_Original.Replace("$value", turns.ToString());
        }
    }
}