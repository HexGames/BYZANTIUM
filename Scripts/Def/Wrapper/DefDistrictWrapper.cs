public class DefDistrictWrapper
{
    public DataBlock _Data;

    public PlanetData _Planet;
    //public SectorData _Sector;
    public DataBlock _BuildingPlanet;
    public DataBlock _BuildingOld;

    public string Name = "";
    public string Type = "";
    public string Icon = "";
    //public int Turns = 0;
    public int Cost = 0;
    public ResourcesWrapper Res_PerSession = null;

    public DefDistrictWrapper(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.ValueS;
        Type = _Data.GetSubValueS("Type");
        Icon = _Data.GetSubValueS("Icon");
        Cost = _Data.GetSubValueI("Cost");

        DataBlock benefitData = _Data.GetSub("Benefit", false);

        if (benefitData != null)
        {
            Res_PerSession = new ResourcesWrapper(benefitData, ResourcesWrapper.ParentType.DISTRICT);
            Res_PerSession.Refresh();
        }
    }

    //public int GetTurns(int production)
    //{
    //
    //}
}