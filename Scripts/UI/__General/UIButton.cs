using Godot;
using Godot.Collections;
using System.ComponentModel;

public partial class UIButton : Button
{
    [Export]
    public UIText BtnText = null;
    [Export]
    public UIText Cooldown = null;
    [Export]
    public UITooltipTrigger ToolTip = null;

    public override void _Ready()
    {
        if (HasNode("Text"))
        {
            BtnText = GetNode<UIText>("Text");
        }

        if (HasNode("Cooldown"))
        {
            Cooldown = GetNode<UIText>("Cooldown");
        }

        if (HasNode("ToolTip")) ToolTip = GetNode<UITooltipTrigger>("ToolTip");
    }
}