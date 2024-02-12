using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Ship Parts")]
    [Export]
    public DataBlock ShipPartsList = null;
    [Export]
    public Array<DataBlock> ShipParts = new Array<DataBlock>();

    public List<ActionTargetInfo> ShipPartsInfo = new List<ActionTargetInfo>();

    public void _Ready_ShipParts()
    {
        for (int idx = 0; idx < ShipParts.Count; idx++)
        {
            ShipPartsInfo.Add(new ActionTargetInfo(Buildings[idx]));
        }
    }

    public DataBlock GetShipPart(string name)
    {
        for (int idx = 0; idx < ShipParts.Count; idx++)
        {
            if (ShipParts[idx].ValueS == name)
            {
                return ShipParts[idx];
            }
        }
        return null;
    }

    public ActionTargetInfo GetShipPartInfo(string name)
    {
        for (int idx = 0; idx < ShipPartsInfo.Count; idx++)
        {
            if (ShipPartsInfo[idx]._Data.ValueS == name)
            {
                return ShipPartsInfo[idx];
            }
        }
        return null;
    }

    public void SaveShipPartsDef()
    {
        Data.SaveToFile(ShipPartsList, "Defs_Mod/ShipParts.mod", this);
    }

    public void LoadShipPartsDefFunc()
    {
        ShipPartsList = Data.LoadCSV("Defs_Mod/ShipParts.table", this);

        ShipParts.Clear();
        ShipParts = ShipPartsList.GetSubs("ShipPart");
    }
}
