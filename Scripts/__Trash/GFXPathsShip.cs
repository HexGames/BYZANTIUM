using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

// Generated
public partial class GFXPathsShip : MeshInstance3D
{
    public Vector3 PathStart = Vector3.Zero;
    public Vector3 PathEnd = Vector3.Zero;

    public int CurrentMove = 0;
    public int TotalMoves = 0;

    public float Progress = 0.0f;
    public bool Moving = false;

    public FleetData _Fleet = null;

    public void Reset()
    {
        Visible = false;

        PathStart = Vector3.Zero;
        PathEnd = Vector3.Zero;

        CurrentMove = 0;
        TotalMoves = 0;
        _Fleet = null;

        Progress = 0.0f;
        Moving = false;
    }

    public void Start(FleetData fleet, int moves)
    {
        CurrentMove = 0;
        TotalMoves = moves;
        _Fleet = fleet;
    }
}

/*
 
    class ShipInfo
    {
        public MeshInstance3D Ship;
        public int CurrentMove = 0;
        public int TotalMoves = 0;

        public Vector3 MoveStart = Vector3.Zero;
        public Vector3 MoveTarget = Vector3.Zero;
        public float Progress = 0.0f;
        public bool Moving = false;

        public FleetData _Fleet = null;

        public ShipInfo(MeshInstance3D ship)
        {
            Ship = ship;
            Reset();
        }

        public void Reset()
        {
            Ship.Visible = false;
            CurrentMove = 0;
            TotalMoves = 0;
            _Fleet = null;

            MoveStart = Vector3.Zero;
            MoveTarget = Vector3.Zero;
            Progress = 0.0f;
            Moving = false;
    }

        public void Start(FleetData fleet, int moves)
        {
            CurrentMove = 0;
            TotalMoves = moves;
            _Fleet = fleet;
        }
    };
...
    private List<ShipInfo> Ships = new List<ShipInfo>();
...
        Ships.Add(new ShipInfo(GetNode<MeshInstance3D>("Ship")));

...
        if (_Fleets.Count > Ships.Count)
        {
            MeshInstance3D newMesh = Ships[0].Ship.Duplicate(7) as MeshInstance3D;
            Ships[0].Ship.GetParent().AddChild(newMesh);
            Ships.Add(new ShipInfo(newMesh));
        }


public void Refresh(StarData from, StarData to, FleetData linkedToFleet)
    {
        _Fleets.Clear();
        _Fleets.Add(linkedToFleet);

        Star_From = from;
        Star_To = to;

        Vector3 offset_A = new Vector3(3.0f, 0.0f, 2.5f);
        if (linkedToFleet.AtStar_PerTurn.System != null && linkedToFleet.AtStar_PerTurn.System._Sector._Player != Game.HumanPlayer)
            offset_A = new Vector3(3.0f, 0.0f, -2.5f);
        Vector3 offset_B = new Vector3(-2.0f, 0.0f, 0.0f);

        Vector3 point_A = Star_From._Node.GFX.Position + offset_A;
        Vector3 point_B = Star_To._Node.GFX.Position + offset_B;
        Vector3 point_A_Offset = (point_B - point_A).Normalized() * 2.5f;
        Vector3 point_B_Offset = (point_A - point_B).Normalized() * 2.5f;
        A = point_A + point_A_Offset;
        B = point_B + point_B_Offset;

        Position = (A + B) * 0.5f;
        Path.Rotation = new Vector3(0.0f, -(A - B).SignedAngleTo(Vector3.Forward, Vector3.Up), 0.0f);
        Path.Scale = new Vector3(1.0f, 1.0f, (A - B).Length());

        for (int idx = 0; idx < Ships.Count; idx++)
        {
            Ships[idx].Reset();
        }

        AddMovingShip(linkedToFleet);

        if (HUD == null)
        {
            Game.GalaxyUI.AddPathLabel(this);
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

        AddMovingShip(fleet);
    }

    public void RemoveFleet(FleetData fleet)
    {
        _Fleets.Remove(fleet);

        RemoveMovingShip(fleet);

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

    private void AddMovingShip(FleetData forFleet)
    {

        int totalMoves = Star_From.DistanceTo(Star_To);

        Ships[Ships.Count - 1].Start(forFleet, totalMoves);
    }

    private void RemoveMovingShip(FleetData forFleet)
    {
        for (int idx = 0; idx < Ships.Count; idx++)
        {
            if (Ships[idx]._Fleet == forFleet)
            {
                Ships[idx].Reset();
            }
        }
    }

    private void EndTurn()
    {
        for (int idx = 0; idx < Ships.Count; idx++)
        {
            if (Ships[idx].TotalMoves > 0)
            {
                Ships[idx].Ship.Rotation = Path.Rotation;
                float currentProgress = 1.0f * Ships[idx].CurrentMove / Ships[idx].TotalMoves;
                float nextProgress = 1.0f * ( Ships[idx].CurrentMove + 1 ) / Ships[idx].TotalMoves;
                Ships[idx].MoveStart = A + (B - A) * currentProgress;
                Ships[idx].MoveTarget = A + (B - A) * nextProgress;
                Ships[idx].Moving = true;

                Ships[idx].CurrentMove++;
            }
        }
    }

    public override void _Process(double delta)
    {
        for (int idx = 0; idx < Ships.Count; idx++)
        {
            if (Ships[idx].Moving)
            {
                Ships[idx].Progress += ((float)delta);

                Ships[idx].Ship.Position = Ships[idx].MoveStart + (Ships[idx].MoveTarget - Ships[idx].MoveStart) * Ships[idx].Progress;
                Ships[idx].Ship.Visible = true;

                if (Ships[idx].Progress >= 1.0f)
                {
                    Ships[idx].Moving = false;
                    if (Ships[idx].CurrentMove >= Ships[idx].TotalMoves)
                    {
                        RemoveFleet(Ships[idx]._Fleet);
                    }
                }
            }
        }
    }
 */
