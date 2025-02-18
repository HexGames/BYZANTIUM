using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIStarInfo : Control
{
    [ExportCategory("Links")]
    [Export]
    public UIText StarName;
    [Export]
    public Control Unexplored;
    [Export]
    public UIStarInfoUncolonized Uncolonized;
    [Export]
    public UIStarInfoSystem System;

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;
    // ---

    private bool Refreshed = false;
    public void NeedsRefresh()
    {
        Refreshed = false;
        if (Visible) Refresh(_Star);
    }

    public void Refresh(StarData star)
    {
        if (star == null) return;
        Visible = true;
        if (_Star == star && Refreshed) return;

        _Star = star;
        _System = _Star.System;
        Refreshed = true;


        StarName.SetTextWithReplace("$name", _Star.StarName);

        //GD.Print("---" + _System);
        if (_Star.Visibility_PerTurn.IsUncoveredBy(Game.self.HumanPlayer))
        {
            if (_System != null)
            {
                Unexplored.Visible = false;
                Uncolonized.Visible = false;
                System.Visible = true;

                //GD.Print("-- xxx ---");
                System.Refresh(_System);
            }
            else
            {
                Unexplored.Visible = false;
                Uncolonized.Visible = true;
                System.Visible = false;

                //GD.Print("-- OOO ---");
                Uncolonized.Refresh(_Star);
            }
        }
        else
        {
            Unexplored.Visible = true;
            Uncolonized.Visible = false;
            System.Visible = false;
        }
    }

    public void OnColonize()
    {
        //_Star.UseFirendlySupply(Game.self.HumanPlayer, 100)
    }
}