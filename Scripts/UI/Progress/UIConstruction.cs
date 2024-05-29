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
    public SectorData _Sector = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel_Original = TitleLabel.Text;
        ProgressLabel_Original = ProgressLabel.Text;
    }

    public void Refresh(SectorData sector)
    {
        _Sector = sector;
        // TitleLabel.Text = TitleLabel_Original.Replace("$name", sector.SectorName);

        if (_Sector.BuildQueue_PerTurn_ActionChange.Buildings.Count > 0)
        {
            while (Queue.Count < _Sector.BuildQueue_PerTurn_ActionChange.Buildings.Count - 1)
            {
                UIConstructionItem newItem = Queue[0].Duplicate(7) as UIConstructionItem;
                Queue[0].GetParent().AddChild(newItem);
                Queue.Add(newItem);
            }

            for (int idx = Queue.Count - 1; idx >= 0; idx--)
            {
                if (idx + 1 < _Sector.BuildQueue_PerTurn_ActionChange.Buildings.Count)
                {
                    Queue[idx].Refresh(_Sector.BuildQueue_PerTurn_ActionChange.Buildings[_Sector.BuildQueue_PerTurn_ActionChange.Buildings.Count - 1 - idx]);
                    Queue[idx].Visible = true;
                }
                else
                {
                    Queue[idx].Visible = false;
                }
            }

            Current.Refresh(_Sector.BuildQueue_PerTurn_ActionChange.Buildings[0]);

            Working.Visible = true;
            Idle.Visible = false;
        }
        else
        {
            Working.Visible = false;
            Idle.Visible = true;
        }

        int production = _Sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal();
        ProgressLabel.Text = ProgressLabel_Original.Replace("$value", Helper.ResValueToString(production));

        ExtraEnergy.Visible = false;
        ExtraBC.Visible = false;
        ExtraPrivateIndustry.Visible = false;

        /*Array<DataBlock> queuedBuildings = sector.ActionBuildQueue.GetSubs("Building");
        int production = _Data.Resources_PerTurn.Get("Production").Value_2;

        if (queuedBuildings.Count > 0)
        {
            int overflow = sector.ActionBuildQueue.GetSub("Overflow").ValueI;
            int turns = 1;
            Current.Refresh(queuedBuildings[0], production, ref overflow, ref turns);

            // grow

            for (int idx = Queue.Count - 1; idx >= 0; idx--)
            {
                if (idx + 1 < queuedBuildings.Count)
                {
                    Queue[idx].Refresh(queuedBuildings[queuedBuildings.Count - 1 - idx], production, ref overflow, ref turns);
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
        }*/
    }
}