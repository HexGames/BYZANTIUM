using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Factions")]
    [Export]
    public DataBlock FactionsList = null;
    [Export]
    public Array<DataBlock> Factions = new Array<DataBlock>();

    public List<DefBuildingWrapper> FactionsInfo = new List<DefBuildingWrapper>();

    public void _Ready_Factions()
    {
        for (int idx = 0; idx < Factions.Count; idx++)
        {
            FactionsInfo.Add(new DefBuildingWrapper(Factions[idx]));
        }
    }

    public DataBlock GetFaction(string name)
    {
        for (int idx = 0; idx < Factions.Count; idx++)
        {
            if (Factions[idx].ValueS == name)
            {
                return Factions[idx];
            }
        }
        return null;
    }

    public DefBuildingWrapper GetFactionInfo(string name)
    {
        for (int idx = 0; idx < FactionsInfo.Count; idx++)
        {
            if (FactionsInfo[idx]._Data.ValueS == name)
            {
                return FactionsInfo[idx];
            }
        }
        return null;
    }

    public void SaveFactionsDef()
    {
        Data.SaveToFile(FactionsList, "Defs_Mod/Factions.mod", this);
    }

    public void LoadFactionsDefFunc()
    {
        // FactionsList = Data.LoadFile("Defs_Mod/Factions.mod", this);
        FactionsList = Data.LoadCSV("Defs_Mod/Factions.table", this);

        Factions.Clear();
        Factions = FactionsList.GetSubs("Empire");
    }
}
