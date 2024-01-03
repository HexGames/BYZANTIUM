using Godot;
using Godot.Collections;
using System.Linq;

// Editor
[Tool]
public partial class DefLibrary : Node
{
    public string LocationsDir = "res:///Def/Locations/";
    [Export]
    public Array<LocationDef> Locations = new Array<LocationDef>();

    public string PawnsDir = "res:///Def/Pawns/";
    [Export]
    public Array<PawnDef> Pawns = new Array<PawnDef>();

    public override void _Ready()
    {
        DirAccess folder = null;
        string[] files = null;

        // Locations
        Locations.Clear();

        folder = DirAccess.Open(LocationsDir);
        files = folder.GetFiles();

        for ( int idx = 0; idx < files.Length; idx++ )
        {
            Resource res = GD.Load<Resource>(LocationsDir + files[idx]);
            Locations.Add(res as LocationDef);
        }

        // Pawns
        Pawns.Clear();

        folder = DirAccess.Open(PawnsDir);
        files = folder.GetFiles();

        for (int idx = 0; idx < files.Length; idx++)
        {
            Resource res = GD.Load<Resource>(PawnsDir + files[idx]);
            Pawns.Add(res as PawnDef);
        }
    }
}
