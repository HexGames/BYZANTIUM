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

    public void SetTextWithReplace(string old_1, string new_1)
    {
        Text = Original.Replace(old_1, new_1).Replace("_", " ");
    }

    public void SetTextWithReplace(string old_1, string new_1, string old_2, string new_2)
    {
        Text = Original.Replace(old_1, new_1).Replace(old_2, new_2).Replace("_", " ");
    }

    public void SetTextWithReplace(string old_1, string new_1, string old_2, string new_2, string old_3, string new_3)
    {
        Text = Original.Replace(old_1, new_1).Replace(old_2, new_2).Replace(old_3, new_3).Replace("_", " ");
    }
}