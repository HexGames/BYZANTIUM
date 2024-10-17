using Godot;

// Session || Per Turn
public partial class ShipData
{
    public FleetData _Fleet = null;

    public DataBlock Data = null;

    public DesignData Design = null;
    public string ShipName = "";
    public int HP;

    public ShipData(DataBlock shipData, FleetData fleet)
    {
        _Fleet = fleet;
        Data = shipData;

        Design = _Fleet._Player.GetDesign(shipData.GetSub("Design").ValueS);
        //ShipName = Data.ValueS
        //ShipType = Data.GetSub("ShipType").ValueS;
    }
}
