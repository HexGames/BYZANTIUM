using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIConstruction : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel TitleLabel;
    private string TitleLabel_Original;
    [Export]
    public Control Working = null;
    [Export]
    public Array<UIConstructionItem> Queue;
    [Export]
    public UIConstructionItem Current;
    [Export]
    public Control Idle = null;
    [Export]
    public RichTextLabel ProgressLabel;
    private string ProgressLabel_Original;
    [Export]
    public TextureRect ExtraEnergy = null;
    [Export]
    public TextureRect ExtraBC = null;
    [Export]
    public TextureRect ExtraPrivateIndustry = null;

    [ExportCategory("Runtime")]
    [Export]
    public SectorData _Data = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel_Original = TitleLabel.Text;
        ProgressLabel_Original = ProgressLabel.Text;
    }

    public void Refresh(SectorData sector)
    {
        _Data = sector;
        // TitleLabel.Text = TitleLabel_Original.Replace("$name", sector.SectorName);

        Array<DataBlock> buildings = sector.ActionBuildQueue.GetSubs("Building");
        int production = _Data.Resources_PerTurn.Get("Production").Value_2;

        if (buildings.Count > 0)
        {
            int overflow = sector.ActionBuildQueue.GetSub("Overflow").ValueI;
            int turns = 1;
            Current.Refresh(buildings[0], production, ref overflow, ref turns);

            // grow
            while (Queue.Count < buildings.Count - 1)
            {
                UIConstructionItem newItem = Queue[0].Duplicate(7) as UIConstructionItem;
                Queue[0].GetParent().AddChild(newItem);
                Queue.Add(newItem);
            }

            for (int idx = Queue.Count - 1; idx >= 0; idx++)
            {
                if (idx + 1 < buildings.Count)
                {
                    Queue[idx].Refresh(buildings[buildings.Count - 1 - idx], production, ref overflow, ref turns);
                    Queue[idx].Visible = true;
                }
                else
                {
                    Queue[idx].Visible = false;
                }
            }

            Working.Visible = true;
            Idle.Visible = false;
        }
        else
        {
            Working.Visible = false;
            Idle.Visible = true;
        }

        ProgressLabel.Text = ProgressLabel_Original.Replace("$value", Helper.ResValueToString(production));

        ExtraEnergy.Visible = false;
        ExtraBC.Visible = false;
        ExtraPrivateIndustry.Visible = false;
    }

    public void OnBuild()
    {
        //Game.WindowsUI.Build(_Data);
    }
}