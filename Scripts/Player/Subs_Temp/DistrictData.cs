
// Session || Per Turn
using Godot;

public partial class DistrictData
{
    public PlanetData _Planet = null;
    public ColonyData _Colony = null; // can be null for new colonies

    public DataBlock Data = null; // slot for possible structures, null for new colonies

    public DefDistrictWrapper DistrictDef = null;

    public DistrictData(DefDistrictWrapper districtDef, PlanetData planet)
    {
        _Planet = planet;
        DistrictDef = districtDef;

        // DATACHECK
        if (DistrictDef == null)
        {
            GD.PrintErr("No Data for District Data");
        }
    }
    public DistrictData(DefDistrictWrapper districtDef, DataBlock districtSlot, PlanetData planet)
    {
        _Planet = planet;
        DistrictDef = districtDef;

        Data = districtSlot;

        // DATACHECK
        if (Data == null || DistrictDef == null)
        {
            GD.PrintErr("No Data for District Data");
        }
    }

    public DistrictData(DataBlock districtData, ColonyData colony)
    {
        _Colony = colony;
        _Planet = colony.Planet;

        Data = districtData;

        if (districtData.ValueS != "")
        {
            DistrictDef = Game.self.Def.GetDistrictInfo(districtData.ValueS);
        }
    }

    public  bool IsFinishedDistrict()
    {
        return DistrictDef != null && Data.HasSub("InQueue") == false;
    }
}
