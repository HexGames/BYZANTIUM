using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Features")]
    [Export]
    public DataBlock FeaturesList = null;
    [Export]
    public Array<DataBlock> Features = new Array<DataBlock>();

    public List<DefFeatureWrapper> FeaturesInfo = new List<DefFeatureWrapper>();

    public DataBlock GetFeature(string name)
    {
        for (int idx = 0; idx < Features.Count; idx++)
        {
            if (Features[idx].ValueS == name)
            {
                return Features[idx];
            }
        }
        return null;
    }

    public void SaveFeaturesDef()
    {
        Data.SaveToFile(FeaturesList, "Defs_Mod/Features.mod", this);
    }

    public void LoadFeaturesDefFunc()
    {
        // FeaturesList = Data.LoadFile("Defs_Mod/Features.mod", this);
        FeaturesList = Data.LoadCSV("Defs_Mod/Features.table", this);

        Features.Clear();
        Features = FeaturesList.GetSubs("Feature");
    }
}
