using Godot;
using Godot.Collections;
using System;

[Tool]
public partial class GFXStar : Node3D
{
    [ExportCategory("Links")]
    public Array<GFXStarOrbit> Orbits = new Array<GFXStarOrbit>();
    public GFXStarShip ShipFriendly = null;
    public GFXStarShip ShipOther = null;

    private Area3D Collision = null;
    private CollisionShape3D CollisionShape = null;
    private MeshInstance3D Border = null;
    private MeshInstance3D PlayerColor = null;
    //private Node3D Hover = null;
    //private Node3D Selected = null;
    private MeshInstance3D Layer_1 = null;
    private MeshInstance3D Layer_2 = null;
    private MeshInstance3D Layer_3 = null;

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public UI3DStar GUI3D = null;

    private Array<FleetData> FriendlyFleets = new Array<FleetData>();
    private Array<FleetData> OtherFleets = new Array<FleetData>();

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
        ShipOther = GetNode<GFXStarShip>("Ship_Other");

        ShipFriendly.Init(true);
        ShipOther.Init(false);

        Collision = GetNode<Area3D>("Star/SectorArea3D");
        CollisionShape = GetNode<CollisionShape3D>("Star/SectorArea3D/CollisionShape3D");
        Border = GetNode<MeshInstance3D>("Border");
        PlayerColor = GetNode<MeshInstance3D>("PlayerColor");
        //Hover = GetNode<Node3D>("Hover");
        //Selected = GetNode<Node3D>("Selected");
        Layer_1 = GetNode<MeshInstance3D>("Layer_1_Cloud");
        Layer_2 = GetNode<MeshInstance3D>("Layer_2_Cloud");
        Layer_3 = GetNode<MeshInstance3D>("Layer_3_Cloud");

        Collision.InputEvent += SignalInputEvent;
        Collision.MouseEntered += OnHover;
        Collision.MouseExited += OnDehover;

        //Selected.Visible = false;
        //Hover.Visible = false;
        PlayerColor.Visible = false;

