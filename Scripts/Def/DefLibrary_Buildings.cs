using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Buildings")]
    [Export]
    public DataBlock DistrictsList = null;
    [Export]
    public Array<DataBlock> Districts = new Array<DataBlock>();

    public List<DefDistrictWrapper> DistrictsInfo = new List<DefDistrictWrapper>();

    public void _Ready_Districts()
    {
        for (int idx = 0; idx < Districts.Count; idx++)
        {
            DistrictsInfo.Add(new DefDistrictWrapper(Districts[idx]));
        }
    }

    public DataBlock GetDistrict(string name)
    {
        for (int idx = 0; idx < Districts.Count; idx++)
        {
            if (Districts[idx].ValueS == name)
            {
                return Districts[idx];
            }
        }
        return null;
    }

    public DefDistrictWrapper GetDistrictInfo(string name)
    {
        for (int idx = 0; idx < DistrictsInfo.Count; idx++)
        {
            if (DistrictsInfo[idx]._Data.ValueS == name)
            {
                return DistrictsInfo[idx];
            }
        }
        return null;
    }

    //public DataBlock SuggestDistrictForPlanet(DataBlock planet)
    //{
    //
    //}

    public void SaveDistrictsDef()
    {
        Data.SaveToFile(DistrictsList, "Defs_Mod/Buildings.mod", this);
    }

    public void LoadDistrictsDefFunc()
    {
        //BuildingsList = Data.LoadFile("Defs_Mod/Buildings.mod", this);
        DistrictsList = Data.LoadCSV("Defs_Mod/Buildings.table", this);

        Districts.Clear();
        Districts = DistrictsList.GetSubs("Building");
    }
}
