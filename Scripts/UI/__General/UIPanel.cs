using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIPanel : Panel
{
    [Export]
    public UITooltipTrigger ToolTip = null;

    public override void _Ready()
    {
        if (HasNode("ToolTip")) ToolTip = GetNode<UITooltipTrigger>("ToolTip");
    }
}