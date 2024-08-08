using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

// Generated
public partial class GFXIncomingsItem : Node3D
{
    public Node3D IncomingRotation_Ship = null;
    public Node3D IncomingPoint_Ship = null;
    public Node3D IncomingRotation_NoShip = null;
    public Node3D IncomingPoint_NoShip = null;


    [ExportCategory("Runtime")]
    [Export]
    public Array<FleetData> _Fleets = new Array<FleetData>();
    [Export]
    public StarData Star = null;
    [Export]
    public float Direction = 0.0f;
    [Export]
    public UIGalaxyPath HUD = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        IncomingRotation_Ship = GetNode<Node3D>("Incomig_Rotation");
        IncomingPoint_Ship = GetNode<Node3D>("Incomig_Rotation/Point");
        IncomingRotation_NoShip = GetNode<Node3D>("Incomig_Rotation2");
        IncomingPoint_NoShip = GetNode<Node3D>("Incomig_Rotation2/Point");
    }

    public void Refresh(StarData from, StarData to, FleetData linkedToFleet)
    {
        _Fleets.Clear();
        _Fleets.Add(linkedToFleet);

        Star = to;

        Vector3 offset_A = new Vector3(3.0f, 0.0f, 2.5f);
        if (linkedToFleet.AtStar_PerTurn.System != null && linkedToFleet.AtStar_PerTurn.System._Sector._Player != Game.HumanPlayer)
            offset_A = new Vector3(3.0f, 0.0f, -2.5f);
        Vector3 offset_B = new Vector3(-2.0f, 0.0f, 0.0f);

        Vector3 point_A = from._Node.GFX.Position + offset_A;
        Vector3 point_B = Star._Node.GFX.Position + offset_B;

        Position = Star._Node.GFX.Position;
        Direction = -(point_A - point_B).SignedAngleTo(Vector3.Forward, Vector3.Up);
        IncomingRotation_Ship.Rotation = new Vector3(0.0f, Direction, 0.0f);
        IncomingRotation_NoShip.Rotation = new Vector3(0.0f, Direction, 0.0f);
        //TEMP01
        // if (Star._Node.GFX.ShipFriendly.Visible || Star._Node.GFX.ShipEnemy.Visible)
        // {
        //     IncomingRotation_Ship.Visible = true;
        // }
        // else
        // {
        //     IncomingRotation_NoShip.Visible = true;
        // }

        if (HUD == null)
        {
            //Game.GalaxyUI.AddIncomingLabel(this); //TEMP02
        }

        HUD.Refresh();
        HUD.Visible = true;

        Visible = true;
    }

    public void AddFleet(FleetData fleet)
    {
        _Fleets.Add(fleet);
        if (HUD != null)
            HUD.Refresh();
    }

    public void RemoveFleet(FleetData fleet)
    {
        _Fleets.Remove(fleet);

        if (HUD != null)
            HUD.Refresh();

        if (_Fleets.Count == 0)
            HidePath();
    }

    public void HidePath()
    {
        if (HUD != null)
            HUD.Visible = false;
        Visible = false;
    }
}
