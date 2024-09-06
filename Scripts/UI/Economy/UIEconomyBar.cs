using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIEconomyBar : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control Empire_Container;
    [Export]
    public UIValueInfo Empire_BC;
    [Export]
    public UIValueInfo Empire_Research;
    [Export]
    public UIValueInfo Empire_Culture;
    [Export]
    public UIValueInfo Empire_Influence;
    [Export]
    public UIValueInfo Empire_Authority;

    [ExportCategory("Runtime")]
    [Export]
    public PlayerData _Player = null;


    public override void _Ready()
    {
    }

    public void Refresh(PlayerData player)
    {
        _Player = player;

        Refresh();
    }

    public void Refresh()
    {
        if (Game.self == null)
            return;

        if (_Player == null)
        {
            _Player = Game.self.HumanPlayer;
        }

        Empire_BC.Refresh(_Player.Resources_PerTurn.GetStockpileString("BC"), _Player.Resources_PerTurn.GetStockpileTooltip("BC"));
        Empire_BC.Visible = true;

        Empire_Research.Refresh(_Player.Resources_PerTurn.GetIncomeString("Research"));
        Empire_Research.Visible = true;

        Empire_Culture.Refresh(_Player.Resources_PerTurn.GetIncomeString("Culture"));
        Empire_Culture.Visible = true;

        Empire_Influence.Refresh(_Player.Resources_PerTurn.GetLimitString("Authority"));
        //Empire_Authority.Text = Empire_Authority_Original.Replace("$value", _Player.Resources_PerTurn.GetLimitString("Influence"));
        Empire_Influence.Visible = true;
    }
}