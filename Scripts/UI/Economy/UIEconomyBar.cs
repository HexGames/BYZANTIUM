using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIEconomyBar : Control
{
    [ExportCategory("Links")]
    [Export]
    public UIEconomyInfoItem BC;
    [Export]
    public UIEconomyInfoItem Trade;
    [Export]
    public UIEconomyInfoItem Research;
    [Export]
    public UIEconomyInfoItem Culture;
    [Export]
    public UIEconomyInfoItem Authority;
    [Export]
    public UIEconomyInfoItem Influence;

    [ExportCategory("Runtime")]
    [Export]
    public PlayerData _Player = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");
    }

    public void Refresh(PlayerData player)
    {
        _Player = player;

        BC.Refresh(_Player.Resources_PerTurn.GetStockpileString("BC"));
        BC.Visible = true;

        Trade.Refresh(_Player.Resources_PerTurn.GetUsedPerTotalString("Trade"));
        Trade.Visible = true;

        Research.Refresh(_Player.Resources_PerTurn.GetIncomeString("TechPoints"));
        Research.Visible = true;

        Culture.Refresh(_Player.Resources_PerTurn.GetIncomeString("CulturePoints"));
        Culture.Visible = true;

        Authority.Refresh(_Player.Resources_PerTurn.GetUsedPerTotalString("Authority"));
        Authority.Visible = true;

        Influence.Refresh(_Player.Resources_PerTurn.GetUsedPerTotalString("Influence"));
        Influence.Visible = true;
    }
}