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

    PlayerData _Player;

    public PlayerStockpilesWrapper(PlayerData player)
    {
        _Player = player;
    }

    public void Refresh()
    {
        BC = _Player.Data.GetSubValueI("Stockpiles/BC");
        Influence = _Player.Data.GetSubValueI("Stockpiles/Influence");
        Research = _Player.Data.GetSubValueI("Stockpiles/Research");
    }
    public void Save()
    {
        _Player.Data.SetSubValueI("Stockpiles/BC", BC, Game.self.Def);
        _Player.Data.SetSubValueI("Stockpiles/Influence", Influence, Game.self.Def);
        _Player.Data.SetSubValueI("Stockpiles/Research", Research, Game.self.Def);
    }
}
