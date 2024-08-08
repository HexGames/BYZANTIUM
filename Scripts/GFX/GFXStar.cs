using Godot;
using Godot.Collections;
using System;

[Tool]
public partial class GFXStar : Node3D
{
    [ExportCategory("Links")]
    private Array<GFXStarOrbit> Orbits = new Array<GFXStarOrbit>();
    private GFXStarShip ShipFriendly = null;
    private GFXStarShip ShipEnemy = null;

    private Area3D Collision = null;
    private CollisionShape3D CollisionShape = null;
    private MeshInstance3D PlayerColor = null;
    private Node3D Hover = null;
    private Node3D Selected = null;
    private MeshInstance3D Layer_1 = null;
    private MeshInstance3D Layer_2 = null;
    private MeshInstance3D Layer_3 = null;

    [ExportCategory("Runtime")]
    [Export]
    public UI3DStar GUI3D = null;

    public void Init()
    {
        Orbits.Clear();
        Orbits.Add(GetNode<GFXStarOrbit>("System/Planets/Orbit_1"));
        Orbits.Add(GetNode<GFXStarOrbit>("System/Planets/Orbit_2"));
        Orbits.Add(GetNode<GFXStarOrbit>("System/Planets/Orbit_3"));
        Orbits.Add(GetNode<GFXStarOrbit>("System/Planets/Orbit_4"));
        Orbits.Add(GetNode<GFXStarOrbit>("System/Planets/Orbit_5"));
        Orbits.Add(GetNode<GFXStarOrbit>("System/Planets/Orbit_6"));
        Orbits.Add(GetNode<GFXStarOrbit>("System/Planets/Orbit_7"));

        for (int idx = 0; idx < Orbits.Count; idx++)
        {
            Orbits[idx].Init();
        }

        ShipFriendly = GetNode<GFXStarShip>("Ship_Friendly");
        ShipEnemy = GetNode<GFXStarShip>("Ship_Other");

        ShipFriendly.Init();
        ShipEnemy.Init();

        Collision = GetNode<Area3D>("Star/SectorArea3D");
        CollisionShape = GetNode<CollisionShape3D>("Star/SectorArea3D/CollisionShape3D");
        PlayerColor = GetNode<MeshInstance3D>("PlayerColor");
        Hover = GetNode<Node3D>("Hover");
        Selected = GetNode<Node3D>("Selected");
        Layer_1 = GetNode<MeshInstance3D>("Layer_1_Cloud");
        Layer_2 = GetNode<MeshInstance3D>("Layer_2_Cloud");
        Layer_3 = GetNode<MeshInstance3D>("Layer_3_Cloud");

        Collision.InputEvent += SignalInputEvent;
        Collision.MouseEntered += OnHover;
        Collision.MouseExited += OnDehover;

        Selected.Visible = false;
        Hover.Visible = false;
        PlayerColor.Visible = false;

        //Node3D planets = GetNode<Node3D>("System/Planets");
        //MeshInstance3D close = GetNode<MeshInstance3D>("../../../Camera3D/Close");
        //planets.VisibilityParent = close.GetPath();
    }

    public void Refresh(StarData system, int angleSeed)
    {
        int angle = 180 + ((angleSeed * 13) % 18) * 10;
        int angleNoise = ((angleSeed * 3) % 18) * 4;
        int idxOffset = 1;
        for (int idx = 0; idx < Orbits.Count + 1; idx++)
        {
            if (idx + idxOffset < system.Planets.Count)
            {
                if (system.Planets[idx + idxOffset].Data.HasSub("Moon"))
                {
                    int size = system.Planets[idx + idxOffset].Data.GetSub("Size").ValueI;
                    string type = system.Planets[idx + idxOffset].Data.GetSub("Type").ValueS;
                    if (system.Planets[idx + idxOffset - 1].Data.HasSub("Rings") || system.Planets[idx + idxOffset - 1].Data.HasSub("Moon"))
                    {
                        Orbits[idx - 1].RefreshMoon2(size, type);
                    }
                    else
                    {
                        Orbits[idx - 1].RefreshMoon1(size, type);
                    }
                    idxOffset++;
                    idx--;
                    continue;
                }
                else if (idx < Orbits.Count)
                {
                    //Orbits[idx].RefreshLocation(idx, angle + angleNoise);
                    Orbits[idx].RefreshAngle(angle + angleNoise);
                    string type = system.Planets[idx + idxOffset].Data.GetSub("Type").ValueS;
                    if (type == "Asteroids")
                    {
                        Orbits[idx].RefreshAsteroids();
                    }
                    else
                    {
                        int size = system.Planets[idx + idxOffset].Data.GetSub("Size").ValueI;
                        bool rings = system.Planets[idx + idxOffset].Data.HasSub("Rings");
                        Orbits[idx].RefreshPlanet(size, rings, type);
                    }

                    angle = (angle + 140) % 360;
                    angleNoise = ((angleSeed * (4 + idx)) % 18) * 5;
                }
            }
            else if (idx < Orbits.Count)
            {
                Orbits[idx].Visible = false;
            }
        }

        Layer_1.RotationDegrees = new Vector3(0, (angleSeed * 7) % 18 * 20, 0); 
        Layer_2.RotationDegrees = new Vector3(0, (angleSeed * 9) % 24 * 15, 0);
        Layer_3.RotationDegrees = new Vector3(0, (angleSeed * 11) % 36 * 10, 0);
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

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
            return;

        // update GUI instance from pool
        Vector2 pos2D = Game.self.Camera.UnprojectPosition(Position + new Vector3(0.0f, 0.0f, 1.0f));

        bool inFOV = pos2D.X > -100 && pos2D.X < GetViewport().GetVisibleRect().Size.X + 100
            && pos2D.Y > -100 && pos2D.Y < GetViewport().GetVisibleRect().Size.Y + 100;

        if (inFOV)
        {
            if (GUI3D == null)
            {
                GUI3D = Game.self.GalaxyUI.UI3DManager.Get_UI3DStar();
                GUI3D.GFX = this;
                GUI3D.Visible = true;
            }

            GUI3D.Position = pos2D;
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

        bool newLODClose = Game.self.Camera.Position.Y < 40.0f;
        //if 
    }

    public void LODClose()
    {
        CollisionShape.Disabled = true;
        for (int idx = 0; idx < Orbits.Count; idx++)
        {
            Orbits[idx].LODClose();
        }
    }

    public void LODFar()
    {
        CollisionShape.Disabled = false;
        for (int idx = 0; idx < Orbits.Count; idx++)
        {
            Orbits[idx].LODFar();
        }
    }

    public void LODAlpha(float alpha)
    {
        if (alpha > 0.05)
            CollisionShape.Disabled = false;

        ShipFriendly.Position = new Vector3(1.5f + 4.5f * alpha, 0, 0.75f + 2.25f * alpha);
        ShipEnemy.Position = new Vector3(1.5f + 4.5f * alpha, 0, -0.75f - 2.25f * alpha);

        ShipFriendly.Scale = (0.3f + 0.7f * alpha) * Vector3.One;
        ShipEnemy.Scale = (0.3f + 0.7f * alpha) * Vector3.One;

        if (alpha > 0.05 && alpha < 0.95)
        {
            ShipFriendly.CollisionShape.Disabled = true;
            ShipEnemy.CollisionShape.Disabled = true;
        }
        else
        {
            ShipFriendly.CollisionShape.Disabled = false;
            ShipEnemy.CollisionShape.Disabled = false;
        }
    }
}