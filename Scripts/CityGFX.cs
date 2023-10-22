using Godot;

// Generated
[Tool]
public partial class CityGFX : Node3D
{
    [Export]
    public Label3D Label = null;

    public void SetCityName( string name )
    {
        Label.Text = name;
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
                    GD.Print("You clicked on " + Label.Text);
                }
            }
        }
    }
}
