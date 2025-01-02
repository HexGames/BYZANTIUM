using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIEconomyBar : Control
{
    [ExportCategory("Links")]
    [Export]
    public UIText BC;
    [Export]
    public UIText Influence;
    [Export]
    public UIText Research;

    [ExportCategory("Runtime")]
    [Export]
    public PlayerData _Player = null;

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

        BC.SetTextWithReplace("$val", Helper.ResValueToString(_Player.Stockpiles_PerTurn.BC) + " (" + Helper.ResValueToString(_Player.Stockpiles_PerTurn.BC_Income, 10, true, true) + ")");
        Influence.SetTextWithReplace("$val", Helper.ResValueToString(_Player.Stockpiles_PerTurn.Influence) + " (" + Helper.ResValueToString(_Player.Stockpiles_PerTurn.Influence_Income, 10, true, true) + ")");
        Research.SetTextWithReplace("$val", Helper.ResValueToString(_Player.Stockpiles_PerTurn.Research) + " (" + Helper.ResValueToString(_Player.Stockpiles_PerTurn.Research_Income, 10, true, true) + ")");
    }
}