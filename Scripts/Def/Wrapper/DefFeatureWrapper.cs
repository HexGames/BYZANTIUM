public class DefFeatureWrapper
{
    public DataBlock _Data;

    public PlanetData _Planet;
    public DataBlock _FeatureOld;

    public string Name = "";
    public string Icon = "";
    public FeatureEconomyWrapper Economy_PerSession = null;

    public DefFeatureWrapper(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.ValueS;
        Icon = _Data.GetSubValueS("Icon");

        Economy_PerSession = new FeatureEconomyWrapper(this);
        Economy_PerSession.Refresh();
    }
}