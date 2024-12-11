
// Session || Per Turn
public partial class FeatureData
{
    public PlanetData _Planet = null;

    public DataBlock Data = null;

    public DefFeatureWrapper FeatureDef = null;

    public FeatureData(DataBlock featureData, PlanetData planet)
    {
        _Planet = planet;
        Data = featureData;
        FeatureDef = Game.self.Def.GetFeatureInfo(featureData.Name);
    }
}
