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

    public void _Ready_Features()
    {
        for (int idx = 0; idx < Features.Count; idx++)
        {
            FeaturesInfo.Add(new DefFeatureWrapper(Features[idx]));
        }
    }

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

    public DefFeatureWrapper GetFeatureInfo(string name)
    {
        for (int idx = 0; idx < FeaturesInfo.Count; idx++)
        {
            if (FeaturesInfo[idx]._Data.ValueS == name)
            {
                return FeaturesInfo[idx];
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
