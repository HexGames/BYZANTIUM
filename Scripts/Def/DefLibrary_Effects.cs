using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Effects")]
    [Export]
    public DataBlock EffectsList = null;
    [Export]
    public Array<DataBlock> Effects = new Array<DataBlock>();

    public List<DefEffectWrapper> EffectsInfo = new List<DefEffectWrapper>();

    public void _Ready_Effects()
    {
        for (int idx = 0; idx < Effects.Count; idx++)
        {
            EffectsInfo.Add(new DefEffectWrapper(Effects[idx]));
        }
    }

    public DataBlock GetEffect(string name)
    {
        for (int idx = 0; idx < Effects.Count; idx++)
        {
            if (Effects[idx].ValueS == name)
            {
                return Effects[idx];
            }
        }
        return null;
    }

    public DefEffectWrapper GetEffectInfo(string name)
    {
        for (int idx = 0; idx < EffectsInfo.Count; idx++)
        {
            if (EffectsInfo[idx]._Data.ValueS == name)
            {
                return EffectsInfo[idx];
            }
        }
        return null;
    }

    public void SaveEffectsDef()
    {
        Data.SaveToFile(EffectsList, "Defs_Mod/Effects.mod", this);
    }

    public void LoadEffectsDefFunc()
    {
        // EffectsList = Data.LoadFile("Defs_Mod/Effects.mod", this);
        EffectsList = Data.LoadCSV("Defs_Mod/Effects.table", this);

        Effects.Clear();
        Effects = EffectsList.GetSubs("Effect");
    }
}
