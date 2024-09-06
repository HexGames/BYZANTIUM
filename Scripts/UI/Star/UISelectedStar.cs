using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISelectedStar : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel StarName;
    private string StarName_Original;
    [Export]
    public Control Uncolonized = null;
    [Export]
    public Control Colonized = null;
    [Export]
    public Control ProductionInfo = null;

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;

    Game Game;
    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");
        StarName_Original = StarName.Text;
    }

    public void Refresh(StarData star)
    {
        _Star = star;
        _System = _Star.System;

        StarName.Text = StarName_Original.Replace("$name", star.StarName);

        if (_System != null)
        {
            Uncolonized.Visible = false;
            Colonized.Visible = true;
            ProductionInfo.Visible = true;
        }
        else
        {
            Uncolonized.Visible = true;
            Colonized.Visible = false;
            ProductionInfo.Visible = false;
        }
    }
}