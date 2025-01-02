using System.Reflection.Metadata;

public class DefDistrictWrapper
{
    //public DataBlock _Data;

    public string Name = "";
    public int Level = 0;
    public string UpgradeOf = "";
    public string Type = "";
    public string Icon = "";
    public int Cost_BC = 0;
    public DefBenefitWrapper Benefit = null;
    public string Control_Type = "";
    public string Privatize_to = "";
    public string Nationalize_to = "";

    //public int Cost_Influence
    //{
    //    get { return 2 * Cost_BC / 5; }
    //}
    //public int Nationalize_Influence
    //{
    //    get { return 5 * Cost_BC / 2; }
    //}
    //public int Nationalize_BC
    //{
    //    get { return 5 * Cost_BC / 2; }
    //}
    //public int Privatize_BC
    //{
    //    get { return Cost_BC; }
    //}

    public DefDistrictWrapper(DataBlock targetData)
    {
        //_Data = targetData;
        DataBlock _Data = targetData;

        Name = _Data.ValueS;
        Level = _Data.GetSubValueI("Level");
        UpgradeOf = _Data.GetSubValueS("UpgradeOf");
        Type = _Data.GetSubValueS("Type");
        Icon = _Data.GetSubValueS("Icon");
        Cost_BC = _Data.GetSubValueI("Cost", "BC");
        Benefit = new DefBenefitWrapper(_Data.GetSub("Benefit"));
        Control_Type = _Data.GetSubValueS("Control", "Type");
        Privatize_to = _Data.GetSubValueS("Control", "PrivatizeTo");
        Nationalize_to = _Data.GetSubValueS("Control", "NationalizeTo");
    }

    public bool CanInvest()
    { 
        return Type == "District" && Control_Type != "State_Owned";
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