using Godot;

// Session || Per Turn
public partial class ShipData
{
    public FleetData _Fleet = null;

    public DataBlock Data = null;

    public DesignData Design = null;
    public int ShipCount = 0;
    public int ShipPower = 0;
    //public string ShipName = "";
    //public int HP;

    public ShipData(DataBlock shipData, FleetData fleet)
    {
        _Fleet = fleet;
        Data = shipData;

        Design = _Fleet._Player.GetDesign(shipData.GetSub("Design").ValueS);
        ShipCount = shipData.GetSubValueI("ShipCount");
        ShipPower = Design.GetPower();
        //ShipName = Data.ValueS
        //ShipType = Data.GetSub("ShipType").ValueS;
    }
}
