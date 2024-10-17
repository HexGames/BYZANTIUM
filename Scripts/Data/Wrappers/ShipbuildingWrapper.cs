using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class ShipbuildingWrapper
{
    public class Info
    {
        public PlanetData Planet;
        public DistrictData District;
        public DataBlock QueueData;
    };

    public SystemData _System = null;
    public DesignData Design = null;

    public int Progress = 0;

    public int EstimatedProgressNextTurn = 0;
    public int EstimatedTurns = 0;

    public ShipbuildingWrapper(SystemData system)
    {
        _System = system;
    
        //Refresh();
    }

    public void Refresh()
    {
        DataBlock actionBuildShip = _System.Data.GetSub("ActionBuildShip");
        Design = _System._Player.GetDesign(actionBuildShip.GetSub("Design").ValueS);
        Progress = actionBuildShip.GetSub("Progress").ValueI;

        int production = _System.Resources_PerTurn.GetIncome("Shipbuilding").IncomeAllTotal(_System);
        int overflow = actionBuildShip.GetSub("Overflow").ValueI;

        int maxProgress = Design.Cost;

        int remaining = maxProgress - Progress;
        EstimatedProgressNextTurn = Mathf.Min(maxProgress, Progress + production + overflow);

        EstimatedTurns = Helper.TurnsToComplete(remaining - overflow, production);
        //overflow = turns * production - (remaining - overflow);
    }
}
