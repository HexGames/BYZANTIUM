using Godot;
using Godot.Collections;

[Tool]
public partial class GFXStarShip : Node3D
{
    // planets and moons
    private MeshInstance3D Ship = null;
    private Node3D Hover = null;
    private Node3D Selected = null;
    private Area3D Collision = null;
    public CollisionShape3D CollisionShape = null;

    [ExportCategory("Runtime")]
    [Export]
    public Array<FleetData> _Fleets = new Array<FleetData>();

    public void Init()
    {
        Ship = GetNode<MeshInstance3D>("Ship");
        Hover = GetNode<Node3D>("Hover");
        Selected = GetNode<Node3D>("Selected");
        Collision = GetNode<Area3D>("Area3D");
        CollisionShape = GetNode<CollisionShape3D>("Area3D/CollisionShape3D");

        Collision.InputEvent += SignalInputEvent;
        Collision.MouseEntered += OnHover;
        Collision.MouseExited += OnDehover;

        Selected.Visible = false;
        Hover.Visible = false;

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
                    Game.self.Input.OnSelectFleets(new Array<FleetData>());
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
        Game.self.Input.OnHoverFleets(new Array<FleetData>());
    }

    public void OnDehover()
    {
        Game.self.Input.OnDehoverFleets();
    }
    public void GFXHover()
    {
        Hover.Visible = true;
    }
    public void GFXDehover()
    {
        Hover.Visible = false;
    }
}