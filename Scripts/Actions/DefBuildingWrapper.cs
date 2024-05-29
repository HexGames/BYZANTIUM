public class DefBuildingWrapper
{
    public DataBlock _Data;

    public PlanetData _Planet;
    public SectorData _Sector;
    public DataBlock _BuildingPlanet;
    public DataBlock _BuildingOld;

    public string Name = "";
    //public int Turns = 0;
    public int Cost = 0;
    public ResourcesWrapper Benefit = null;

    public DefBuildingWrapper(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.ValueS;

        //DataBlock turns = _Data.GetSub("Turns");
        DataBlock costData = _Data.GetSub("Cost", false);
        DataBlock benefitData = _Data.GetSub("Benefit", false);

        //if (turns != null) Turns = turns.ValueI;
        if (costData != null)
        {
            //Cost = new ResourcesWrapper(costData);
            //Cost.Refresh();
            Cost = costData.GetSub("Production").ValueI;
        }
        if (benefitData != null)
        {
            Benefit = new ResourcesWrapper(benefitData, ResourcesWrapper.ParentType.Building);
            Benefit.Refresh();
        }
    }

    //public int GetTurns(int production)
    //{
    //
    //}
}