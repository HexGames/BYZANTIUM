using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Empires")]
    [Export]
    public DataBlock EmpiresList = null;
    [Export]
    public Array<DataBlock> Empires = new Array<DataBlock>();

    public List<DefBuildingWrapper> EmpiresInfo = new List<DefBuildingWrapper>();

    public void _Ready_Empires()
    {
        for (int idx = 0; idx < Empires.Count; idx++)
        {
            EmpiresInfo.Add(new DefBuildingWrapper(Empires[idx]));
        }
    }

    public DataBlock GetEmpire(string name)
    {
        for (int idx = 0; idx < Empires.Count; idx++)
        {
            if (Empires[idx].ValueS == name)
            {
                return Empires[idx];
            }
        }
        return null;
    }

    public DefBuildingWrapper GetEmpireInfo(string name)
    {
        for (int idx = 0; idx < EmpiresInfo.Count; idx++)
        {
            if (EmpiresInfo[idx]._Data.ValueS == name)
            {
                return EmpiresInfo[idx];
            }
        }
        return null;
    }

    public void SaveEmpiresDef()
    {
        Data.SaveToFile(EmpiresList, "Defs_Mod/Empires.mod", this);
    }

    public void LoadEmpiresDefFunc()
    {
        EmpiresList = Data.LoadFile("Defs_Mod/Empires.mod", this);

        Empires.Clear();
        Empires = EmpiresList.GetSubs("Empire");
    }
}
