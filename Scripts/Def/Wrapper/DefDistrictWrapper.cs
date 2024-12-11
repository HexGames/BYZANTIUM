using System.Reflection.Metadata;

public class DefDistrictWrapper
{
    public DataBlock _Data;

    public string Name = "";
    public string Type = "";
    public string Icon = "";
    //public int Turns = 0;
    public int Cost = 0;

    public DistrictEconomyWrapper Economy_PerSession = null;

    public DefDistrictWrapper(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.ValueS;
        Type = _Data.GetSubValueS("Type");
        Icon = _Data.GetSubValueS("Icon");
        Cost = _Data.GetSubValueI("Cost");

        Economy_PerSession = new DistrictEconomyWrapper(this);
    }

    public bool CanInvest()
    { 
        return Type == "District" && _Data.GetSubValueS("Control/Typ") != "State_Owned";
    }

    public bool CanPrivatize()
    {
        string name = Name;
        if (name.StartsWith("State_"))
        {
            name.Replace("State_", "Private_");
            return Game.self.Def.GetDistrictInfo(name) != null;
        }
        return false;
    }

    public bool CanNationalize()
    {
        string name = Name;
        if (name.StartsWith("Private_"))
        {
            name.Replace("Private_", "State_");
            return Game.self.Def.GetDistrictInfo(name) != null;
        }
        return false;
    }
}