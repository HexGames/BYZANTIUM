using Godot;
using Godot.Collections;

public partial class UITooltipManager : Control
{
    [Export]
    public Array<UITooltip> Tooltips = null;

    Game Game;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");
    }

    public UITooltip GetTooltip()
    {
        return Tooltips[0];
    }
}