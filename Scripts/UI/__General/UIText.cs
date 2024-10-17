using Godot;
using Godot.Collections;
using System.ComponentModel;

public partial class UIText : RichTextLabel
{
    [Export]
    public string Original = "";
    [Export]
    public UITooltipTrigger ToolTip = null;

    public override void _Ready()
    {
        if (Original == "") Original = Text;

        if (HasNode("ToolTip")) ToolTip = GetNode<UITooltipTrigger>("ToolTip");
        if (ToolTip == null && HasNode("../ToolTip"))  ToolTip = GetNode<UITooltipTrigger>("../ToolTip");
    }
}