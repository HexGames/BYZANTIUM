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
    public Panel Selected = null;
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

        if (HasNode("Selected"))
        {
            Selected = GetNode<Panel>("Selected");
        }

        if (HasNode("ToolTip")) ToolTip = GetNode<UITooltipTrigger>("ToolTip");
    }

    public void RefreshCooldown(int cooldown)
    {
        if (cooldown > 0)
        {
            Disabled = true;
            BtnText.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 1.0f);
            Cooldown.Visible = true;
            Cooldown.SetTextWithReplace("$t", cooldown.ToString());
        }
        else
        {
            Disabled = false;
            BtnText.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            Cooldown.Visible = false;
        }
    }
}
