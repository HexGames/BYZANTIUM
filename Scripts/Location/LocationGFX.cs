using Godot;

// Generated
[Tool]
public partial class LocationGFX : Node3D
{
    [Export]
    public Label3D Label = null;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        // anti error hack
        CollisionObject3D collision = GetNode<CollisionObject3D>("MeshInstance3D/Area3D");
        collision.InputEvent += SignalInputEvent;
    }

    public void SetLocationName( string name )
    {
        Label.Text = name;
    }

    static public void SetLocationName(LocationGFX location, string name) // for generating / editor
    {
        location.Label.Text = name;
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
                    LocationNode parentLocationNode = GetParent<LocationNode>();
                    parentLocationNode.Select();
                }
            }
        }
    }
}
