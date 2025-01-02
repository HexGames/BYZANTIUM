using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    //[ExportCategory("Def Economy")]
    //[Export]
    //public DataBlock EconomyData = null;

    //public void _Ready_Economy()
    //{
    //    for (int idx = 0; idx < Empires.Count; idx++)
    //    {
    //        EmpiresInfo.Add(new DefEmpireWrapper(Empires[idx]));
    //    }
    //}

    //public void SaveEmpiresDef()
    //{
    //    Data.SaveToFile(EmpiresList, "Defs_Mod/Empires.mod", this);
    //}

    public void LoadEconomyDefFunc()
    {
        //EconomyData = Data.LoadFile("Defs_Mod/Economy.mod", this);
    }
}
