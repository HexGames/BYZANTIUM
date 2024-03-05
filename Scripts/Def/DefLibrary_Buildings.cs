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

    public List<ActionTargetInfo> BuildingsInfo = new List<ActionTargetInfo>();

    public void _Ready_Buildings()
    {
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            BuildingsInfo.Add(new ActionTargetInfo(Buildings[idx]));
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

    public ActionTargetInfo GetBuildingInfo(string name)
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
        BuildingsList = Data.LoadFile("Defs_Mod/Buildings.mod", this);

        Buildings.Clear();
        Buildings = BuildingsList.GetSubs("Building");
    }
}
