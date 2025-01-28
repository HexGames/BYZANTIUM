using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIActionButton : Control
{
    private UIText Cost;
    private Panel Card;
    private UIText CardID;
    private UITooltipTrigger Tooltip;

    public override void _Ready()
    {
        Cost = GetNode<UIText>("Cost");
        Card = GetNode<Panel>("Card");
        CardID = GetNode<UIText>("Card/ID");
        Tooltip = GetNode<UITooltipTrigger>("ToolTip");
    }

    public void Refresh(int cost = -1, int cardID = -1)
    {
        if (cost >= 0)
        {
            Cost.Visible = true;
            Cost.SetTextWithReplace("$v", Helper.ResValueToString(cost));
        }
        else
        {
            Cost.Visible = false;
        }

        if (cardID >= 0)
        {
            Card.Visible = true;
            CardID.SetTextWithReplace("$v", cardID.ToString());
        }
        else
        {
            Card.Visible = false;
        }


    }
}
