public class ActionTargetInfo
{
    public DataBlock _Data;

    public PlanetData _Planet;
    public SectorData _Sector;

    public string Name = "";
    //public int Turns = 0;
    public ResourcesWrapper Cost = null;
    public ResourcesWrapper Benefit = null;

    public ActionTargetInfo(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.ValueS;

        //DataBlock turns = _Data.GetSub("Turns");
        DataBlock costData = _Data.GetSub("Cost");
        DataBlock benefitData = _Data.GetSub("Benefit");

        //if (turns != null) Turns = turns.ValueI;
        if (costData != null)
        {
            Cost = new ResourcesWrapper(costData);
            Cost.Refresh();
        }
        if (benefitData != null)
        {
            Benefit = new ResourcesWrapper(benefitData);
            Benefit.Refresh();
        }
    }

    //public int GetTurns(int production)
    //{
    //
    //}
}