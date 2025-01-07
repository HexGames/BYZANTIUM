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
    //public Vector3 RestrictedPosition; //camera position
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
    private float ZoomMinHeight = 8f; //minimnal height
    private float ZoomHeightDampening = 15.0f;
    private float ZoomRotationDampening = 7.5f;
    private float ZoomKeyboardSensitivity = -15f;
    private float ZoomScrollWheelZSensitivity = -200f;

    private float RotationXDefault = 55;

    private bool Locked = false;
    private bool ZoomAndScroll = true;
    private bool Panning = false;

    //private Vector3 GoToTarget; //target to go to
    //private bool GoToTargetReached = true;

    //private bool LockedNew = false; //lock height

    //private float LockedHeight = 28f; //lock height
    //private float LockedRotationX = 45f; //lock rotation

    //private float LockedHeight_new = 10f; //lock height
    //private float LockedRotationX_new = 10f; //lock rotation

    //private float LockedOffsetFromInputX = 0f;

    public List<Vector3> PointsOfIntrest = new List<Vector3>();
    public Vector3 ChosenPointOfIntrest = Vector3.Zero;

    [Export]
    public float ScreenEdgeBorder = 25f;
    [Export]
    public bool UILockSystem = false;

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
    }

    public void Init(MapData map)
    {
        float maxX = 0;
        float maxY = 0;
        for (int starIdx = 0; starIdx < map.Stars.Count; starIdx++ )
        {
            Vector3 pos = map.Stars[starIdx]._Node.GFX.GlobalPosition + Vector3.Left * 1.0f;
            PointsOfIntrest.Add(pos);
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

            //else if (Input.IsActionJustPressed("Camera_Scroll_Up")) Input_Scroll += 0.1f * mouseButtonEvent.Factor; // Input.GetActionStrength("Camera_Scroll_Up");
            //if (Input.IsActionPressed("Camera_Scroll_Down")) Input_Scroll -= 0.01f * mouseButtonEvent.Factor; // Input.GetActionStrength("Camera_Scroll_Down");
            //else if (Input.IsActionJustPressed("Camera_Scroll_Down")) Input_Scroll -= 0.1f * mouseButtonEvent.Factor; // Input.GetActionStrength("Camera_Scroll_Down");
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

    public bool IsPointerOverGUI()
    {
        return UILockSystem;
    }

    // Camera methods
    private void CameraUpdate(float deltaTime)
    {
        ChoosePointOfIntrest();

        //f (GoToTargetReached == false)
        //
        //   MoveToTarget(deltaTime);
        //
        //lse
        //
            Move(deltaTime);
        //}

        HeightCalculation(deltaTime);

        RotationCalculation(deltaTime);

        LimitPosition(deltaTime);

        //MouseOffset();

        TransformNode.Rotation = new Vector3(-Mathf.DegToRad(CurrentRotationX), Mathf.DegToRad(CurrentRotationY), 0.0f);
        //Quaternion q = Quaternion.FromEuler(TransformNode.Rotation);
        //TransformNode.Position = TargetPosition + Vector3.Right * LockedOffsetFromInputX + q * Vector3.Back * TargetDistance;
        //TransformNode.Position = TargetPosition + Vector3.Right * LockedOffsetFromInputX + Vector3.Up * TargetHeight;
        TransformNode.Position = TargetPosition + Vector3.Right + Vector3.Up * TargetHeight;
        //TransformNode.Position = RestrictedPosition + Vector3.Right + Vector3.Up * TargetHeight;
    }

    private void ChoosePointOfIntrest()
    {
        Vector3 mouseProjectedPosition = ProjectPosition(GetViewport().GetMousePosition(), 1.0f);
        Vector3 intersect = (Vector3)XOZPlane.IntersectsRay(Position, mouseProjectedPosition - Position);

        float distMin = float.MaxValue;
        for (int idx = 0; idx < PointsOfIntrest.Count; idx++)
        {
            float dist = PointsOfIntrest[idx].DistanceSquaredTo(intersect);

            if (dist < distMin)
            {
                ChosenPointOfIntrest = PointsOfIntrest[idx];
                distMin = dist;
            }
        }
    }

    private void Move(float deltaTime)
    {
        if (Locked == false && !IsPointerOverGUI() && ZoomAndScroll)
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
                if (GetViewport().GetMousePosition().Y < ScreenEdgeBorder) borderInput += Vector3.Forward;
                if (GetViewport().GetMousePosition().Y > GetViewport().GetVisibleRect().Size.Y - ScreenEdgeBorder) borderInput += Vector3.Back;
                if (GetViewport().GetMousePosition().X > GetViewport().GetVisibleRect().Size.X - ScreenEdgeBorder) borderInput += Vector3.Right;
                if (GetViewport().GetMousePosition().X < ScreenEdgeBorder) borderInput += Vector3.Left;
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

    Plane XOZPlane = new Plane(Vector3.Up);
    private void HeightCalculation(float deltaTime)
    {
        if (Locked == false && !IsPointerOverGUI() && ZoomAndScroll) ZoomPos += Input_Scroll * deltaTime * ZoomScrollWheelZSensitivity * (0.25f + 0.75f * ZoomPos); 
        
        ZoomPos = Mathf.Clamp(ZoomPos, 0.0f, 1.0f);

        float targetHeight = Mathf.Lerp(ZoomMinHeight, ZoomMaxHeight, ZoomPos);
        //if (Locked) targetHeight = LockedNew ? LockedHeight_new : LockedHeight;

        float difference = 0;
        if (TargetHeight != targetHeight) difference = targetHeight - TargetHeight;

        CurrentHeightDampening = Mathf.Max(0.03f, deltaTime * ZoomHeightDampening);
        float lerpedDifference = Mathf.Lerp(0, difference, CurrentHeightDampening);

        //float oldDistance = TargetHeight;
        TargetHeight = Mathf.Clamp(TargetHeight + lerpedDifference, ZoomMinHeight, ZoomMaxHeight);

        //Game.self.GalaxyUI.DEBUGText.Text = ChosenPointOfIntrest.ToString();
        //if (Engine.GetFramesDrawn() % 100 == 0)
        //Game.self.GalaxyUI.DEBUGText.Text = difference.ToString() + "\n" + CurrentHeightDampening.ToString() + "\n" + lerpedDifference.ToString();

        //Plane plane = new Plane(Vector3.Up, TargetHeight);
        //Vector3? intersect = plane.IntersectsRay(Position, ProjectPosition(GetViewport().GetMousePosition(), 1.0f) - Position);
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
        //else
        //{
        //    Vector3 moveVector = Position - ChosenPointOfIntrest;
        //    moveVector /= 0.01f * TargetHeight;
        //    moveVector.X *= 0.01f;
        //    moveVector.Y = 0;
        //    moveVector.Z *= 0.01f;
        //    TargetPosition += moveVector * lerpedDifference;
        //}

        Input_Scroll = 0.0f;
    }

    private void RotationCalculation(float deltaTime)
    {
        float targetRotationX = RotationXDefault;
        //if (Locked) targetRotationX = LockedNew ? LockedRotationX_new : LockedRotationX;

        CurrentRotationDampening = Mathf.Max(0.05f, deltaTime * ZoomHeightDampening);

        float difference = 0;
        if (CurrentRotationX != targetRotationX) difference = targetRotationX - CurrentRotationX;
        CurrentRotationX = Mathf.LerpAngle(CurrentRotationX, CurrentRotationX + difference, CurrentRotationDampening);

        difference = RotationAngle - CurrentRotationY;
        CurrentRotationY = Mathf.LerpAngle(CurrentRotationY, CurrentRotationY + difference, CurrentRotationDampening);
    }

    //private void MoveToTarget(float deltaTime)
    //{
    //    //TargetPosition = Vector3.MoveTowards( TargetPosition, GoToTarget, Time.deltaTime * MoveSpeedFollowingSpeed );
    //    //CurrentMoveSpeed = Mathf.Clamp( Time.deltaTime * MoveSpeedDampening * ( GoToTarget - TargetPosition ).magnitude, Time.deltaTime * MoveSpeedMin, Time.deltaTime * MoveSpeedMax );
    //    CurrentMoveSpeed = Mathf.Max(0.05f, deltaTime * ZoomHeightDampening) * (GoToTarget - TargetPosition).Length();
    //    TargetPosition += GoToTarget.Normalized() * CurrentMoveSpeed;
    //    GoToTargetReached = (GoToTarget - TargetPosition).LengthSquared() < 1.0f;
    //}

    //private void MoveToHeight()
    //{
    //	targetDistance = Mathf.Lerp( targetDistance, GoToHeight, Time.deltaTime * heightDampening );
    //	zoomPos = ( GoToHeight - minHeight) / (maxHeight - minHeight); 
    //
    //	GoToHeightReached = ( GoToHeight - targetDistance ) < 1.0f;
    //
    //	float targetHeight = Mathf.Lerp(minHeight, maxHeight, zoomPos);
    //}

    private Vector3 lockedToPoint = Vector3.Zero;
    private void LimitPosition(float deltaTime)
    {
        TargetPosition = new Vector3(Mathf.Clamp(TargetPosition.X, -0.5f * MoveLimitX, 0.5f * MoveLimitX),
            TargetPosition.Y,
            Mathf.Clamp(TargetPosition.Z, -0.5f * MoveLimitY + 0.5f * TargetHeight, 0.5f * MoveLimitY + 0.5f * TargetHeight));

        //RestrictedPosition = TargetPosition;

        //
        //int chosenIdx = 0;
        //float distMin = float.MaxValue;
        //// get closest point of interest
        //Vector3 estimatedLookAt = TargetPosition + Vector3.Forward * 0.82f * TargetHeight;
        //for (int idx = 0; idx < PointsOfIntrest.Count; idx++)
        //{
        //    float dist = PointsOfIntrest[idx].DistanceSquaredTo(estimatedLookAt);
        //
        //    if (dist < distMin)
        //    {
        //        chosenIdx = idx;
        //        distMin = dist;
        //    }
        //}
        //
        ////Game.self.GalaxyUI.DEBUGText.Text = chosenIdx.ToString() + "\n" + PointsOfIntrest[chosenIdx].ToString() + "\n" + lockedToPoint.ToString();
        //if ((PointsOfIntrest[chosenIdx] - lockedToPoint).LengthSquared() > 0.1f)
        //{
        //    lockedToPoint = lockedToPoint.Slerp(PointsOfIntrest[chosenIdx], 20.0f * deltaTime);
        //}
        //else
        //{
        //    lockedToPoint = PointsOfIntrest[chosenIdx];
        //}

        //if (TargetHeight < 30.0f)
        //{
        //    float w = 1.0f - Mathf.Clamp((TargetHeight - 10.0f) / 20.0f, 0.0f, 1.0f);
        //    Vector3 withOffset = lockedToPoint + Vector3.Back * 0.82f * TargetHeight;
        //    RestrictedPosition = RestrictedPosition.Slerp(withOffset, w);
        //}
    }

    //private void MouseOffset()
    //{
    //    if (Locked && LockedNew)
    //    {
    //        LockedOffsetFromInputX = 4.0f * ((GetViewport().GetMousePosition().X / GetViewport().GetVisibleRect().Size.X) * 2.0f - 1.0f);
    //    }
    //    else
    //    {
    //        LockedOffsetFromInputX = 0.0f;
    //    }
    //}

    //public void SetTarget(Vector3 target, float angle = 0.0f, float zoom = 0.0f, bool lockCamera = true)
    //{
    //    if (LockedNew)
    //    {
    //        GoToTarget = new Vector3(target.X, 0.0f + 2.0f, target.Z - 9.0f);
    //    }
    //    else
    //    {
    //        GoToTarget = new Vector3(target.X, 0.0f, target.Z);
    //    }
    //
    //    GoToTargetReached = false;
    //
    //    RotationAngle = angle;
    //
    //    ZoomPos = zoom;
    //
    //    Locked = lockCamera;
    //}

    //public void SetTargetNoLock(Vector3 target)
    //{
    //    GoToTarget = new Vector3(target.X, 0.0f, target.Z);
    //    GoToTargetReached = false;
    //}

    //public void SetTargetNoLockWithHeight(Vector3 target)
    //{
    //    GoToTarget = target;
    //    GoToTargetReached = false;
    //}

    //public void SetTargetArea( Vector3 minXZ, Vector3 maxXZ, bool lockCamera = false )
    //{
    //    Vector3 average = new Vector3( ( minXZ.x + maxXZ.x )* 0.5f, 0.0f, ( minXZ.z + maxXZ.z ) * 0.5f );
    //    float maxDistance = Mathf.Max( maxXZ.x - minXZ.x, maxXZ.x - minXZ.x );
    //
    //    float zoom = 0.5f * ( Mathf.Clamp( maxDistance, 200.0f, 400.0f ) / 200.0f );
    //
    //    SetTarget( average, zoom, lockCamera );
    //}

    public void Unlock()
    {
        Locked = false;
        RotationAngle = 0.0f;
    }

    public void SetZoomAndScroll(bool b)
    {
        ZoomAndScroll = b;
    }

    public bool ZoomedOut
    {
        get { return ZoomPos > 0.5f; }
    }

    // unused for the moment
    //private float DistanceToGround()
    //{
    //    Ray ray = new Ray(CameraTransform.position, Vector3.down);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //        return (hit.point - CameraTransform.position).magnitude;
    //
    //    return 0f;
    //}
}