using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

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

        Vector3 offset_A = new Vector3(5.6f, 0.0f, 2f);
        if (linkedToFleet.StarAt_PerTurn.System != null && linkedToFleet.StarAt_PerTurn.System._Player != Game.HumanPlayer)
            offset_A = new Vector3(5.6f, 0.0f, -2f);
        Vector3 offset_B = new Vector3(0.0f, 0.0f, 0.0f);

        Vector3 point_A = Star_From._Node.GFX.Position + offset_A;
        Vector3 point_B = Star_To._Node.GFX.Position + offset_B;
        Vector3 point_A_Offset = (point_B - point_A).Normalized() * 2.0f;
        Vector3 point_B_Offset = (point_A - point_B).Normalized() * 5.0f;
        Vector3 A = point_A + point_A_Offset;
        Vector3 B = point_B + point_B_Offset;

        Position = (A + B) * 0.5f;
        Path.Rotation = new Vector3(0.0f, -(A - B).SignedAngleTo(Vector3.Forward, Vector3.Up), 0.0f);
        Path.Scale = new Vector3(1.0f, 1.0f, (A - B).Length());

        if (HUD == null)
        {
            Game.self.UIGalaxy.AddPathLabel(this);
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
