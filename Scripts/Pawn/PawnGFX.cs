using Godot;

// Generated
[Tool]
public partial class PawnGFX : Node3D
{
    [Export]
    public Color Color = new Color(1.0f, 0.0f, 0.0f);

    public void SetPawnColor( string name )
    {
        //Label.Text = name;
    }

    public void SignalInputEvent(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, int shapeIdx)
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
