using Godot;
using System;

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
    public int LOD = 2; 
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

    [Export]
    public float MoveLimitX = 60f; //x limit of map
    [Export]
    public float MoveLimitY = 25f; //z limit of map

    private float MoveSpeed = 100f; //speed with keyboard movement
    private float MoveSpeedDampening = 7.5f;

    private float ZoomMaxHeight = 160f; //maximal height
    private float ZoomMinHeight = 8f; //minimnal height
    private float ZoomHeightDampening = 7.5f;
    private float ZoomRotationDampening = 7.5f;
    private float ZoomKeyboardSensitivity = -15f;
    private float ZoomScrollWheelZSensitivity = -200f;

    private float RotationXDefault = 55;

    private Vector3 GoToTarget; //target to go to
    private bool GoToTargetReached = true;
    private bool Locked = false;
    private bool ZoomAndScroll = true;

    private bool LockedNew = false; //lock height

    private float LockedHeight = 28f; //lock height
    private float LockedRotationX = 45f; //lock rotation

    private float LockedHeight_new = 10f; //lock height
    private float LockedRotationX_new = 10f; //lock rotation

    private float LockedOffsetFromInputX = 0f;

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

        LOD = 2;
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
            return;

        CameraUpdate((float) delta);

        ProcessLOD((float)delta);
    }


    public Vector3 Input_Keyboard = Vector3.Zero;
    public float Input_Scroll = 0.0f;
    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (Engine.IsEditorHint())
            return;

        // Keyboard Input
        if (Input.IsActionPressed("Camera_Up")) Input_Keyboard += Vector3.Forward;
        if (Input.IsActionPressed("Camera_Down")) Input_Keyboard += Vector3.Back;
        if (Input.IsActionPressed("Camera_Right")) Input_Keyboard += Vector3.Right;
        if (Input.IsActionPressed("Camera_Left")) Input_Keyboard += Vector3.Left;

        // Zoom Scrool Wheel
        if (Input.IsActionPressed("Camera_Scroll_Up")) Input_Scroll += 0.01f; // Input.GetActionStrength("Camera_Scroll_Up");
        else if (Input.IsActionJustPressed("Camera_Scroll_Up")) Input_Scroll += 0.1f; // Input.GetActionStrength("Camera_Scroll_Up");
        if (Input.IsActionPressed("Camera_Scroll_Down")) Input_Scroll -= 0.01f; // Input.GetActionStrength("Camera_Scroll_Down");
        else if (Input.IsActionJustPressed("Camera_Scroll_Down")) Input_Scroll -= 0.1f; // Input.GetActionStrength("Camera_Scroll_Down");
    }

    public bool IsPointerOverGUI()
    {
        return UILockSystem;
    }

    // Camera methods
    private void CameraUpdate(float deltaTime)
    {
        if (GoToTargetReached == false)
        {
            MoveToTarget(deltaTime);
        }
        else
        {
            Move(deltaTime);
        }

        HeightCalculation(deltaTime);

        RotationCalculation(deltaTime);

        LimitPosition();

        MouseOffset();

        TransformNode.Rotation = new Vector3(-Mathf.DegToRad(CurrentRotationX), Mathf.DegToRad(CurrentRotationY), 0.0f);
        Quaternion q = Quaternion.FromEuler(TransformNode.Rotation);
        //TransformNode.Position = TargetPosition + Vector3.Right * LockedOffsetFromInputX + q * Vector3.Back * TargetDistance;
        TransformNode.Position = TargetPosition + Vector3.Right * LockedOffsetFromInputX + Vector3.Up * TargetHeight;
    }

    private void Move(float deltaTime)
    {
        // TO DO add panning 

        if (Locked == false && !IsPointerOverGUI() && ZoomAndScroll)
        {

            // mouse Input
            Vector3 mouseInput = new Vector3();

            if (GetViewport().GetMousePosition().Y < ScreenEdgeBorder) mouseInput += Vector3.Forward;
            if (GetViewport().GetMousePosition().Y > GetViewport().GetVisibleRect().Size.Y - ScreenEdgeBorder) mouseInput += Vector3.Back;
            if (GetViewport().GetMousePosition().X > GetViewport().GetVisibleRect().Size.X - ScreenEdgeBorder) mouseInput += Vector3.Right;
            if (GetViewport().GetMousePosition().X < ScreenEdgeBorder) mouseInput += Vector3.Left;

            Vector3 movement = Input_Keyboard + mouseInput;
            movement = movement.Normalized();

            movement *= (0.5f + ZoomPos) * MoveSpeed * deltaTime * (0.23f + 0.75f * ZoomPos);
            movement = Quaternion.FromEuler(new Vector3(0f, TransformNode.Rotation.Y, 0f)) * movement;

            TargetPosition += movement;
        }

        Input_Keyboard = Vector3.Zero;
    }

    private void HeightCalculation(float deltaTime)
    {
        if (Locked == false && !IsPointerOverGUI() && ZoomAndScroll) ZoomPos += Input_Scroll * deltaTime * ZoomScrollWheelZSensitivity * (0.25f + 0.75f * ZoomPos); 
        
        ZoomPos = Mathf.Clamp(ZoomPos, 0.0f, 1.0f);

        float targetHeight = Mathf.Lerp(ZoomMinHeight, ZoomMaxHeight, ZoomPos);
        if (Locked) targetHeight = LockedNew ? LockedHeight_new : LockedHeight;

        float difference = 0;
        if (TargetHeight != targetHeight) difference = targetHeight - TargetHeight;

        CurrentHeightDampening = Mathf.Max(0.15f, deltaTime * ZoomHeightDampening);
        float lerpedDifference = Mathf.Lerp(0, difference, CurrentHeightDampening);

        float oldDistance = TargetHeight;
        TargetHeight = Mathf.Clamp(TargetHeight + lerpedDifference, ZoomMinHeight, ZoomMaxHeight);

        Plane plane = new Plane(Vector3.Up, TargetHeight);
        if (lerpedDifference > 0)
        {
            Vector3? intersect = plane.IntersectsRay(Position, Position  - ProjectPosition(GetViewport().GetMousePosition(), 1.0f));
            if (intersect != null)
            {
                Vector3 moveVector = intersect.Value - Position;
                moveVector.Y = 0;
                TargetPosition += moveVector;
            }
        }
        else
        {
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

    private void RotationCalculation(float deltaTime)
    {
        float targetRotationX = RotationXDefault;
        if (Locked) targetRotationX = LockedNew ? LockedRotationX_new : LockedRotationX;

        CurrentRotationDampening = Mathf.Max(0.05f, deltaTime * ZoomHeightDampening);

        float difference = 0;
        if (CurrentRotationX != targetRotationX) difference = targetRotationX - CurrentRotationX;
        CurrentRotationX = Mathf.LerpAngle(CurrentRotationX, CurrentRotationX + difference, CurrentRotationDampening);

        difference = RotationAngle - CurrentRotationY;
        CurrentRotationY = Mathf.LerpAngle(CurrentRotationY, CurrentRotationY + difference, CurrentRotationDampening);
    }

    private void MoveToTarget(float deltaTime)
    {
        //TargetPosition = Vector3.MoveTowards( TargetPosition, GoToTarget, Time.deltaTime * MoveSpeedFollowingSpeed );
        //CurrentMoveSpeed = Mathf.Clamp( Time.deltaTime * MoveSpeedDampening * ( GoToTarget - TargetPosition ).magnitude, Time.deltaTime * MoveSpeedMin, Time.deltaTime * MoveSpeedMax );
        CurrentMoveSpeed = Mathf.Max(0.05f, deltaTime * ZoomHeightDampening) * (GoToTarget - TargetPosition).Length();
        TargetPosition += GoToTarget.Normalized() * CurrentMoveSpeed;
        GoToTargetReached = (GoToTarget - TargetPosition).LengthSquared() < 1.0f;
    }

    //private void MoveToHeight()
    //{
    //	targetDistance = Mathf.Lerp( targetDistance, GoToHeight, Time.deltaTime * heightDampening );
    //	zoomPos = ( GoToHeight - minHeight) / (maxHeight - minHeight); 
    //
    //	GoToHeightReached = ( GoToHeight - targetDistance ) < 1.0f;
    //
    //	float targetHeight = Mathf.Lerp(minHeight, maxHeight, zoomPos);
    //}

    private void LimitPosition()
    {
        TargetPosition = new Vector3(Mathf.Clamp(TargetPosition.X, -0.5f * MoveLimitX, 0.5f * MoveLimitX),
            TargetPosition.Y,
            Mathf.Clamp(TargetPosition.Z, -0.5f * MoveLimitY + 0.5f * TargetHeight, 0.5f * MoveLimitY + 0.5f * TargetHeight));
    }

    private void MouseOffset()
    {
        if (Locked && LockedNew)
        {
            LockedOffsetFromInputX = 4.0f * ((GetViewport().GetMousePosition().X / GetViewport().GetVisibleRect().Size.X) * 2.0f - 1.0f);
        }
        else
        {
            LockedOffsetFromInputX = 0.0f;
        }
    }

    public void SetTarget(Vector3 target, float angle = 0.0f, float zoom = 0.0f, bool lockCamera = true)
    {
        if (LockedNew)
        {
            GoToTarget = new Vector3(target.X, 0.0f + 2.0f, target.Z - 9.0f);
        }
        else
        {
            GoToTarget = new Vector3(target.X, 0.0f, target.Z);
        }

        GoToTargetReached = false;

        RotationAngle = angle;

        ZoomPos = zoom;

        Locked = lockCamera;
    }

    public void SetTargetNoLock(Vector3 target)
    {
        GoToTarget = new Vector3(target.X, 0.0f, target.Z);
        GoToTargetReached = false;
    }

    public void SetTargetNoLockWithHeight(Vector3 target)
    {
        GoToTarget = target;
        GoToTargetReached = false;
    }

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