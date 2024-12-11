
using Godot;

// Session || Per Turn
public partial class PopData
{
    public DistrictData _District = null;

    public DataBlock Data = null;

    public PopData(DistrictData district)
    {
        _District = district;
        Data = _District._Data.GetSub("Pop");
    }

    public int GetProgress()
    {
        return Data.GetSubValueI("GrowthProgress");
    }
}
