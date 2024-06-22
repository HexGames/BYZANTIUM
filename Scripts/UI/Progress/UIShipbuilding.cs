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
    public TextureRect ShipIcon;
    [Export]
    public RichTextLabel ShipName;
    private string ShipName_Original;
    [Export]
    public Button Right;
    [Export]
    public Button Left;
    [Export]
    public UIConstructionItem Current;
    [Export]
    public RichTextLabel ProgressLabel;
    private string ProgressLabel_Original;

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

        if (_Sector.BuildQueue_PerTurn_ActionChange.Buildings.Count > 0)
        {
            Current.Refresh(_Sector.BuildQueue_PerTurn_ActionChange.Buildings[0]);
        }
        else
        {
        }

        int production = _Sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal();
        ProgressLabel.Text = ProgressLabel_Original.Replace("$value", Helper.ResValueToString(production));
    }
}