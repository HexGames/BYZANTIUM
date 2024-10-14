using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class FleetData : Node
{
    [ExportCategory("PlayerParent")]
    [Export]
    public PlayerData _Player = null;

    [ExportCategory("FleetRawData")]
    [Export]
    public DataBlock Data = null;

    [ExportCategory("FleetData")]
    [Export]
    public string FleetName = "";
    [Export]
    public DataBlock ActionData = null;

    //[Export]
    //public DataBlock ShipsData = null;

    //[ExportCategory("SystemData-Actions")]
    //[Export]
    //public DataBlock ActionBuild = null;

    [ExportCategory("SystemData-Links")]
    [Export]
    public Array<ShipData> Ships = new Array<ShipData>();

    public FleetStatsWrapper Stats_PerTurn = null;
    public StarData StarAt_PerTurn = null;

    // actions
    public List<StarData> AvailableMoves_PerTurn = new List<StarData>();

    // --------------------------------------------------------------------------------------------
    public string GetLongName()
    {
        return Data.GetSub("Name").ValueS.Replace("_", " ");
    }

    public ShipData GetShip(string ship)
    {
        for (int idx = 0; idx < Ships.Count; idx++)
        {
            if (Ships[idx].ShipName == ship)
            {
                return Ships[idx];
            }
        }

        return null;
    }

    public int GetMoveActionTurns()
    {
        if (ActionData != null)
        {
            return ActionData.GetSub("ProgressMax").ValueI - ActionData.GetSub("Progress").ValueI + 1;
        }
        else
        {
            return -1;
        }
    }
}
