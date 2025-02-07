using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDistrictsGroupPlanetPopsItem : Control
{
    // is beeing duplicated
    public TextureRect Pop;
    public UITooltipTrigger Tooltip;

    // runtime
    public PopData _Pop = null;

    public override void _Ready()
    {
        Pop = GetNode<TextureRect>("TextureRect");
        Tooltip = GetNode<UITooltipTrigger>("TextureRect/ToolTip");
    }

    public void Refresh(PopData pop)
    {
        _Pop = pop;
    }
}