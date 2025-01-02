using System.Collections.Generic;
using System.Reflection.Metadata;

public class DefBenefitWrapper
{
    //public DataBlock _Data;

    public string Resource = "";
    public int Income = 0;
    public int Wealth = 0;
    public int Bonus = 0;
    public int ExtraBC = 0;
    public int LocalBonus = 0;

    public List<int> Invest_CostBC = new List<int>();
    public List<int> Invest_ExtraLocalBonus = new List<int>();

    public int Construction = 0;

    public DefBenefitWrapper(DataBlock targetData)
    {
        //_Data = targetData;
        DataBlock _Data = targetData;

        Resource = _Data.GetSubValueS("Resource");
        Income = _Data.GetSubValueI("Income");
        Wealth = _Data.GetSubValueI("Wealth");
        Bonus = _Data.GetSubValueI("Bonus");
        ExtraBC = _Data.GetSubValueI("ExtraBC");
        LocalBonus = _Data.GetSubValueI("LocalBonus");

        Invest_CostBC.Clear();
        Invest_ExtraLocalBonus.Clear();
        Invest_CostBC.Add(_Data.GetSubValueI("Invest_1", "CostBC"));
        Invest_ExtraLocalBonus.Add(_Data.GetSubValueI("Invest_1", "ExtraLocalBonus"));
        Invest_CostBC.Add(_Data.GetSubValueI("Invest_2", "CostBC"));
        Invest_ExtraLocalBonus.Add(_Data.GetSubValueI("Invest_2", "ExtraLocalBonus"));
        Invest_CostBC.Add(_Data.GetSubValueI("Invest_3", "CostBC"));
        Invest_ExtraLocalBonus.Add(_Data.GetSubValueI("Invest_3", "ExtraLocalBonus"));

        LocalBonus = _Data.GetSubValueI("LocalBonus");
        Construction = _Data.GetSubValueI("Construction");

    }
}