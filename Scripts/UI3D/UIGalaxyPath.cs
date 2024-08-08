using Godot;
using Godot.Collections;

public partial class UIGalaxyPath : Control
{
    // is beeing duplicated
    Control PathBg = null;

    RichTextLabel PathFleets = null;
    private static string PathFleets_Original = "";
    RichTextLabel PathTime = null;
    private static string PathTime_Original = "";
    UITooltipTrigger PathTooltip;

    RichTextLabel PathValue_1 = null;
    private static string PathValue_1_Original = "";
    RichTextLabel PathValue_2 = null;
    private static string PathValue_2_Original = "";
    RichTextLabel PathValue_3 = null;
    private static string PathValue_3_Original = "";
    RichTextLabel PathValue_More = null;
    UITooltipTrigger Tooltip = null;

    [ExportCategory("Runtime")]
    [Export]
    public GFXPathsItem _PathGFX = null;
    [Export]
    public GFXIncomingsItem _IncomingGFX = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        PathBg = GetNode<Control>("Container");

        PathFleets = GetNode<RichTextLabel>("Container/VBox/Container_Fleets/Text");
        if (PathFleets_Original.Length == 0) PathFleets_Original = PathFleets.Text;
        PathTime = GetNode<RichTextLabel>("Container/VBox/Container_Time/Text");
        if (PathTime_Original.Length == 0) PathTime_Original = PathTime.Text;
        PathTooltip = GetNode<UITooltipTrigger>("Container/ToolTip");

