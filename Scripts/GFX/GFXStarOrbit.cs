using Godot;
using Godot.Collections;

[Tool]
public partial class GFXStarOrbit : Node3D
{
    // planets and moons
    private MeshInstance3D OrbitLine = null;
    private Node3D Offset = null;
    private Area3D Collision = null;
    private CollisionShape3D CollisionShape = null;
    private MeshInstance3D Planet = null;
    private Node3D Hover = null;
    private Node3D Selected = null;
    // only for planets
    private MeshInstance3D AsteroidField = null;
    private MeshInstance3D Rings = null;
    private GFXStarOrbit Moon_1 = null;
    private GFXStarOrbit Moon_2 = null;

    private bool Moon = false;

    public void Init()
    {
        OrbitLine = GetNode<MeshInstance3D>("OrbitLine");
        Offset = GetNode<Node3D>("Offset");
        Collision = GetNode<Area3D>("Offset/Area3D");
        CollisionShape = GetNode<CollisionShape3D>("Offset/Area3D/CollisionShape3D");
        Planet = GetNode<MeshInstance3D>("Offset/Planet");
        Selected = GetNode<Node3D>("Offset/Selected");
        Hover = GetNode<Node3D>("Offset/Hover");
        if (HasNode("Offset/Moon_1"))
        {
            AsteroidField = GetNode<MeshInstance3D>("Offset/AsteroidField");
            Rings = GetNode<MeshInstance3D>("Offset/Rings");
            Moon_1 = GetNode<GFXStarOrbit>("Offset/Moon_1");
            Moon_1.Init();
            Moon_2 = GetNode<GFXStarOrbit>("Offset/Moon_2");
            Moon_2.Init();
        }
        else
        {
            Moon = true;
        }

        Collision.InputEvent += SignalInputEvent;
        Collision.MouseEntered += OnHover;
        Collision.MouseExited += OnDehover;

        Selected.Visible = false;
        Hover.Visible = false;

        Visible = false;
    }
    /*public void RefreshLocation(int orbit, int angle)
    {
        RotationDegrees = new Vector3(0.0f, angle, 0.0f);

        TorusMesh tm = OrbitLine.Mesh as TorusMesh;
        if (tm != null)
        {
            if (Moon)
            {
                tm.InnerRadius = 0.5f + 0.25f * orbit;
                tm.OuterRadius = 0.51f + 0.25f * orbit;
            }
            else
            {
                tm.InnerRadius = 2.5f + 1.0f * orbit;
                tm.OuterRadius = 2.51f + 1.0f * orbit;
            }
            OrbitLine.Mesh = tm;
        }

        Offset.Position = new Vector3(0.0f, 0.0f, 2.5f + 1.0f * orbit);
    }*/
    public void RefreshAngle( int angle)
    {
        RotationDegrees = new Vector3(0.0f, angle, 0.0f);
    }

    public void RefreshPlanet(int size, bool rings, string type)
    {
        Planet.Scale = (0.4f + 0.2f * size) * Vector3.One;
        Hover.Scale = (0.12f + 0.03f * size) * Vector3.One;
        Selected.Scale = (0.12f + 0.03f * size) * Vector3.One;
        if (Rings != null) Rings.Visible = rings;
        Visible = true;

        Material mat = DefLibrary.self.AssetLib.GetMaterialPlanet("Planet" + type + ".tres");

        if (mat != null)
        {
            Planet.SetSurfaceOverrideMaterial(0, mat);
            //Planet.MaterialOverlay = mat;
        }
        else
        {
            Planet.SetSurfaceOverrideMaterial(0, DefLibrary.self.AssetLib.GetMaterialPlanet("PlanetBarren.tres"));
            //Planet.MaterialOverlay = DefLibrary.self.AssetLib.GetMaterialPlanet("PlanetBarren.tres");
        }
    }

    public void RefreshAsteroids()
    {
        Planet.Visible = false;
        Hover.Scale = 0.21f * Vector3.One;
        Selected.Scale = 0.21f * Vector3.One;
        AsteroidField.Visible = true;
        Visible = true;
    }

    public void RefreshMoon1(int size, string type)
    {
        Moon_1.RefreshPlanet(size, false, type);
    }

    public void RefreshMoon2(int size, string type)
    {
        Moon_2.RefreshPlanet(size, false, type);
    }

    public void Optimize()
    {
        if (Planet.Visible == false) Planet.Dispose();
        if (AsteroidField.Visible == false) AsteroidField.Dispose();
        if (Rings.Visible == false) Rings.Dispose();
        if (Moon_1.Visible == false) Moon_1.Dispose();
        if (Moon_2.Visible == false) Moon_2.Dispose();
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
                }
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                }
            }
        }
    }

    public void OnHover()
    {
        Hover.Visible = true;
    }

    public void OnDehover()
    {
        Hover.Visible = false;
    }

    public void LODClose()
    {
        CollisionShape.Disabled = false;
    }

    public void LODFar()
    {
        CollisionShape.Disabled = true;
    }
}