using Godot;

public partial class UITooltipTrigger : Control
{
    public enum Orientation
    {
        Auto,
        RightUp,
        LeftUp,
        RightDown,
        LeftDown,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft,
    };

    [ExportCategory("Setup")]
    [Export]
    public bool CanBeHovered = true;
    [Export]
    public Orientation Direction = Orientation.Auto;
    [Export]
    public int Width = 128;

    [ExportCategory("Runtime Texts")]
    [Export(PropertyHint.MultilineText)]
    public string Title = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_1 = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_1_Right = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_2 = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_2_Right = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_3 = "";
    [Export(PropertyHint.MultilineText)]
    public string Row_3_Right = "";

    Game Game;

    [ExportCategory("Runtime Hover")]
    [Export]
    public bool Disabled = false;
    [Export]
    public UITooltip _Tooltip = null;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");
    }

    public void OnHoverEnter()
    {
        if (Disabled)
            return;

        // just the first time
        if (Direction == Orientation.Auto)
        {
            Vector2 center = GetGlobalRect().Position + GetGlobalRect().GetCenter();
            Vector2 screenSize = GetViewport().GetVisibleRect().Size;
            if (center.X > screenSize.X / 2 && center.Y > screenSize.Y / 2)
            {
                Direction = Orientation.LeftUp;
            }
            else if (center.X <= screenSize.X / 2 && center.Y > screenSize.Y / 2)
            {
                Direction = Orientation.RightUp;
            }
            else if (center.X > screenSize.X / 2 && center.Y <= screenSize.Y / 2)
            {
                Direction = Orientation.LeftDown;
            }
            else
            {
                Direction = Orientation.RightDown;
            }
        }

        // ---
        _Tooltip = Game.Tooltips.GetTooltip();
        _Tooltip.Refresh(Title, Row_1, Row_1_Right, Row_2, Row_2_Right, Row_3, Row_3_Right);
        _Tooltip.CanBeHovered = CanBeHovered;
        _Tooltip.CustomMinimumSize = new Vector2(Width, 0);
        _Tooltip.Size = Vector2.Zero;

        switch (Direction)
        {
            case Orientation.LeftUp:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X - _Tooltip.Size.X, GetGlobalRect().Position.Y + Size.Y - _Tooltip.Size.Y);
                    break;
                }
            case Orientation.RightUp:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X + Size.X, GetGlobalRect().Position.Y + Size.Y - _Tooltip.Size.Y);
                    break;
                }
            case Orientation.LeftDown:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X - _Tooltip.Size.X, GetGlobalRect().Position.Y);
                    break;
                }
            case Orientation.RightDown:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X + Size.X, GetGlobalRect().Position.Y);
                    break;
                }
            case Orientation.UpLeft:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X + Size.X - _Tooltip.Size.X, GetGlobalRect().Position.Y - _Tooltip.Size.Y);
                    break;
                }
            case Orientation.UpRight:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X, GetGlobalRect().Position.Y - _Tooltip.Size.Y);
                    break;
                }
            case Orientation.DownLeft:
                {
                    _Tooltip.Position = new Vector2(GetGlobalRect().Position.X + Size.X - _Tooltip.Size.X, GetGlobalRect().Position.Y + Size.Y);
                    break;
                }
            case Orientation.DownRight:
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