        PathValue_1 = GetNode<RichTextLabel>("Container/VBox/Container_1/Fleet");
        if (PathValue_1_Original.Length == 0) PathValue_1_Original = PathValue_1.Text;
        PathValue_2 = GetNode<RichTextLabel>("Container/VBox/Container_2/Fleet");
        if (PathValue_2_Original.Length == 0) PathValue_2_Original = PathValue_2.Text;
        PathValue_3 = GetNode<RichTextLabel>("Container/VBox/Container_3/Fleet");
        if (PathValue_3_Original.Length == 0) PathValue_3_Original = PathValue_3.Text;
        PathValue_More = GetNode<RichTextLabel>("Container/VBox/Container_4/Fleet");
        //Tooltip = GetNode<PanelContainer>("PanelContainer");
    }

    public void Refresh()
    {
        /*if (_PathGFX != null && _PathGFX._Fleets.Count > 0)
        {
            string fleets = "";
            string fleets_Tooltip = "";
            for (int idx = 0; idx < _PathGFX._Fleets.Count; idx++)
            {
                if (idx < 3)
                {
                    if (idx > 0) fleets += " ";
                    fleets += Helper.Split_0(_PathGFX._Fleets[idx].FleetName);
                }
                if (idx > 0) fleets_Tooltip += "\n";
                fleets_Tooltip += Helper.GetIcon("Fleet") + "[color=RED][b]" + _PathGFX._Fleets[idx].FleetName + "[/b][/color]: " + _PathGFX._Fleets[idx].GetLongName();
            }

            PathFleets.Text = PathFleets_Original.Replace("$v", fleets);
            PathTime.Text = PathTime_Original.Replace("$t", _PathGFX._Fleets[0].GetMoveActionTurns().ToString());
            PathTooltip.Title = "Preparing to jump Fleets";
            PathTooltip.Row_1 = fleets_Tooltip;
            PathTooltip.Row_2 = "Time to system:";
            PathTooltip.Row_2_Right = _PathGFX._Fleets[0].GetMoveActionTurns().ToString() + Helper.GetIcon("Turn") + " Turns";

            PathFleets.Visible = true;
            PathTime.Visible = true;
        }
        else
        {
            PathFleets.Visible = false;
            PathTime.Visible = false;
        }

        if (_IncomingGFX != null && _IncomingGFX._Fleets.Count > 0)
        {
            string fleets = "";
            string fleets_Tooltip = "";
            for (int idx = 0; idx < _IncomingGFX._Fleets.Count; idx++)
            {
                if (idx < 3)
                {
                    if (idx > 0) fleets += " ";
                    fleets += Helper.Split_0(_IncomingGFX._Fleets[idx].FleetName);
                }
                if (idx > 0) fleets_Tooltip += "\n";
                fleets_Tooltip += Helper.GetIcon("Fleet") + "[color=RED][b]" + _IncomingGFX._Fleets[idx].FleetName + "[/b][/color]: " + _IncomingGFX._Fleets[idx].GetLongName();
            }

            PathTooltip.Title = "Preparing to jump Fleets";
            PathTooltip.Row_1 = fleets_Tooltip;
            PathTooltip.Row_2 = "Time to system:";
            PathTooltip.Row_2_Right = _IncomingGFX._Fleets[0].GetMoveActionTurns().ToString() + Helper.GetIcon("Turn") + " Turns";

            if (_IncomingGFX._Fleets.Count > 0)
            {
                PathValue_1.Text = PathValue_1_Original.Replace("$v", Helper.Split_0(_IncomingGFX._Fleets[0].FleetName)).Replace("$t", _IncomingGFX._Fleets[0].GetMoveActionTurns().ToString());
                PathValue_1.Visible = true;
            }
            else
            {
                PathValue_1.Visible = false;
            }
            if (_IncomingGFX._Fleets.Count > 1)
            {
                PathValue_2.Text = PathValue_2_Original.Replace("$v", Helper.Split_0(_IncomingGFX._Fleets[1].FleetName)).Replace("$t", _IncomingGFX._Fleets[1].GetMoveActionTurns().ToString());
                PathValue_2.Visible = true;
            }
            else
            {
                PathValue_2.Visible = false;
            }
            if (_IncomingGFX._Fleets.Count > 2)
            {
                PathValue_3.Text = PathValue_3_Original.Replace("$v", Helper.Split_0(_IncomingGFX._Fleets[2].FleetName)).Replace("$t", _IncomingGFX._Fleets[2].GetMoveActionTurns().ToString());
                PathValue_3.Visible = true;
            }
            else
            {
                PathValue_3.Visible = false;
            }
            if (_IncomingGFX._Fleets.Count > 3)
            {
                PathValue_More.Visible = true;
            }
            else
            {
                PathValue_More.Visible = false;
            }
        }
        else
        {
            PathValue_1.Visible = false;
            PathValue_2.Visible = false;
            PathValue_3.Visible = false;
            PathValue_More.Visible = false;
        }*/

        PathFleets.Visible = false;
        PathTime.Visible = false;
        if (_PathGFX != null && _PathGFX._Fleets.Count > 0)
        {
            string fleets = "";
            string fleets_Tooltip = "";
            for (int idx = 0; idx < _PathGFX._Fleets.Count; idx++)
            {
                if (idx < 3)
                {
                    if (idx > 0) fleets += " ";
                    fleets += Helper.Split_0(_PathGFX._Fleets[idx].FleetName);
                }
                if (idx > 0) fleets_Tooltip += "\n";
                fleets_Tooltip += Helper.GetIcon("Fleet") + "[color=RED][b]" + _PathGFX._Fleets[idx].FleetName + "[/b][/color]: " + _PathGFX._Fleets[idx].GetLongName();
            }

            PathTooltip.Title = "Preparing to jump Fleets";
            PathTooltip.Row_1 = fleets_Tooltip;
            PathTooltip.Row_2 = "Time to system:";
            PathTooltip.Row_2_Right = _PathGFX._Fleets[0].GetMoveActionTurns().ToString() + Helper.GetIcon("Turn") + " Turns";

            if (_PathGFX._Fleets.Count > 0)
            {
                PathValue_1.Text = PathValue_1_Original.Replace("$v", Helper.Split_0(_PathGFX._Fleets[0].FleetName)).Replace("$t", _PathGFX._Fleets[0].GetMoveActionTurns().ToString());
                PathValue_1.Visible = true;
            }
            else
            {
                PathValue_1.Visible = false;
            }
            if (_PathGFX._Fleets.Count > 1)
            {
                PathValue_2.Text = PathValue_2_Original.Replace("$v", Helper.Split_0(_PathGFX._Fleets[1].FleetName)).Replace("$t", _PathGFX._Fleets[1].GetMoveActionTurns().ToString());
                PathValue_2.Visible = true;
            }
            else
            {
                PathValue_2.Visible = false;
            }
            if (_PathGFX._Fleets.Count > 2)
            {
                PathValue_3.Text = PathValue_3_Original.Replace("$v", Helper.Split_0(_PathGFX._Fleets[2].FleetName)).Replace("$t", _PathGFX._Fleets[2].GetMoveActionTurns().ToString());
                PathValue_3.Visible = true;
            }
            else
            {
                PathValue_3.Visible = false;
            }
            if (_PathGFX._Fleets.Count > 3)
            {
                PathValue_More.Visible = true;
            }
            else
            {
                PathValue_More.Visible = false;
            }
        }
        else
        {
            PathValue_1.Visible = false;
            PathValue_2.Visible = false;
            PathValue_3.Visible = false;
            PathValue_More.Visible = false;
        }
    }

    public override void _Process(double delta)
    {
        if (_PathGFX != null)
        {
            Vector2 pos2D = Game.Camera.UnprojectPosition(_PathGFX.GlobalPosition);
            Position = pos2D;
        }

        /*if (_IncomingGFX != null)
        {
            if (_IncomingGFX.IncomingRotation_Ship.Visible)
            {
                Vector2 pos2D = Game.Camera.UnprojectPosition(_IncomingGFX.IncomingPoint_Ship.GlobalPosition);
                Position = pos2D;
            }
            else // if (_IncomingGFX.IncomingRotation_NoShip.Visible)
            {
                Vector2 pos2D = Game.Camera.UnprojectPosition(_IncomingGFX.IncomingPoint_NoShip.GlobalPosition);
                Position = pos2D;
            }
        }*/
    }
}