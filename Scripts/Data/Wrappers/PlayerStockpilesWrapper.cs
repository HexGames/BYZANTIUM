using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

public class PlayerStockpilesWrapper
{
    public int BC;
    public int Influence;
    public int Research;

    public int BC_Income;
    public int Influence_Income;
    public int Research_Income;

    PlayerData _Player;

    public PlayerStockpilesWrapper(PlayerData player)
    {
        _Player = player;
    }

    public void Refresh_5()
    {
        BC = _Player.Data.GetSubValueI("Stockpiles", "BC");
        Influence = _Player.Data.GetSubValueI("Stockpiles", "Influence");
        Research = _Player.Data.GetSubValueI("Stockpiles", "Research");

        BC_Income = 0;
        Influence_Income = 0;
        Research_Income = 0;
        for (int idx = 0; idx < _Player.Systems.Count; idx++)
        {
            BC_Income += _Player.Systems[idx].Economy_PerTurn.BC;
            Influence_Income += _Player.Systems[idx].Economy_PerTurn.Influence;
            Research_Income += _Player.Systems[idx].Economy_PerTurn.Research;
        }
    }

    public void EndTurn()
    {
        BC += BC_Income;
        _Player.Data.SetSubValueI("Stockpiles", "BC", BC, Game.self.Def);
        Influence += Influence_Income;
        _Player.Data.SetSubValueI("Stockpiles", "Influence", Influence, Game.self.Def);
        Research += Research_Income;
        _Player.Data.SetSubValueI("Stockpiles", "Research", Research, Game.self.Def);
    }


    public void Save()
    {
        _Player.Data.SetSubValueI("Stockpiles", "BC", BC, Game.self.Def);
        _Player.Data.SetSubValueI("Stockpiles", "Influence", Influence, Game.self.Def);
        _Player.Data.SetSubValueI("Stockpiles", "Research", Research, Game.self.Def);
    }
}
