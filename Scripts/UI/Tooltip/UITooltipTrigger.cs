using Godot;

public partial class UITooltipTrigger : Control
{
    public enum Orientation
    {
        AUTO,
        RIGHT_UP,
        LEFT_UP,
        RIGHT_DOWN,
        LEFT_DOWN,
        UP_RIGHT,
        UP_LEFT,
        DOWN_RIGHT,
        DOWN_LEFT,
    };

    [ExportCategory("Setup")]
    [Export]
    public bool CanBeHovered = true;
    [Export]
    public Orientation Direction = Orientation.AUTO;
    [Export]
    public int Width = 128;

    [ExportCategory("Runtime Texts")]
    [Export(PropertyHint.MultilineText)]
    public string Title = "";
    public string Title_Original = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_1 = "";
    public string Row_1_Original = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_1_Right = "";
    public string Row_1_Right_Original = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_2 = "";
    public string Row_2_Original = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_2_Right = "";
    public string Row_2_Right_Original = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_3 = "";
    public string Row_3_Original = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_3_Right = "";
    public string Row_3_Right_Original = "";


    [ExportCategory("Runtime Hover")]
    [Export]
    public bool Disabled = false;
    [Export]
    public UITooltip _Tooltip = null;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        Title_Original = Title;
        Row_1_Original = Row_1;
        Row_1_Right_Original = Row_1_Right;
        Row_2_Original = Row_2;
        Row_2_Right_Original = Row_2_Right;
        Row_3_Original = Row_3;
        Row_3_Right_Original = Row_3_Right;
    }

    public void OnHoverEnter()
    {
        if (Disabled)
            return;

        // just the first time
        if (Direction == Orientation.AUTO)
        {
            Vector2 center = GetGlobalRect().Position + GetGlobalRect().GetCenter();
            Vector2 screenSize = GetViewport().GetVisibleRect().Size;
            if (center.X > screenSize.X / 2 && center.Y > screenSize.Y / 2)
            {
                Direction = Orientation.LEFT_UP;
            }
            else if (center.X <= screenSize.X / 2 && center.Y > screenSize.Y / 2)
            {
                Direction = Orientation.RIGHT_UP;
            }
            else if (center.X > screenSize.X / 2 && center.Y <= screenSize.Y / 2)
            {
                Direction = Orientation.LEFT_DOWN;
            }
            else
            {
                Direction = Orientation.RIGHT_DOWN;
            }
        }

        // ---
        _Tooltip = Game.self.Tooltips.GetTooltip();
        _Tooltip.Refresh(Title, Row_1, Row_1_Right, Row_2, Row_2_Right, Row_3, Row_3_Right);
        _Tooltip.CanBeHovered = CanBeHovered;
        _Tooltip.CustomMinimumSize = new Vector2(Width, 0);
        _Tooltip.Size = Vector2.Zero;

        switch (Direction)
        {
            case Orientation.LEFT_UP:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X - _Tooltip.Size.X, GetGlobalRect().Position.Y + Size.Y - _Tooltip.Size.Y);
                    break;
                }
            case Orientation.RIGHT_UP:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X + Size.X, GetGlobalRect().Position.Y + Size.Y - _Tooltip.Size.Y);
                    break;
                }
            case Orientation.LEFT_DOWN:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X - _Tooltip.Size.X, GetGlobalRect().Position.Y);
                    break;
                }
            case Orientation.RIGHT_DOWN:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X + Size.X, GetGlobalRect().Position.Y);
                    break;
                }
            case Orientation.UP_LEFT:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X + Size.X - _Tooltip.Size.X, GetGlobalRect().Position.Y - _Tooltip.Size.Y);
                    break;
                }
            case Orientation.UP_RIGHT:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X, GetGlobalRect().Position.Y - _Tooltip.Size.Y);
                    break;
                }
            case Orientation.DOWN_LEFT:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X + Size.X - _Tooltip.Size.X, GetGlobalRect().Position.Y + Size.Y);
                    break;
                }
            case Orientation.DOWN_RIGHT:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X, GetGlobalRect().Position.Y + Size.Y);
                    break;
                }
        }

        _Tooltip.TargetHoverEnter();
    }

    public void OnHoverExit()
    {
        if (_Tooltip != null)
        {
            _Tooltip.TargetHoverExit();
            _Tooltip = null;
        }
    }
}