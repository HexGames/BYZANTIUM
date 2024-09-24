using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISelectedStar : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel StarName;
    private static string StarName_Original = "";
    [Export]
    public UISelectedStarColonize Uncolonized;
    [Export]
    public UISelectedStarSystem Colonized;
    [Export]
    public UISelectedStarProduction ProductionInfo;

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;

    public override void _Ready()
    {
        if (StarName_Original != null) StarName_Original = StarName.Text;
    }

    public void Refresh(StarData star)
    {
        _Star = star;
        _System = _Star.System;

        StarName.Text = StarName_Original.Replace("$name", _Star.StarName);

        if (_System != null)
        {
            Uncolonized.Visible = false;
            Colonized.Visible = true;
            ProductionInfo.Visible = true;

            Colonized.Refresh(_System);
            ProductionInfo.Refresh(_System);
        }
        else
        {
            Uncolonized.Visible = true;
            Colonized.Visible = false;
            ProductionInfo.Visible = false;

            Uncolonized.Refresh(_Star);
        }
    }

    public void OnColonize()
    {
        //_Star.UseFirendlySupply(Game.self.HumanPlayer, 100);


    }
}