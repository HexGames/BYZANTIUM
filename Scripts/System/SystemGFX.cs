using Godot;

// Generated
[Tool]
public partial class SystemGFX : Node3D
{
    [Export]
    public SystemNode _Node = null;
    [Export]
    public UIGalaxySystem HUD = null;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        // anti error hack
        CollisionObject3D collision = GetNode<CollisionObject3D>("Star/Area3D");
        collision.InputEvent += SignalInputEvent;
    }

    public void SignalInputEvent(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, long shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if ( !mouseButtonEvent.IsPressed() )
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    SystemNode parentLocationNode = GetParent<SystemNode>();
                    parentLocationNode.Select();
                }
            }
        }
    }
}
