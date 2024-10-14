public class DefFeatureWrapper
{
    public DataBlock _Data;

    public PlanetData _Planet;
    public DataBlock _FeatureOld;

    public string Name = "";
    public string Icon = "";
    public ResourcesWrapper Res_PerSession = null;

    public DefFeatureWrapper(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.ValueS;
        Icon = _Data.GetSubValueS("Icon");

        DataBlock benefitData = _Data.GetSub("Benefit", false);

        if (benefitData != null)
        {
            Res_PerSession = new ResourcesWrapper(benefitData, ResourcesWrapper.ParentType.FEATURE);
            Res_PerSession.Refresh();
        }
    }
}