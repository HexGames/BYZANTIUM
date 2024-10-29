public class DefEffectWrapper
{
    public DataBlock _Data;

    public PlanetData _Planet;

    public string Name = "";
    public ResourcesWrapper Res_PerSession = null;

    public DefEffectWrapper(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.GetSubValueS("Name");

        DataBlock benefitData = _Data.GetSub("Benefit", false);

        if (benefitData != null)
        {
            Res_PerSession = new ResourcesWrapper(benefitData, ResourcesWrapper.ParentType.EFFECT);
            Res_PerSession.Refresh();
        }
    }
}