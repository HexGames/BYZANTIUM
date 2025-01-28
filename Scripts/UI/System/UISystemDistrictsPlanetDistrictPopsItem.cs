using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISystemDistrictsPlanetDistrictPopsItem : Control
{
    // is beeing duplicated
    public TextureRect Pop;
    public UITooltipTrigger Tooltip;

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;

    public override void _Ready()
    {
        Pop = GetNode<TextureRect>("TextureRect");
        Tooltip = GetNode<UITooltipTrigger>("TextureRect/ToolTip");
    }

    public void Refresh(StarData star)
    {
        _Star = star;
        _System = _Star.System;
    }
}