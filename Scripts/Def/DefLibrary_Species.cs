using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    //[ExportCategory("Def Species")]
    //[Export]
    //public DataBlock SpeciesList = null;
    //[Export]
    //public Array<DataBlock> Species = new Array<DataBlock>();
    //
    //public List<DefDistrictWrapper> SpeciesInfo = new List<DefDistrictWrapper>();

    public void _Ready_Species()
    {
        //for (int idx = 0; idx < Species.Count; idx++)
        //{
        //    SpeciesInfo.Add(new DefDistrictWrapper(Species[idx]));
        //}
    }

    public DataBlock GetSpecie(string name)
    {
        //for (int idx = 0; idx < Species.Count; idx++)
        //{
        //    if (Species[idx].ValueS == name)
        //    {
        //        return Species[idx];
        //    }
        //}
        return null;
    }

    public DefDistrictWrapper GetSpecieInfo(string name)
    {
        //for (int idx = 0; idx < SpeciesInfo.Count; idx++)
        //{
        //    if (SpeciesInfo[idx]._Data.ValueS == name)
        //    {
        //        return SpeciesInfo[idx];
        //    }
        //}
        return null;
    }

    public void SaveSpeciesDef()
    {
        //Data.SaveToFile(SpeciesList, "Defs_Mod/Species.mod", this);
    }

    public void LoadSpeciesDefFunc()
    {
        //SpeciesList = Data.LoadFile("Defs_Mod/Species.mod", this);
        //
        //Species.Clear();
        //Species = SpeciesList.GetSubs("Specie");
    }
}