        //Node3D planets = GetNode<Node3D>("System/Planets");
        //MeshInstance3D close = GetNode<MeshInstance3D>("../../../Camera3D/Close");
        //planets.VisibilityParent = close.GetPath();
    }

    public void Refresh(StarData star, int angleSeed)
    {
        _Star = star;

        int angle = 180 + ((angleSeed * 13) % 18) * 10;
        int angleNoise = ((angleSeed * 3) % 18) * 4;
        int idxOffset = 1;
        for (int idx = 0; idx < Orbits.Count + 1; idx++)
        {
            if (idx + idxOffset < _Star.Planets.Count)
            {
                if (_Star.Planets[idx + idxOffset].Data.HasSub("Moon"))
                {
                    int size = _Star.Planets[idx + idxOffset].Data.GetSub("Size").ValueI;
                    string type = _Star.Planets[idx + idxOffset].Data.GetSub("Type").ValueS;
                    if (_Star.Planets[idx + idxOffset - 1].Data.HasSub("Rings") || _Star.Planets[idx + idxOffset - 1].Data.HasSub("Moon"))
                    {
                        Orbits[idx - 1].RefreshMoon2(_Star.Planets[idx + idxOffset], size, type);
                    }
                    else
                    {
                        Orbits[idx - 1].RefreshMoon1(_Star.Planets[idx + idxOffset], size, type);
                    }
                    idxOffset++;
                    idx--;
                    continue;
                }
                else if (idx < Orbits.Count)
                {
                    //Orbits[idx].RefreshLocation(idx, angle + angleNoise);
                    Orbits[idx].RefreshAngle(angle + angleNoise);
                    string type = _Star.Planets[idx + idxOffset].Data.GetSub("Type").ValueS;
                    if (type == "Asteroids")
                    {
                        Orbits[idx].RefreshAsteroids(_Star.Planets[idx + idxOffset]);
                    }
                    else
                    {
                        int size = _Star.Planets[idx + idxOffset].Data.GetSub("Size").ValueI;
                        bool rings = _Star.Planets[idx + idxOffset].Data.HasSub("Rings");
                        if (_Star.Planets[idx + idxOffset].Data.HasSub("Custom"))
                            type = _Star.Planets[idx + idxOffset].PlanetName;
                        Orbits[idx].RefreshPlanet(_Star.Planets[idx + idxOffset], size, rings, type);
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

        FriendlyFleets.Clear();
        OtherFleets.Clear();

        //for

        //ShipFriendly.RefreshShip(_Star.FleetsFriendly_PerTurn);
        //ShipEnemy.RefreshShip(_Star.FleetsOthers_PerTurn);

        Layer_1.RotationDegrees = new Vector3(0, (angleSeed * 7) % 18 * 20, 0);
        Layer_2.RotationDegrees = new Vector3(0, (angleSeed * 9) % 24 * 15, 0);
        Layer_3.RotationDegrees = new Vector3(0, (angleSeed * 11) % 36 * 10, 0);
    }

    public void RefreshShips()
    {
        FriendlyFleets.Clear();
        OtherFleets.Clear();

        FriendlyFleets.AddRange(_Star.GetFriendlyFleets(Game.self.HumanPlayer));
        OtherFleets.AddRange(_Star.GetEnemyFleets(Game.self.HumanPlayer));

        if (OtherFleets.Count == 0) OtherFleets.AddRange(_Star.GetNeutralFleets(Game.self.HumanPlayer));
        else FriendlyFleets.AddRange(_Star.GetNeutralFleets(Game.self.HumanPlayer));

        ShipFriendly.RefreshShip(FriendlyFleets);
        ShipOther.RefreshShip(OtherFleets);
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
                    Game.self.Input.OnSelectStar(_Star);
                }
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    if (Game.self.Input.TryFleetsMoveToStar(_Star) == false)
                    {
                        Game.self.Input.DeselectOneStep();
                    }
                }
            }
        }
    }

    public void RefreshPlayerColor()
    {
        float alpha = 0.35f;
        if (Selected) alpha = 1.0f;
        else if (Hover) alpha = 0.7f;

        if (_Star.System != null)
        {
            StandardMaterial3D newMaterial = PlayerColor.GetSurfaceOverrideMaterial(0).Duplicate() as StandardMaterial3D;
            Color color = Game.self.UILib.GetPlayerColor(_Star.System._Player.PlayerID);
            newMaterial.AlbedoColor = new Color(color.R, color.G, color.B, alpha);
            PlayerColor.SetSurfaceOverrideMaterial(0, newMaterial);
            PlayerColor.Visible = true;
        }
    }

    public void OnHover()
    {
        Game.self.Input.OnHoverStar(_Star);
    }
    public void OnDehover()
    {
        Game.self.Input.OnDehoverStar(_Star);
    }

    bool Hover = false;
    public void GFXHover()
    {
        Border.SetSurfaceOverrideMaterial(0, Game.self.Assets.StarHover);
        Hover = true;

        RefreshPlayerColor();
    }
    public void GFXDehover()
    {
        if (Selected == false)
        {
            Border.SetSurfaceOverrideMaterial(0, Game.self.Assets.StarNormal);
        }
        Hover = false;

        RefreshPlayerColor();
    }

    bool Selected = false;
    public void GFXSelect()
    {
        Border.SetSurfaceOverrideMaterial(0, Game.self.Assets.StarSelected);
        Selected = true;

        RefreshPlayerColor();
    }
    public void GFXDeselect()
    {
        if (Hover == false) Border.SetSurfaceOverrideMaterial(0, Game.self.Assets.StarNormal);
        Selected = false;

        RefreshPlayerColor();
    }

    public void ShowPlanets3DGUI()
    {
        for (int idx = 0; idx < Orbits.Count; idx++)
        {
            Orbits[idx].ShowGUI3D();
        }
    }

    public void HidePlanets3DGUI()
    {
        for (int idx = 0; idx < Orbits.Count; idx++)
        {
            Orbits[idx].HideGUI3D();
        }
    }

    public void LODClose()
    {
        //CollisionShape.Disabled = false;
        for (int idx = 0; idx < Orbits.Count; idx++)
        {
            if (Orbits[idx]._Planet != null)
            {
                Orbits[idx].LODClose();
            }
        }
    }

    public void LODFar()
    {
        //CollisionShape.Disabled = false;
        for (int idx = 0; idx < Orbits.Count; idx++)
        {
            if (Orbits[idx]._Planet != null)
            {
                Orbits[idx].LODFar();
            }
        }
    }

    public void LODAlpha(float alpha)
    {
        if (alpha > 0.05 && alpha < 0.95)
            CollisionShape.Disabled = false;

        //ShipFriendly.Position = new Vector3(1.5f + 4.5f * alpha, 0, -0.75f - 2.25f * alpha);
        //ShipOther.Position = new Vector3(1.5f + 4.5f * alpha, 0, 0.75f + 2.25f * alpha);
        //
        //ShipFriendly.Scale = (0.3f + 0.7f * alpha) * Vector3.One;
        //ShipOther.Scale = (0.3f + 0.7f * alpha) * Vector3.One;
        //
        //if (alpha > 0.05 && alpha < 0.95)
        //{
        //    ShipFriendly.CollisionShape.Disabled = true;
        //    ShipOther.CollisionShape.Disabled = true;
        //}
        //else
        //{
        //    ShipFriendly.CollisionShape.Disabled = false;
        //    ShipOther.CollisionShape.Disabled = false;
        //}
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
            return;

        if (Game.self.Camera.LOD > 0)
        {
            // update GUI instance from pool
            Vector2 pos2D = Game.self.Camera.UnprojectPosition(GlobalPosition + new Vector3(0.0f, 0.0f, 5.0f));

            bool inFOV = pos2D.X > -100 && pos2D.X < GetViewport().GetVisibleRect().Size.X + 100
            && pos2D.Y > -100 && pos2D.Y < GetViewport().GetVisibleRect().Size.Y + 100;

            if (inFOV)
            {
                if (GUI3D == null)
                {
                    GUI3D = Game.self.GalaxyUI.UI3DManager.Get_UI3DStar();
                    GUI3D.GFX = this;
                    GUI3D.Refresh();
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