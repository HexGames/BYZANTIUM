using Godot;
using Godot.Collections;

[Tool]
public partial class GFXStarShip : Node3D
{
    // planets and moons
    private MeshInstance3D Ship = null;
    private MeshInstance3D ShipColor = null;
    private Area3D Collision = null;
    //public CollisionShape3D CollisionShape = null;

    private bool Friendly = false;

    [ExportCategory("Runtime")]
    [Export]
    public Array<FleetData> _Fleets = new Array<FleetData>();

    public void Init(bool friendly)
    {
        Friendly = friendly;

        Ship = GetNode<MeshInstance3D>("Ship");
        ShipColor = GetNode<MeshInstance3D>("Ship/Color");
        Collision = GetNode<Area3D>("Area3D");
        //CollisionShape = GetNode<CollisionShape3D>("Area3D/CollisionShape3D");

        //Collision.InputEvent += SignalInputEvent;
        //Collision.MouseEntered += OnHover;
        //Collision.MouseExited += OnDehover;

        Visible = false;
        Collision.CollisionLayer = 0;
    }

    public void RefreshShip(Array<FleetData> fleets)
    {
        _Fleets.Clear();

        if (fleets.Count > 0)
        {
            _Fleets.AddRange(fleets);

            Color color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            PlayerData playerWithTheBiggestFleet = fleets[0]._Player; // TODO it should be...
            color = Game.self.UILib.GetPlayerColor(playerWithTheBiggestFleet.PlayerID);
            for (int idx = 0; idx < fleets.Count; idx++)
            {
                if (fleets[idx]._Player == Game.self.HumanPlayer)
                {
                    color = Game.self.UILib.GetPlayerColor(fleets[idx]._Player.PlayerID);
                    break;
                }
            }

            StandardMaterial3D newMaterial = ShipColor.GetSurfaceOverrideMaterial(0).Duplicate() as StandardMaterial3D;
            newMaterial.AlbedoColor = color;
            ShipColor.SetSurfaceOverrideMaterial(0, newMaterial);

            Visible = true;
            Collision.CollisionLayer = 1;
        }
        else
        {
            Visible = false;
            Collision.CollisionLayer = 0;
        }
    }

    //public void OnHover()
    //{
    //    GD.Print("Log_In");
    //    Game.self.Input.OnHoverFleets(_Fleets);
    //}
    //
    //public void OnDehover()
    //{
    //    GD.Print("Log_Out");
    //    Game.self.Input.OnDehoverFleets();
    //}
    //public void SignalInputEvent(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, long shapeIdx)
    //{
    //    if (inputEvent is InputEventMouseButton mouseButtonEvent)
    //    {
    //        if (!mouseButtonEvent.IsPressed())
    //        {
    //            // on mouse button release
    //            if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
    //            {
    //                Game.self.Input.OnSelectFleets(_Fleets);
    //            }
    //            // on mouse button release
    //            if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
    //            {
    //                Game.self.Input.DeselectOneStep();
    //            }
    //        }
    //    }
    //}
}