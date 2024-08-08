using System.Collections.Generic;

public class Action
{
    public DataBlock ActionData = null;

    public DataBlock Selected = null;
    public DefBuildingWrapper TargetInfo = null;

    public DataBlock Cost = null;

    public int SpawnWeight = 1;
    public int TimeToComplete = 1;

    public string NoStartReason = "";

    public bool Started = false;
    public int Step = 0;
    public int TurnCurrent = 0;

    protected DefLibrary DefLib = null;

    public void Init(DefLibrary df)
    {
        DefLib = df;
        ActionData = Data.CreateData("Action", "none", df);
    }

    public bool SpawnCondition() { return true; }

    public void CalculateWight()
    {
        SpawnWeight = 1;
        Data.AddData(ActionData, "SpawnWeight", 1, DefLib);
    }

    public void CalculateCost()
    {
        Cost = Data.AddData(ActionData, "Cost", "none", DefLib);
    }

    public void CalculateTime()
    {
        TimeToComplete = 1;
        Data.AddData(ActionData, "Time", 1, DefLib);
    }

    public void OngoingEffect()
    {
    }

    public bool IntreruptConfition()
    {
        return false;
    }

    public bool StartCondition()
    {
        NoStartReason = "";
        return true;
    }

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void End()
    {
    }
}