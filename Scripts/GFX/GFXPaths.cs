using Godot;
using Godot.Collections;

public partial class GFXPaths : Node
{
    [ExportCategory("Links")]
    [Export]
    public Array<GFXPathsItem> Paths = new Array<GFXPathsItem>();
   
    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Paths.Clear();
        Paths.Add(GetNode<GFXPathsItem>("GfxPath"));
    }

    public void ClearAllPaths()
    {
        for (int idx = 0; idx < Paths.Count; idx++)
        {
            Paths[idx]._Fleets.Clear();
            Paths[idx].HidePath();
        }
    }

    public void ClearPathForFleet(FleetData fleet)
    {
        for (int idx = 0; idx < Paths.Count; idx++)
        {
            if (Paths[idx]._Fleets.Contains(fleet))
            {
                Paths[idx].RemoveFleet(fleet);
            }
        }
    }

    public void AddPath(FleetData fleet, StarData toStar)
    {
        ClearPathForFleet(fleet);

        bool needNewPath = true;
        for (int idx = 0; idx < Paths.Count; idx++)
        {
            if (Paths[idx].Star_From == fleet.Star_At_PerTurn && Paths[idx].Star_To == toStar) 
            {
                if (Paths[idx]._Fleets.Count > 0) // there already is an arrow between these locations
                {
                    Paths[idx].AddFleet(fleet);
                    needNewPath = false;
                    break;
                }
            }
        }

        if (needNewPath)
        {
            for (int idx = 0; idx < Paths.Count; idx++)
            {
                if (Paths[idx]._Fleets.Count == 0)
                {
                    Paths[idx].Refresh(fleet.Star_At_PerTurn, toStar, fleet); // reuse an unused arrow
                    needNewPath = false;
                    break;
                }
            }
        }

        if (needNewPath) 
        {
            GFXPathsItem newItem = Paths[0].Duplicate(7) as GFXPathsItem; // create a new arrow
            Paths[0].GetParent().AddChild(newItem);
            Paths.Add(newItem);

            newItem.HUD = null; // this needs to be reset here so a new HUD object is created
            newItem.Refresh(fleet.Star_At_PerTurn, toStar, fleet);
            newItem.Visible = true;
        }
    }
}