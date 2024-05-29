using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Ethics")]
    [Export]
    public DataBlock EthicsList = null;
    [Export]
    public Array<DataBlock> Ethics = new Array<DataBlock>();

    public List<DefBuildingWrapper> EthicsInfo = new List<DefBuildingWrapper>();

    public void _Ready_Ethics()
    {
        for (int idx = 0; idx < Ethics.Count; idx++)
        {
            EthicsInfo.Add(new DefBuildingWrapper(Ethics[idx]));
        }
    }

    public DataBlock GetEthic(string name)
    {
        for (int idx = 0; idx < Ethics.Count; idx++)
        {
            if (Ethics[idx].ValueS == name)
            {
                return Ethics[idx];
            }
        }
        return null;
    }

    public DefBuildingWrapper GetEthicInfo(string name)
    {
        for (int idx = 0; idx < EthicsInfo.Count; idx++)
        {
            if (EthicsInfo[idx]._Data.ValueS == name)
            {
                return EthicsInfo[idx];
            }
        }
        return null;
    }

    public void SaveEthicsDef()
    {
        Data.SaveToFile(EthicsList, "Defs_Mod/Ethics.mod", this);
    }

    public void LoadEthicsDefFunc()
    {
        EthicsList = Data.LoadFile("Defs_Mod/Ethics.mod", this);

        Ethics.Clear();
        Ethics = EthicsList.GetSubs("Empire");
    }
}
