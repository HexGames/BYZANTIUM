public class DefFeatureWrapper
{
    public DataBlock _Data;

    public PlanetData _Planet;
    public DataBlock _FeatureOld;

    public string Name = "";
    public ResourcesWrapper Benefit = null;

    public DefFeatureWrapper(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.ValueS;

        DataBlock benefitData = _Data.GetSub("Benefit", false);

        if (benefitData != null)
        {
            Benefit = new ResourcesWrapper(benefitData, ResourcesWrapper.ParentType.Building);
            Benefit.Refresh();
        }
    }
}