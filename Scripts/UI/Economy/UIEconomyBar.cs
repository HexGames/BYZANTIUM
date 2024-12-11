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

        BC.SetTextWithReplace("$val", _Player.Stockpiles_PerTurn.BC.ToString());
        Influence.SetTextWithReplace("$val", _Player.Stockpiles_PerTurn.Influence.ToString());
        Research.SetTextWithReplace("$val", _Player.Stockpiles_PerTurn.Research.ToString());
    }
}