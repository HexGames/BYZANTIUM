using Godot;
using Godot.Collections;
using System.Linq;

// Editor
[Tool]
public partial class DefLibrary : Resource
{
    [Export]
    public Array<CityDef> Cities = new Array<CityDef>(); 

    //public int CitiesCount
    //{
    //    get
    //    {
    //        return CitiesRes.Count;
    //    }
    //}
    //
    //public void CitiesClear()
    //{
    //    CitiesRes.Clear();
    //}
    //
    //public void CitiesAdd(CityDef def)
    //{
    //    CitiesRes.Add(def);
    //}
    //
    //public CityDef Cities(int idx)
    //{
    //    return (CityDef)CitiesRes[idx];
    //}
}
