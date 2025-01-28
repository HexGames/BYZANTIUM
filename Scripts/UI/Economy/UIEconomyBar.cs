using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIEconomyBar : Control
{
    [ExportCategory("Links")]
    [Export]
    public UIText Income_Pops;
    [Export]
    public UIText Income_Factories;
    [Export]
    public UIText Income_Research;
    [Export]
    public UIText Income_Shipbuilding;

    [Export]
    public UIText Stockpile_BC;
    [Export]
    public UIText Stockpile_Influence;

    [Export]
    public UIText Cores;
    [Export]
    public UIText Cores_System;
    [Export]
    public UIText Cores_Fleets;
    [Export]
    public UIText Cores_Embassies;
    [Export]
    public UIText Cores_Spys;

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

        Income_Pops.SetTextWithReplace("$v", Helper.ResValueToString(_Player.Stats_PerTurn.Pops, 10, true, true));
        Income_Factories.SetTextWithReplace("$v", Helper.ResValueToString(0, 10, true, true));
        Income_Research.SetTextWithReplace("$v", Helper.ResValueToString(_Player.Stockpiles_PerTurn.Research, 10, true, true));
        Income_Shipbuilding.SetTextWithReplace("$v", Helper.ResValueToString(0, 10, true, true));

        Stockpile_BC.SetTextWithReplace("$val", Helper.ResValueToString(_Player.Stockpiles_PerTurn.BC) + " (" + Helper.ResValueToString(_Player.Stockpiles_PerTurn.BC_Income, 10, true, true) + ")");
        Stockpile_Influence.SetTextWithReplace("$val", Helper.ResValueToString(_Player.Stockpiles_PerTurn.Influence) + " (" + Helper.ResValueToString(_Player.Stockpiles_PerTurn.Influence_Income, 10, true, true) + ")");

        Cores.SetTextWithReplace("$val", "0", "$max", "0");
        Cores_System.SetTextWithReplace("$v", "0");
        Cores_Fleets.SetTextWithReplace("$v", "0");
        Cores_Embassies.SetTextWithReplace("$v", "0");
        Cores_Spys.SetTextWithReplace("$v", "0");
    }
}