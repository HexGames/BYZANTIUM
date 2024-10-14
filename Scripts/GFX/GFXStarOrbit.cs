using Godot;
using Godot.Collections;

[Tool]
public partial class GFXStarOrbit : Node3D
{
    // planets and moons
    public MeshInstance3D Planet = null;
    private MeshInstance3D OrbitLine = null;
    private Node3D Offset = null;
    private Area3D Collision = null;
    private CollisionShape3D CollisionShape = null;
    // only for planets
    private MeshInstance3D AsteroidField = null;
    private MeshInstance3D Rings = null;
    private GFXStarOrbit Moon_1 = null;
    private GFXStarOrbit Moon_2 = null;

    private bool Moon = false;

    [ExportCategory("Runtime")]
    [Export]
    public PlanetData _Planet = null;
    [Export]
    public UI3DPlanet GUI3D = null;
    [Export]
    public bool NeedsGUI3D = false;

    public void Init()
    {
        OrbitLine = GetNode<MeshInstance3D>("OrbitLine");
        Offset = GetNode<Node3D>("Offset");
        Collision = GetNode<Area3D>("Offset/Area3D");
        CollisionShape = GetNode<CollisionShape3D>("Offset/Area3D/CollisionShape3D");
        Planet = GetNode<MeshInstance3D>("Offset/Planet");
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

        CollisionShape.Disabled = true;

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

    public void RefreshPlanet(PlanetData planet, int size, bool rings, string type)
    {
        _Planet = planet;
        _Planet.GFX = this;

        Planet.Scale = (0.4f + 0.2f * size) * Vector3.One;
        if (Rings != null) Rings.Visible = rings;
        Visible = true;

        Material mat = DefLibrary.self.AssetLib.GetMaterial_Planet("Planet" + type + ".tres");

        if (mat != null)
        {
            Planet.SetSurfaceOverrideMaterial(0, mat);
            //Planet.MaterialOverlay = mat;
        }
        else
        {
            Planet.SetSurfaceOverrideMaterial(0, DefLibrary.self.AssetLib.GetMaterial_Planet("PlanetBarren.tres"));
            //Planet.MaterialOverlay = DefLibrary.self.AssetLib.GetMaterialPlanet("PlanetBarren.tres");
        }
    }

    public void RefreshAsteroids(PlanetData planet)
    {
        _Planet = planet;
        _Planet.GFX = this;

        Planet.Visible = false;
        AsteroidField.Visible = true;
        Visible = true;
    }

    public void RefreshMoon1(PlanetData planet,int size, string type)
    {
        Moon_1.RefreshPlanet(planet, size, false, type);
    }

    public void RefreshMoon2(PlanetData planet, int size, string type)
    {
        Moon_2.RefreshPlanet(planet, size, false, type);
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
                    Game.self.Input.OnSelectPlanet(_Planet);
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
        //Hover.Visible = true;
        Game.self.Input.OnHoverPlanet(_Planet);
    }
    public void OnDehover()
    {
        Game.self.Input.OnDehoverPlanet(_Planet);
        //Hover.Visible = false;
    }

    public void LODClose()
    {
        CollisionShape.Disabled = false;

        if (Moon_1.Planet != null) Moon_1.CollisionShape.Disabled = false;
        if (Moon_2.Planet != null) Moon_2.CollisionShape.Disabled = false;
    }

    public void LODFar()
    {
        CollisionShape.Disabled = true;

        if (Moon_1.Planet != null) Moon_1.CollisionShape.Disabled = true;
        if (Moon_2.Planet != null) Moon_2.CollisionShape.Disabled = true;
    }

    public void ShowGUI3D()
    {
        NeedsGUI3D = true;

        if (Moon_1.Planet != null) Moon_1.NeedsGUI3D = true;
        if (Moon_2.Planet != null) Moon_2.NeedsGUI3D = true;
    }
    public void HideGUI3D()
    {
        NeedsGUI3D = false;

        if (Moon_1.Planet != null) Moon_1.NeedsGUI3D = false;
        if (Moon_2.Planet != null) Moon_2.NeedsGUI3D = false;
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
            return;

        if (NeedsGUI3D && Game.self.Camera.LOD < 2)
        {
            // update GUI instance from pool
            Vector2 pos2D = Game.self.Camera.UnprojectPosition(Planet.GlobalPosition /*+ new Vector3(0.0f, 0.0f, 1.0f)*/);

            bool inFOV = pos2D.X > -100 && pos2D.X < GetViewport().GetVisibleRect().Size.X + 100
            && pos2D.Y > -100 && pos2D.Y < GetViewport().GetVisibleRect().Size.Y + 100;

            if (inFOV)
            {
                if (_Planet != null)
                {
                    if (GUI3D == null)
                    {
                        GUI3D = Game.self.GalaxyUI.UI3DManager.Get_UI3DPlanet();
                        GUI3D.GFX = this;
                        GUI3D.Refresh();
                        GUI3D.Visible = true;
                    }

                    GUI3D.Position = pos2D;
                }
            }
            else
            {
                if (GUI3D != null)
                {
                    GUI3D.Visible = false;
                    GUI3D.GFX = null;
                    GUI3D = null;
                }
            }
        }
        else
        {
            if (GUI3D != null)
            {
                GUI3D.Visible = false;
                GUI3D.GFX = null;
                GUI3D = null;
            }
        }
    }
}