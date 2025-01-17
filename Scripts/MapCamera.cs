using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class MapCamera : Camera3D
{
    [Export]
    private Camera3D Camera;
    [Export]
    private Node3D TransformNode;

    // calculated
    [ExportCategory("Runtime")]
    [Export]
    public int LOD = 0; 
    [Export]
    public Vector3 TargetPosition; //camera position
    private float TargetHeight;
    private float CurrentRotationX;

    private float RotationAngle = 0.0f;
    private float CurrentRotationY = 0.0f;
    private float ZoomPos = 0.2f; //value in range (0, 1) used as t in Matf.Lerp

    private float CurrentMoveSpeed;
    private float CurrentHeightDampening;
    private float CurrentRotationDampening;

    //[Export]
    public float MoveLimitX = 300.0f; //x limit of map
    //[Export]
    public float MoveLimitY = 300.0f; //z limit of map

    private float MoveSpeed = 100f; //speed with keyboard movement
    private float MoveSpeedDampening = 7.5f;

    private float ZoomMaxHeight = 160f; //maximal height
    private float ZoomMinHeight = 7f; //minimnal height
    private float ZoomHeightDampening = 15.0f;
    private float ZoomRotationDampening = 7.5f;
    private float ZoomKeyboardSensitivity = -15f;
    private float ZoomScrollWheelZSensitivity = -180f;

    private float RotationXDefault = 55;

    private int Locked = 0;
    private bool ZoomAndScroll = true;
    private bool Panning = false;

    //public List<Vector3> PointsOfIntrest = new List<Vector3>();
    //public Vector3 ChosenPointOfIntrest = Vector3.Zero;


    [Export]
    public Vector3 MouseXOZ = Vector3.Zero; //camera position
    [Export]
    public Vector3 CenterCameraXOZ = Vector3.Zero; //camera position

    [Export]
    public float ScreenEdgeBorder = 25f;
    [Export]
    public bool UILock = false;

    // Godot specific
    public override void _Ready()
    {
        if (Engine.IsEditorHint())
            return;

        TargetPosition = new Vector3(TransformNode.Position.X, 0f, TransformNode.Position.Z);
        TargetHeight = ZoomMaxHeight / 2;
        CurrentRotationX = RotationXDefault;

        LOD = 0;
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
            return;

        CameraUpdate((float) delta);

        ProcessLOD((float)delta);

        Vector3 mouseProjectedPosition = ProjectPosition(GetViewport().GetMousePosition(), 1.0f);
        MouseXOZ = (Vector3)XOZPlane.IntersectsRay(Position, mouseProjectedPosition - Position);

        Vector3 centerProjectedPosition = ProjectPosition(GetViewport().GetVisibleRect().Size / 2, 1.0f);
        CenterCameraXOZ = (Vector3)XOZPlane.IntersectsRay(Position, centerProjectedPosition - Position);
    }

    public void Init(MapData map)
    {
        float maxX = 0;
        float maxY = 0;
        for (int starIdx = 0; starIdx < map.Stars.Count; starIdx++ )
        {
            Vector3 pos = map.Stars[starIdx]._Node.GFX.GlobalPosition + Vector3.Left * 1.0f;
            //PointsOfIntrest.Add(pos);
            maxX = Mathf.Max(maxX, Mathf.Abs(pos.X));
            maxY = Mathf.Max(maxY, Mathf.Abs(pos.Z));
        }

        MoveLimitX = maxX + 145.0f;
        MoveLimitY = maxY + 160.0f;
    }

    public Vector3 Input_Panning = Vector3.Zero;
    public float Input_Scroll = 0.0f;
    private Vector2 LastMousePosition = Vector2.Zero;
    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (Engine.IsEditorHint())
            return;

        bool justClicked = false;
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            // Zoom Scrool Wheel
            if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp) Input_Scroll += 0.1f * mouseButtonEvent.Factor;
            if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown) Input_Scroll -= 0.1f * mouseButtonEvent.Factor;

            if (mouseButtonEvent.ButtonIndex == MouseButton.Left && mouseButtonEvent.IsPressed())
            {
                if (Panning == false)
                {
                    justClicked = true;
                }
                Panning = true;
            }
            else
            {
                Panning = false;
            }
        }

        if (Panning)
        {
            if (justClicked)
            {
                LastMousePosition = GetViewport().GetMousePosition();
            }
            else
            {
                Vector3 mouseProjectedPosition_1 = ProjectPosition(LastMousePosition, 1.0f);
                Vector3 intersect_1 = (Vector3)XOZPlane.IntersectsRay(Position, mouseProjectedPosition_1 - Position);

                Vector2 mousePos = GetViewport().GetMousePosition();
                Vector3 mouseProjectedPosition_2 = ProjectPosition(mousePos, 1.0f);
                Vector3 intersect_2 = (Vector3)XOZPlane.IntersectsRay(Position, mouseProjectedPosition_2 - Position);

                Input_Panning += intersect_1 - intersect_2;
                Input_Panning.Y = 0.0f;

                LastMousePosition = mousePos;
            }
        }
    }

    public bool IsLockedByUI()
    {
        return UILock;
    }

    // --------------------------------------------------------------------------------------------
    // Camera methods
    private void CameraUpdate(float deltaTime)
    {
        //ChoosePointOfIntrest();

        Move(deltaTime);

        HeightCalculation(deltaTime);

        RotationCalculation(deltaTime);

        LimitPosition(deltaTime);

        TransformNode.Rotation = new Vector3(-Mathf.DegToRad(CurrentRotationX), Mathf.DegToRad(CurrentRotationY), 0.0f);
        TransformNode.Position = TargetPosition + Vector3.Right + Vector3.Up * TargetHeight;
    }

    // --------------------------------------------------------------------------------------------
    //private void ChoosePointOfIntrest()
    //{
    //    Vector3 mouseProjectedPosition = ProjectPosition(GetViewport().GetMousePosition(), 1.0f);
    //    Vector3 intersect = (Vector3)XOZPlane.IntersectsRay(Position, mouseProjectedPosition - Position);
    //
    //    float distMin = float.MaxValue;
    //    for (int idx = 0; idx < PointsOfIntrest.Count; idx++)
    //    {
    //        float dist = PointsOfIntrest[idx].DistanceSquaredTo(intersect);
    //
    //        if (dist < distMin)
    //        {
    //            ChosenPointOfIntrest = PointsOfIntrest[idx];
    //            distMin = dist;
    //        }
    //    }
    //}

    // --------------------------------------------------------------------------------------------
    private void Move(float deltaTime)
    {
        if (Locked == 0 && !IsLockedByUI() && ZoomAndScroll)
        {
            Vector3 keyboardInput = Vector3.Zero;
            // Keyboard Input
            if (Input.IsActionPressed("Camera_Up")) keyboardInput += Vector3.Forward;
            if (Input.IsActionPressed("Camera_Down")) keyboardInput += Vector3.Back;
            if (Input.IsActionPressed("Camera_Right")) keyboardInput += Vector3.Right;
            if (Input.IsActionPressed("Camera_Left")) keyboardInput += Vector3.Left;

            // mouse Input
            Vector3 borderInput = Vector3.Zero;
            if (Input_Panning == Vector3.Zero)
            {
                Vector2 mousePos = GetViewport().GetMousePosition();
                if (mousePos.Y < ScreenEdgeBorder) borderInput += Vector3.Forward;
                if (mousePos.Y > GetViewport().GetVisibleRect().Size.Y - ScreenEdgeBorder) borderInput += Vector3.Back;
                if (mousePos.X > GetViewport().GetVisibleRect().Size.X - ScreenEdgeBorder) borderInput += Vector3.Right;
                if (mousePos.X < ScreenEdgeBorder) borderInput += Vector3.Left;
            }

            Vector3 movement = keyboardInput + borderInput;
            movement = movement.Normalized();

            movement *= (0.5f + ZoomPos) * MoveSpeed * deltaTime * (0.23f + 0.75f * ZoomPos);
            movement = Quaternion.FromEuler(new Vector3(0f, TransformNode.Rotation.Y, 0f)) * movement;

            movement += Input_Panning;

            TargetPosition += movement;
        }

        Input_Panning = Vector3.Zero;
    }


    // --------------------------------------------------------------------------------------------
    Plane XOZPlane = new Plane(Vector3.Up);
    private void HeightCalculation(float deltaTime)
    {
        if (Locked == 0 && !IsLockedByUI() && ZoomAndScroll) ZoomPos += Input_Scroll * deltaTime * ZoomScrollWheelZSensitivity * (0.2f + 0.8f * ZoomPos); 
        
        ZoomPos = Mathf.Clamp(ZoomPos, 0.0f, 1.0f);

        float targetHeight = Mathf.Lerp(ZoomMinHeight, ZoomMaxHeight, ZoomPos);

        float difference = 0;
        if (TargetHeight != targetHeight) difference = targetHeight - TargetHeight;

        CurrentHeightDampening = Mathf.Max(0.03f, deltaTime * ZoomHeightDampening);
        float lerpedDifference = Mathf.Lerp(0, difference, CurrentHeightDampening);

        TargetHeight = Mathf.Clamp(TargetHeight + lerpedDifference, ZoomMinHeight, ZoomMaxHeight);

        if (lerpedDifference > 0)
        {
            Plane plane = new Plane(Vector3.Up, TargetHeight);
            Vector3? intersect = plane.IntersectsRay(Position, Position - ProjectPosition(GetViewport().GetMousePosition(), 1.0f));
            if (intersect != null)
            {
                Vector3 moveVector = intersect.Value - Position;
                moveVector.Y = 0;
                TargetPosition += moveVector;
            }
        }
        else
        {
            Plane plane = new Plane(Vector3.Up, TargetHeight);
            Vector3? intersect = plane.IntersectsRay(Position, ProjectPosition(GetViewport().GetMousePosition(), 1.0f) - Position);
            if (intersect != null)
            {
                Vector3 moveVector = intersect.Value - Position;
                moveVector.Y = 0;
                TargetPosition += moveVector;
            }
        }

        Input_Scroll = 0.0f;
    }

    // --------------------------------------------------------------------------------------------
    private void RotationCalculation(float deltaTime)
    {
        float targetRotationX = RotationXDefault;
        //if (Locked == 0) targetRotationX = LockedNew ? LockedRotationX_new : LockedRotationX;

        CurrentRotationDampening = Mathf.Max(0.05f, deltaTime * ZoomHeightDampening);

        float difference = 0;
        if (CurrentRotationX != targetRotationX) difference = targetRotationX - CurrentRotationX;
        CurrentRotationX = Mathf.LerpAngle(CurrentRotationX, CurrentRotationX + difference, CurrentRotationDampening);

        difference = RotationAngle - CurrentRotationY;
        CurrentRotationY = Mathf.LerpAngle(CurrentRotationY, CurrentRotationY + difference, CurrentRotationDampening);
    }

    // --------------------------------------------------------------------------------------------
    private Vector3 lockedToPoint = Vector3.Zero;
    private void LimitPosition(float deltaTime)
    {
        TargetPosition = new Vector3(Mathf.Clamp(TargetPosition.X, -0.5f * MoveLimitX, 0.5f * MoveLimitX),
            TargetPosition.Y,
            Mathf.Clamp(TargetPosition.Z, -0.5f * MoveLimitY + 0.5f * TargetHeight, 0.5f * MoveLimitY + 0.5f * TargetHeight));
    }

    // --------------------------------------------------------------------------------------------
    public void Lock()
    {
        Locked++;
    }

    public void Unlock()
    {
        Locked--;
    }

    public void SetZoomAndScroll(bool b)
    {
        ZoomAndScroll = b;
    }

    public bool ZoomedOut
    {
        get { return ZoomPos > 0.5f; }
    }
}