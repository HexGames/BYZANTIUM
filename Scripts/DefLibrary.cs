using Godot;
using Godot.Collections;
using System.Linq;

// Editor
[Tool]
public partial class DefLibrary : Node
{
    public string CitiesDir = "res:///Def/Cities/";
    [Export]
    public Array<CityDef> Cities = new Array<CityDef>();

    public override void _Ready()
    {
        Cities.Clear();

        DirAccess folder = DirAccess.Open(CitiesDir);
        string[] files = folder.GetFiles();

        for ( int idx = 0; idx < files.Length; idx++ )
        {
            Resource res = GD.Load<Resource>(CitiesDir + files[idx]);
            Cities.Add(res as CityDef);
        }
    }
}
