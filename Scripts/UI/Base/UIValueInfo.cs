using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIValueInfo : Panel
{
    // is beeing duplicated
    private RichTextLabel ValueLabel;
    private string ValueLabel_Original;
    private UITooltipTrigger Tooltip = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        ValueLabel = GetNode<RichTextLabel>("Value");
        ValueLabel_Original = ValueLabel.Text;

        if (HasNode("ToolTip"))
            Tooltip = GetNode<UITooltipTrigger>("ToolTip");
    }

    public void Refresh(string value, string tooltip = "")
    {
        ValueLabel.Text = ValueLabel_Original.Replace("$value", value);

        if (Tooltip != null)
            Tooltip.Row_2 = tooltip;
    }
}