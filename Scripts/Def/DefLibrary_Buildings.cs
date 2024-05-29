using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Buildings")]
    [Export]
    public DataBlock BuildingsList = null;
    [Export]
    public Array<DataBlock> Buildings = new Array<DataBlock>();

    public List<DefBuildingWrapper> BuildingsInfo = new List<DefBuildingWrapper>();

    public void _Ready_Buildings()
    {
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            BuildingsInfo.Add(new DefBuildingWrapper(Buildings[idx]));
        }
    }

    public DataBlock GetBuilding(string name)
    {
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            if (Buildings[idx].ValueS == name)
            {
                return Buildings[idx];
            }
        }
        return null;
    }

    public DefBuildingWrapper GetBuildingInfo(string name)
    {
        for (int idx = 0; idx < BuildingsInfo.Count; idx++)
        {
            if (BuildingsInfo[idx]._Data.ValueS == name)
            {
                return BuildingsInfo[idx];
            }
        }
        return null;
    }

    public void SaveBuildingsDef()
    {
        Data.SaveToFile(BuildingsList, "Defs_Mod/Buildings.mod", this);
    }

    public void LoadBuildingsDefFunc()
    {
        //BuildingsList = Data.LoadFile("Defs_Mod/Buildings.mod", this);
        BuildingsList = Data.LoadCSV("Defs_Mod/Buildings.table", this);

        Buildings.Clear();
        Buildings = BuildingsList.GetSubs("Building");
    }
}
