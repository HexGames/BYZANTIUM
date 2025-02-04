
using Godot;

// Session || Per Turn
public partial class PopData
{
    public DistrictData _District = null;

    public DataBlock Data = null;

    public PopData(DataBlock data, DistrictData parent)
    {
        Data = data;
        _District = parent;
    }
}
