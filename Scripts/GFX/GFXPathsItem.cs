using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

// Generated
public partial class GFXPathsItem : Node3D
{
    private MeshInstance3D Path = null;

    [ExportCategory("Runtime")]
    [Export]
    public Array<FleetData> _Fleets = new Array<FleetData>();
    [Export]
    public StarData Star_From = null;
    [Export]
    public StarData Star_To = null;
    [Export]
    public UIGalaxyPath HUD = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Path = GetNode<MeshInstance3D>("Path");
    }

    public void Refresh(StarData from, StarData to, FleetData linkedToFleet)
    {
        _Fleets.Clear();
        _Fleets.Add(linkedToFleet);

        Star_From = from;
        Star_To = to;

        Vector3 offset = new Vector3(5.0f, 0.0f, 2.5f);
        if (linkedToFleet.AtStar_PerTurn.System != null && linkedToFleet.AtStar_PerTurn.System._Sector._Player != Game.HumanPlayer)
            offset = new Vector3(5.0f, 0.0f, -2.5f);

        Vector3 point_A = Star_From._Node.GFX.Position + offset;
        Vector3 point_B = Star_To._Node.GFX.Position;
        Vector3 point_A_Offset = (point_B - point_A).Normalized() * 2.5f;
        Vector3 point_B_Offset = (point_A - point_B).Normalized() * 2.5f;
        Vector3 A = point_A + point_A_Offset;
        Vector3 B = point_B + point_B_Offset;

        Position = (A + B) * 0.5f;
        Path.Rotation = new Vector3(0.0f, -(A - B).SignedAngleTo(Vector3.Forward, Vector3.Up), 0.0f);
        Path.Scale = new Vector3(1.0f, 1.0f, (A - B).Length());

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
