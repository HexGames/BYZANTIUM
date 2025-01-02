using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionTax
{
    static public void SetTax(SystemData system, int tax)
    {
        system.Data.SetSubValueI("ActionTax", "Tax", tax, Game.self.Def);

        system.Init_DistrictData();
        system.Init_Resources();
    }
}