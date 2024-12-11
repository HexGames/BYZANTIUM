using Godot;

// Session || Per Turn
public partial class DesignData
{
    public PlayerData _Player = null;

    public DataBlock Data = null;

    public string DesignName = "";
    public string ShipType = "";
    public int Cost = 100;

    //[Export]
    //public DataBlock ShipsData = null;

    //[ExportCategory("SystemData-Actions")]
    //[Export]
    //public DataBlock ActionBuild = null;

    // actions
    //public List<ActionSectorBuild> ActionsBuildPossible = new List<ActionSectorBuild>();

    public DesignData(DataBlock designData, PlayerData player)
    {
        _Player = player;
        Data = designData;

        DesignName = Data.ValueS;

        ShipType = Data.GetSubValueS("ShipType");
        Cost = Data.GetSubValueI("Cost");
    }

    public int GetPower()
    {
        if (ShipType == "Colony_Ship")
        {
            return 5;
        }
        return 10;
    }
}
