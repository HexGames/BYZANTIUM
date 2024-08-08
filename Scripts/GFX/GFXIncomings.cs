using Godot;
using Godot.Collections;

public partial class GFXIncomings : Node
{
    [ExportCategory("Links")]
    [Export]
    public Array<GFXIncomingsItem> Paths = new Array<GFXIncomingsItem>();
   
    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Paths.Clear();
        Paths.Add(GetNode<GFXIncomingsItem>("GfxIncoming"));
    }

    public void ClearAllPaths()
    {
        for (int idx = 0; idx < Paths.Count; idx++)
        {
            Paths[idx]._Fleets.Clear();
            Paths[idx].HidePath();
        }
    }

    public void ClearIncomingForFleet(FleetData fleet)
    {
        for (int idx = 0; idx < Paths.Count; idx++)
        {
            if (Paths[idx]._Fleets.Contains(fleet))
            {
                Paths[idx].RemoveFleet(fleet);
            }
        }
    }

    public void AddIncoming(FleetData fleet, StarData toStar)
    {
        ClearIncomingForFleet(fleet);

        bool needNewIncoming = true;
        for (int idx = 0; idx < Paths.Count; idx++)
        {
            Vector3 point_A = fleet.AtStar_PerTurn._Node.GFX.Position;
            Vector3 point_B = toStar._Node.GFX.Position;
            if (Paths[idx].Star == toStar && Mathf.Abs(Paths[idx].Direction + (point_A - point_B).SignedAngleTo(Vector3.Forward, Vector3.Up)) < 45.0f)
            {
                if (Paths[idx]._Fleets.Count > 0) // there already is an arrow between these locations
                {
                    Paths[idx].AddFleet(fleet);
                    needNewIncoming = false;
                    break;
                }
            }
        }

        if (needNewIncoming)
        {
            for (int idx = 0; idx < Paths.Count; idx++)
            {
                if (Paths[idx]._Fleets.Count == 0)
                {
                    Paths[idx].Refresh(fleet.AtStar_PerTurn, toStar, fleet); // reuse an unused arrow
                    needNewIncoming = false;
                    break;
                }
            }
        }

        if (needNewIncoming) 
        {
            GFXIncomingsItem newItem = Paths[0].Duplicate(7) as GFXIncomingsItem; // create a new arrow
            Paths[0].GetParent().AddChild(newItem);
            Paths.Add(newItem);

            newItem.HUD = null; // this needs to be reset here so a new HUD object is created
            newItem.Refresh(fleet.AtStar_PerTurn, toStar, fleet);
            newItem.Visible = true;
        }
    }
}