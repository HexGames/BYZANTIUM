using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

// Generated
[Tool]
public partial class StarGFXIncomingFleet : Node3D
{
    private Node3D HUDPoint = null;
    [Export]
    public GFXStar _Parent = null;
    [Export]
    public UIGalaxyPath HUD = null;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        HUDPoint = GetNode<Node3D>("Point");
    }

    public void Refresh()
    {
        HUD.Visible = true;

        if (HUD == null)
        {
            //Game.self.GalaxyUI.AddIncomingLabel(this);
        }

        HUD.Refresh();
        HUD.Visible = true;
    }
}
