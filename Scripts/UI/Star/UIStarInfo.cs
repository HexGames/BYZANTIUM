using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIStarInfo : Control
{
    [ExportCategory("Links")]
    [Export]
    public UIText StarName;
    [Export]
    public UIStarInfoUncolonized Uncolonized;
    [Export]
    public UIStarInfoSystem System;

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;

    public void Refresh(StarData star)
    {
        _Star = star;
        _System = _Star.System;

        StarName.SetTextWithReplace("$name", _Star.StarName);

        //GD.Print("---" + _System);
        if (_System != null)
        {
            Uncolonized.Visible = false;
            System.Visible = true;

            //GD.Print("-- xxx ---");
            System.Refresh(_System);
        }
        else
        {
            Uncolonized.Visible = true;
            System.Visible = false;

            //GD.Print("-- OOO ---");
            Uncolonized.Refresh(_Star);
        }
    }

    public void OnColonize()
    {
        //_Star.UseFirendlySupply(Game.self.HumanPlayer, 100)
    }
}