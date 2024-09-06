using Godot;
using Godot.Collections;

[Tool]
public partial class GFXStarShip : Node3D
{
    // planets and moons
    private MeshInstance3D Ship = null;
    private Area3D Collision = null;
    public CollisionShape3D CollisionShape = null;

    private bool Friendly = false;

    [ExportCategory("Runtime")]
    [Export]
    public Array<FleetData> _Fleets = new Array<FleetData>();

    public void Init(bool friendly)
    {
        Friendly = friendly;

        Ship = GetNode<MeshInstance3D>("Ship");
        Collision = GetNode<Area3D>("Area3D");
        CollisionShape = GetNode<CollisionShape3D>("Area3D/CollisionShape3D");

        Collision.InputEvent += SignalInputEvent;
        Collision.MouseEntered += OnHover;
        Collision.MouseExited += OnDehover;

        //Visible = false;
    }

    public void RefreshShip(Array<FleetData> fleets)
    {
        _Fleets.Clear();

        if (fleets.Count > 0)
        {
            _Fleets.AddRange(fleets);

            CollisionShape.Disabled = false;
            Visible = true;
        }
        else
        {
            CollisionShape.Disabled = true;
            Visible = false;
        }
    }

    public void SignalInputEvent(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, long shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if (!mouseButtonEvent.IsPressed())
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                {
                    Game.self.Input.OnSelectFleets(_Fleets);
                }
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    Game.self.Input.DeselectOneStep();
                }
            }
        }
    }

    public void OnHover()
    {
        Game.self.Input.OnHoverFleets(_Fleets);
    }

    public void OnDehover()
    {
        Game.self.Input.OnDehoverFleets();
    }
}